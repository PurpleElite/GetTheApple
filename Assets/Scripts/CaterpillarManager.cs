using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct DialogueLine
{
    public string Text;
    public float Duration;

    public DialogueLine(string text, float duration)
    {
        Text = text;
        Duration = duration;
    }
}

public class CaterpillarManager : MonoBehaviour {

    public float despawnDistance;
    public int faceplantLineProb = 2;
    private int faceplantCount;
    [SerializeField]
    private bool present;
    [SerializeField]
    private List<Transform> holes;
    private GameObject caterpillar;
    private GameObject activeClone;
    [SerializeField]
    private bool talking;
    private Queue<DialogueLine> lines;
    private System.Random randomizer;

    //Lots of variables to track various player actions
    private int numJumps;
    private bool powerJump;

	// Use this for initialization
	void Start () {
        lines = new Queue<DialogueLine>();
        faceplantCount = 0;
        present = false;
        foreach (Transform child in transform)
        {
            holes.Add(child);
        }
        caterpillar = GameObject.FindGameObjectWithTag("Caterpillar");
        caterpillar.GetComponent<Animator>().enabled = false;
        caterpillar.GetComponentInChildren<MeshRenderer>().sortingLayerName = "UI";
        randomizer = new System.Random();
    }
	
	// Update is called once per frame
	void Update () {
        if (lines.Count > 0 && !talking)
        {
            talking = true;
            NextLine();
        }
        if (present && !talking)
        {
            Transform playerLoc = GameObject.FindGameObjectWithTag("Player").transform;
            if (Mathf.Abs(Mathf.Abs(activeClone.transform.position.y) - Mathf.Abs(playerLoc.position.y)) > despawnDistance)
            {
                Despawn();
            }
        }
        
    }

    public void Jump(float jumpPower)
    {
        numJumps++;
        if (jumpPower > 200)
        {
            powerJump = true;
        }
        if (numJumps == 3 && powerJump == false)
        {
            lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
            lines.Enqueue(new DialogueLine("You can hold down the jump button\nto charge up your jump!", 7f));
        }
    }

    public void FacePlant() {
        Debug.Log(Time.time - StartGame.startTime);
        faceplantCount++;
        if (faceplantCount == 1)
        {
            lines.Enqueue(new DialogueLine("Hey there basket!\nAre you here to Get The Apple?", 5f));
            lines.Enqueue(new DialogueLine("Well, don't you worry!\nIt's easy!", 5f));
            lines.Enqueue(new DialogueLine("All you gotta do\nis climb up this tree!", 5f));
            lines.Enqueue(new DialogueLine("It'd probably be\neasier if you had arms...", 5f));
            lines.Enqueue(new DialogueLine("I'm rooting for you though!", 5f));
            lines.Enqueue(new DialogueLine("Good Luck!", 3f));
        }
        else if (randomizer.Next(faceplantLineProb) == 0 && Time.time - StartGame.startTime > 360 && !talking)
        {
            if (randomizer.Next(faceplantLineProb) == 0)
            {
                switch (randomizer.Next(8))
                {
                    case 0:
                        lines.Enqueue(new DialogueLine("*yawn*", 5f));
                        break;
                    case 1:
                        lines.Enqueue(new DialogueLine("You still haven't gotten the apple?", 5f));
                        break;
                    case 2:
                        lines.Enqueue(new DialogueLine("If you can't Get The Apple\nyou should just Give Up!", 7f));
                        break;
                    case 3:
                        lines.Enqueue(new DialogueLine("You're not so good at this!", 5f));
                        break;
                    case 4:
                        lines.Enqueue(new DialogueLine("Everyone watching you right now\nis very unimpressed.", 8f));
                        break;
                    case 5:
                        lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
                        lines.Enqueue(new DialogueLine("There are plenty of other exhibits\nhere at ICAT day.", 5f));
                        lines.Enqueue(new DialogueLine("You should check those out too!", 5f));
                        lines.Enqueue(new DialogueLine("Just remember to Give Up first.", 5f));
                        break;
                    case 6:
                        lines.Enqueue(new DialogueLine("Give Up.", 8f));
                        break;
                    case 7:
                        lines.Enqueue(new DialogueLine("Maybe perserverance can only\nget you so far in life?", 9f));
                        break;
                }
            }
        }
        else if (randomizer.Next(faceplantLineProb) == 0 && Time.time - StartGame.startTime > 180 && !talking)
        {
            if (randomizer.Next(faceplantLineProb) == 0)
            {
                switch (randomizer.Next(6))
                {
                    case 0:
                        lines.Enqueue(new DialogueLine("Maybe it's time to\ngive someone else a turn?", 5f));
                        lines.Enqueue(new DialogueLine("All you have to do\nis Give Up.", 7f));
                        break;
                    case 1:
                        lines.Enqueue(new DialogueLine("You are very persistent. Why?", 5f));
                        break;
                    case 2:
                        lines.Enqueue(new DialogueLine("If at first you don't succeed,\njust Give Up.", 5f));
                        break;
                    case 3:
                        lines.Enqueue(new DialogueLine("What are you trying to prove?", 5f));
                        break;
                    case 4:
                        lines.Enqueue(new DialogueLine("Why do you want that apple so bad?", 6f));
                        break;
                    case 5:
                        lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
                        lines.Enqueue(new DialogueLine("The Give Up button\nis right there.", 5f));
                        lines.Enqueue(new DialogueLine("You should press it!", 5f));
                        break;
                }
            }
        }
        else if (!talking)
        {
            if (randomizer.Next(faceplantLineProb) == 0)
            {
                switch(randomizer.Next(11))
                {
                    case 0:
                        lines.Enqueue(new DialogueLine("It's not about the destination\nIt's about the journey!", 7f));
                        break;
                    case 1:
                        lines.Enqueue(new DialogueLine("Ouch!", 2f));
                        lines.Enqueue(new DialogueLine("Remember that you can\ngive up with the GIVE UP button!\nJust press it with the mouse.", 7f));
                        break;
                    case 2:
                        lines.Enqueue(new DialogueLine("Oh, close!", 4f));
                        lines.Enqueue(new DialogueLine("You'll get to\nthe top in no time!", 5f));
                        break;
                    case 3:
                        lines.Enqueue(new DialogueLine("That looked painful!", 5f));
                        lines.Enqueue(new DialogueLine("Don't push yourself too hard, okay?", 5f));
                        break;
                    case 4:
                        lines.Enqueue(new DialogueLine("Remember: if something is too hard,\nit's always okay to Give Up!", 7f));
                        lines.Enqueue(new DialogueLine("I'm sure no one will judge you.", 5f));
                        break;
                    case 5:
                        lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
                        lines.Enqueue(new DialogueLine("Remember to use the\nspace bar or up arrow to jump!", 5f));
                        break;
                    case 6:
                        lines.Enqueue(new DialogueLine("Don't push yourself too hard!", 5f));
                        break;
                    case 7:
                        lines.Enqueue(new DialogueLine("Oof! That was quite the fall.", 5f));
                        break;
                    case 8:
                        lines.Enqueue(new DialogueLine("Slow and steady wins the race.", 5f));
                        break;
                    case 9:
                        lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
                        lines.Enqueue(new DialogueLine("Always remember to eat a\nbalanced breakfast!", 5f));
                        break;
                    case 10:
                        lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
                        lines.Enqueue(new DialogueLine("An apple a day keeps\n the doctor away!", 6f));
                        break;
                }
            }
        }
    }

    private void Despawn()
    {
        present = false;
        activeClone.GetComponent<Animator>().SetTrigger("Leave");
        Destroy(activeClone, 0.556f);
    }

    private void NextLine()
    {
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
            StartCoroutine(NextLineDelay(0.75f));
        }
        else
        {
            if (lines.Count > 0)
            {
                DialogueLine line = lines.Dequeue();
                DisplayText(line.Text);
                StartCoroutine(NextLineDelay(line.Duration));
            }
            else
            {
                DisplayText("");
                talking = false;
            }
        }
    }

    private IEnumerator NextLineDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextLine();
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
