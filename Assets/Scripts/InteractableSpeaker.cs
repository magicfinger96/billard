using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableSpeaker : MonoBehaviour {

    [SerializeField]
    private InteractableSpeakerSO speaker;


    /* GUI REQUIREMENT */
    [SerializeField]
    private GameObject DicussionGUIParent;
    [SerializeField]
    private GameObject TopicGUIParent;

    [SerializeField]
    private Text speakerNameArea;
    [SerializeField]
    private Text dicussionTextArea;
    [SerializeField]
    private List<Text> topicsAreas;
    
	void Start () {
		
	}
	
	void Update () {
		
	}
    
    private void OnMouseDown()
    {
        Debug.Log("Interactable : " + speaker.name + " - " + speaker.entranceSentence[0]);
    }

    private void BuildGUIDiscussions()
    {

    }
}
