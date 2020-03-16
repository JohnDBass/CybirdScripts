using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    [SerializeField]
    private float startingHealth = 5;
    private GameObject player;

    private float currentHealth;
    [SerializeField]
    private SceneLoader sceneLoad;
    public Image healthBar;
    public int score = 500;

    private void OnEnable()
    {
        currentHealth = startingHealth;
        healthBar.fillAmount = currentHealth / startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.fillAmount = currentHealth / startingHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        player = GameObject.Find("Player");
        player.SendMessage("AddScore", score);
        sceneLoad.LoadComicPagefour();
        gameObject.SetActive(false);
    }
}
