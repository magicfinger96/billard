using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderButtonManager : MonoBehaviour {

    Button[] buttons;

	// Use this for initialization
	void Start () {
       
        buttons = GetComponentsInChildren<Button>();
	}
	
	public void DisableAllButtons()
    {
        foreach(Button b in buttons)
        {
            Debug.Log("here");
            b.GetComponent<MenuButtonHeader>().AsUnselected();
            b.GetComponent<MenuButtonHeader>().isClicked = false;
        }
    }
}
