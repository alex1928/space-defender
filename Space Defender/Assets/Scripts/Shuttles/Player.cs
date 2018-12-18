using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(FieldOfView))]

public class Player : SpaceShuttle {

	public float angularDrag = 1.5f;

	// Use this for initialization
	public override void Start () {
		
		base.Start();

		rb.angularDrag = angularDrag;

		UIManager.instance.UpdatePlayerHealthBar(health, startHealth);
	}
	
	// Update is called once per frame
	public override void Update() {

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

	public override void FixedUpdate()
	{
		base.FixedUpdate();

		afterburner = Input.GetKey(KeyCode.LeftShift);
		
		if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.1) {

			float accelerationAxis = Input.GetAxis("Vertical") * Time.deltaTime;
			AddAccelerationForce(accelerationAxis, transform.up);
		} 

		SetEnginesEffectPower(Mathf.Clamp01(Input.GetAxis("Vertical")));

		if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1) {

			float directionAxis = Input.GetAxis("Horizontal") * Time.deltaTime;
			AddDirectionForce(directionAxis);
		}
	}

	override protected void DealDamage(int damage) {

		int currentHealth = health - damage;
		if(currentHealth < 0)
			currentHealth = 0;
		
		UIManager.instance.UpdatePlayerHealthBar(currentHealth, startHealth);

		base.DealDamage(damage);

		if(currentHealth <= 0) {
			
			GameManager.instance.GameOver();
		}
	}
}
