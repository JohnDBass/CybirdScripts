using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateResetBox : MonoBehaviour {

    [Header("Deactivate whichever ResetBox this is pointing too")]
    [SerializeField]
    private GameObject whichResetBox;

    void Start () {
		
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            whichResetBox.SetActive(true);
        }
    }
}
