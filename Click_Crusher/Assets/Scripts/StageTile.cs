using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageTile : MonoBehaviour
{
    public TMP_Text nextStageText;
    public bool stageNext;

    void Start()
    {
        
    }

    
    void Update()
    {
        StartCoroutine(NextStage());
    }

    IEnumerator NextStage()
    {
        while (stageNext)
        {
            nextStageText.text = "스테이지 넘어가는중.";
            yield return new WaitForSeconds(1f);
            nextStageText.text = "스테이지 넘어가는중..";
            yield return new WaitForSeconds(1f);
            nextStageText.text = "스테이지 넘어가는중...";
            yield return new WaitForSeconds(1f);
        }
    }
}
