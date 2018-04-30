using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour {
    public float timer = 2.5f;
    public AudioSource Audio;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartDelay());
    }
	
    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(timer);
        Audio.Play();
    }
}
