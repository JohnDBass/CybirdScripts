using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour {

    private AudioSource source;
    [SerializeField]
    private AudioClip dingNoise;
    [SerializeField]
    private float dingVolume = 0.5f;



    public GameObject DownPos;
    public GameObject UpPos;
    public GameObject platform;

    private GameObject player;
    private bool Active = false;
    private int vatorSpeed = 5;
    private bool goingUp;
    private bool goingDown;
    private float DistanceUp = 1f;
    private float DistanceDown = 0f;
	// Use this for initialization
	void Start () {
        DistanceUp = UpPos.transform.position.y - platform.transform.position.y;
        DistanceDown = platform.transform.position.y - DownPos.transform.position.y;
        player = GameObject.Find("Player");
        source = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (player.transform.position.y <= DownPos.transform.position.y + 1.9 && platform.transform.position.y >= DownPos.transform.position.y + .5)
        {
            platform.transform.Translate(0, Time.deltaTime * -1 * vatorSpeed, 0);
            Active = false;
        }
        
        if (Active == true)
        {
            if(DistanceDown > DistanceUp)
            {
                goingDown = true;
                goingUp = false;
                //Debug.Log("GoingDown");
            }
            else if(DistanceUp > DistanceDown)
            {
                goingUp = true;
                goingDown = false;
                //Debug.Log("GoingUp");

            }

            if(goingUp)
            {
                
                if (platform.transform.position.y >= UpPos.transform.position.y)
                {
                    Active = false;
                }
                if (!Active)
                {
                    Ding();
                    goingUp = false;
                }
                platform.transform.Translate(0, Time.deltaTime * vatorSpeed, 0);
            }
            if(goingDown)
            {
                
                if (platform.transform.position.y <= DownPos.transform.position.y)
                {
                    Active = false;
                }
                if(!Active)
                {
                    Ding();
                    goingDown = false;
                }
                platform.transform.Translate(0, Time.deltaTime * -1 * vatorSpeed, 0);
            }
            
        }
        
        
	}

    void Ding()
    {
        source.Stop();
        source.PlayOneShot(dingNoise, dingVolume);
    }

    void DoTheThing()
    {
        Active = true;

        //source.Play();

        DistanceUp = UpPos.transform.position.y - platform.transform.position.y;
        DistanceDown = platform.transform.position.y - DownPos.transform.position.y; ;
        
        //Debug.Log("ElevatorUsed");
    }
}
