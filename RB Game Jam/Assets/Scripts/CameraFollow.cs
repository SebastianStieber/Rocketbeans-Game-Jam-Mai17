﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
	private GameObject oldTarget;

	[SerializeField]
	[Range(0, 1)]
	private float zoom;
	private float currentZoom;
	public float smoothZoom = 1.5f;

	public float smoothMove = .5f;

    public float minDistance = 2f;
	public float maxDistance = 4f;

	private GameObject gameCam;

    void Start () {
		gameCam = GameObject.FindGameObjectWithTag ("MainCamera");
		currentZoom = 1f;
	}

	void Update () {
		target = GetComponent<Player> ().currentPlanet;
		
		Zoom ();
		Move ();
    }

	void CenterOnTarget(){
		if (!target) {
			target = GameObject.FindGameObjectWithTag ("World").GetComponent<World>().planets[5];
		}
		transform.position = target.transform.position;
		oldTarget = target;
	}

	void Zoom(){
		float wheel = Input.GetAxis ("Mouse ScrollWheel");
		if (wheel != 0) {
			currentZoom -= wheel;

			if (currentZoom > 1)
				currentZoom = 1;
			else if (currentZoom < 0)
				currentZoom = 0;
		}

		zoom = Mathf.Lerp (zoom, currentZoom, smoothZoom * Time.deltaTime);

		float distanceToTarget = minDistance + (maxDistance - minDistance) * zoom;

		gameCam.transform.localPosition = Vector3.zero + new Vector3 (0, 0, distanceToTarget);

	}

	void Move(){
		if (transform.position != target.transform.position) {
			transform.position = Vector3.Lerp (transform.position, target.transform.position, smoothMove * Time.deltaTime);
		}
		Quaternion r = Quaternion.LookRotation (target.transform.position - gameCam.transform.position, Vector3.up);
		gameCam.transform.rotation = Quaternion.Lerp (gameCam.transform.rotation, r, smoothMove * Time.deltaTime);
	}
}
