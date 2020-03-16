using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int scoreGained = 5;
    [SerializeField]
    private int recoverAmount;

    [SerializeField]
    private GameObject player;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.Find("Player");
            player.SendMessage("AddScore", scoreGained);
            playerHealth.HealDamage(recoverAmount);
            this.gameObject.SetActive(false);
        }
    }

}
