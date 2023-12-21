using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditorInternal.Profiling.Memory.Experimental;

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

    public int fireLv;
    public int fireShotLv;
    public int holyShiledLv;
    public int holyShotLv;
    public int meleeLv;
    public int posionLv;
    public int rockLv;
    public int sturnLv;

    public bool fireSelect;
    public bool fireShotSelect;
    public bool holyShiledSelect;
    public bool holyShotSelect;
    public bool meleeSelect;
    public bool posionSelect;
    public bool rockSelect;
    public bool sturnSelect;

    public GameObject selectItemMenu;

    public Canvas canvas;

    /*public void ItemSelect()
    {
        // 1. items의 아이템중 랜덤으로 3개의 아이템 선택
        // 2. itemPos1,2,3 위치로 차례로 하나씩 이동

        // 5. 만약 selectedItems의 카운트가 4개가 되었다면 items중에서 고르지않고 selectedItems 중에서 선택하도록
    }

    // 아이템 선택 버튼
    public void CloseMenu()
    {
        // 3. 랜덤으로 선택된 아이템 중 선택한 아이템의 selectNum에 맞는 아이템을 selectItemPos1에 생성(Instantiate)(canvas안에 생성되도록)
        // 3-1. 선택한 아이템이 맞는 bool값을 true로
        // 4. selectItemPos1에 이미 아이템이 생성되었다면 selectItemPos2에(2가 있다면 3, 3에 있다면 4)
        // 5. 선택한 아이템은 selectedItems에 추가

        // 6. selectedItems 카운트가 4이상이라면 이후부터는 생성은 하지말고 Lv만 오르도록 
    }*/

    public void ItemSelect()
    {
        selectItemMenu.SetActive(true);

        // 1. items의 아이템 중 랜덤으로 3개의 아이템 선택
        selectedItems.Clear(); // 기존에 선택된 아이템 초기화
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, items.Length);
            GameObject selectedItem = items[randomIndex];
            selectedItems.Add(selectedItem);

            // 해당 아이템을 itemPos1, 2, 3 위치로 이동
            GameObject itemPos = null;
            if (i == 0) itemPos = itemPos1;
            else if (i == 1) itemPos = itemPos2;
            else if (i == 2) itemPos = itemPos3;

            selectedItem.transform.position = itemPos.transform.position;
        }

        // 5. 만약 selectedItems의 카운트가 4개가 되었다면 items 중에서 고르지 않고 selectedItems 중에서 선택
        if (selectedItems.Count >= 4)
        {
            selectNum = Random.Range(0, selectedItems.Count) + 1;
        }
    }

    public void CloseMenu()
    {
        GameObject newItem = null;

        // 3. 랜덤으로 선택된 아이템 중 선택한 아이템의 selectNum에 맞는 아이템을 selectItemPos에 생성(Instantiate)(canvas 안에 생성되도록)
        switch (selectedItems.Count)
        {
            case 1:
                newItem = Instantiate(items[selectNum - 1], selectItemPos1.transform.position, Quaternion.identity);
                newItem.transform.SetParent(canvas.transform, false);
                newItem.transform.position = selectItemPos1.transform.position;
                break;
            case 2:
                newItem = Instantiate(items[selectNum - 1], selectItemPos2.transform.position, Quaternion.identity);
                newItem.transform.SetParent(canvas.transform, false);
                newItem.transform.position = selectItemPos2.transform.position;
                break;
            case 3:
                newItem = Instantiate(items[selectNum - 1], selectItemPos3.transform.position, Quaternion.identity);
                newItem.transform.SetParent(canvas.transform, false);
                newItem.transform.position = selectItemPos3.transform.position;
                break;
            case 4:
                newItem = Instantiate(items[selectNum - 1], selectItemPos4.transform.position, Quaternion.identity);
                newItem.transform.SetParent(canvas.transform, false);
                newItem.transform.position = selectItemPos4.transform.position;
                break;
        }

        // 3-1. 선택한 아이템이 맞는 bool값을 true로
        if (newItem != null)
        {
            UpdateBoolValues(newItem.name);

            // 4. selectItemPos에 이미 아이템이 생성되었다면 selectItemPos2에(2가 있다면 3, 3에 있다면 4)
            int nextPos = Mathf.Min(selectedItems.Count + 1, 4);
            GameObject nextSelectItemPos = null;
            switch (nextPos)
            {
                case 1:
                    nextSelectItemPos = selectItemPos1;
                    break;
                case 2:
                    nextSelectItemPos = selectItemPos2;
                    break;
                case 3:
                    nextSelectItemPos = selectItemPos3;
                    break;
                case 4:
                    nextSelectItemPos = selectItemPos4;
                    break;
            }

            // 5. 선택한 아이템은 selectedItems에 추가
            selectedItems.Add(newItem);

            // 6. selectedItems 카운트가 4 이상이라면 이후부터는 생성은 하지 말고 Lv만 오르도록
            if (selectedItems.Count < 4)
            {
                newItem.transform.SetParent(canvas.transform, false);
                newItem.transform.position = nextSelectItemPos.transform.position;
            }
        }

        // 모든 아이템을 숨김
        foreach (GameObject item in items)
        {
            item.transform.position = new Vector3(0, 2000, 0);
        }

        // 메뉴 닫기
        selectItemMenu.SetActive(false);
    }


    void UpdateBoolValues(string itemName)
    {
        // 선택한 아이템에 따라 bool 값을 업데이트
        switch (itemName)
        {
            case "Fire":
                fireSelect = true;
                fireLv++;
                break;
            case "FireShot":
                fireShotSelect = true;
                fireShotLv++;
                break;
            case "HolyShiled":
                holyShiledSelect = true;
                holyShiledLv++;
                break;
            case "HolyShot":
                holyShotSelect = true;
                holyShotLv++;
                break;
            case "Melee":
                meleeSelect = true;
                meleeLv++;
                break;
            case "Posion":
                posionSelect = true;
                posionLv++;
                break;
            case "Rock":
                rockSelect = true;
                rockLv++;
                break;
            case "Sturn":
                sturnSelect = true;
                sturnLv++;
                break;
        }
    }

    // 버튼에 할당되어 있음
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
