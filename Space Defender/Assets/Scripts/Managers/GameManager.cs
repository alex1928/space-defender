using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[HideInInspector] public static GameManager instance;

	public GameObject player;

	public int points = 0;

	public int startEnemiesCount = 2;

	public float additionalEnemiesPerPoint = 0.5f;

	[SerializeField] private int maxEnemiesCount;

	public List<GameObject> enemyPrefabs = new List<GameObject>();

	public bool gameOver = false;



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

		if(!gameOver && EnemyManager.instance.spawnedEnemies.Count < maxEnemiesCount) {

			SpawnEnemies();
		}	
	}


	public void GameOver() {

		if(gameOver)	
			return;

		gameOver = true;
		UIManager.instance.ShowGameOver();
	}


	public void StartGame() {

		SceneManager.LoadScene("Game");
	}

	public void ExitGame() {

		Application.Quit();
	}


	public void SpawnEnemies() {
		
		GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
		EnemyManager.instance.SpawnEnemy(randomPrefab);
		maxEnemiesCount = startEnemiesCount + (int)Mathf.Floor((float)points * additionalEnemiesPerPoint);
	}

	public void AddPoints(int points) {

		this.points += points;
		UIManager.instance.UpdatePoints(this.points);
	}

}
