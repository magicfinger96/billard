using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHeader : MonoBehaviour {

    private Button button;
    public bool isClicked;

    [SerializeField]
    private Color colorUnclicked = new Color(255, 255, 255, 0);
    [SerializeField]
    private Color colorHighlight = new Color(253, 118, 118, 159);
    [SerializeField]
    private Color colorClicked = new Color(255, 16, 16, 159);

	// Use this for initialization
	void Start () {
        isClicked = false;
        button = GetComponent<Button>();
	}

    public void AsSelected()
    {
        ColorBlock cb = button.colors;
        cb.normalColor = colorClicked;
        cb.highlightedColor = colorClicked;
        button.colors = cb;
    }

    public void AsUnselected()
    {
        ColorBlock cb = button.colors;
        cb.normalColor = colorUnclicked;
        cb.highlightedColor = colorHighlight;
        button.colors = cb;
    }

    public void OnClick()
    {
        isClicked = !isClicked;
        if(isClicked)
        {
            AsSelected();
        }
        else
        {
            AsUnselected();
        }
    }
}
