using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntController : MonoBehaviour {

    public AudioSource source;
    public GameObject tauntObject;
    //private bool hasPlayed = false; //Incredible feature re-implemented


	// Use this for initialization
	void Start () {

        source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //if (!hasPlayed)
            //{
                source.Play();
                showTaunt(10.0f);
                //hasPlayed = true;
            //}
        }       
    }

    IEnumerator showTaunt(float tauntTime)
    {
        tauntObject.SetActive(true);
        yield return new WaitForSeconds(tauntTime);
        tauntObject.SetActive(false);

    }

}
