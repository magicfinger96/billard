using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableSpeaker", menuName = "NPC/SpeakerNPCTopic")]
public class InteractableSpeakerTopicSO : ScriptableObject {

    public string topicName;
    public List<string> content;
    public List<InteractableSpeakerTopicSO> othersTopic;
}
