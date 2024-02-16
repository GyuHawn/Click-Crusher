using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageTile : MonoBehaviour
{
    public TMP_Text nextStageText; // 현재 스테이지 확인
    public bool stageNext; // 스테이지 넘어가는 중인지 확인
    
    void Update()
    {
        StartCoroutine(NextStage());
    }

    // 스테이지 텍스트 
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
