using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject player;

    public float zoom;

    private Vector3 offset;

    public float dista;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        float maxDist = 3;
        dista = dist;
        Vector3 planet= new Vector3(0.0f,0.0f,0.0f);

        zoom = Input.GetAxis("Mouse ScrollWheel");

        Debug.Log(zoom);

       
       // transform.position = player.transform.position + offset;
       if( (transform.forward.z >0)&& (dist< 3))
        {
            transform.position = new Vector3(0,0,-3);
        }
        if ((transform.forward.z < 0) && (dist < 3))
        {
            transform.position = new Vector3(0, 0, 3);
        }

        else
        {
            transform.position += new Vector3(0, 0, zoom);
        }


        //Vector3 distance= planet



        bool rightMouseButtonPresed = Input.GetMouseButton(1);

        if (rightMouseButtonPresed)
        {
            transform.RotateAround(planet, Vector3.up, 20 * Time.deltaTime);
        }


        
    }
}
