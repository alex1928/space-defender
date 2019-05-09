using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	private bool shooting = false;
	private float reloadTimer = 0;
	[SerializeField] private float reloadTime = 1f;

	[SerializeField] private GameObject projectilePrefab; 
	private GameObject target;

	private List<GameObject> projectilesPool = new List<GameObject>();

	public int projectilesPoolSize = 10;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(reloadTimer > 0)
			reloadTimer -= Time.deltaTime;

		if(shooting) {

			OneShot();
		}
	}

	private void PrepareProjectilesPool() {

		for(int i = 0; i < projectilesPoolSize; i++) {

			AddProjectileToPool();
		}
	}

	private GameObject AddProjectileToPool() {

		GameObject projectile = Instantiate(projectilePrefab);
		projectile.SetActive(false);
		projectilesPool.Add(projectile);

		return projectile;
	}

	private GameObject GetUnusedProjectile() {

		foreach(GameObject projectile in projectilesPool) {

			if(!projectile.activeInHierarchy)
				return projectile;
		}
		
		return null;
	}

	public void OneShot() {

		if(reloadTimer > 0)
			return;

		GameObject projectile = GetUnusedProjectile();

		if(projectile == null)
			projectile = AddProjectileToPool();

		projectile.transform.position = transform.position;
		projectile.transform.rotation = transform.rotation;
		projectile.SetActive(true);

		//if projectile is missile type, then sets its target.
		Missile missile = projectile.GetComponent<Missile>();
		if(missile != null) {

			missile.target = target;
		}

		reloadTimer = reloadTime;
	}

	public void SetTarget(GameObject target) {

		this.target = target;
	}


	public void StartShooting() {

		shooting = true;
	}

	public void StopShooting() {

		shooting = false;
	}
}
