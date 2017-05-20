using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour {

    GameObject SelectedObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo) && Input.GetMouseButton(0))
        {

            GameObject hitObject = hitInfo.transform.root.gameObject;

            
            SelectObject(hitObject);
        }
        else if (Input.GetMouseButton(0))
        {
            ClearSelection();
        }
	}

    void SelectObject(GameObject obj)
    {
        if(SelectedObject != null)
        {
            if (obj == SelectedObject)
                return;
            ClearSelection();
        }
        SelectedObject = obj;

        Renderer r = SelectedObject.GetComponent<Renderer>();
        Material m = r.material;
        m.color = Color.green;
        r.material = m;
    }

    void ClearSelection()
    {
        if (SelectedObject == null)
        {
            return;
        }

        Renderer r = SelectedObject.GetComponent<Renderer>();
        Material m = r.material;
        m.color = Color.white;
        r.material = m;

        SelectedObject = null;
    }
}
