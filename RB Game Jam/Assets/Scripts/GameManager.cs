using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int currentRound;
	public List<Player> allPlayers;
	public Player currentPlayer;
	public int currentPlayerCount;

    public bool gameOver = false;

	public int maxAP = 5;
	public int currentAP = 1;

	public World world;

	public float aiCooldown = 1;
	float currentAICooldown;

	public int maxAIMoves = 1000;

	GameObject lastAIPlanet; 

	void Start () {
		world = GameObject.FindGameObjectWithTag ("World").GetComponent<World> ();
		currentAICooldown = aiCooldown;
		StartGame ();
	}

    void Update() {
		CheckButton ();

		if(currentPlayer == allPlayers[1])
			UpdateAI ();

		if (currentPlayer == allPlayers [1] && currentPlayer.ap <= 0) {
			StartNextTurn ();
		}

		GameObject.FindGameObjectWithTag ("PlayerAPUI").GetComponent<AP> ().SetImages (currentPlayer);
	}

	private void CheckButton() {
		if (currentPlayer == allPlayers [1] || (currentPlayerCount == 0 && currentPlayer.ap > 0)) {
			GameObject.FindGameObjectWithTag ("Ready").GetComponent<Animator> ().SetBool ("IsActive", false);
		} else {
			GameObject.FindGameObjectWithTag ("Ready").GetComponent<Animator> ().SetBool ("IsActive", true);
		}

	}

	public void StartGame(){
		currentRound = 0;
		currentPlayer = allPlayers [0];

		foreach (Player p in allPlayers) {
			GameObject planet = world.planets[Random.Range(0, world.planets.Count - 1)];
			p.currentPlanet = planet;
			p.SetCurrentPlanet (planet);
            p.ap = 1;
		}
	}

	public void OnReadyButton(){
		if(currentPlayer.ap <= 0)
			StartNextTurn ();
	}
	public void StartNextRound() {
		currentRound++;
		currentPlayerCount = 0;

		currentAP++;
		if (currentAP > maxAP)
			currentAP = maxAP;

		foreach (Player player in allPlayers){
			player.Reset ();
		}

		currentPlayer = allPlayers [0];
	}

	public void StartNextTurn () {
		currentPlayerCount++;
		if (currentPlayerCount == allPlayers.Count) {
			StartNextRound ();
		} else {
			currentPlayer = allPlayers [currentPlayerCount];
		}

		if (currentPlayerCount == 0) {
			GameObject.FindGameObjectWithTag ("PlayerUI").GetComponent<Text> ().text = "DU SPIELST";
		} else if (currentPlayerCount == 1) {
			GameObject.FindGameObjectWithTag ("PlayerUI").GetComponent<Text> ().text = "CPU SPIELT";
		}
	}

	void UpdateAI(){
		currentAICooldown -= Time.deltaTime;
		if (currentAICooldown <= 0) {
			currentAICooldown = aiCooldown;
			if (currentPlayer.ap > 0) {
				List<GameObject> nodes = currentPlayer.currentPlanet.GetComponent<Planet> ().nodes;
					
				bool hasNewPlanet = false;
				foreach (GameObject planet in nodes) {
					if (!hasNewPlanet && planet.GetComponent<Planet> ().ownedByPlayer == null) {
						hasNewPlanet = true;
						currentPlayer.SetCurrentPlanet (planet);
					}
				}
				
				if(!hasNewPlanet){
					List<GameObject> ownedPlanets = new List<GameObject> ();

					foreach (GameObject planet in nodes) {
						if (planet.GetComponent<Planet> ().ownedByPlayer == currentPlayer && planet != lastAIPlanet) {
							ownedPlanets.Add (planet);
						}
					}

					int i = Random.Range (0, ownedPlanets.Count);
					lastAIPlanet = currentPlayer.currentPlanet;
					currentPlayer.SetCurrentPlanet (ownedPlanets [i]);
				}
			} else {
				StartNextTurn ();
			}
		}
	}

	public void SelectPlanet(Player player, Planet planet){
		if (player.ap > 0) {
			player.SetCurrentPlanet (planet.gameObject);
			CheckWinState ();
		}
	}

	void OnDrawGizmos(){
		if (currentPlayer != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (currentPlayer.transform.position, 1);
		}
	}

	void CheckWinState(){
		List<GameObject> allPlanets = world.planets;
		List<Planet> freePlanets = new List<Planet> ();
		int c = 0;
		foreach (GameObject planet in allPlanets){
			if (planet.GetComponent<Planet> ().ownedByPlayer == null) {
				freePlanets.Add (planet.GetComponent<Planet>());
				c++;
			}
		}

		if (c == 0) {
			GameOver (true, true);
			return;
		}

		bool pl = false;
		bool ai = false;
		foreach (Planet freePlanet in freePlanets){
			foreach (GameObject p in freePlanet.nodes){
				if(p.GetComponent<Planet>().ownedByPlayer == allPlayers[0])
					pl = true;
				if (p.GetComponent<Planet>().ownedByPlayer == allPlayers [1])
					ai = true;
			}
		}

		if (!pl || !ai)
			GameOver (pl, ai);
	}

	void GameOver(bool pl, bool ai){
		Debug.Log ("ai " + ai + " pl " + pl);

		GameObject.FindGameObjectWithTag ("GameOverScreen").SetActive (true);

		string text = "";

		if (allPlayers [0].ownedPlanets.Count< allPlayers [1].ownedPlanets.Count) {
			text = "Die KI gewinnnt";
		} else {
			text = "Du gewinnst";
		}

		text += "\n" + allPlayers [0].ownedPlanets.Count + "\ngegen\n" + allPlayers [1].ownedPlanets.Count;

		GameObject.FindGameObjectWithTag ("GameOverText").GetComponent<Text> ().text = text;
	}
}
