using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	[HideInInspector] public static EnemyManager instance;
	[HideInInspector] public List<GameObject> spawnedEnemies = new List<GameObject>();

	// Use this for initialization
	void Awake () {
		
		if(instance == null)
			instance = this;
		else
			Destroy(gameObject);
	}
	

	public void SpawnEnemy(GameObject enemyPrefab, int count = 1) {

		for(int i = 0; i < count; i++) {

			SpawnEnemy(enemyPrefab);
		}
	}


	public void SpawnEnemy(GameObject enemyPrefab) {

		GameObject newEnemy = InstantiateEnemyOnRandomPosition(enemyPrefab);
		Enemy enemyObject = newEnemy.GetComponent<Enemy>();
	
		enemyObject.target = GameManager.instance.player;

		spawnedEnemies.Add(newEnemy);
	}


	private GameObject InstantiateEnemyOnRandomPosition(GameObject enemyPrefab) {

		Vector2 position = EnviromentManager.instance.GetRandomPositionInAvaliableSpace();
		GameObject newEnemy = Instantiate(enemyPrefab);
		newEnemy.transform.position = position;

		return newEnemy;
	}
}	
