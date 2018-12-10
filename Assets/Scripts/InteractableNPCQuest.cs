using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCQuest : MonoBehaviour, Interactable
{
    [SerializeField]
    private NPC_Quest_Scriptable_Object questAttached;
    [SerializeField]
    private NPC_Quest_Objective_Scriptable_Object objectiveAttached;
    [SerializeField]
    private PlayerQuestManager playerQuestManager;
    [SerializeField]
    private bool shouldDissapearOnInteract = false;
    [SerializeField]
    private bool hoopAttached = false;

    /* Datas */
    [SerializeField]
    private NPC_Scriptable_Object npc;
    public NPC_Scriptable_Object Npc
    {
        get { return npc;  }
        private set { }
    }

    [SerializeField]
    private NPC_Quest_Scriptable_Object quest;
    public NPC_Quest_Scriptable_Object Quest
    {
        get { return quest; }
        private set { }
    }

    /* Logic REQUIREMENT */
    [SerializeField]
    private InteractableNPCManager manager;
    private Transform positionForInteraction;
    public Transform PositionForInteraction
    {
        get { return positionForInteraction; }
        private set { }
    }

    private void Start()
    {
        this.npc.hasAlreadyProposeQuest = false;
        this.npc.hasAlreadyMeetPlayer = false;
        GetCameraPositionFromComponent();
    }

    private void GetCameraPositionFromComponent()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        foreach(Transform t in transforms)
        {
            if(t.tag == "CameraPositionning")
            {
                positionForInteraction = t;
            }
        }
    }

    public void Interact()
    {
        //manager.SetCurrentSpeaker(this);
    }

    public NPC_Discussion_Scriptable_Object GetIntroDiscussion()
    {
        if(npc.hasAlreadyMeetPlayer)
        {
            return RandomAlreadyMeetIntro();
        }
        else
        {
            npc.hasAlreadyMeetPlayer = true;
            return RandomNotAlreadyMeetIntro();
        }
    }

    private NPC_Discussion_Scriptable_Object RandomAlreadyMeetIntro()
    {
        int rand = Random.Range(0, npc.discussionsAlreadyMeet.Count);
        return npc.discussionsAlreadyMeet[rand];
    }

    private NPC_Discussion_Scriptable_Object RandomNotAlreadyMeetIntro()
    {
        int rand = Random.Range(0, npc.discussionNeverMeet.Count);
        return npc.discussionNeverMeet[rand];
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
        return true;
    }

    public bool IsNPC()
    {
        return true;
    }
}
