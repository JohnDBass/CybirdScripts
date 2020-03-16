using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
    [SerializeField]
    private GameObject player; 
    private PlayerController pcontroller;

    private void Start()
    {
        pcontroller = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        
    }

    
    
}
