using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sight : MonoBehaviour {

    public Transform target;                //跟隨相機移動
    private float distance = -30f;          //畫面和相機距離
    private Vector3 view_position;          //畫面位置
    private Vector3 view_rotation;          //畫面角度

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
        view_position = new Vector3(-distance * Mathf.Sin(Mathf.PI * target.eulerAngles.y / 180.0f), 0, -distance * Mathf.Cos(Mathf.PI * target.eulerAngles.y / 180.0f)) + target.position;
        view_rotation = target.eulerAngles + new Vector3(90f, 180, 0);

        transform.position = view_position;
        transform.eulerAngles = view_rotation;
    }
}
