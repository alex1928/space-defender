﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	private bool shooting = false;
	private float reloadTimer = 0;
	[SerializeField] private float reloadTime = 1f;

	[SerializeField] private GameObject projectilePrefab; 


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(reloadTimer > 0)
			reloadTimer -= Time.deltaTime;

		if(shooting && reloadTimer <= 0) {

			OneShot();
			reloadTimer = reloadTime;
		}
	}

	public void OneShot() {

		GameObject projectile = Instantiate(projectilePrefab);
		projectile.transform.position = transform.position;
		projectile.transform.rotation = transform.rotation;
	}


	public void StartShooting() {

		shooting = true;
	}

	public void StopShooting() {

		shooting = false;
	}
}