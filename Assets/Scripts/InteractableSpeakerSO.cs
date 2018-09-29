using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableSpeaker", menuName = "NPC/SpeakerNPC")]
public class InteractableSpeakerSO : ScriptableObject
{
    public new string name;
    [TextArea]
    public List<string> entranceSentence;

    public List<InteractableSpeakerTopicSO> topics;
}
