using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentManager : MonoBehaviour {

	[HideInInspector] public static EnviromentManager instance;

	[Header("Obstacles")]
	public List<GameObject> obstaclesPrefabs = new List<GameObject>();
	private Camera gameCamera;
	
	[Range(0, 100)] public float rotationSpeedRandRange = 10f;
	[Range(0, 20)] public float velocitySpeedRandRange = 5f;
	[Range(0, 1f)] public float randomScaleMultipilier = 0.3f;

	public int startObstaclesCount = 15;
	public int maxSpawnedObjects = 15;
	[Range(5f, 50f)] public float removeDistance = 30f;

	[HideInInspector] public List<GameObject> spawnedObstacles = new List<GameObject>();

	void Awake() {

		if(instance == null)
			instance = this;
		else
			Destroy(gameObject);
	}


	// Use this for initialization
	void Start () {
		
		gameCamera = Camera.main;

		SpawnObstacles();
	}
	

	// Update is called once per frame
	void Update () {

		SpawnObstacles();
		RemoveObstacles();
		RespawnObstaclesOutOfRange();
	}


	public void SpawnObstacles() {

		while(spawnedObstacles.Count < startObstaclesCount) {

			Vector2 randomPosition = GetRandomPositionInAvaliableSpace();
			SpawnObstacle(randomPosition);
		}
	}


	public void RespawnObstaclesOutOfRange() {

		Vector2 cameraCenter = gameCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));

		foreach(GameObject obstacle in spawnedObstacles) {

			float distance = Vector2.Distance(obstacle.transform.position, cameraCenter);

			if(distance > removeDistance) {

				Vector2 randomPosition = GetRandomPositionInAvaliableSpace();
				ConfigureNewObstacleMovement(obstacle);
				obstacle.transform.position = randomPosition;
			}
		}
	}


	public void RemoveObstacles() {

		Vector2 cameraCenter = gameCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
		
		while(spawnedObstacles.Count > maxSpawnedObjects) {

			bool removedAnything = false;
			for(int i = 0; i < spawnedObstacles.Count; i++) {

				GameObject obstacleToRemove = spawnedObstacles[i];
				
				if(Vector2.Distance(obstacleToRemove.transform.position, cameraCenter) > removeDistance) {

					spawnedObstacles.Remove(obstacleToRemove);
					Destroy(obstacleToRemove);
					removedAnything = true;
					break;
				}
			}			

			if(!removedAnything) 
				break;
		}
	}


	private void ConfigureNewObstacleMovement(GameObject obstacle) {

		Rigidbody2D obstacleRB = obstacle.GetComponent<Rigidbody2D>();

		obstacleRB.velocity = new Vector2(Random.Range(-velocitySpeedRandRange, velocitySpeedRandRange), Random.Range(-velocitySpeedRandRange, velocitySpeedRandRange));
		obstacleRB.angularVelocity = Random.Range(-rotationSpeedRandRange, rotationSpeedRandRange);

		float randomScale = Random.Range(1f - randomScaleMultipilier, 1f + randomScaleMultipilier);
		
		Vector3 defaultObstacleScale = obstacle.GetComponent<ScaleObject>().defaultScale;

		obstacle.transform.localScale = defaultObstacleScale * randomScale;
		
	}


	private Vector2 GetRandomPositionInAvaliableSpace() {

		float randX = Random.Range(0,2) == 0 ? Random.Range(-2f, -0.05f) : Random.Range(1.05f, 3f); 
		float randY = Random.Range(0,2) == 0 ? Random.Range(-2f, -0.05f) : Random.Range(1.05f, 3f); 

		Vector2 viewPortRandomPosition = new Vector2(randX, randY);
		Vector2 position = gameCamera.ViewportToWorldPoint(viewPortRandomPosition);

		return position;
	}


	private GameObject SpawnObstacle(Vector2 position) {

		GameObject obstacle = Instantiate(GetRandomObstaclePrefab());
		ConfigureNewObstacleMovement(obstacle);
		obstacle.transform.position = position;

		spawnedObstacles.Add(obstacle);

		return obstacle;
	}


	public GameObject SpawnObstacle(Vector2 position, Vector2 scale) {

		GameObject spawnedObstacle = SpawnObstacle(position);
		spawnedObstacle.transform.localScale = scale;

		return spawnedObstacle;
	}


	public GameObject GetRandomObstaclePrefab() {

		return obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Count)];
	}

}
