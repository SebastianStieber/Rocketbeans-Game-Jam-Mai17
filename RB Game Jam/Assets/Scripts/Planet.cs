using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public Vector3 coordinates;

	void Start () {
		
	}

	void Update () {
		
	}

	public void SetCoordinates(Vector3 position){
		coordinates = position;

		transform.position = coordinates;
	}
}
