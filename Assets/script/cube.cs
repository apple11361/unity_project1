using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class cube : MonoBehaviour {

    public float speed;             //移動速度
    private MeshRenderer mr;        //cube外觀
    private StreamReader sr;        //讀入移動資料
    private StreamWriter sw;        

    // Use this for initialization
    void Start ()
    {
        speed = 5;

        mr = GetComponent<MeshRenderer>();
        mr.material.color = Color.red;

        sr = new StreamReader("move_file.txt");
        sw = new StreamWriter("move_file.txt");
    }

    // Update is called once per frame
    void Update()
    {
        /***************移動****************/
        if (Input.GetKey(KeyCode.W))
        {
            forward(Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            left(Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            backward(Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            right(Time.deltaTime);
        }

    }

    //前進
    void forward(float unit)
    {
        transform.localPosition += speed * unit * Vector3.forward;
    }

    //後退
    void backward(float unit)
    {
        transform.localPosition += speed * unit * Vector3.back;
    }

    //左轉
    void left(float unit)
    {
        transform.localPosition += speed * unit * Vector3.left;
    }

    //右轉
    void right(float unit)
    {
        transform.localPosition += speed * unit * Vector3.right;
    }
}
