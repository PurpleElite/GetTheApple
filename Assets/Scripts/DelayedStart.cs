using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour {
    public int timer = 1000;
    public AudioSource audio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0)
        {
            timer--;
        }
        else
        {
            audio.Play();
            this.enabled = false;
        }
	}
}
