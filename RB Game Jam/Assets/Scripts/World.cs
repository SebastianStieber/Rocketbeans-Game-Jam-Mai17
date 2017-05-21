using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
	
	public int range = 10;
	public int maxPlanets = 50;
	public float distanceBetweenPlanets = 10f;
	public float randomness = 1f;
	public float streetDistanceMultiplier = 3f;

	public List<GameObject> planets = new List<GameObject>();

	public GameObject planetPrefab1;
	public GameObject planetPrefab2;
	public GameObject planetPrefab3;
	public GameObject streetPrefab;

	void OnEnable () {
		GeneratePlanets ();
		ApplyRandomness ();
		BuildStreets ();

		foreach (GameObject go in planets)
			if (go.transform.position == Vector3.zero)
				Destroy (go);
	}

	void Update () {
	}

	void GeneratePlanets(){
		for (int i = 0; i < maxPlanets; i++) {
			GeneratePlanet ();
		}
	}

	GameObject GeneratePlanet(){
		Vector3 position = CalculatePosition ();

		int r = Random.Range (0, 2);

		GameObject planet;
		if (r == 0)
			planet = Instantiate (planetPrefab1);
		if (r == 1)
			planet = Instantiate (planetPrefab2);
		else
			planet = Instantiate (planetPrefab3);

		planet.GetComponent<Planet> ().SetCoordinates (position);

		planets.Add (planet);

		return planet;
	}

	Vector3 CalculatePosition (){
		float x = Random.Range (0, range) * distanceBetweenPlanets;
		float y = Random.Range (0, range) * distanceBetweenPlanets;
		float z = Random.Range (0, range) * distanceBetweenPlanets;

		Vector3 position = new Vector3 (x, y, z);

		if (IsIntersecting (position)) {
			position = CalculatePosition ();
		}

		return position;
	}

	bool IsIntersecting(Vector3 position){
		foreach (GameObject p in planets) {
			if (position == p.GetComponent<Planet> ().coordinates) {
				return true;
			}
		}

		return false;
	}

	void ApplyRandomness () {
		foreach (GameObject p in planets) {
			Vector3 r = new Vector3 (Random.Range(-randomness, randomness),Random.Range(-randomness, randomness),Random.Range(-randomness, randomness));
			p.transform.position = p.transform.position + r;
		}
	}

	void BuildStreets(){
		List<GameObject> removePlanets = new List<GameObject> ();
		foreach (GameObject p in planets) {
			bool hasBuildStreet = BuildStreet (p);

			if (!hasBuildStreet) {
				removePlanets.Add (p);
			}
		}

		foreach (GameObject p in removePlanets) {
			planets.Remove (p);
			Destroy (p);

			GeneratePlanet ();
		}

		if (planets.Count != 0)
			Debug.Log ("keins mehr da");
		else
			Debug.Log ("done");
	}

	bool BuildStreet(GameObject planet){
		int c = 0;
		foreach (GameObject p2 in planets) {
			if (p2 != planet && Vector3.Distance (planet.GetComponent<Planet> ().coordinates, p2.GetComponent<Planet> ().coordinates) <= distanceBetweenPlanets * streetDistanceMultiplier) {
				GameObject street = Instantiate (streetPrefab);
				Vector3 dir = p2.transform.position - planet.transform.position;
				dir.Normalize ();
				float factor = 5;
				street.GetComponent<LineRenderer> ().SetPositions (new Vector3 [] {planet.transform.position + dir * factor, p2.transform.position - dir * factor});
				planet.GetComponent<Planet> ().nodes.Add (p2);
				c++;
			}
		}
		if (c == 0) {
			return false;
		} else
			planet.GetComponent<Planet> ().GetComponent<Planet> ().hasStreet = true;

		return true;
	}
}
