using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestManager : MonoBehaviour {

    [SerializeField]
    private GameObject questListGUI;
    [SerializeField]
    private GameObject questItemPrefab;

    private List<NPC_Quest_Scriptable_Object> currentQuests;
    private int currentQuestSelected;

	// Use this for initialization
	void Start () {
        this.currentQuests = new List<NPC_Quest_Scriptable_Object>();
        this.currentQuestSelected = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            AcceptQuest();
        }
	}

    void AcceptQuest(/*NPC_Quest_Scriptable_Object quest*/)
    {
        GameObject questItem = Instantiate(questItemPrefab);
        Text[] texts = questItem.GetComponentsInChildren<Text>();
        texts[0].text = "Une nouvelle aventure";
        texts[1].text = "Chasser le gibier";

        questItem.transform.SetParent(questListGUI.transform);
        /*currentQuests.Add(quest);*/
    }
}
