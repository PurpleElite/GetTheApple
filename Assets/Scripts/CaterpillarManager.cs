using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarManager : MonoBehaviour {

    public float despawnDistance;
    [SerializeField]
    private int faceplantCount;
    [SerializeField]
    private bool present;
    [SerializeField]
    private List<Transform> holes;
    [SerializeField]
    private GameObject caterpillar;
    [SerializeField]
    private GameObject activeClone;

	// Use this for initialization
	void Start () {
        faceplantCount = 0;
        present = false;
        foreach (Transform child in transform)
        {
            holes.Add(child);
        }
        caterpillar = GameObject.FindGameObjectWithTag("Caterpillar");
        caterpillar.GetComponent<Animator>().enabled = false;
        caterpillar.GetComponentInChildren<MeshRenderer>().sortingLayerName = "UI";
    }
	
	// Update is called once per frame
	void Update () {
        if (present)
        {
            Transform playerLoc = GameObject.FindGameObjectWithTag("Player").transform;
            if (Mathf.Abs(Mathf.Abs(activeClone.transform.position.y) - Mathf.Abs(playerLoc.position.y)) > despawnDistance)
            {
                Despawn();
            }
        }
    }

    public void FacePlant() {
        faceplantCount++;
        if (faceplantCount == 1)
        {
            Dialogue("Hey there basket!\nAre you here to Get The Apple?", 0f);
            Dialogue("Well, don't you worry!\nIt's easy!", 5f);
            Dialogue("All you gotta do\nis climb up this tree!", 10f);
            Dialogue("It'd probably be\neasier if you had arms...", 15f);
            Dialogue("I'm rooting for you though!", 20f);
            Dialogue("Good Luck!", 23f);
            Dialogue("", 25f);
        }
    }

    private void Despawn()
    {
        Animator anim = activeClone.GetComponent<Animator>();
        anim.SetTrigger("Leave");
        Destroy(activeClone, 0.556f);
    }

    private void Dialogue(string text, float delay) {
        if (!present)
        {
            Transform hole = FindNearestHole(GameObject.FindGameObjectWithTag("Player").transform);
            activeClone = Instantiate(caterpillar, hole.position, hole.rotation);
            
            if (hole.GetComponent<SpriteRenderer>().flipX)
            {
                activeClone.GetComponent<SpriteRenderer>().flipX = true;
                activeClone.transform.Translate(new Vector2(-0.11f, 0.085f));
            }
            else
            {
                activeClone.transform.Translate(new Vector2(0.12f, 0.085f));
            }
            Animator anim = activeClone.GetComponent<Animator>();
            anim.enabled = true;
            present = true;
            StartCoroutine(DisplayTextWithDelay(text, 0.75f + delay));
        }
        else
        {
            StartCoroutine(DisplayTextWithDelay(text, delay));
        }
    }

    private IEnumerator DisplayTextWithDelay(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        DisplayText(text);
    }

    private void DisplayText(string text)
    {
        activeClone.GetComponentInChildren<TextMesh>().text = text;
    }

    private Transform FindNearestHole(Transform location) {
        float minDist = Mathf.Infinity;
        float distance;
        Transform ret = null;
        Vector2 currentLoc = location.position;
        foreach( Transform hole in holes)
        {
            distance = Vector2.Distance(currentLoc, hole.position);
            if (distance < minDist)
            {
                minDist = distance;
                ret = hole;
            }
        }
        return ret;
    }
}
