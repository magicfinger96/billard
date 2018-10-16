using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quests/Quest")]
public class NPC_Quest_Scriptable_Object : ScriptableObject {

    public int questId;
    public new string name;
    [TextArea]
    public string description;
    [TextArea]
    public List<string> objectifs;
    public int xpEarn;
    
}
