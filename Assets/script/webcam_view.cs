using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class webcam_view : MonoBehaviour {

    public Transform target;                //跟隨相機移動
    private float distance = -37.5f;        //畫面和相機距離
    private Vector3 view_position;          //畫面位置
    private Vector3 view_rotation;          //畫面角度
    private Renderer rd;

    /******************************************/
    //The texture that holds the video captured by the webcam
    private WebCamTexture webCamTexture;
    //An array that stores a reference to the names of all connected webcams  
    private string[] nameWebCams;
    //The current webcam  
    private int currentCam = 0;
    //The selected webcam  
    private int selectedCam = 0;

    // Use this for initialization
    void Start ()
    {
        //An integer that stores the number of connected webcams  
        int numOfCams = WebCamTexture.devices.Length;

        //Initialize the nameWebCams array to hold the same number of strings as there are webcams  
        this.nameWebCams = new string[numOfCams];

        //Get the name of each connected camera and store it into the 'nameWebCams' array  
        for (int i = 0; i < numOfCams; i++)
        {
            this.nameWebCams[i] = WebCamTexture.devices[i].name;
        }

        rd = GetComponent<Renderer>();
        webCamTexture = new WebCamTexture();
        rd.material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //跟隨目標
    void LateUpdate()
    {
        view_position = new Vector3(-distance * Mathf.Sin(Mathf.PI * target.eulerAngles.y / 180.0f), 0, -distance*Mathf.Cos(Mathf.PI*target.eulerAngles.y/180.0f)) + target.position;
        view_rotation = target.eulerAngles + new Vector3(90f, 180, 0);
        
        transform.position = view_position;
        transform.eulerAngles = view_rotation;
    }

    void OnGUI()
    {
        //Render the SelectionGrid listing all the cameras and save the selected one at 'selectedCam'  
        selectedCam = GUI.SelectionGrid(new Rect(2, 2, 200, 40), currentCam, nameWebCams, 1);

        //If the selected camera isn't the current camera  
        if (selectedCam != currentCam)
        {
            //Assign the value of currentCam to selectCam  
            currentCam = selectedCam;
            //Stop the streaming of captured images  
            webCamTexture.Stop();
            //Assign a different webcam to the webCamTexture  
            webCamTexture.deviceName = WebCamTexture.devices[currentCam].name;
            //Start streaming the captured images from this webcam to the texture  
            webCamTexture.Play();
        }
    }
}
