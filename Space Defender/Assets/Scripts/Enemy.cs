using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]

public class Enemy : SpaceShuttle {

	public Transform target;

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
		
		Vector2 positionDiff = target.position - transform.position;

		Quaternion rotation = Quaternion.LookRotation(positionDiff, Vector3.forward);
	}
}
