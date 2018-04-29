using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour {

    public int ID = 0;
    private GameObject caterpillar;

    // Use this for initialization
    void Start () {
        caterpillar = GameObject.FindGameObjectWithTag("CaterpillarManager");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !other.isTrigger)
        {
            caterpillar.GetComponent<CaterpillarManager>().ZoneEnter(ID);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !other.isTrigger)
        {
            caterpillar.GetComponent<CaterpillarManager>().ZoneExit(ID);
        }
    }
}
