using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerControl : MonoBehaviour {

    [SerializeField]
    [Range(0.5f, 1.5f)]
    private float fireRate = 1;

    [SerializeField]
    [Range(1, 10)]
    private int damage = 1;

    //[SerializeField]
    //private Transform firePoint;

    [SerializeField]
    private ParticleSystem muzzleParticle;

    private float timer;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
            {
                Debug.Log("I'm here");
                timer = 0f;
                FireGun();
            }
        }
    }

    private void FireGun()
    {
        muzzleParticle.Play();

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
        //Ray ray = new Ray(firePoint.position, firePoint.forward);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            var health = hitInfo.collider.GetComponent<HealthController>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }


    }
}
