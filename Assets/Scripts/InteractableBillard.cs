using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableBillard : MonoBehaviour, Interactable {

    public PlayerManager player;
    public Pool.Main poolMain;
    public Text textInfo;

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
            poolMain.loadGame(); //REMETTRE APRES 
        }
        else
        {
            textInfo.text = "Vous ne pouvez pas jouer, il semblerait qu'il manque un objet... Renseignez vous auprès des gens";
            textInfo.gameObject.SetActive(true);
            textInfo.CrossFadeAlpha(0, 0.0f, false);
            textInfo.CrossFadeAlpha(1, 1.0f, false);
            StartCoroutine(FadeOutTextQuestSucceed());
        }*/
    }

    IEnumerator FadeOutTextQuestSucceed()
    {
        yield return new WaitForSeconds(3f);
        textInfo.CrossFadeAlpha(0, 1.0f, false);
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
