using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    public GameObject[] items; // 아이템 배열
    public GameObject itemPos1; // 선택된 아이템 위치
    public GameObject itemPos2;
    public GameObject itemPos3;

    public List<GameObject> selectedItems; // 선택한 아이템
    public GameObject selectItemPos1;
    public GameObject selectItemPos2;
    public GameObject selectItemPos3;
    public GameObject selectItemPos4;
    public int selectNum; // 선택한 아이템 번호

    public TMP_Text itemName;
    public TMP_Text itemEx;

    public GameObject selectItemMenu;

    public Canvas canvas;

    public void ItemSelect()
    {
        selectItemMenu.SetActive(true);

        if (selectedItems.Count < 4)
        {
            List<GameObject> RandomItem = new List<GameObject>(items);

            for (int i = 0; i < 3; i++)
            {
                int randomIndex = Random.Range(0, RandomItem.Count);

                // 중복 체크
                while (selectedItems.Contains(RandomItem[randomIndex]))
                {
                    randomIndex = Random.Range(0, RandomItem.Count);
                }

                switch (i)
                {
                    case 0:
                        RandomItem[randomIndex].transform.position = itemPos1.transform.position;
                        break;
                    case 1:
                        RandomItem[randomIndex].transform.position = itemPos2.transform.position;
                        break;
                    case 2:
                        RandomItem[randomIndex].transform.position = itemPos3.transform.position;
                        break;
                }

                RandomItem.RemoveAt(randomIndex);
            }
        }
        else
        {
            // 이미 4개의 아이템이 선택된 경우의 처리
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = Random.Range(0, selectedItems.Count);

                switch (i)
                {
                    case 0:
                        selectedItems[randomIndex].transform.position = itemPos1.transform.position;
                        break;
                    case 1:
                        selectedItems[randomIndex].transform.position = itemPos2.transform.position;
                        break;
                    case 2:
                        selectedItems[randomIndex].transform.position = itemPos3.transform.position;
                        break;
                }
            }
        }
    }

    public void CloseMenu()
    {
        if (selectNum != 0)
        {
            selectedItems.Add(items[selectNum - 1]);

            // 선택된 아이템을 selectItemPos1,2,3,4로 이동
            switch (selectedItems.Count)
            {
                case 1:
                    selectedItems[0].transform.position = selectItemPos1.transform.position;
                    break;
                case 2:
                    selectedItems[1].transform.position = selectItemPos2.transform.position;
                    break;
                case 3:
                    selectedItems[2].transform.position = selectItemPos3.transform.position;
                    break;
                case 4:
                    selectedItems[3].transform.position = selectItemPos4.transform.position;
                    break;
            }
        }

        foreach (var item in items)
        {
            // 선택되지 않은 아이템만 (0, 2000) 위치로 이동
            if (!selectedItems.Contains(item))
            {
                item.transform.position = new Vector3(0, 2000, 0);
            }
        }   

        selectItemMenu.SetActive(false);
    }


    public void Fire()
    {
        //items[0]
        selectNum = 1;
        itemName.text = "화염소환";
        itemEx.text = "주변의 랜덤한 위치에 불기둥을 소환하여 일정 시간 동안 데미지를 입힘니다.";
    }
    public void FireShot()
    {
        //items[1]
        selectNum = 2;
        itemName.text = "폭발하는 불꽃";
        itemEx.text = "넓은 범위에 불꽃을 퍼뜨려 대량의 데미지를 입힘니다.";
    }
    public void HolyShiled()
    {
        //items[2]
        selectNum = 3;
        itemName.text = "빛의 보호막";
        itemEx.text = "한 번의 공격을 완벽하게 방어하는 빛의 방패를 소환합니다.";
    }
    public void HolyShot()
    {
        //items[3]
        selectNum = 4;
        itemName.text = "빛의 심판";
        itemEx.text = "선택한 범위에 빛을 집중하여 일정 시간 동안 데미지를 입힘니다.";
    }
    public void Melee()
    {
        //items[4]
        selectNum = 5;
        itemName.text = "폭풍의 일격";
        itemEx.text = "이후 5번의 공격동안 추가적으로 한 번 더 공격합니다.";
    }
    public void Posion()
    {
        //items[5]
        selectNum = 6;
        itemName.text = "맹독";
        itemEx.text = "다음 공격한 몬스터에게 일정 시간 동안 지속적으로 데미지를 입힘니다.";
    }
    public void Rock()
    {
        //items[6]
        selectNum = 7;
        itemName.text = "압도적인 힘";
        itemEx.text = "다음 공격에 강력한 데미지를 입힘니다.";
    }
    public void Sturn()
    {
        //items[7]
        selectNum = 8;
        itemName.text = "시간의 결속";
        itemEx.text = "몬스터가 다음 공격을 수행할 수 없도록 시간을 억제합니다.";
    }
}
