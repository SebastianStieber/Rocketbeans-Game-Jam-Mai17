using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

	public Vector3 startDrag;

	public bool isPressed;

    void Start() {
	}

    void  Update() {
		Drag ();
    }

	void Drag(){
		if (Input.GetMouseButtonDown (1)) {
			isPressed = true;

			startDrag = Input.mousePosition;
		} else if (Input.GetMouseButtonUp (1)) {
			isPressed = false;
		}

		if (isPressed) {
			Vector3 drag = Input.mousePosition - startDrag;
			transform.rotation = transform.rotation * Quaternion.Euler (new Vector3(drag.y * Time.deltaTime / 2, drag.x * Time.deltaTime / 2, 0));
		}
	}
}
