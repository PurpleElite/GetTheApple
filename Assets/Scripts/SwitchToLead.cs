using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToLead : MonoBehaviour {

	/*private void OnGUI() {
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

		GUILayout.Space(Screen.height - 30);
		if (GUILayout.Button("Give Up")) {
			SceneManager.LoadScene ("LeaderBoard");
		}

		GUILayout.EndArea();
	}*/

    public void GiveUp()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
}
