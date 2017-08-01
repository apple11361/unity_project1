using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public GameObject boom;

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, 5);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        Instantiate(boom, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
