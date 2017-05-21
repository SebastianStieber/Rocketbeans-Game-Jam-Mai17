using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public Vector3 coordinates;
	public List<GameObject> nodes = new List<GameObject>();

	public Material[] materials;
	public int currentMaterialPosition = 0;

	public bool hasStreet;

	public Player ownedByPlayer;

	public float cooldown = .5f;
	float currentCooldown;

	bool forward = true;

	public bool hasSelector = false;

	GameObject gameCam;

	void Start () {
		currentCooldown = cooldown;

		gameCam = GameObject.FindGameObjectWithTag ("MainCamera");
		transform.localScale = transform.localScale * -1;
	}

	void Update () {
		AnimateTexture ();
	}

	public void SetCoordinates(Vector3 position){
		coordinates = position;

		transform.position = coordinates;
	}

	void AnimateTexture(){
		currentCooldown -= Time.deltaTime;

		if (forward) {
			if (currentCooldown <= 0) {
				currentCooldown = cooldown;
				currentMaterialPosition++;
				if (currentMaterialPosition > materials.Length - 1) {
					currentMaterialPosition = materials.Length - 2;
					forward = false;
				}
			}
		} else {
			if (currentCooldown <= 0) {
				currentCooldown = cooldown;
				currentMaterialPosition--;
				if (currentMaterialPosition < 0) {
					currentMaterialPosition = 1;
					forward = true;
				}
			}
		}

		GetComponent<MeshRenderer> ().material = materials [currentMaterialPosition];

		transform.LookAt (gameCam.transform);
	}
}
