using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int currentRound;
	public List<Player> allPlayers;
	public Player currentPlayer;
	public int currentPlayerCount;

    public bool gameOver=false;

	public int maxAP = 5;
	public int currentAP = 1;

	public World world;

	void Start () {
		world = GameObject.FindGameObjectWithTag ("World").GetComponent<World> ();
		StartGame ();
	}

    void Update() {
        checkWin();

		if (currentPlayer == allPlayers [0] && currentPlayer.ap <= 0) {
			GameObject.FindGameObjectWithTag("Ready").GetComponent<Button> ().interactable = true;
		} else {
			GameObject.FindGameObjectWithTag("Ready").GetComponent<Button> ().interactable = false;
		}

		if (currentPlayer == allPlayers [1] && currentPlayer.ap <= 0) {
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

        if(currentPlayer==allPlayers[1])
        {
            aiMove();
        }
	}

    void aiMove()
    {
        GameObject referenz = null;
        List<GameObject> friendlyPlanets = new List<GameObject>(); ;


        while (currentPlayer.ap > 0)
        {
            //Loop ob Planet verfügbar ist
            for (int i = 0; i < currentPlayer.currentPlanet.GetComponent<Planet>().nodes.Count; i++)
            {
                if (currentPlayer.currentPlanet.GetComponent<Planet>().nodes[i].GetComponent<Planet>().ownedByPlayer == null)
                {
                    referenz = currentPlayer.currentPlanet.GetComponent<Planet>().nodes[i]; break;
                }
                if (currentPlayer.currentPlanet.GetComponent<Planet>().nodes[i].GetComponent<Planet>().ownedByPlayer == currentPlayer)
                {
                    friendlyPlanets.Add(currentPlayer.currentPlanet.GetComponent<Planet>().nodes[i]);
                }
            }
            //Befreundeten Planeten finden und zufällig einen wählen
                if (referenz == null)
            {
                int planet = Random.Range(0, friendlyPlanets.Count); // liefert nur 0
                referenz = friendlyPlanets[planet];
                currentPlayer.AiSetCurrentPlanet(referenz);
            }

            //fremder Planet wurde gefunden
            else if (referenz != null)
            {
                currentPlayer.ap--;
                currentPlayer.AiSetCurrentPlanet(referenz);
            }
            friendlyPlanets.Clear();
        }
        //Zug vorbei
        StartNextTurn();
    }


	public void SelectPlanet(Player player, Planet planet){
		if (player.ap > 0) {
			if (planet.GetComponent<Planet> ().ownedByPlayer == player) {
				player.ap++;
			}
			player.SetCurrentPlanet (planet.gameObject);
		}
	}

    bool checkWin()
    {
        int playerPlanets = 0;
        int aiPlanets = 0;
        for(int i=0;i<world.planets.Count;i++)
        {
            if (world.planets[i].GetComponent<Planet>().ownedByPlayer == allPlayers[0])
                playerPlanets++;
            else if (world.planets[i].GetComponent<Planet>().ownedByPlayer == allPlayers[1])
                aiPlanets++;
        }

        if (playerPlanets+aiPlanets == world.planets.Count)
        {
            gameOver = true;
            if(playerPlanets>aiPlanets)
            {
                Debug.Log("Don Melone gewinnt der Kinderhandelskrieg mit " +playerPlanets+" Planeten!");
            }
            if(aiPlanets > playerPlanets)
            {
                Debug.Log("Du hattest " + playerPlanets + " Planeten! Die Regierung hat dich mit " + aiPlanets +" geschlagen!");
            }
            else
            {
                Debug.Log("Dafuq, Unentschieden?");
            }
            return true;
        }
        return false;
    }
}
