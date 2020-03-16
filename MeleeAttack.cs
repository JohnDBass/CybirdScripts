using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private int damage = 10; // How much damage the player does
    [SerializeField]
    private float timeActive = 3; // how long the attack is Active

    private float spawnTime;

    /*
     * Despawns that attack after time has passed
     */
    private void LateUpdate()
    {
        if (spawnTime < Time.time)
        {
            Debug.Log("Not active");
            this.gameObject.SetActive(false);
        }
    }

    /*
     * Public function that spawns attack
     */
    public void SetTime()
    {
        spawnTime = Time.time + timeActive;
    }

    /*
     * Checks for collision and deals damage
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hitting");
            other.SendMessage("TakeDamage", damage);
        }
    }
}
