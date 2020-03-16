using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    private GameObject laserEnd;
    private GameObject player;
    private ShipHealth shipHealth;
    //Rigidbody rigid;

    [SerializeField]
    private int laserSpeed = 100;

    [SerializeField]
    private float damage = 20f;

    SphereCollider endCollider;
    MeshRenderer endMesh;
    
    void Start ()
    {
        player = GameObject.Find("Player");
        shipHealth = FindObjectOfType<ShipHealth>();
        //rigid = this.GetComponent<Rigidbody>();

        //creating an invisible 'end point' where the player was last standing
        laserEnd = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        endCollider = laserEnd.GetComponent<SphereCollider>();
        endCollider.isTrigger = true;
        endMesh = laserEnd.GetComponent<MeshRenderer>();
        endMesh.enabled = false;
        laserEnd.transform.position = player.transform.position;
        
    }	
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, laserEnd.transform.position, laserSpeed * Time.deltaTime);       
        float endDistance = Vector3.Distance(transform.position, laserEnd.transform.position);

        
        if (endDistance <= 0)
        {
            Destroy(laserEnd.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            Destroy(laserEnd.gameObject);
            shipHealth.TakeDamage(damage);
        }
    }        
}
