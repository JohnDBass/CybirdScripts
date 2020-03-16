using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    public int pointsGained = 15;
    [SerializeField]
    private int startingHealth = 5;

    private int currentHealth;
    
    public GameObject explosion;


    private void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            
            Die();
        }
    }

    private void Die()
    {
        
        GameObject player = GameObject.Find("Player");
        player.SendMessage("AddScore", pointsGained);
        gameObject.SetActive(false);
    }

 
}
