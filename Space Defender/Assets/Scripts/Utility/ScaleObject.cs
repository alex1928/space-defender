using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour {

	[SerializeField] public Vector3 targetScale = Vector3.zero;
	public float scalingTime = 1f;
	public bool scaleOnStart = false;
	private bool scaling = false;

	[HideInInspector] public Vector3 defaultScale;

	void Awake() {

		defaultScale = transform.localScale;
	}

	// Use this for initialization
	void Start () {
	
		if(scaleOnStart)
			scaling = true;
	}

	public void ScaleTo(Vector3 scale, float time) {

		targetScale = scale;
		scalingTime = time;
		scaling = true;
	}

	public void StartScaling() {

		scaling = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!scaling)
			return;

		if(transform.localScale != targetScale) {

			transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scalingTime / Time.deltaTime);
		}
	}
}
