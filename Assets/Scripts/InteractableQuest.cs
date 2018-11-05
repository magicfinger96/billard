using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class InteractableQuest : MonoBehaviour, Interactable {

    [SerializeField]
    private NPC_Quest_Scriptable_Object questAttached;
    [SerializeField]
    private NPC_Quest_Objective_Scriptable_Object objectiveAttached;
    [SerializeField]
    private PlayerQuestManager playerQuestManager;
    [SerializeField]
    private bool shouldDissapearOnInteract = false;


    public string GetInteractableText()
    {
        return "Interagir : " + KeyCode.F.ToString();
    }

    public KeyCode GetKeyInteract()
    {
        return KeyCode.F;
    }

    public void Interact()
    {
        if(IsInteractable())
        {
            playerQuestManager.ValidateObjectiveOnQuest(questAttached.name, objectiveAttached.description);
            if (shouldDissapearOnInteract)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public bool IsInteractable()
    {
        return playerQuestManager.GetQuestByName(questAttached.name) != null;
    }

    public bool IsNPC()
    {
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
