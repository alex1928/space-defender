using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed = 10f;
	public int damage = 10;
	
	public GameObject hitDamageEffectPrefab;

	[SerializeField] protected float lifeTime = 1f;
	private float lifeLeft = 0f;

	// Use this for initialization
	public virtual void Start () {
		
		lifeLeft = lifeTime;
		rb = GetComponent<Rigidbody2D>(); 
		StartMove();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
		lifeLeft -= Time.deltaTime;

		if(lifeLeft <= 0) {

			Remove();
		}
	}

	public void OnEnable() {

		lifeLeft = lifeTime;
		StartMove();
	}

	public void StartMove() {

		if(rb != null)
			rb.velocity = transform.up * speed;
	}

	public virtual void Remove() {

		if(hitDamageEffectPrefab != null) {
			
			GameObject effect = Instantiate(hitDamageEffectPrefab);
			effect.transform.position = transform.position;

			Destroy(effect, 1f);
		}

		rb.velocity = Vector3.zero;
		gameObject.SetActive(false);
	}
}
