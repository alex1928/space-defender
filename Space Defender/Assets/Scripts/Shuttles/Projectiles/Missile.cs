using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile {

	public GameObject target;

	// Use this for initialization
	override public void Start () {
		
		base.Start();
	}
	
	// Update is called once per frame
	override public void Update () {
		
		base.Update();
	}	

	void FixedUpdate()
	{

		if(target == null)
			return;

		Vector2 positionDiff = target.transform.position - transform.position;
		Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, positionDiff);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 2 * Time.deltaTime);
		StartMove();
	}

	public override void Remove() {



		base.Remove();
	}
}
