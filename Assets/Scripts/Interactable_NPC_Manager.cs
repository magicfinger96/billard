using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable_NPC_Manager : MonoBehaviour {

    [SerializeField]
    private List<Interactable_NPC> speakers;
    private Interactable_NPC currentSpeaker;
    private NPC_Discussion_Scriptable_Object currentDiscussion;

    [SerializeField]
    private Text speakerName;
    [SerializeField]
    private Text discussionArea;

    [SerializeField]
    private GameObject discussionGUI;
    [SerializeField]
    private GameObject topicGUI;
    private List<GameObject> topicsPanels;

    // Use this for initialization
    private void Start () {
        currentSpeaker = null;
        currentDiscussion = null;
        topicsPanels = new List<GameObject>();
        RetrieveTopicsPanels();
	}

    private void RetrieveTopicsPanels()
    {
        Transform[] objects = topicGUI.GetComponentsInChildren<Transform>();
        foreach(Transform obj in objects)
        {
            if(obj.tag == "TopicPanel")
            {
                topicsPanels.Add(obj.gameObject);
            }
        }
    }
	
    public void SetCurrentSpeaker(Interactable_NPC speaker)
    {
        currentSpeaker = speaker;
        currentDiscussion = speaker.GetIntroDiscussion();
        BuildDiscussionArea();
        BuildRelatedDiscussionsArea();
    }

    public void SetCurrentSpeakerTopic(int topicIndex)
    {
        NPC_Discussion_Scriptable_Object nextDiscussion = currentDiscussion.relatedDiscussions[topicIndex];
        currentDiscussion = nextDiscussion;
        BuildDiscussionArea();
        BuildRelatedDiscussionsArea();
    }

    private void BuildDiscussionArea()
    {
        speakerName.text = currentSpeaker.Npc.name;
        discussionArea.text = currentDiscussion.content[0];
        discussionGUI.SetActive(true);
    }

    private void BuildRelatedDiscussionsArea()
    {
        int iterator = 0;
        int relatedCount = currentDiscussion.relatedDiscussions.Count;
        for(int i = 0; i < topicsPanels.Count; i++)
        {
            topicsPanels[i].SetActive(false);
            if(iterator < relatedCount)
            {
                topicsPanels[i].GetComponentInChildren<Text>().text = currentDiscussion.relatedDiscussions[iterator].title;
                topicsPanels[i].SetActive(true);
                iterator++;
            }
        }
        topicGUI.SetActive(true);
    }
}
