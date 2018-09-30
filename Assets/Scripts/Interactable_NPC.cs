using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_NPC : MonoBehaviour
{
    /* Datas */
    [SerializeField]
    private NPC_Scriptable_Object npc;
    public NPC_Scriptable_Object Npc
    {
        get { return npc;  }
        private set { }
    }

    /* Logic REQUIREMENT */
    [SerializeField]
    private Interactable_NPC_Manager manager;

    private void OnMouseDown()
    {
        manager.SetCurrentSpeaker(this);
    }
    
    public NPC_Discussion_Scriptable_Object GetIntroDiscussion()
    {
        if(npc.hasAlreadyMeetPlayer)
        {
            return RandomAlreadyMeetIntro();
        }
        else
        {
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


}
