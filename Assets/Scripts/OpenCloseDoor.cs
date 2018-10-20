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
        StartCoroutine(DoorAnimation());
    }

    IEnumerator DoorAnimation()
    {
        isInteractable = false;
        anim.SetBool("ShouldOpen", true);
        yield return new WaitForSeconds(1.5f);

        yield return new WaitForSeconds(2);

        anim.SetBool("ShouldOpen", false);
        yield return new WaitForSeconds(1.5f);
        isOpeningClosing = false;
        isInteractable = true;
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
}
