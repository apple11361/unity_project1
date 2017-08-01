using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour {

    public Transform target;                //跟隨相機旋轉
    private Vector3 light_rotation;         //光線角度

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        light_rotation = target.eulerAngles + new Vector3(30f, 0, 0);

        transform.eulerAngles = light_rotation;
    }
}
