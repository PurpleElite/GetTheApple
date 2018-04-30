using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarLeaderboard : MonoBehaviour {

    public RectTransform spawnLocation;
    private GameObject caterpillar;
    private Queue<DialogueLine> lines;
    private System.Random randomizer;
    private bool present = false;

    // Use this for initialization
    void Start () {
        lines = new Queue<DialogueLine>();
        caterpillar = GameObject.FindGameObjectWithTag("Caterpillar");
        caterpillar.GetComponent<Animator>().enabled = false;
        caterpillar.GetComponentInChildren<MeshRenderer>().sortingLayerName = "UI";
        randomizer = new System.Random();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void Talk(float score, int totalAttempts, float totalSeconds)
    {
        lines.Enqueue(new DialogueLine("Congratulations!\nIt took you " + System.Math.Floor(score) + " seconds to Give Up!", 4f));
        lines.Enqueue(new DialogueLine("No one's gotten the apple, but\n" + totalAttempts + " attempts have been made!", 4f));
        lines.Enqueue(new DialogueLine("That's a whole " + System.Math.Floor(totalSeconds) + " seconds\nspent on this.", 4f));
        switch (randomizer.Next(5))
        {
            case 0:
                lines.Enqueue(new DialogueLine("Why's everyone want that\napple so bad anyway?", 4f));
                lines.Enqueue(new DialogueLine("It's not real, you know.", 3f));
                break;
            case 1:
                lines.Enqueue(new DialogueLine("Who are the real\nwinners here?", 3f));
                lines.Enqueue(new DialogueLine("The ones who kept\non persevering?", 3f));
                lines.Enqueue(new DialogueLine("Or the ones who knew\nwhen to quit?", 3f));
                break;
            case 2:
                lines.Enqueue(new DialogueLine("It sure is frustrating\nwatching someone fail\nat a simple task, isn't it?", 5f));
                lines.Enqueue(new DialogueLine("But don't give them a hard time\nunless you've tried it yourself!", 4f));
                break;
            case 3:
                lines.Enqueue(new DialogueLine("But, does it really matter\nwhether anyone's gotten the apple?", 3f));
                lines.Enqueue(new DialogueLine("The game is fun regardless\nisn't it?", 3f));
                lines.Enqueue(new DialogueLine("Heck if I know,\nI'm just a caterpillar!", 3f));
                break;
            case 4:
                lines.Enqueue(new DialogueLine("But surely, the\nnext player will get it?", 4f));
                lines.Enqueue(new DialogueLine("Seems unlikely.", 2f));
                lines.Enqueue(new DialogueLine("But what do I know?\nI'm just a caterpillar!", 3f));
                break;
        }
    StartCoroutine(NextLineDelay(1.25f));
    }

    private void NextLine()
    {
        if (!present)
        {
            caterpillar.transform.position = spawnLocation.position;
            Animator anim = caterpillar.GetComponent<Animator>();
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
                Despawn();
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
        caterpillar.GetComponentInChildren<TextMesh>().text = text;
    }

    private void Despawn()
    {
        present = false;
        caterpillar.GetComponent<Animator>().SetTrigger("Leave");
        Destroy(caterpillar, 0.556f);
    }
}
