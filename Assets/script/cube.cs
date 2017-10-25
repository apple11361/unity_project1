using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using System.Threading;

public class cube : MonoBehaviour {

    public float speed;                     //移動速度
    public GameObject bullet;               //子彈
    private bool which_gun;                 //選槍
    private MeshRenderer mr;                //cube外觀
    private string GPS_data;                //GPS資料
    private string GPS_information = "";    //parse後的需要資訊
    private double longitude = 0;           //經度
    private double latitude = 0;            //緯度

    /****************variable about com port****************/
    /*const int dollar_sign = 36;         //$
    /*const int carriage_return = 13;     //CR
    /*const int line_feed = 10;           //LF
    /*
    /*private bool receiving;             //decide if execute DoReceive()         
    /*private SerialPort comport;
    /*private Thread t;
    *********************************************************/

    /********************receive mavlink********************/
    MAVLink.MavlinkParse mavlink = new MAVLink.MavlinkParse();
    MAVLink.MAVLinkMessage packet;
    private SerialPort comport;
    object readlock = new object();     // locking to prevent multiple reads on serial port


    // Use this for initialization
    void Start ()
    {
        speed = 10;
        which_gun = false;

        mr = GetComponent<MeshRenderer>();
        mr.material.color = Color.red;

        /***********setting about com port***********/
        //receiving = true;

        /*comport = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
        if(!comport.IsOpen)
        {
            comport.Open();
            comport.ReadTimeout = 2000;     // set timeout to 2 seconds

            //t = new Thread(DoReceive);
            //t.IsBackground = true;
            //t.Start();
        }*/
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
    /*void DoReceive()
    {
        List<Byte> tempList = new List<Byte>();         //store receivedValue temporarily until CR LF
        int receivedValue;

        while (receiving)
        {
            receivedValue = comport.ReadByte();
            switch (receivedValue)
            {
                case dollar_sign:
                    tempList.Clear();
                    tempList.Add((Byte)receivedValue);
                    break;

                case carriage_return:                   //for computer test
                    tempList.Add((Byte)receivedValue);
                    parse(tempList);
                    break;

                case line_feed:                         //for GPS input
                    tempList.Add((Byte)receivedValue);
                    parse(tempList);
                    break;

                case -1:
                    break;

                default:
                    tempList.Add((Byte)receivedValue);
                    break;
            }
        }
    }*/

    void parse(List<Byte> tempList)
    {
        string header = System.Text.ASCIIEncoding.ASCII.GetString(tempList.ToArray(), 0, 6);
        //string pat = @"\w+,\w+,([0-9]*.[0-9]*,\w),([0-9]*.[0-9]*,\w),\w+";
        string pat = @".*,([0-9]*\.[0-9]*),[S,N].*,([0-9]*\.[0-9]*),[E,W].*";
        Regex r = new Regex(pat, RegexOptions.IgnoreCase);
        Match m;

        print(System.Text.ASCIIEncoding.ASCII.GetString(tempList.ToArray()));
        if (header == "$GPGGA")
        {
            GPS_data = System.Text.ASCIIEncoding.ASCII.GetString(tempList.ToArray());
            m = r.Match(GPS_data);

            print(GPS_data);
            print(m.Groups[1]);
            print(m.Groups[2]);

            if (m.Success)
            {
               //longitude = Convert.ToDouble(m.Groups[2]);      //經度，英文好難QQ
               //latitude = Convert.ToDouble(m.Groups[1]);       //緯度，英文好難QQ

                GPS_information = "";
                GPS_information += (m.Groups[1] + "\n");
                GPS_information += m.Groups[2];
            }
        }
    }

    void OnGUI()
    {
        GUI.TextArea(new Rect(2, 50, 200, 50), GPS_information);
        GUI.TextArea(new Rect(2, 100, 200, 50), longitude.ToString());
    }

    private void OnApplicationQuit()
    {
        //receiving = false;
        comport.DiscardInBuffer();
        comport.Close();
        //t.Abort();
    }


}
