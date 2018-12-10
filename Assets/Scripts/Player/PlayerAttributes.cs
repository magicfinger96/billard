using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {

    #region LevelAndExperienceAttributs

    // Representing the amount needed to levelup for the player
    // This attribut is auto-incremented when player level up
    private int amountToLevelUp = 100;
    public int AmountToLevelUp
    {
        get { return amountToLevelUp; }
        private set { }
    }

    // Representing the level of a player
    // A player can level up if he has enough experience
    //  according to a certain amount needed.
    // When player level up, he can unlock new abilities
    private int level;
    public int Level
    {
        get { return level; }
        private set { }
    }

    // Representing experience that the player have
    // A new level is passed when the player has
    //  enought experience according to a certain amount 
    //  of experience needed
    private int xp;
    public int Xp
    {
        get { return xp; }
        set { EarnExperience(value); }
    }

    [SerializeField]
    private Text levelText;
    [SerializeField]
    private ProgressXPBar barProgress;

    #endregion
    #region LevelAndExperienceMethods

    public GUIManager guiManager;

    // Method allowing to add experience of the player
    // If the player has enough experience, he'll automatically
    //  level up.
    void EarnExperience(int value)
    {
        xp += value;
        if (xp >= amountToLevelUp)
        {
            xp -= amountToLevelUp;
            amountToLevelUp *= 2;
            level++;

            levelText.text = level.ToString();
        }
        bool wasActive = guiManager.HUPIsActive();
        guiManager.ShowHUDPlayer();
        barProgress.UpdateBar(amountToLevelUp, xp);
        if(!wasActive)
        {
            guiManager.HideHUDPlayer();
        }
    }
    #endregion

    #region MovementAttributs

    // Attribut representing the speed of the player when
    //  he's moving forward, it's mean only when the player
    //  is pressing z and only z.
    [SerializeField]
    private float speedForward;
    public float SpeedForward
    {
        get { return speedForward; }
        set { speedForward = value; }
    }

    // Attribut representing the speed of the player when
    //  he's moving backward, horizontally, or combinaison of them.
    // The only case that's the player is not moving backward is when
    //  he's moving forward
    [SerializeField]
    private float speedBackward;
    public float SpeedBackward
    {
        get { return speedBackward; }
        set { speedBackward = value; }
    }

    // Attribut representing the current speed of the player when
    //  he's moving
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }

    }

    // Attribut representing the speed at which the player
    //  can make his character rotating to change his direction
    [SerializeField]
    private float rotationSpeed;
    public float RotationSpeed
    {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }

    #endregion

    #region InteractionAttributs

    // Attribut representing the minimal interaction distance
    //  that the player should respect to be able to an 
    //  interactable object
    [SerializeField]
    private float interactionDistance;
    public float InteractionDistance
    {
        get { return interactionDistance; }
        set { interactionDistance = value; }
    }
    #endregion

    // Use this for initialization
    void Start () {
        this.currentSpeed = 0;
        this.level = 1;
        this.xp = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

}
