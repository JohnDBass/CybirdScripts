using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShot : MonoBehaviour {

    bool Ispawned = false;
    Rigidbody rigid;

    [SerializeField]
    private float speed, aliveTime = 2f;

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
            rigid.velocity = transform.forward * 20;
            Ispawned = true;
        }
        //Debug.Log(transform.rotation);
    }    
}

