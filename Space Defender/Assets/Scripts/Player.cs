using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]

public class Player : SpaceShuttle {


	// Use this for initialization
	void Start () {
		
		base.Start();


	}
	
	// Update is called once per frame
	void Update () {

		base.Update();
	}

	void FixedUpdate()
	{
		
		float accelerationAxis = Input.GetAxis("Vertical") * Time.deltaTime;
		float directionAxis = Input.GetAxis("Horizontal") * Time.deltaTime;

		afterburner = Input.GetKey(KeyCode.LeftShift);

		AddAccelerationForce(accelerationAxis);
		AddDirectionForce(directionAxis);

	}
}
