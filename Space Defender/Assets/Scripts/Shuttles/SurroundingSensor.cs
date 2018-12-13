using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SurroundingSensor : MonoBehaviour {

	public float up = 0f;
	public float down = 0f;
	public float left = 0f;
	public float right = 0f;

	float checkInterval = .2f;

	[Range(0, 20)] public float sensorRadius = 2f;

	// Use this for initialization
	void Start () {
		
		StartCoroutine("StartCheckingSurrounding", checkInterval);
	}

	IEnumerator StartCheckingSurrounding(float delay) {

		while(true) {

			yield return new WaitForSeconds(delay);
			CheckSurrounding();
		}
	}

	private void CheckSurrounding() {

		up = CheckDistanceInDirection(transform.up);
		down = CheckDistanceInDirection(transform.up * -1f);
		right = CheckDistanceInDirection(transform.right);
		left = CheckDistanceInDirection(transform.right * -1f);
	}

	private float CheckDistanceInDirection(Vector2 direction) {

		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, sensorRadius);

		foreach(RaycastHit2D hit in hits) {

			if(hit.transform.gameObject == gameObject)
				continue;
			
			return Vector2.Distance(transform.position, hit.transform.position);
		}

		return 0f;
	}
}
