using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public Vector3 coordinates;
	public List<GameObject> nodes = new List<GameObject>();

	public Texture t1, t2, t3, t4, t5;

	public bool hasStreet;

	public Player ownedByPlayer;

	void Start () {
		
	}

	void Update () {
		
	}

	public void SetCoordinates(Vector3 position){
		coordinates = position;

		transform.position = coordinates;
	}

	void AnimateTexture(){
	}
}
