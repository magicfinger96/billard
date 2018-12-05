using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerManager))]
public class PlayerQuestManager : MonoBehaviour {
    
    private PlayerManager player;

    [SerializeField]
    private Text questSucceedText;
    [SerializeField]
    private GameObject questListGUI;
    [SerializeField]
    private GameObject questInDetailPanel;
    [SerializeField]
    private GameObject questItemPrefab;

    [SerializeField]
    private Text titleDetailedQuest;
    [SerializeField]
    private Text briefDetailedQuest;
    [SerializeField]
    private Text descriptionDetailedQuest;
    [SerializeField]
    private Text objectivesDetailedQuest;
    [SerializeField]
    private Text rewardsDetailedQuest;
    [SerializeField]
    private GameObject pool;

    private List<NPC_Quest_Scriptable_Object> currentQuests;
    private int currentQuestSelected;

	// Use this for initialization
	void Start () {
        this.player = GetComponent<PlayerManager>();
        this.currentQuests = new List<NPC_Quest_Scriptable_Object>();
        this.currentQuestSelected = -1;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void hoopGot()
    {
        this.player.HasHoop = true;
    }

    public void AddNewQuest(NPC_Quest_Scriptable_Object quest)
    {
        GameObject questItem = Instantiate(questItemPrefab);
        Text[] texts = questItem.GetComponentsInChildren<Text>();
        texts[0].text = quest.name;
        texts[1].text = quest.brief;
        questItem.transform.SetParent(questListGUI.transform);
        foreach(NPC_Quest_Objective_Scriptable_Object obj in quest.objectives)
        {
            obj.isComplete = false;
        }
        currentQuests.Add(quest);
    }

    public void ValidateObjectiveOnQuest(string questName, string objectiveName)
    {
        NPC_Quest_Scriptable_Object quest = GetQuestByName(questName);
        if(quest != null)
        {
            NPC_Quest_Objective_Scriptable_Object objective = quest.objectives.Find(x => x.description == objectiveName);
            if(objective != null)
            {
                objective.isComplete = true;
                bool allComplete = true;
                foreach(NPC_Quest_Objective_Scriptable_Object obj in quest.objectives)
                {
                    if(obj.isComplete == false)
                    {
                        allComplete = false;
                    }
                }

                if(allComplete && !quest.needValidation)
                {
                    RemoveQuestFromQuestList(quest.name);
                    questSucceedText.text = "Félicitation\nVous venez de terminer la quête\n\"" + quest.name+"\"";
                    questSucceedText.gameObject.SetActive(true);
                    questSucceedText.CrossFadeAlpha(0, 0.0f, false);
                    questSucceedText.CrossFadeAlpha(1, 1.0f, false);
                    StartCoroutine(FadeOutTextQuestSucceed());
                    foreach(NPC_Quest_Reward_Scriptable_Object reward in quest.rewards)
                    {
                        reward.Claim(player);
                    }

                    if (quest.name == "Récupérer l'arceau")
                    {
                        pool.GetComponent<Pool.Main>().hoopFound();
                    }
                }
            }
        }
    }

    IEnumerator FadeOutTextQuestSucceed()
    {
        yield return new WaitForSeconds(2f);
        questSucceedText.CrossFadeAlpha(0, 1.0f, false);
    }

    private void RemoveQuestFromQuestList(string questName)
    {
        Component[] components = questListGUI.GetComponentsInChildren<RectTransform>(true);
        foreach (Component comp in components)
        {
            if(comp.tag == "GUIQuestItem")
            {
                Text[] text = comp.GetComponentsInChildren<Text>();
                if (text[0].text == questName)
                {
                    Debug.Log(comp.name);
                    Destroy(comp.gameObject);
                }
            }
        }
    }

    public NPC_Quest_Scriptable_Object GetQuestByName(string name)
    {
        return currentQuests.Find(x => x.name == name);
    }

    public void CloseDetailedQuest()
    {
        questInDetailPanel.SetActive(false);
    }

    public void BuildGUIForDetailedQuest(string questName)
    {

        questInDetailPanel.SetActive(false);
        NPC_Quest_Scriptable_Object quest = currentQuests.Find(x => x.name == questName);

        if (quest != null)
        {
            titleDetailedQuest.text = quest.name;
            briefDetailedQuest.text = quest.brief;
            descriptionDetailedQuest.text = quest.description;

            foreach(NPC_Quest_Objective_Scriptable_Object objective in quest.objectives)
            {
                if(objective.isComplete)
                {
                    objectivesDetailedQuest.text = objective.description + " (1/1)";
                }
                else
                {
                    objectivesDetailedQuest.text = objective.description + " (0/1)";
                }
            }
            foreach (NPC_Quest_Reward_Scriptable_Object reward in quest.rewards)
            {
                rewardsDetailedQuest.text = reward.quantity + " " + reward.name;
            }
        }

        questInDetailPanel.SetActive(true);
    }

}
