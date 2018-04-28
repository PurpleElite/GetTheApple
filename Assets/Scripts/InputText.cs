﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour {

	[SerializeField]
	private InputField input;
	void Awake()
	{
		//
	}

	public void GetInput(string entry)
	{
        PlayerPrefs.SetString("Username", entry);
        Debug.Log (entry);
		input.text = entry;
	}
}
