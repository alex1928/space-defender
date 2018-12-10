using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]

public abstract class SpaceShuttle : MonoBehaviour {

	public float acceleration = 100f;
	public float agility = 50f;
	public float maxSpeed = 30f;

	public float afterburderMultiplier = 1.4f;

	public float horizontalMovementStabilization = 30f;

	protected bool afterburner = false;


	protected Rigidbody2D rb;

	// Use this for initialization
	protected void Start () {
	
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}

	protected void FixedUpdate()
	{
		StabilizeHorizontalMovement();
	}

	protected void AddAccelerationForce(float force) {

		if(afterburner) {

			force *= afterburderMultiplier;
		}

		float currentSpeed = GetSpeed();

		if(force > 0 && currentSpeed > maxSpeed)
			return;

		if(force < 0 && currentSpeed < -maxSpeed)
			return;

		rb.AddForce(transform.up * force * acceleration);
	}

	protected void AddDirectionForce(float force) {

		rb.AddTorque(force * -agility);
	}

	protected void StabilizeHorizontalMovement() {
		
		float horizontalMovement = GetHorizontalMovement();
		
		rb.AddForce(transform.right * -horizontalMovement * Time.deltaTime * horizontalMovementStabilization);
	}

	protected float GetSpeed() {

		Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

		return localVelocity.y;	
	}

	protected float GetHorizontalMovement() {

		Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

		return localVelocity.x;
	}

}
