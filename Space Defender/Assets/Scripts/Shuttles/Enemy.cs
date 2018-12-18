using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]


public class Enemy : SpaceShuttle {

	public GameObject target;
	public float followDistance = 3f;

	public float breakingDistance = 1f;
	public float breakingSpeed = 2f;

	public float minDistanceFromObjects = 3.5f;



	// Use this for initialization
	override public void Start () {
		
		base.Start();
	}

	
	// Update is called once per frame
	public override void Update() {

		base.Update();

		if(fieldOfView.VisibleObjectsContains(target)) {
			StartShooting();
		} else {
			StopShooting();
		}
	}

	override public void Explode(){

		base.Explode();

		GameManager.instance.AddPoints(1);		
	}

	override public void FixedUpdate()
	{
		base.FixedUpdate();

		if(target == null)
			return;

		Vector2 positionDiff = target.transform.position - transform.position;
		Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, positionDiff);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, agility / 10f * Time.deltaTime);

		float distance = positionDiff.magnitude - followDistance;
		float speed = GetSpeed();
		
		if(distance < breakingDistance && speed >= breakingSpeed) {
			
			AddAccelerationForce(-1f * Time.deltaTime, transform.up);
			SetEnginesEffectPower(0);

		} else {

			float accelerationForce = Mathf.Clamp(distance / followDistance, -1f, 1f);
			accelerationForce = accelerationForce * Time.deltaTime;
			AddAccelerationForce(accelerationForce, transform.up);
			SetEnginesEffectPower(1f);
		}

		if(surroundingSensor.right != 0 && surroundingSensor.right < minDistanceFromObjects) 
			AddAccelerationForce(-Time.deltaTime / 2f, transform.right);
		
		if(surroundingSensor.left != 0 && surroundingSensor.left < minDistanceFromObjects) 
			AddAccelerationForce(Time.deltaTime / 2f, transform.right);
		
		if(surroundingSensor.up != 0 && surroundingSensor.up < minDistanceFromObjects)
			AddAccelerationForce(-Time.deltaTime / 2f, transform.right);
		

	}
	
}
