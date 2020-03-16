using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetter : MonoBehaviour {


    public int levelNum = 0;

    private GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        player.SendMessage("SetLevel", levelNum);
        Destroy(this.gameObject);
	}
}
