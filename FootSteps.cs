using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour {


    AudioSource audioSource;

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private AudioClip Steps;
    [SerializeField]
    private float NoiseVolume = 0.7f;

    
    void Start () {
        playerController = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }
	
	
	void Update () {
		
	}


    private void Step()
    {
        if (playerController.isOnGround)
        {
            audioSource.PlayOneShot(Steps, NoiseVolume);
        }
        
    }
}

