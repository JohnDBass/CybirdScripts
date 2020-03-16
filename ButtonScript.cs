using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
    private AudioSource source;
    [SerializeField]
    private AudioClip buttonNoise;
    [SerializeField]
    private float buttonVolume = 0.5f;


    public GameObject myItem;

    private bool pressed = false;
    private Animator anim;
 
	// Use this for initialization
	void Start () {
        source = this.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (pressed)
        {
            //Debug.Log("Pressed");
            pressed = false;
            source.PlayOneShot(buttonNoise, buttonVolume);
            anim.SetTrigger("Pressed");
            myItem.SendMessage("DoTheThing");            
        }        
    }
    void Pressed()
    {        
        pressed = true;
        Debug.Log("Button was Pressed");
    }
}
