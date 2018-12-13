using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(SurroundingSensor))]

public abstract class SpaceShuttle : MonoBehaviour {

	protected Rigidbody2D rb;
	protected bool afterburner = false;

	[SerializeField] private float acceleration = 100f;
	[SerializeField] protected float agility = 50f;
	[SerializeField] private float maxSpeed = 30f;
	[SerializeField] protected float afterburderMultiplier = 1.4f;
	[SerializeField] private float horizontalMovementStabilization = 30f;

	public List<GameObject> weapons = new List<GameObject>();

	public GameObject rocketLauncher;

	public int health = 100;

	protected SurroundingSensor surroundingSensor;

	
	// Use this for initialization
	public virtual void Start () {
	
		rb = GetComponent<Rigidbody2D>();
		surroundingSensor = GetComponent<SurroundingSensor>();
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

		Weapon weaponObj = rocketLauncher.GetComponent<Weapon>();
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

	protected void DealDamage(int damage) {

		health -= damage;

		if(health <= 0) {

			Explode();
		}
	}


	public void Explode() {

		Destroy(gameObject);
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
