﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour
{
    public int scoreGained = 10;
    public GameObject player; // Game object that represents Player
    private PlayerController pcontroller;

    private void Start()
    {
        pcontroller = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Where to make the object float or rotate
    }

    /*
    * Calls public function in Player that enables double jump
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.SendMessage("AddScore", scoreGained);
            pcontroller.EnableDoubleJump();
            this.gameObject.SetActive(false);
        }
    }
}