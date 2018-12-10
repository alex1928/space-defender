using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]

public class Enemy : SpaceShuttle {

	public Transform target;
	public float followDistance = 3f;

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
		base.FixedUpdate();

		Vector2 positionDiff = target.position - transform.position;
		Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, positionDiff);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, agility / 10f * Time.deltaTime);

		float accelerationForce = Mathf.Clamp(positionDiff.magnitude - followDistance, -1f, 1f) * Time.deltaTime;
		AddAccelerationForce(accelerationForce);
	}
}
