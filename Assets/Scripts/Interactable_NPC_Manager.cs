using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable_NPC_Manager : MonoBehaviour {

    [SerializeField]
    private List<Interactable_NPC> speakers;
    private bool IsWriting;
    private Interactable_NPC currentSpeaker;
    private NPC_Discussion_Scriptable_Object currentDiscussion;
    private int currentDiscussionContentIndex;

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
        IsWriting = false;
        currentSpeaker = null;
        currentDiscussion = null;
        currentDiscussionContentIndex = -1;
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
	
    public bool DiscussionInProcess()
    {
        return currentSpeaker != null;
    }

    public void SetCurrentSpeaker(Interactable_NPC speaker)
    {
        topicGUI.SetActive(false);
        discussionGUI.SetActive(false);
        currentSpeaker = speaker;
        currentDiscussion = speaker.GetIntroDiscussion();
        currentDiscussionContentIndex = 0;

        speakerName.text = currentSpeaker.Npc.name;
        BuildDiscussionArea();
    }

    public void SetCurrentSpeakerTopic(int topicIndex)
    {
        NPC_Discussion_Scriptable_Object nextDiscussion = currentDiscussion.relatedDiscussions[topicIndex];
        currentDiscussion = nextDiscussion;
        currentDiscussionContentIndex = 0;
        topicGUI.SetActive(false);
        discussionGUI.SetActive(false);
        BuildDiscussionArea();
        BuildRelatedDiscussionsArea();
    }

    public void SetNextDiscussionContent()
    {
        if(currentDiscussion != null && currentDiscussionContentIndex < currentDiscussion.content.Count - 1)
        {
            currentDiscussionContentIndex++;
            BuildDiscussionArea();
        }
    }

    private void BuildDiscussionArea()
    {
        discussionArea.text = currentDiscussion.content[currentDiscussionContentIndex];
        discussionGUI.SetActive(true);
        if(currentDiscussionContentIndex == currentDiscussion.content.Count-1)
        {
            BuildRelatedDiscussionsArea();
        }
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
