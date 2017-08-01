using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class cube : MonoBehaviour {

    public float speed;             //移動速度
    public GameObject bullet;       //子彈
    private bool which_gun;         //選槍
    private MeshRenderer mr;        //cube外觀
    private StreamReader sr;        //讀入移動資料
    private StreamWriter sw;
    private string move;     

    // Use this for initialization
    void Start ()
    {
        speed = 3;
        which_gun = false;

        mr = GetComponent<MeshRenderer>();
        mr.material.color = Color.red;

        //sr = new StreamReader("move_file.txt");
        //sw = new StreamWriter("move_file.txt");
    }

    // Update is called once per frame
    void Update()
    {
        /***************移動****************/
        if (Input.GetKey(KeyCode.W))
        {
            forward(Time.deltaTime);
            //sw.WriteLine("w");
        }
        if (Input.GetKey(KeyCode.A))
        {
            left(Time.deltaTime);
            //sw.WriteLine("a");
        }
        if(Input.GetKey(KeyCode.S))
        {
            backward(Time.deltaTime);
            //sw.WriteLine("s");
        }
        if(Input.GetKey(KeyCode.D))
        {
            right(Time.deltaTime);
            //sw.WriteLine("d");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            left_rotate(Time.deltaTime);
            //sw.WriteLine("q");
        }
        if (Input.GetKey(KeyCode.E))
        {
            right_rotate(Time.deltaTime);
            //sw.WriteLine("w");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            fire();
        }
        /*if (Input.GetKey(KeyCode.Z))
        {
            sw.Close();
        }*/

        /*************檔案控制移動************/
        /*move = sr.ReadLine();
        if(move == "w")
        {
            forward(Time.deltaTime);
        }
        else if (move == "a")
        {
            left(Time.deltaTime);
        }
        else if (move == "s")
        {
            backward(Time.deltaTime);
        }
        else if (move == "d")
        {
            right(Time.deltaTime);
        }
        else if (move == "q")
        {
            left_rotate(Time.deltaTime);
        }
        else if (move == "e")
        {
            right_rotate(Time.deltaTime);
        }*/
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
}
