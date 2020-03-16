using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipHealth : MonoBehaviour {

    AudioSource audioSource;
    [SerializeField]
    private ShipController shipController;
    [SerializeField]
    private AudioClip HealNoise;
    [SerializeField]
    private float health = 200f, HealVolume;
    [SerializeField]
    private Image healthBar;
    private float maxHealth;
    public int respawnPenalty = 50;

    void Start () {
        SetMessage();
        maxHealth = health;
        shipController = FindObjectOfType<ShipController>();
        audioSource = GetComponent<AudioSource>();
    }	

    public void HealDamage(float recover)
    {
        if ((health + recover) > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += recover;
        }
        audioSource.PlayOneShot(HealNoise, HealVolume);
        SetMessage();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        SetMessage();
        IsDead();
    }

    private void IsDead()
    {
        if (health <= 0)
        {
            this.SendMessage("SubtractScore", respawnPenalty);
            SceneManager.LoadScene("PreBossFight");
        }
    }

    private void SetMessage()
    {
        // healthText.text = "Health: " + health;
        healthBar.fillAmount = health / maxHealth;
    }
}
