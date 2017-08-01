using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public Transform target;            //跟隨cube
    public float distance = 0f;         //攝影機和cube的距離
    private Vector3 camera_position;    //相機位置
    private Vector3 camera_rotation;    //相機角度

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
        camera_position = new Vector3(0, 1f, -distance) + target.position;
        camera_rotation = target.eulerAngles;

        transform.position = camera_position;
        transform.eulerAngles = camera_rotation;
    }
}
