using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageTile : MonoBehaviour
{
    public TMP_Text nextStageText; // ���� �������� Ȯ��
    public bool stageNext; // �������� �Ѿ�� ������ Ȯ��
    
    void Update()
    {
        StartCoroutine(NextStage());
    }

    // �������� �ؽ�Ʈ 
    IEnumerator NextStage()
    {
        while (stageNext)
        {
            nextStageText.text = "�������� �Ѿ����.";
            yield return new WaitForSeconds(1f);
            nextStageText.text = "�������� �Ѿ����..";
            yield return new WaitForSeconds(1f);
            nextStageText.text = "�������� �Ѿ����...";
            yield return new WaitForSeconds(1f);
        }
    }
}
