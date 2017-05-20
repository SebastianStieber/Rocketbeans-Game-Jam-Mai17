using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	[Range(1, 10)]
	public int range = 10;
	[Range(1, 1000)]
	public int maxPlanets = 50;
	[Range(1, 10)]
	public float distanceBetweenPlanets = 10f;
	[Range(0.1f, 5)]
	public float randomness = 1f;
	[Range(1, 10)]
	public float streetDistanceMultiplier = 3f;

	public List<GameObject> planets = new List<GameObject>();

	public GameObject planetPrefab;

	int oldRange = 10;
	int oldMaxPlanets = 50;
	float oldDistanceBetweenPlanets = 10f;
	float oldRandomness = 1f;
	float oldStreetDistanceMultiplier = 3f;

	void Start () {
		GeneratePlanets ();
		ApplyRandomness ();
	}

	void Update () {
		BuildStreets ();

		CheckChanges ();

		oldRange = range;
		oldMaxPlanets = maxPlanets;
		oldRandomness = randomness;
		oldStreetDistanceMultiplier = streetDistanceMultiplier;
		oldDistanceBetweenPlanets = distanceBetweenPlanets;
	}

	void CheckChanges(){
		if (oldRange != range || oldMaxPlanets != maxPlanets || oldRandomness != randomness ||
			oldStreetDistanceMultiplier != streetDistanceMultiplier || oldDistanceBetweenPlanets != distanceBetweenPlanets) {
			for (int i = maxPlanets - 1; i >= 0; i--) {
				GameObject p = planets [i];
				planets.Remove (p);
			}

			planets = new List<GameObject>();

			GameObject[] g = GameObject.FindGameObjectsWithTag ("Planet");
			for (int i = 0; i < g.Length; i++)
				Destroy(g[i]);

			GeneratePlanets ();
			ApplyRandomness ();
		}
	}

	void GeneratePlanets(){
		for (int i = 0; i < maxPlanets; i++) {
			GeneratePlanet ();
		}
	}

	void GeneratePlanet(){
		Vector3 position = CalculatePosition ();

		GameObject planet = Instantiate (planetPrefab);
		planet.GetComponent<Planet> ().SetCoordinates (position);

		planets.Add (planet);
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

	void ApplyRandomness (){
		foreach (GameObject p in planets) {
			Vector3 r = new Vector3 (Random.Range(-randomness, randomness),Random.Range(-randomness, randomness),Random.Range(-randomness, randomness));
			p.transform.position = p.transform.position + r;
		}
	}

	void BuildStreets(){
		foreach (GameObject p in planets) {
			foreach (GameObject p2 in planets) {
				if (Vector3.Distance (p.GetComponent<Planet> ().coordinates, p2.GetComponent<Planet> ().coordinates) <= distanceBetweenPlanets * streetDistanceMultiplier) {
					Debug.DrawLine (p.transform.position, p2.transform.position);
				}
			}
		}
	}
}
