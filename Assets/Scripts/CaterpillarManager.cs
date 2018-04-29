using System;
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

    private bool powerJump = false;
    private int currentZone;
    private int lastZone;
    private bool checkpoint1 = false;
    private bool checkpoint2 = false;
    private bool top = false;
    private bool CHEATER = false;


	// Use this for initialization
	void Start () {
        lines = new Queue<DialogueLine>();
        faceplantCount = 0;
        present = false;
        foreach (Transform child in transform)
        {
            if(child.gameObject.tag == "Hole")
            {
                holes.Add(child);
            }
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
        faceplantCount++;
        if (CHEATER)
        {
            lines.Enqueue(new DialogueLine("That's what you deserve. Cheater.", 5f));
        }
        else if (faceplantCount == 1)
        {
            lines.Enqueue(new DialogueLine("Hey there basket!\nAre you here to Get The Apple?", 5f));
            lines.Enqueue(new DialogueLine("Well, don't you worry!\nIt's easy!", 4f));
            lines.Enqueue(new DialogueLine("All you gotta do\nis climb up this tree!", 4f));
            lines.Enqueue(new DialogueLine("It'd probably be\neasier if you had arms...", 3f));
            lines.Enqueue(new DialogueLine("I'm rooting for you though!", 4f));
            lines.Enqueue(new DialogueLine("Good Luck!", 2f));
        }
        else if (!talking)
        {
            if (randomizer.Next(faceplantLineProb) == 0 && Time.time - StartGame.startTime > 360)
            {
                if (randomizer.Next(faceplantLineProb) == 0)
                {
                    switch (randomizer.Next(8))
                    {
                        case 0:
                            lines.Enqueue(new DialogueLine("*yawn*", 3f));
                            break;
                        case 1:
                            lines.Enqueue(new DialogueLine("You still haven't gotten the apple?", 3f));
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
                            lines.Enqueue(new DialogueLine("Maybe perserverance can only\nget you so far in life?", 7f));
                            break;
                    }
                }
            }
            else if (randomizer.Next(faceplantLineProb) == 0 && Time.time - StartGame.startTime > 180)
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
                            lines.Enqueue(new DialogueLine("You are very persistent. Why?", 4f));
                            break;
                        case 2:
                            lines.Enqueue(new DialogueLine("If at first you don't succeed,\njust Give Up.", 5f));
                            break;
                        case 3:
                            lines.Enqueue(new DialogueLine("What are you trying to prove?", 4f));
                            break;
                        case 4:
                            lines.Enqueue(new DialogueLine("Why do you want that apple so bad?", 4f));
                            break;
                        case 5:
                            lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
                            lines.Enqueue(new DialogueLine("The Give Up button\nis right there.", 5f));
                            lines.Enqueue(new DialogueLine("You should press it!", 3f));
                            break;
                    }
                }
            }
            else
            {
                if (randomizer.Next(faceplantLineProb) == 0)
                {
                    switch (randomizer.Next(11))
                    {
                        case 0:
                            lines.Enqueue(new DialogueLine("It's not about the destination\nIt's about the journey!", 4f));
                            break;
                        case 1:
                            lines.Enqueue(new DialogueLine("Ouch!", 2f));
                            lines.Enqueue(new DialogueLine("Remember that you can\ngive up with the GIVE UP button!\nJust press it with the mouse.", 7f));
                            break;
                        case 2:
                            lines.Enqueue(new DialogueLine("Oh, close!", 2f));
                            lines.Enqueue(new DialogueLine("You'll get to\nthe top in no time!", 3f));
                            break;
                        case 3:
                            lines.Enqueue(new DialogueLine("That looked painful!", 2f));
                            lines.Enqueue(new DialogueLine("Don't push yourself too hard, okay?", 3f));
                            break;
                        case 4:
                            lines.Enqueue(new DialogueLine("Remember: if something is too hard,\nit's always okay to Give Up!", 7f));
                            lines.Enqueue(new DialogueLine("I'm sure no one will judge you.", 3f));
                            break;
                        case 5:
                            lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
                            lines.Enqueue(new DialogueLine("Remember to use the\nspace bar or up arrow to jump!", 4f));
                            break;
                        case 6:
                            lines.Enqueue(new DialogueLine("Don't push yourself too hard!", 3f));
                            break;
                        case 7:
                            lines.Enqueue(new DialogueLine("Oof! That was quite the fall.", 3f));
                            break;
                        case 8:
                            lines.Enqueue(new DialogueLine("Slow and steady wins the race.", 4f));
                            break;
                        case 9:
                            lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
                            lines.Enqueue(new DialogueLine("Always remember to eat a\nbalanced breakfast!", 4f));
                            break;
                        case 10:
                            lines.Enqueue(new DialogueLine("Here's a caterpillar tip:", 3f));
                            lines.Enqueue(new DialogueLine("An apple a day keeps\n the doctor away!", 4f));
                            break;
                    }
                }
            }
        }
    }
    internal void ZoneEnter(int zone)
    {
        currentZone = zone;
        if (zone == 4 && !CHEATER)
        {
            if (present)
            {
                Despawn();
            }
            lines = new Queue<DialogueLine>();
            lines.Enqueue(new DialogueLine("Wha-? Hey! You're not supposed\nto be able to get there!", 4f));
            lines.Enqueue(new DialogueLine("Fine, I'll just have to remove the apple.", 4f));
            Destroy(GameObject.FindGameObjectWithTag("Apple"), 7.5f);
            lines.Enqueue(new DialogueLine("No apple for you.", 4f));
            lines.Enqueue(new DialogueLine("Cheater.", 4f));
            lines.Enqueue(new DialogueLine("Honestly, the nerve.", 4f));
            lines.Enqueue(new DialogueLine("You think you can just go around beating games that are supposed to be impossible?", 4f));
            lines.Enqueue(new DialogueLine("Think again. Doing stuff like that has CONSEQUENCES. So just Give Up because" +
                "now the game is even more impossible than it was before.\n" +
                "I am just so upset that you would do this. Just up and try to steal an apple. Did it ever occur to you that maybe that apple belonged to someone??\n" +
                "No, of course not, you didn't think that. You just waltzed in here expecting to take apple and leave victorious. No such luck buddy chum pal.\n" +
                "Now go press the Give Up button and give someone who isn't a CHEATER a try.", 8f));
            CHEATER = true;
        }
        if (faceplantCount > 1 && !CHEATER)
        {
            if (zone == 1)
            {
                if (top)
                {
                    top = false;
                    switch(randomizer.Next(3))
                    {
                        case 0:
                            lines.Enqueue(new DialogueLine("Oof, back to the start.", 3f));
                            lines.Enqueue(new DialogueLine("If you don't feel like climbing back up\n remember that Giving Up is an option!", 7f));
                            break;
                        case 1:
                            lines.Enqueue(new DialogueLine("It's always rough to lose progress.", 4f));
                            lines.Enqueue(new DialogueLine("After a fall like that\na lot of people Give Up.", 5f));
                            lines.Enqueue(new DialogueLine("There's no shame in that.", 3f));
                            break;
                        case 2:
                            lines.Enqueue(new DialogueLine("Ah! So close!", 3f));
                            lines.Enqueue(new DialogueLine("You gave it your best,\nmaybe it's time to Give Up?", 6f));
                            break;
                    }
                }
            }
            if (zone == 2)
            {
                if (!checkpoint1)
                {
                    checkpoint1 = true;
                    lines.Enqueue(new DialogueLine("You're halfway there!", 3f));
                    lines.Enqueue(new DialogueLine("Keep going!", 2f));
                }
            }
            if (zone == 3)
            {
                top = true;
                if (!checkpoint2)
                {
                    checkpoint2 = true;
                    lines.Enqueue(new DialogueLine("There's the apple,\nit's so close now!", 3f));
                    lines.Enqueue(new DialogueLine("I'm proud of you for\nmaking it this far.", 3f));
                    lines.Enqueue(new DialogueLine("Now Get the Apple!", 2f));
                }
                else if (!top)
                {
                    lines.Enqueue(new DialogueLine("And now you're back up here again.", 3f));
                    lines.Enqueue(new DialogueLine("Maybe you'll get it this time?", 3f));
                }
            }
        }
    }


    internal void ZoneExit(int zone)
    {
        currentZone = 0;
        lastZone = zone;
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
