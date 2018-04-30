using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour {
	private static string _nameInput = "";
	private static string _scoreInput = "0";
    private int rec = 0;
    public Vector2 scrollPosition;
    private void OnGUI() {
		//GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
        GUIStyle myStyle = new GUIStyle();
        GUIStyle titleStyle = new GUIStyle();
        Font myFont = (Font)Resources.Load("Fonts/CODERS_CRUX", typeof(Font));
        myStyle.font = myFont;
        myStyle.fontSize = 32;
        myStyle.normal.textColor = Color.white;
        titleStyle.font = myFont;
        titleStyle.fontSize = 48;
        titleStyle.normal.textColor = Color.white;
        GUILayout.Label("Leaderboard", titleStyle);
        GUILayout.Space(25);
        // Display high scores!
        for (int i = 0; i < LeaderBoardScript.EntryCount; ++i) {
			var entry = LeaderBoardScript.GetEntry(i);
			GUILayout.Label((i + 1) + ". Name: " + entry.name + ", Time: " + entry.score, myStyle);
        }

		// Interface for reporting test scores.
		//GUILayout.Space(Screen.height - 300);

		//_nameInput = GUILayout.TextField(_nameInput);
		//_scoreInput = GUILayout.TextField(_scoreInput);

	    int score;
		int.TryParse(_scoreInput, out score);
        if (rec < 1)
        {
            LeaderBoardScript.Record(_nameInput, score);
            // Reset for next input.
            _nameInput = "";
            _scoreInput = "0";
            rec = 1;
            //StartCoroutine(Wait(5.0f));
        }
            
        //Wait 10 seconds to go back to name screen.
        Invoke("DelayedFunction", 10.0f);
        GUILayout.EndArea();
	}
    private void DelayedFunction()
    {
        SceneManager.LoadScene(0);
    }
}
