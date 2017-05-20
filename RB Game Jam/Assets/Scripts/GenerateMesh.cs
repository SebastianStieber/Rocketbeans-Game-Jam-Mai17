using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMesh : MonoBehaviour {

	Vector3[] vertices = new Vector3[]{new Vector3(0, 0, 0), new Vector3(-2, -2, 0), new Vector3(2, -2, 0), new Vector3(0, 0, 2)};
	int[] triangles = new int[]{0, 2, 1, 2,3, 1, 0, 3, 2, 0, 1, 3};

	// Use this for initialization
	void Start () {
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
