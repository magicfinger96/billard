using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestResumeButton : EventTrigger
{

    private float alphaOnMouseHover = 73.0f / 255.0f;
    private PlayerQuestManager playerQuestManager;
    private AudioSource sourceClick;
    private string nameQuestAttached;

    private void Start()
    {
        GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
        playerQuestManager = GameObject.FindGameObjectWithTag("PlayerMain").GetComponent<PlayerQuestManager>();
        sourceClick = GetComponent<AudioSource>();
        nameQuestAttached = GetComponentsInChildren<Text>()[0].text;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().CrossFadeAlpha(1.0f, 0.2f, false);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().CrossFadeAlpha(0.0f, 0.2f, false);
    }

    public override void OnPointerDown(PointerEventData data)
    {
        sourceClick.Play();
        playerQuestManager.BuildGUIForDetailedQuest(nameQuestAttached);
    }
}
