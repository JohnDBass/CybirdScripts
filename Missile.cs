using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    AudioSource audioSource;
    private GameObject player;
    private CapsuleCollider thisCollider;
    private ShipHealth shipHealth;

    Rigidbody rigid;

    [SerializeField]
    private int missileSpeed = 100;
    [SerializeField]
    private float damage = 20f, maxFlightTime = 4.0f, rotateSpeed = 5.0f;

    [SerializeField]
    private AudioClip flightNoise, hitNoise;

    [SerializeField]
    private float flightVolume, hitVolume;

    private float flightTime = 0.0f;

    void Start()
    {
        player = GameObject.Find("Player");
        rigid = this.GetComponent<Rigidbody>();
        shipHealth = FindObjectOfType<ShipHealth>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(flightNoise, flightVolume);
        thisCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        flightTime += Time.deltaTime;
        rigid.velocity = transform.forward * missileSpeed;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, missileSpeed * Time.deltaTime);
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        rigid.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed));

        if (flightTime >= maxFlightTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            audioSource.PlayOneShot(hitNoise, hitVolume);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            thisCollider.isTrigger = false;
            Destroy(this.gameObject, 2f);
            shipHealth.TakeDamage(damage);
        }
    }


}
