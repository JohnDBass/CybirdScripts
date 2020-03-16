using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoxScript : MonoBehaviour {

    [Header("Should find the player automatically")]
    [SerializeField]
    private PlayerController playerController;
  
    void Start () {
        playerController = FindObjectOfType<PlayerController>();
    }
	
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.Respawn();
        }
    }
}
