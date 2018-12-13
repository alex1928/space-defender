using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]

public class Player : SpaceShuttle {

	public float angularDrag = 1.5f;

	// Use this for initialization
	void Start () {
		
		base.Start();

		rb.angularDrag = angularDrag;
	}
	
	// Update is called once per frame
	void Update () {

		base.Update();
		
		if(Input.GetButtonDown("Fire1")) {
			StartShooting();
		}

		if(Input.GetButtonUp("Fire1")) {
			StopShooting();
		}

		if(Input.GetButtonDown("Fire2")) {
			FireRocket();
		}
	}

	void FixedUpdate()
	{
		base.FixedUpdate();
		
		float accelerationAxis = Input.GetAxis("Vertical") * Time.deltaTime;
		float directionAxis = Input.GetAxis("Horizontal") * Time.deltaTime;
		afterburner = Input.GetKey(KeyCode.LeftShift);

		AddAccelerationForce(accelerationAxis);
		AddDirectionForce(directionAxis);

	}
}
