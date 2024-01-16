using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageTimeLimit : MonoBehaviour
{
    private StageManager stageManager;

    public Image timeImage;

    public float stageTime;
    public float stageFail;
    public TMP_Text timeText;

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    private void Start()
    {
        stageTime = 60;
    }

    void Update()
    {
        timeText.text = "스테이지 남은 시간 : " + (int)(stageTime - stageFail) + "초";

        if (stageManager.gameStart)
        {
            if (!stageManager.selectingItem)
            {
                if (stageFail > stageTime)
                {
                    stageFail = 0.0f;
                    timeImage.fillAmount = 0.0f;
                }
                else
                {
                    stageFail = stageFail + Time.deltaTime;
                    timeImage.fillAmount = 1.0f - (Mathf.Lerp(0, 100, stageFail / stageTime) / 100);
                }
            }          
        }
    }
}
