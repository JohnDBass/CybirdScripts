using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedPickup : MonoBehaviour {

    public int scoreGained = 10;
    public GameObject player;
    private PlayerController pcontroller;

    private void Start()
    {
        pcontroller = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Where to make the object float or rotate
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.SendMessage("AddScore", scoreGained);
            pcontroller.EnableRanged();
            this.gameObject.SetActive(false);
        }
    }
}
