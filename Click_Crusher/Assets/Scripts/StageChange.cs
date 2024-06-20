using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageChange : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject nextStageUI;

    public TMP_Text mainStageText;
    public TMP_Text subStageText;

    public int mainStage;
    public int subStage;

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    private void Start()
    {
        mainStage = stageManager.mainStage;
        subStage = stageManager.subStage;
    }

    private void Update()
    {     
        mainStageText.text = stageManager.mainStage.ToString();
        subStageText.text = stageManager.subStage.ToString();
    }

    public void ChangeStageText()
    {
        StartCoroutine(ChangeText());
    }

    IEnumerator ChangeText()
    { 
        nextStageUI.SetActive(true);
        yield return new WaitForSeconds(2f);

        mainStage = stageManager.mainStage;
        subStage = stageManager.subStage;
        yield return new WaitForSeconds(1f);
        nextStageUI.SetActive(false);
    }
}
