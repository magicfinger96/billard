using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quests/QuestReward/XP")]
public class NPC_Quest_Reward_XP_Scriptable_Object : NPC_Quest_Reward_Scriptable_Object
{
    public override void Claim(PlayerManager player)
    {
        player.Attributs.Xp += quantity;
    }
}
