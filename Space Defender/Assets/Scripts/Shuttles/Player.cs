﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(FieldOfView))]

public class Player : SpaceShuttle {

	public float angularDrag = 1.5f;


	// Use this for initialization
	public void Start () {
		
		rb.angularDrag = angularDrag;

		UIManager.instance.UpdatePlayerHealthBar(health, startHealth);
		StopShooting();
	}
	
	// Update is called once per frame
	public void Update() {

		if(SystemInfo.deviceType == DeviceType.Desktop) {

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
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();

		afterburner = Input.GetKey(KeyCode.LeftShift);

		Vector2 controls = UIManager.instance.GetControlAxis();
		
		if(Mathf.Abs(controls.y) > 0.1) {

			float accelerationAxis = controls.y * Time.deltaTime;
			AddAccelerationForce(accelerationAxis, transform.up);
		} 

		SetEnginesEffectPower(Mathf.Clamp01(controls.y));

		if(Mathf.Abs(controls.x) > 0.1) {

			float directionAxis = controls.x * Time.deltaTime;
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
