using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    public Transform sphere;
    public float speed = 0.001f;

    private Transform center;

    void Start() {
        center = new GameObject().transform;
        center.parent = sphere;
        center.position = Vector3.zero;
        transform.parent = center; }

    void  Update()
    { // if the sphere moves, uncomment the following line // center.position = sphere.position;
        
    }
}
