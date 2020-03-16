using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour {

    public GameObject myBridge;
    private bool active = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (active)
        {
            myBridge.SetActive(true);
        }
    }

    void DoTheThing()
    {
        Debug.Log("BridgeUsed");
        active = true;
    }
}
