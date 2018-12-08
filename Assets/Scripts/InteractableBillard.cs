using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBillard : MonoBehaviour, Interactable {

    public PlayerManager player;
    public Pool.Main poolMain;

    public string GetInteractableText()
    {
        return "Jouer : " + GetKeyInteract().ToString();
    }

    public KeyCode GetKeyInteract()
    {
        return KeyCode.F;
    }

    public void Interact()
    {
        poolMain.loadGame();
        /*if (player.HasHoop)
        {
            poolMain.loadGame(); REMETTRE APRES 
        }*/
    }

    public bool IsInteractable()
    {
        return true;
        //return player.HasHoop; REMETTRE APRES
    }

    public bool IsNPC()
    {
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
