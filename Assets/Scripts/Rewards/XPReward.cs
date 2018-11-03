using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPReward : MonoBehaviour, IReward {

    [SerializeField]
    private int xpToEarn;
    [SerializeField]
    private NPC_Quest_Reward_Scriptable_Object rewardXP;

    public void Claim(PlayerManager player)
    {
        player.Attributs.Xp += rewardXP.quantity;
    }
}
