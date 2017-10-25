using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using System.Drawing;


public class cube : MonoBehaviour {

    public float speed;                     //移動速度
    public GameObject bullet;               //子彈
    private bool which_gun;                 //選槍
    private MeshRenderer mr;                //cube外觀


    // Use this for initialization
    void Start ()
    {
        speed = 10;
        which_gun = false;

        mr = GetComponent<MeshRenderer>();
        mr.material.color = UnityEngine.Color.red;

        
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
        if (Input.GetKey(KeyCode.Q))
        {
            left_rotate(Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            right_rotate(Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            fire();
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

    //左移
    void left(float unit)
    {
        transform.localPosition += speed * unit * Vector3.left;
    }

    //右移
    void right(float unit)
    {
        transform.localPosition += speed * unit * Vector3.right;
    }

    //左轉
    void left_rotate(float unit)
    {
        transform.localEulerAngles -= 5 * speed * unit * Vector3.up;
    }

    //右轉
    void right_rotate(float unit)
    {
        transform.localEulerAngles += 5 * speed * unit * Vector3.up;
    }

    //開槍
    void fire()
    {
        GameObject new_bullet = Instantiate(bullet);

        if (which_gun)
        {
            new_bullet.transform.localPosition = transform.localPosition + transform.forward * 2 - transform.right * 1;
            new_bullet.GetComponent<Rigidbody>().velocity = 70 * transform.forward + 1 * transform.right;

            which_gun = false;
        }
        else
        {
            new_bullet.transform.localPosition = transform.localPosition + transform.forward * 2 + transform.right * 1;
            new_bullet.GetComponent<Rigidbody>().velocity = 70 * transform.forward - 1 * transform.right;

            which_gun = true;
        }
    }


    void OnGUI()
    {
        GUI.TextArea(new Rect(2, 50, 200, 50), "test");
    }




}
