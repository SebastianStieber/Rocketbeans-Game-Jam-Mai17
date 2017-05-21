﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int ap;
	public int turn;

	private GameManager gameManager;

	public List<GameObject> ownedPlanets = new List<GameObject>();

	public GameObject currentPlanet;

	public float smooth = 3f;

	public Material material;

	private World world;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
		world = GameObject.FindGameObjectWithTag ("World").GetComponent<World>();
	}

	void Update () {
		if (transform.position != currentPlanet.transform.position) {
			transform.position = Vector3.Lerp(transform.position, currentPlanet.transform.position, smooth * Time.deltaTime);
		}
	}

	public void Reset(){
		ap = gameManager.currentAP;
	}

	public void SetCurrentPlanet(GameObject planet){
		currentPlanet = planet;
		planet.GetComponent<Planet> ().ownedByPlayer = this;

		ap--;
		if (ap == 0) {
			gameManager.StartNextTurn ();
		}

		world.GetComponent<Selector> ().UpdateSelectors ();
	}

    public void AiSetCurrentPlanet(GameObject planet)
    {
        currentPlanet = planet;
		planet.GetComponent<Planet>().ownedByPlayer = this;
		world.GetComponent<Selector> ().UpdateSelectors ();
    }
}
