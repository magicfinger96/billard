using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour, Interactable {

    private Animator anim;
    private bool isOpeningClosing;
    private bool isInteractable;

	// Use this for initialization
	void Start ()
    {
        isOpeningClosing = false;
        anim = GetComponent<Animator>();
        isInteractable = true;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Interact()
    {
        isOpeningClosing = true;
        if (isInteractable)
        {
            StartCoroutine(DoorAnimation());
        }
    }

    IEnumerator DoorAnimation()
    {
        isInteractable = false;
        anim.SetBool("ShouldOpen", true);
        yield return new WaitForSeconds(1.5f);

        yield return new WaitForSeconds(3);
        anim.SetBool("ShouldClose", true);
        
        yield return new WaitForSeconds(1.5f);
        isOpeningClosing = false;
        isInteractable = true;

        anim.SetBool("ShouldClose", false);
        anim.SetBool("ShouldOpen", false);
    }

    public KeyCode GetKeyInteract()
    {
        return KeyCode.F;
    }

    public string GetInteractableText()
    {
        return "Interagir : " + KeyCode.F.ToString();
    }

    public bool IsInteractable()
    {
        return isInteractable;
    }
   
    public bool IsNPC()
    {
        return false;
    }
}
