using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public Transform target;    //跟隨cube
    public float distance = 7;  //攝影機和cube的距離
    private Vector3 camera_position;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //跟隨目標
    void LateUpdate()
    {
        camera_position = new Vector3(0, 1.5f, -distance) + target.position;

        transform.position = camera_position;
    }
}
