using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class SelectItem : MonoBehaviour
{
    private Character character;

    public bool itemSelecting;

    public GameObject[] items; // 아이템 배열
    public GameObject itemPos1; // 선택된 아이템 위치
    public GameObject itemPos2;
    public GameObject itemPos3;
    public List<GameObject> selectItems; // 선택한 아이템
    public List<GameObject> playerItems; // 플레이어 아이템

    public GameObject selectItemPos1;
    public GameObject selectItemPos2;
    public GameObject selectItemPos3;
    public GameObject selectItemPos4;
    public int selectNum; // 선택한 아이템 번호

    public TMP_Text itemName;
    public TMP_Text itemEx;

    public TMP_Text passLvText;
    public TMP_Text item1LvText;
    public TMP_Text item2LvText;
    public TMP_Text item3LvText;
    public TMP_Text item4LvText;

    public GameObject[] characters;
    public GameObject charPos;

    public int passLv;
    public int fireLv;
    public int fireShotLv;
    public int holyWaveLv;
    public int holyShotLv;
    public int meleeLv;
    public int posionLv;
    public int rockLv;
    public int sturnLv;

    public bool fireSelect;
    public bool fireShotSelect;
    public bool holyWaveSelect;
    public bool holyShotSelect;
    public bool meleeSelect;
    public bool posionSelect;
    public bool rockSelect;
    public bool sturnSelect;

    public GameObject selectItemMenu;

    public bool selectedItem;

    public Canvas canvas;

    private void Start()
    {
        character = GameObject.Find("Manager").GetComponent<Character>();

        itemSelecting = false;       
    }

    private void Update()
    {
        ItemLevelOpen();
        CharacterInstant();
    }
    
    void CharacterInstant()
    {
        GameObject newCharacter = null;
        if (newCharacter == null)
        {
            if (character.currentCharacter == 1)
            {
                newCharacter = Instantiate(characters[0], charPos.transform.position, Quaternion.identity);
                newCharacter.transform.SetParent(canvas.transform, false);
                newCharacter.transform.position = charPos.transform.position;
            }
            if (character.currentCharacter == 2)
            {
                newCharacter = Instantiate(characters[1], charPos.transform.position, Quaternion.identity);
                newCharacter.transform.SetParent(canvas.transform, false);
                newCharacter.transform.position = charPos.transform.position;
            }
            if (character.currentCharacter == 3)
            {
                newCharacter = Instantiate(characters[2], charPos.transform.position, Quaternion.identity);
                newCharacter.transform.SetParent(canvas.transform, false);
                newCharacter.transform.position = charPos.transform.position;
            }
            if (character.currentCharacter == 4)
            {
                newCharacter = Instantiate(characters[3], charPos.transform.position, Quaternion.identity);
                newCharacter.transform.SetParent(canvas.transform, false);
                newCharacter.transform.position = charPos.transform.position;
            }
        }
    }

    public void ItemSelect()
    {
        selectItemMenu.SetActive(true);
        selectItems.Clear();

        itemSelecting = true;
        selectedItem = false;

        foreach (GameObject playerItem in playerItems)
        {
            playerItem.SetActive(false);
        }

        if (playerItems.Count >= 4)
        {
            while (selectItems.Count < 3)
            {
                int randomIndex = UnityEngine.Random.Range(0, playerItems.Count);
                GameObject selectedItem = playerItems[randomIndex];

                selectItems.Add(selectedItem);

                GameObject itemFromItems = Array.Find(items, item => item.name == selectedItem.name);

                GameObject itemPos = null;
                if (selectItems.Count == 1) itemPos = itemPos1;
                else if (selectItems.Count == 2) itemPos = itemPos2;
                else if (selectItems.Count == 3) itemPos = itemPos3;

                itemFromItems.transform.position = itemPos.transform.position;
            }
        }
        else
        {
            while (selectItems.Count < 3)
            {
                int randomIndex = UnityEngine.Random.Range(0, items.Length);
                GameObject selectedItem = items[randomIndex];

                if (!selectItems.Contains(selectedItem))
                {
                    selectItems.Add(selectedItem);

                    GameObject itemPos = null;
                    if (selectItems.Count == 1) itemPos = itemPos1;
                    else if (selectItems.Count == 2) itemPos = itemPos2;
                    else if (selectItems.Count == 3) itemPos = itemPos3;

                    selectedItem.transform.position = itemPos.transform.position;
                }
            }
        }

        selectNum = UnityEngine.Random.Range(0, selectItems.Count) + 1;
    }

    public void CloseMenu()
    {
        if (selectedItem)
        {
            bool isItemExist = false;

            foreach (GameObject item in playerItems)
            {
                if (item.name == items[selectNum - 1].name)
                {
                    isItemExist = true;
                    break;
                }
            }

            if (!isItemExist && playerItems.Count < 4)
            {
                InstantiateItem();
            }

            foreach (GameObject selectItem in selectItems)
            {
                if (!playerItems.Contains(selectItem))
                {
                    selectItem.transform.position = new Vector3(0, 2000, 0);
                }
            }
            foreach (GameObject item in items)
            {
                item.transform.position = new Vector3(0, 2000, 0);
            }

            foreach (GameObject playerItem in playerItems)
            {
                playerItem.SetActive(true);
            }
            
            ItemTextClear();
            ItemLevelUp();
            itemSelecting = false;
            selectItemMenu.SetActive(false);
        }
    }

    void InstantiateItem()
    {
        GameObject newItem = null;
        switch (selectItems.Count)
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

        playerItems.Add(newItem);

        if (newItem != null)
        {
            newItem.name = newItem.name.Replace("(Clone)", "");
            int nextPos = Mathf.Min(playerItems.Count, 4);
            switch (nextPos)
            {
                case 1:
                    newItem.transform.position = selectItemPos1.transform.position;
                    break;
                case 2:
                    newItem.transform.position = selectItemPos2.transform.position;
                    break;
                case 3:
                    newItem.transform.position = selectItemPos3.transform.position;
                    break;
                case 4:
                    newItem.transform.position = selectItemPos4.transform.position;
                    break;
            }
        }
    }

    int GetItemLevel(GameObject item)
    {
        switch (item.name)
        {
            case "Fire": return fireLv;
            case "Fire Shot": return fireShotLv;
            case "Holy Wave": return holyWaveLv;
            case "Holy Shot": return holyShotLv;
            case "Melee": return meleeLv;
            case "Posion": return posionLv;
            case "Rock": return rockLv;
            case "Sturn": return sturnLv;
            default: return 0;
        }
    }

    


    void ItemLevelOpen()
    {
        if (playerItems.Count > 0)
        {
            int item1Level = GetItemLevel(playerItems[0]);
            item1LvText.text = "Lv." + item1Level.ToString();
            item1LvText.gameObject.SetActive(true);
        }
        else
        {
            item1LvText.gameObject.SetActive(false);
        }

        if (playerItems.Count > 1)
        {
            int item2Level = GetItemLevel(playerItems[1]);
            item2LvText.text = "Lv." + item2Level.ToString();
            item2LvText.gameObject.SetActive(true);
        }
        else
        {
            item2LvText.gameObject.SetActive(false);
        }

        if (playerItems.Count > 2)
        {
            int item3Level = GetItemLevel(playerItems[2]);
            item3LvText.text = "Lv." + item3Level.ToString();
            item3LvText.gameObject.SetActive(true);
        }
        else
        {
            item3LvText.gameObject.SetActive(false);
        }

        if (playerItems.Count > 3)
        {
            int item4Level = GetItemLevel(playerItems[3]);
            item4LvText.text = "Lv." + item4Level.ToString();
            item4LvText.gameObject.SetActive(true);
        }
        else
        {
            item4LvText.gameObject.SetActive(false);
        }
    }

    public void ItemLevelUp()
    {
        if(character.currentCharacter == 4)
        {
            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                switch (selectNum)
                {
                    case 1:
                        fireLv += 2;
                        break;
                    case 2:
                        fireShotLv += 2;
                        break;
                    case 3:
                        holyWaveLv += 2;
                        break;
                    case 4:
                        holyShotLv += 2;
                        break;
                    case 5:
                        meleeLv += 2;
                        break;
                    case 6:
                        posionLv += 2;
                        break;
                    case 7:
                        rockLv += 2;
                        break;
                    case 8:
                        sturnLv += 2;
                        break;
                }
            }
            else
            {
                switch (selectNum)
                {
                    case 1:
                        fireLv++;
                        break;
                    case 2:
                        fireShotLv++;
                        break;
                    case 3:
                        holyWaveLv++;
                        break;
                    case 4:
                        holyShotLv++;
                        break;
                    case 5:
                        meleeLv++;
                        break;
                    case 6:
                        posionLv++;
                        break;
                    case 7:
                        rockLv++;
                        break;
                    case 8:
                        sturnLv++;
                        break;
                }
            }
        }
        else
        {
            switch (selectNum)
            {
                case 1:
                    fireLv++;
                    break;
                case 2:
                    fireShotLv++;
                    break;
                case 3:
                    holyWaveLv++;
                    break;
                case 4:
                    holyShotLv++;
                    break;
                case 5:
                    meleeLv++;
                    break;
                case 6:
                    posionLv++;
                    break;
                case 7:
                    rockLv++;
                    break;
                case 8:
                    sturnLv++;
                    break;
            }
        }     
    }

    void ItemTextClear()
    {
        itemName.text = "";
        itemEx.text = "";
    }

    public void Fire()
    {
        if (itemSelecting)
        {
            //items[0]
            selectedItem = true;
            selectNum = 1;
            itemName.text = "화염소환";
            itemEx.text = "주변의 랜덤한 위치에 불기둥을 소환하여 일정 시간 동안 데미지를 입힘니다.";
        }
    }
    public void FireShot()
    {
        if (itemSelecting)
        {
            //items[1]
            selectedItem = true;
            selectNum = 2;
            itemName.text = "폭발하는 불꽃";
            itemEx.text = "넓은 범위에 불꽃을 퍼뜨려 대량의 데미지를 입힘니다.";
        }
    }
    public void HolyWave()
    {
        if (itemSelecting)
        {
            //items[2]
            selectedItem = true;
            selectNum = 3;
            itemName.text = "빛의 파동";
            itemEx.text = "빛의 파동이 일렁이며 맵 전체에 에너지를 퍼뜨려, 일정 시간 동안 적들에게 지속적인 피해를 입힙니다.";
        }
    }
    public void HolyShot()
    {
        if (itemSelecting)
        {
            //items[3]
            selectedItem = true;
            selectNum = 4;
            itemName.text = "빛의 심판";
            itemEx.text = "선택한 범위에 빛을 집중하여 일정 시간 동안 데미지를 입힘니다.";
        }
    }
    public void Melee()
    {
        if (itemSelecting)
        {
            //items[4]
            selectedItem = true;
            selectNum = 5;
            itemName.text = "폭풍의 일격";
            itemEx.text = "이후 5번의 공격동안 추가적으로 한 번 더 공격합니다.";
        }
    }
    public void Posion()
    {
        if (itemSelecting)
        {
            //items[5]
            selectedItem = true;
            selectNum = 6;
            itemName.text = "맹독";
            itemEx.text = "다음 공격한 몬스터에게 일정 시간 동안 지속적으로 데미지를 입힘니다.";
        }
    }
    public void Rock()
    {
        if (itemSelecting)
        {
            //items[6]
            selectedItem = true;
            selectNum = 7;
            itemName.text = "압도적인 힘";
            itemEx.text = "다음 공격에 강력한 데미지를 입힘니다.";
        }
    }
    public void Sturn()
    {
        if (itemSelecting)
        {
            //items[7]
            selectedItem = true;
            selectNum = 8;
            itemName.text = "흙의 강타";
            itemEx.text = "강력한 흙의 힘으로 적을 공격하여 일정 시간 동안 기절 상태로 만듭니다.";
        }
    }
}
