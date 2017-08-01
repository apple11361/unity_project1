using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour {

    private MeshRenderer mr;                //building外觀

    // Use this for initialization
    void Start ()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material.color = Color.blue;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
