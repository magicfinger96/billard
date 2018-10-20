using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    #region interactableAttributs
    [SerializeField]
    private Text interactableText;
    #endregion
    #region interactableMethods
    public void SetInteractableTextContent(string content)
    {
        interactableText.text = content;
    }

    public void ShowInteractableTextContent()
    {
        interactableText.gameObject.SetActive(true);
    }

    public void HideInteractableTextContent()
    {
        interactableText.gameObject.SetActive(false);
    }
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
