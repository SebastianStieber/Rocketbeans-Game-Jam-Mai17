using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

	World world;

	public GameObject selector1, selector2;
	public GameManager gameManager;
	GameObject gameCam;

	public List<GameObject> selectors = new List<GameObject>();

	void Start () {
		world = GetComponent<World> ();
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		gameCam = GameObject.FindGameObjectWithTag ("MainCamera");

	}

	void Update () {
		foreach (GameObject s in selectors) {
			s.transform.LookAt (gameCam.transform);
		}
	}

	public void UpdateSelectors(){
		List<GameObject> planets = world.planets;

		foreach (GameObject planet in planets) {
			if (planet.GetComponent<Planet> ().ownedByPlayer == gameManager.allPlayers [0] && !planet.GetComponent<Planet> ().hasSelector) {
				GameObject s = Instantiate (selector1);
				s.transform.position = planet.transform.position;
				s.transform.localScale = Vector3.one * 18;
				planet.GetComponent<Planet> ().hasSelector = true;
				s.transform.localScale = s.transform.localScale * -1;

				selectors.Add (s);
			} else if (planet.GetComponent<Planet> ().ownedByPlayer == gameManager.allPlayers [1] && !planet.GetComponent<Planet> ().hasSelector) {
				GameObject s = Instantiate (selector2);
				s.transform.position = planet.transform.position;
				s.transform.localScale = Vector3.one * 18;
				planet.GetComponent<Planet> ().hasSelector = true;
				s.transform.localScale = s.transform.localScale * -1;

				selectors.Add (s);
			}
		}
	}
}
