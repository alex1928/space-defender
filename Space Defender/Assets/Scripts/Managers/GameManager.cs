using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[HideInInspector] public static GameManager instance;

	public GameObject player;

	public int points = 0;

	public int startEnemiesCount = 2;

	public float additionalEnemiesPerPoint = 0.5f;

	[SerializeField] private int maxEnemiesCount;

	public List<GameObject> enemyPrefabs = new List<GameObject>();



	// Use this for initialization
	void Awake () {
		
		if(instance == null)
			instance = this;
		else
			Destroy(gameObject);

		maxEnemiesCount = startEnemiesCount;
	}


	void Start() {

		SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {

		if(EnemyManager.instance.spawnedEnemies.Count < maxEnemiesCount) {

			SpawnEnemies();
		}	
	}


	public void SpawnEnemies() {

		GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
		EnemyManager.instance.SpawnEnemy(randomPrefab);
		maxEnemiesCount = startEnemiesCount + (int)Mathf.Floor((float)points * additionalEnemiesPerPoint);
	}


	public void EnemyDestroyed(int points) {

		this.points += points;
	}
}
