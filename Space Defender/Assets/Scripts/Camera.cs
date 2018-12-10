using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public Transform target;
	public float cameraSpeed = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 targetPos = new Vector3(target.position.x, target.position.y, -10f);

		transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed * Time.deltaTime);
	}
}
