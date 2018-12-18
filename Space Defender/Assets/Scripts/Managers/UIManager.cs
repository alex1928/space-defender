using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[HideInInspector] public static UIManager instance;

	public Text pointsText;
	public Text healthText;
	public Slider healthBar;

	public GameObject gameOverPanel;


	void Awake() {

		if(instance == null) 
			instance = this;
		else
			Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowGameOver() {

		gameOverPanel.SetActive(true);
	}

	public void UpdatePlayerHealthBar(int currentHealth, int maxHealth) {

		healthText.text = currentHealth + "/" + maxHealth;
		healthBar.value = (float)currentHealth / (float)maxHealth;
	}

	public void UpdatePoints(int points) {

		pointsText.text = points.ToString();
	}
}
