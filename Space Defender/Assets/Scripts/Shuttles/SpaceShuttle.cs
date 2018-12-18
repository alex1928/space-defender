using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(SurroundingSensor))]
[RequireComponent(typeof(ScaleObject))]

public abstract class SpaceShuttle : MonoBehaviour {

	protected Rigidbody2D rb;
	protected bool afterburner = false;

	[Header("Moving")]
	[SerializeField] private float acceleration = 100f;
	[SerializeField] protected float agility = 50f;
	[SerializeField] private float maxSpeed = 30f;
	[SerializeField] protected float afterburderMultiplier = 1.4f;
	[SerializeField] private float horizontalMovementStabilization = 30f;

	public List<ParticleSystem> engines = new List<ParticleSystem>();
	public GameObject explosionParticle;
	public float maxEnginesSpeed = -0.5f;
	public int maxEnginesEmmision = 15;

	[Header("Weapons")]
	public List<GameObject> weapons = new List<GameObject>();
	public GameObject rocketLauncher;
	
	public int health = 100;
	public int startHealth;


	protected SurroundingSensor surroundingSensor;
	protected FieldOfView fieldOfView;

	
	void Awake() {

		rb = GetComponent<Rigidbody2D>();
		surroundingSensor = GetComponent<SurroundingSensor>();
		fieldOfView = GetComponent<FieldOfView>();

		startHealth = health;
	}
	
	// Use this for initialization
	public virtual void Start () {
	}


	public virtual void Update() {

		
	}

	 
	public virtual void FixedUpdate()
	{
		StabilizeHorizontalMovement();
	}

	protected void StartShooting() {

		foreach(GameObject weapon in weapons) {

			Weapon weaponObj = weapon.GetComponent<Weapon>();
			weaponObj.StartShooting();
		}
	}

	protected void StopShooting() {

		foreach(GameObject weapon in weapons) {

			Weapon weaponObj = weapon.GetComponent<Weapon>();
			weaponObj.StopShooting();
		}
	}

	protected void FireRocket() {

		if(fieldOfView.visibleTargets.Count == 0)
			return;

		GameObject closestTarget = null;
		float bestDistance = float.PositiveInfinity;

		foreach(GameObject possibleTarget in fieldOfView.visibleTargets) {

			float distance = Vector2.Distance(transform.position, possibleTarget.transform.position);

			if(distance < bestDistance) {

				closestTarget = possibleTarget;
				bestDistance = distance;
			}
		}

		Weapon weaponObj = rocketLauncher.GetComponent<Weapon>();
		weaponObj.SetTarget(closestTarget);
		
		weaponObj.OneShot();
	}

	protected void AddAccelerationForce(float force, Vector2 direction) {

		if(afterburner) {

			force *= afterburderMultiplier;
		}

		float currentSpeed = GetSpeed();

		if(force > 0 && currentSpeed > maxSpeed)
			return;

		if(force < 0 && currentSpeed < -maxSpeed)
			return;

		rb.AddForce(direction * force * acceleration);
	}

	protected void AddDirectionForce(float force) {

		rb.AddTorque(force * -agility);
	}

	protected void StabilizeHorizontalMovement() {
		
		float horizontalMovement = GetHorizontalMovement();
		
		rb.AddForce(transform.right * -horizontalMovement * Time.deltaTime * horizontalMovementStabilization);
	}

	protected float GetSpeed() {

		Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

		return localVelocity.y;	
	}

	protected float GetHorizontalMovement() {

		Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

		return localVelocity.x;
	}	

	protected virtual void DealDamage(int damage) {

		health -= damage;

		if(health <= 0) {

			Explode();
		}
	}


	public virtual void Explode() {

		GameObject explosion = Instantiate(explosionParticle);
		explosion.transform.position = transform.position;

		EnemyManager.instance.spawnedEnemies.Remove(gameObject);

		Destroy(explosion, 2f);
		Destroy(gameObject);
	}

	
	protected void SetEnginesEffectPower(float power) {

		foreach(ParticleSystem engine in engines) {

			var emmision = engine.emission;
			var main = engine.main;
			emmision.rateOverTime = maxEnginesEmmision * power;
			main.startSpeed = maxEnginesSpeed * power;
		}
	}


	void OnTriggerEnter2D(Collider2D collider)
    {
		GameObject trigger = collider.gameObject;

		if(trigger.tag == "Projectile") {

			Projectile projectile = trigger.GetComponent<Projectile>();

			DealDamage(projectile.damage);

			projectile.Remove();
			
		}
        
    }
}
