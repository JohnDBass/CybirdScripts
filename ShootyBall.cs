using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyBall : MonoBehaviour
{

    private GameObject firePoint;
    private GameObject player;

    SphereCollider myCollider;
    MeshRenderer myMesh;

    [SerializeField]
    private int bulletSpeed = 20, damage = 20;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = FindObjectOfType<PlayerHealth>();

        firePoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        myCollider = firePoint.GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myMesh = firePoint.GetComponent<MeshRenderer>();
        myMesh.enabled = false;
        firePoint.transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, firePoint.transform.position, bulletSpeed * Time.deltaTime);
        float dist = Vector3.Distance(transform.position, firePoint.transform.position);
        if (dist <= 0)
        {
            Destroy(firePoint.gameObject);
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            Destroy(firePoint.gameObject);
            playerHealth.TakeDamage(damage);
        }
    }
}
