using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed = 10f;

	public int damage = 10;

	[SerializeField] protected float lifeTime = 1f;

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
		lifeTime -= Time.deltaTime;

		if(lifeTime <= 0) {

			Remove();
		}
	}


	public void Remove() {

		Destroy(gameObject);
	}
}
