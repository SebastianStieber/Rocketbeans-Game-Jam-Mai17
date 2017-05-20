﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KI : MonoBehaviour
{

    public int ap;
    public int turn;

    private GameManager gameManager;

    public List<GameObject> ownedPlanets = new List<GameObject>();

    public GameObject currentPlanet;

    public float smooth = 3f;

    public Material material;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (transform.position != currentPlanet.transform.position)
        {
            Debug.Log(33);
            transform.position = Vector3.Lerp(transform.position, currentPlanet.transform.position, smooth * Time.deltaTime);
        }
    }

    public void Reset()
    {
        ap = gameManager.currentAP;
    }

    public void SetCurrentPlanet(GameObject planet)
    {
        currentPlanet = planet;
<<<<<<< HEAD
        //planet.GetComponent<Planet>().ownedByPlayer = this;
=======
//        planet.GetComponent<Planet>().ownedByPlayer = this;
>>>>>>> d6b6348d8bd1c1dac54059dc3c5e11d2a7324f6c
        planet.GetComponent<MeshRenderer>().material = material;

        ap--;
        if (ap == 0)
        {
            gameManager.StartNextTurn();
        }
    }
}