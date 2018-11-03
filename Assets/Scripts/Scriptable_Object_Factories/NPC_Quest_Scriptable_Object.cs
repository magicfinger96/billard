using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quests/Quest")]
public class NPC_Quest_Scriptable_Object : ScriptableObject {

    public string questNameGiver;
    public int questId;
    public new string name;
    public string brief;
    public bool needValidation;
    [TextArea]
    public string description;
    
    public List<NPC_Quest_Objective_Scriptable_Object> objectives;
    
    public List<NPC_Quest_Reward_Scriptable_Object> rewards;

}
