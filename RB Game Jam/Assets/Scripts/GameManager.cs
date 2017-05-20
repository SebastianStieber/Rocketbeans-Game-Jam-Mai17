using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int currentRound;
	public List<Player> allPlayers;
	public Player currentPlayer;
	public int currentPlayerCount;

	public int maxAP = 5;
	public int currentAP = 1;

	public World world;

	void Start () {
		world = GameObject.FindGameObjectWithTag ("World").GetComponent<World> ();
		StartGame ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			StartNextTurn ();
		}
	}

	public void StartGame(){
		currentRound = 0;
		currentPlayer = allPlayers [0];

		foreach (Player p in allPlayers) {
			GameObject planet = world.planets[Random.Range(0, world.planets.Count - 1)];
			p.currentPlanet = planet;
			p.SetCurrentPlanet (planet);
		}
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
	}

	public void SelectPlanet(Player player, Planet planet){
		player.SetCurrentPlanet (planet.gameObject);
	}
}
