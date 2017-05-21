using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
    
    public AudioSource source;
    public AudioClip one;
    public AudioClip two;
    public AudioClip three;
    

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void setSound(GameObject planet, Player player)
    {
        if (planet.GetComponent<Planet>().ownedByPlayer == player)
        {
            planet.GetComponent<AudioSource>().clip = two;
            planet.GetComponent<AudioSource>().Play();
        }
        else 
        {
            planet.GetComponent<AudioSource>().clip = one;
            planet.GetComponent<AudioSource>().Play();
        }



    }

}
