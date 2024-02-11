using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Combo : MonoBehaviour
{
    public GameObject comboObj;
    public int comboNum;
    public int maxComboNum;
    public TMP_Text comboText;

    public float maxScale;
    public float minScale;
    public float scaleSpeed;

    void Start()
    {
        comboObj.SetActive(false);

        maxScale = 100f;
        minScale = 60f;
        scaleSpeed = 400f;
        maxComboNum = 0;
    }

    void Update()
    {
        comboText.text = comboNum.ToString();

        if (comboNum <= 0)
        {
            comboObj.SetActive(false);
        }
        else
        {
            comboObj.SetActive(true);
        }
    }

    public void ComboUp()
    {
        StartCoroutine(ScaleComboText());

        if (comboNum > maxComboNum)
        {
            maxComboNum = comboNum;
        }
    }

    IEnumerator ScaleComboText()
    {
        float currentScale = comboText.fontSize;

        while (currentScale < maxScale)
        {
            currentScale += scaleSpeed * Time.deltaTime;
            comboText.fontSize = Mathf.Min(currentScale, maxScale);
            yield return null;
        }

        while (currentScale > minScale)
        {
            currentScale -= scaleSpeed * Time.deltaTime;
            comboText.fontSize = Mathf.Max(currentScale, minScale);
            yield return null;
        }
    }
}
