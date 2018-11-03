using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressXPBar : MonoBehaviour {

    [SerializeField]
    private RectTransform completeXPBar;
    [SerializeField]
    private RectTransform currentXPBar;

    public void UpdateBar(int completeXP, int currentXP)
    {
        float widthMax = completeXPBar.rect.width;
        float percent = ((float)currentXP / (float)completeXP);
        float widthCurrent = widthMax * percent;

        StartCoroutine(IncreaseBarSmoothly(currentXPBar.sizeDelta.x, widthCurrent));
    }

    IEnumerator IncreaseBarSmoothly(float widthCurrent, float widthMax)
    {
        while(widthCurrent != widthMax)
        {
            if(widthCurrent + 1 <= widthMax)
            {
                widthCurrent += 1f;
                currentXPBar.sizeDelta = new Vector2(widthCurrent, currentXPBar.sizeDelta.y);
            }
            else
            {
                widthCurrent = widthCurrent + (widthMax - widthCurrent);
                currentXPBar.sizeDelta = new Vector2(widthCurrent, currentXPBar.sizeDelta.y);
            }
            
            yield return new WaitForSeconds(0.01f);
        }
    }
}
