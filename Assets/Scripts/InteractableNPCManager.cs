using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableNPCManager : MonoBehaviour {

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private PlayerController player;

    private bool IsWriting;
    private InteractableNPC currentSpeaker;
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
    [SerializeField]
    private GameObject nextGUI;

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

    private void EndOfDiscussion()
    {
        topicGUI.SetActive(false);
        discussionGUI.SetActive(false);
        nextGUI.SetActive(false);
        currentSpeaker = null;
        currentDiscussion = null;
        currentDiscussionContentIndex = -1;
        player.ExitInteractionWithNPC();
    }

    public void SetCurrentSpeaker(InteractableNPC speaker)
    {
        topicGUI.SetActive(false);
        discussionGUI.SetActive(false);
        nextGUI.SetActive(false);
        currentSpeaker = speaker;
        currentDiscussion = speaker.GetIntroDiscussion();
        currentDiscussionContentIndex = 0;
        mainCamera.transform.position = currentSpeaker.PositionForInteraction.position;
        mainCamera.transform.LookAt(currentSpeaker.transform.position);

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

        if(currentDiscussion.content.Count == 0 && currentDiscussion.relatedDiscussions.Count == 0)
        {
            EndOfDiscussion();
        }
        else
        {
            BuildDiscussionArea();
        }
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
        nextGUI.SetActive(true);
        if (currentDiscussionContentIndex == currentDiscussion.content.Count-1)
        {
            BuildRelatedDiscussionsArea();
            nextGUI.SetActive(false);
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
