using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour {

    private Animator anim;
    private bool isOpeningClosing;

	// Use this for initialization
	void Start ()
    {
        isOpeningClosing = false;
        anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void Interact()
    {
        isOpeningClosing = true;
        StartCoroutine(DoorAnimation());
    }

    IEnumerator DoorAnimation()
    {
        anim.SetBool("ShouldOpen", true);
        yield return new WaitForSeconds(1.5f);

        yield return new WaitForSeconds(2);

        anim.SetBool("ShouldOpen", false);
        yield return new WaitForSeconds(1.5f);
        isOpeningClosing = false;
    }
}
