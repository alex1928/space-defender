using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile {

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
		Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, positionDiff);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 2 * Time.deltaTime);
		StartMove();
	}

	public override void Remove() {



		base.Remove();
	}
}
