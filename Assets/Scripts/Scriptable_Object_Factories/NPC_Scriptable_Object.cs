using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC_Speaker", menuName = "NPC_Speaker/NPC")]
public class NPC_Scriptable_Object : ScriptableObject
{
    public new string name;
    public bool hasAlreadyMeetPlayer = false;
    public List<NPC_Discussion_Scriptable_Object> discussionNeverMeet;
    public List<NPC_Discussion_Scriptable_Object> discussionsAlreadyMeet;
}
