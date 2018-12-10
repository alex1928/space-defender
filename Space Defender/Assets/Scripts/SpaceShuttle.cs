using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]

public abstract class SpaceShuttle : MonoBehaviour {

	public float acceleration = 100f;
	public float agility = 50f;

	public float afterburderMultiplier = 1.4f;

	protected bool afterburner = false;


	protected Rigidbody2D rb;

	// Use this for initialization
	protected void Start () {
	
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}


	protected void AddAccelerationForce(float force) {

		if(afterburner) {

			force *= afterburderMultiplier;
		}

		rb.AddForce(transform.up * force * acceleration);
	}

	protected void AddDirectionForce(float force) {

		rb.AddTorque(force * -agility);
	}
}
