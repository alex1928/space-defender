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

	// Use this for initialization
	public virtual void Start () {
		
		rb = GetComponent<Rigidbody2D>(); 
		StartMove();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
		lifeTime -= Time.deltaTime;

		if(lifeTime <= 0) {

			Remove();
		}
	}

	public virtual void StartMove() {

		rb.velocity = transform.up * speed;
	}

	public virtual void Remove() {

		if(hitDamageEffectPrefab != null) {
			GameObject effect = Instantiate(hitDamageEffectPrefab);
			effect.transform.position = transform.position;

			Destroy(effect, 1f);
		}

		Destroy(gameObject);
	}
}
