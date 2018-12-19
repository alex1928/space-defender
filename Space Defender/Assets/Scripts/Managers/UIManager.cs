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
	public GameObject mobileControls;
	public FixedJoystick joystick;


	void Awake() {

		if(instance == null) 
			instance = this;
		else
			Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		
		SetupInterface();
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

	public void SetupInterface() {

		mobileControls.SetActive(SystemInfo.deviceType == DeviceType.Handheld);
	}

	public Vector2 GetControlAxis() {

		if(SystemInfo.deviceType == DeviceType.Handheld) {

			return new Vector2(joystick.Horizontal, joystick.Vertical);
		}
		else if(SystemInfo.deviceType == DeviceType.Desktop) {

			return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		}

		return Vector2.zero;
	}
}
