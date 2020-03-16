using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public Renderer rend; 
    public Material cpOn; 
    public Material cpOff;
    private AudioSource source;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private AudioClip checkpointSound;
    [SerializeField]
    private float checkpointVolume = 0.5f;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        source = this.GetComponent<AudioSource>();
    }
    public void CheckpointOn()
    {
        source.PlayOneShot(checkpointSound, checkpointVolume);
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>(); //finds all objects using checkpoint script
        foreach (Checkpoint cp in checkpoints)
        {
            cp.CheckpointOff();
        }

        transform.GetChild(0).gameObject.SetActive(true);
        rend.material = cpOn;
    }

    public void CheckpointOff()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        rend.material = cpOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.SetRespawnPoint(transform.position);
            CheckpointOn();
        }
    }
}
