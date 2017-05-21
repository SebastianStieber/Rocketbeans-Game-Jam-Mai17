using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour {

	GameObject selectedPlanet;
	GameManager gameManager;

	GameObject gameObjectHover;

    void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

		if(Physics.Raycast(ray, out hitInfo)){
			if (hitInfo.collider.gameObject.tag == "Planet" && Input.GetMouseButtonDown(0)) {
				GameObject hitObject = hitInfo.transform.gameObject;
				SelectObject (hitObject);
			} 
		} else {
			ClearSelection();
		}

		Ray rayHover = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hoverHit;


		if(Physics.Raycast(rayHover, out hoverHit)){
			if (hoverHit.collider.gameObject.tag == "Planet") {
				GameObject g = hoverHit.collider.gameObject;

				g.transform.localScale = Vector3.Lerp(g.transform.localScale, Vector3.one * -1 * 14f, 3f * Time.deltaTime);

				gameObjectHover = g;
			}
		} else if (gameObjectHover != null) {
			gameObjectHover.transform.localScale = Vector3.one * -1 * 10f;
			gameObjectHover = null;
		}
	}

	void SelectObject(GameObject planet) {
		if (planet == selectedPlanet) {
			if (planet.GetComponent<Planet> ().ownedByPlayer == gameManager.currentPlayer || !planet.GetComponent<Planet> ().ownedByPlayer) {
				List<GameObject> planetNodes = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().currentPlanet.GetComponent<Planet> ().nodes;
				if(planetNodes.Contains(planet))
					gameManager.SelectPlanet (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(), selectedPlanet.GetComponent<Planet>());
			}
		} else {
			if (selectedPlanet != null) {
				if (planet == selectedPlanet)
					return;
				ClearSelection ();
			}
			selectedPlanet = planet;
		}
    }

    void ClearSelection() {
        if (selectedPlanet == null) {
            return;
        }

        selectedPlanet = null;
    }
}
