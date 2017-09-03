using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using System.Threading;

public class cube : MonoBehaviour {

    public float speed;             //移動速度
    public GameObject bullet;       //子彈
    private bool which_gun;         //選槍
    private MeshRenderer mr;        //cube外觀

    /****************variable about com port****************/
    const int dollar_sign = 36;         //$
    const int carriage_return = 13;     //CR
    const int line_feed = 10;           //LF

    private bool receiving;         //decide if execute DoReceive()         
    private SerialPort comport;
    private Thread t;
    

    // Use this for initialization
    void Start ()
    {
        speed = 3;
        which_gun = false;

        mr = GetComponent<MeshRenderer>();
        mr.material.color = Color.red;

        /***********setting about com port***********/
        receiving = true;

        comport = new SerialPort("COM5", 9600, Parity.None, );
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

    //serial port communication for GPS
    void DoReceive()
    {
        List<Byte> tempList = new List<Byte>();     //store receivedValue temporarily until CR LF
        int receivedValue;

        while (receiving)
        {
            if (comport.BytesToRead > 0)
            {
                receivedValue = comport.ReadByte();

                switch (receivedValue)
                {
                    case dollar_sign:
                        tempList.Clear();
                        tempList.Add((Byte)receivedValue);
                        break;

                    case line_feed:
                        tempList.Add((Byte)receivedValue);

                        break;

                    case -1:
                        break;

                    default:
                        tempList.Add((Byte)receivedValue);
                        break;
                }
            }
            else
            {
                Thread.Sleep(16);
            }
        }
    }
}
