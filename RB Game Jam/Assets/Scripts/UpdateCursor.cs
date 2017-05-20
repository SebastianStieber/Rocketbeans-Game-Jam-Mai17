using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCursor : MonoBehaviour {

    HandleCursor cursor;
    bool left = false;
    // Use this for initialization

    bool carrying;
	void Start () {
        cursor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HandleCursor>();
        cursor.setMouse();
	}
	
	// Update is called once per frame
	void Update () {
        if(left)
        {
            cursor.setGrab();
        }
	}

    private void OnMouseEnter()
    {
        cursor.setHand();
    }

    private void OnMouseExit()
    {
        cursor.setMouse();
    }

    private void OnMouseDown()
    {
        left = true;
    }

    private void OnMouseUp()
    {
        left = false;
        cursor.setMouse();
    }
    private void OnMouseOver()
    {
        cursor.setHand();
    }

}
