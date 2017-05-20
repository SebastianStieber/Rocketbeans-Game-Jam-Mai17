using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour {

	GameObject selectedPlanet;
	GameManager gameManager;

    void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo) && Input.GetMouseButtonDown(0))
        {
			if (hitInfo.collider.gameObject.tag == "Planet") {
				GameObject hitObject = hitInfo.transform.gameObject;

            
				SelectObject (hitObject);
			} else {
				ClearSelection();
			}
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

			Renderer r = selectedPlanet.GetComponent<Renderer> ();
			Material m = r.material;
			m.color = Color.green;
			r.material = m;
		}
    }

    void ClearSelection() {
        if (selectedPlanet == null) {
            return;
        }

        Renderer r = selectedPlanet.GetComponent<Renderer>();
        Material m = r.material;
        m.color = Color.white;
        r.material = m;

        selectedPlanet = null;
    }
}
