using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

	[SerializeField]
	private Vector3 drag;
	private Vector3 startDrag;

	private bool isPressed;

	public float rotationSpeed = 10f;
	public float smoothRotation = 3f;

	private Quaternion currentRotation;

	void Start () {
	}

	void Update () {
		if (Input.GetMouseButtonDown (2)) {
			isPressed = true;
		}
		if (Input.GetMouseButtonUp (2)) {
			isPressed = false;
		}

		if (isPressed) {
			drag = Input.mousePosition - startDrag;
			startDrag = Input.mousePosition;
		} else {
			drag = Vector3.zero;
		}

		currentRotation = Quaternion.Euler (transform.rotation.eulerAngles.x + drag.y * Time.deltaTime * rotationSpeed, transform.rotation.eulerAngles.y + drag.x * Time.deltaTime * rotationSpeed, transform.rotation.eulerAngles.z);

		transform.rotation = Quaternion.Lerp (transform.rotation, currentRotation, 3f * Time.deltaTime);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + transform.forward);
	}
}
