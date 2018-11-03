using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quests/QuestObjective")]
public class NPC_Quest_Objective_Scriptable_Object : ScriptableObject {

    public bool isComplete = false;
    public string description;
}
