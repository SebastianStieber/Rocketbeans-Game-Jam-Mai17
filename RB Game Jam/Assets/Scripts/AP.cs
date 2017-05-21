using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AP : MonoBehaviour {

	public List<Image> images = new List<Image> ();

	void Start () {
		
	}

	void Update () {
		
	}

	public void SetImages(Player player){
		for (int i = 0; i < images.Count; i++) {
			if (i < player.ap) {
				images [i].gameObject.SetActive (true);
			} else {
				images [i].gameObject.SetActive (false);
			}
		}
	}
}
