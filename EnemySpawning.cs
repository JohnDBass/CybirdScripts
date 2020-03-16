using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {


    public int enemySetType = 1;
    public GameObject EnemyType;
    public int numSpawned = 1;
    public int currentNum = 0;


    bool heDied = true;
    float SpawnTimer = 5f;
    private GameObject newEnemy;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(EnemyType, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnTimer >= 5 && currentNum < numSpawned)
        {
            heDied = true;
            SpawnTimer = 0;
        }
        SpawnTimer += Time.deltaTime;

        if (heDied)
        {
            newEnemy = Instantiate(EnemyType, transform.position, transform.rotation);
            currentNum++;
            heDied = false;
            newEnemy.SendMessage("SetType", enemySetType);
        }

       
    }

    void OhNoHeDed()
    {
        heDied = true;
    }
}
