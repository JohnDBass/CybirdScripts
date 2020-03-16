using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    AudioSource audioSource;
    [SerializeField]
    private PlayerController playerController;
    private float maxHealth;
    //public Text healthText; // UI element that is updated to match player health
    [SerializeField]
    private AudioClip HealNoise;
    [SerializeField]
    private float health = 100f, HealVolume;
    [SerializeField]
    private Image healthBar;

    private void Start()
    {
        SetMessage();
        maxHealth = health;
        playerController = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    /*
     * Takes an integer and deals damage to player
     */
    public void TakeDamage(int damage)
    {
        health -= damage;
        SetMessage();
        IsDead();
    }

    /*
     * Takes an integer and heals player
     */
    public void HealDamage(int recover)
    {
        if ((health + recover) > 100)
        {
            health = 100;
        }
        else
        {
            health += recover;
        }
        audioSource.PlayOneShot(HealNoise, HealVolume);
        SetMessage();
    }

    /*
     * Checks if the player's hp is 0
     */
    private void IsDead()
    {
        if (health <= 0)
        {
            playerController.Respawn();
            HealDamage(100);
        }
    }

    /*
     * Updates the UI element to match player's health
     */
    private void SetMessage()
    {
       // healthText.text = "Health: " + health;
        healthBar.fillAmount = health/maxHealth;
    }
}
