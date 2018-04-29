using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputText : MonoBehaviour {

    private bool dumbVarToAddASlightDelay = false;
	[SerializeField]
	private InputField input;
	void Awake()
	{
        input.Select();
        input.ActivateInputField();
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            FindObjectOfType<Button>().onClick.Invoke();
        }
        if (!input.isFocused)
        {
            input.Select();
            dumbVarToAddASlightDelay = true;
        }
    }

    void LateUpdate()
    {
        if (dumbVarToAddASlightDelay)
        {
            input.MoveTextEnd(true);
            dumbVarToAddASlightDelay = false;
        }
    }

	public void GetInput(string entry)
	{
        if (entry == "")
        {
            entry = "Anonymous";
        }
        PlayerPrefs.SetString("Username", entry);
		input.text = entry;
	}
}
