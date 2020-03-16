using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimShot : MonoBehaviour {

    bool Ispawned = false;
    Rigidbody rigid;

    [SerializeField]
    private float speed = 100f, aliveTime = 2f;

    [SerializeField]
    private int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {            
            Debug.Log("Hitting");
            other.SendMessage("TakeDamage", damage);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime -= Time.deltaTime;
        if (aliveTime <= 0)
        {
            Destroy(this.gameObject);
        }

        if (Ispawned == false)
        {
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            Debug.DrawRay(ray.origin, ray.direction * 300, Color.red, 2f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                //transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
                //rigid.AddForce(transform.forward * 500);
                rigid.AddForce((hit.point - transform.position) * speed);
                transform.LookAt(hit.point);                
            }
            Ispawned = true;
        }
        //Debug.Log(transform.rotation);
    }    
}

