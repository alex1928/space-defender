using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScaleObject))]

public class ObstacleDestroy : MonoBehaviour {

	private int crushCounter = 0;
	public int crushLimit = 2;

	public GameObject crushParticlePrefab;

	private ScaleObject scaleAnimator;

	// Use this for initialization
	void Start () {
		
		scaleAnimator = GetComponent<ScaleObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {

		GameObject trigger = collider.gameObject;

		if(trigger.tag == "Projectile") {

			Projectile projectile = trigger.GetComponent<Projectile>();
			projectile.Remove();

			CrushIntoParts(2);
		}
	}

	public void CrushIntoParts(int partsCount) {

		if(partsCount <= 1)
			return;

		if(crushCounter >= crushLimit)
			return;

		crushCounter++;

		Vector2 newScale = transform.localScale / (float)partsCount;

		for(int i = 0; i < partsCount; i++) {

			EnviromentManager.instance.SpawnObstacle(transform.position, newScale);
		}
		
		GameObject particle = Instantiate(crushParticlePrefab);
		particle.transform.position = transform.position;
		Destroy(particle, 1f);
		Remove();
	}


	public void Remove() {

		EnviromentManager.instance.spawnedObstacles.Remove(gameObject);
		scaleAnimator.ScaleTo(Vector3.zero, 1f);
		Destroy(gameObject, 1.1f);
	}
}
