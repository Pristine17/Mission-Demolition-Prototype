﻿using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

     static public bool goalMet = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Projectile")
        {
            Goal.goalMet = true;
            Color c = GetComponent<Renderer>().material.color;
            
            c = Color.cyan;
            c.a = 1;
            GetComponent<Renderer>().material.color = c;
        }
    }
}
