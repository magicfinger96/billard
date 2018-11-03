using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quests/QuestReward")]
public abstract class NPC_Quest_Reward_Scriptable_Object : ScriptableObject {

    public int quantity;
    public new string name;

    public abstract void Claim(PlayerManager player);
}
