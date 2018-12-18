using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour {

	public Transform target;
	public float cameraMoveSpeed = 5f;
	public float cameraZoomSpeed = 2f;
	public float cameraMinSize = 5f;
	public float cameraMaxSize = 6.5f;
	public float targetMaxSpeed = 4f;
	new private Camera camera;

	void Start() {

		camera = GetComponent<Camera>();
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(target == null)
			return;

		Vector3 targetPos = new Vector3(target.position.x, target.position.y, -10f);

		transform.position = Vector3.Lerp(transform.position, targetPos, cameraMoveSpeed * Time.deltaTime);

		float targetSpeed = Mathf.Abs(target.transform.InverseTransformDirection(target.gameObject.GetComponent<Rigidbody2D>().velocity).y);
		float cameraSize = ((targetSpeed * (cameraMaxSize - cameraMinSize)) / targetMaxSpeed) + cameraMinSize;

		camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, cameraSize, cameraZoomSpeed * Time.deltaTime);
	}
}
