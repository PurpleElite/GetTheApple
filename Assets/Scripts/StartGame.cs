using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;﻿

public class StartGame : MonoBehaviour {
    public static float startTime;


	public void PlayGame() {
        startTime = Time.time;
		SceneManager.LoadScene(1);
	}
}
