using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class SelectItem : MonoBehaviour
{
    public GameObject[] items; // ������ �迭
    public GameObject itemPos1; // ���õ� ������ ��ġ
    public GameObject itemPos2;
    public GameObject itemPos3;

    public List<GameObject> selectedItems; // ������ ������
    public GameObject selectItemPos1;
    public GameObject selectItemPos2;
    public GameObject selectItemPos3;
    public GameObject selectItemPos4;
    public int selectNum; // ������ ������ ��ȣ

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
        // 1. items�� �������� �������� 3���� ������ ����
        // 2. itemPos1,2,3 ��ġ�� ���ʷ� �ϳ��� �̵�

        // 5. ���� selectedItems�� ī��Ʈ�� 4���� �Ǿ��ٸ� items�߿��� �����ʰ� selectedItems �߿��� �����ϵ���
    }

    // ������ ���� ��ư
    public void CloseMenu()
    {
        // 3. �������� ���õ� ������ �� ������ �������� selectNum�� �´� �������� selectItemPos1�� ����(Instantiate)(canvas�ȿ� �����ǵ���)
        // 3-1. ������ �������� �´� bool���� true��
        // 4. selectItemPos1�� �̹� �������� �����Ǿ��ٸ� selectItemPos2��(2�� �ִٸ� 3, 3�� �ִٸ� 4)
        // 5. ������ �������� selectedItems�� �߰�

        // 6. selectedItems ī��Ʈ�� 4�̻��̶�� ���ĺ��ʹ� ������ �������� Lv�� �������� 
    }*/

    public void ItemSelect()
    {
        selectItemMenu.SetActive(true);

        // 1. items�� ������ �� �������� 3���� ������ ����
        selectedItems.Clear(); // ������ ���õ� ������ �ʱ�ȭ
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, items.Length);
            GameObject selectedItem = items[randomIndex];
            selectedItems.Add(selectedItem);

            // �ش� �������� itemPos1, 2, 3 ��ġ�� �̵�
            GameObject itemPos = null;
            if (i == 0) itemPos = itemPos1;
            else if (i == 1) itemPos = itemPos2;
            else if (i == 2) itemPos = itemPos3;

            selectedItem.transform.position = itemPos.transform.position;
        }

        // 5. ���� selectedItems�� ī��Ʈ�� 4���� �Ǿ��ٸ� items �߿��� ���� �ʰ� selectedItems �߿��� ����
        if (selectedItems.Count >= 4)
        {
            selectNum = Random.Range(0, selectedItems.Count) + 1;
        }
    }

    public void CloseMenu()
    {
        GameObject newItem = null;

        // 3. �������� ���õ� ������ �� ������ �������� selectNum�� �´� �������� selectItemPos�� ����(Instantiate)(canvas �ȿ� �����ǵ���)
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

        // 3-1. ������ �������� �´� bool���� true��
        if (newItem != null)
        {
            UpdateBoolValues(newItem.name);

            // 4. selectItemPos�� �̹� �������� �����Ǿ��ٸ� selectItemPos2��(2�� �ִٸ� 3, 3�� �ִٸ� 4)
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

            // 5. ������ �������� selectedItems�� �߰�
            selectedItems.Add(newItem);

            // 6. selectedItems ī��Ʈ�� 4 �̻��̶�� ���ĺ��ʹ� ������ ���� ���� Lv�� ��������
            if (selectedItems.Count < 4)
            {
                newItem.transform.SetParent(canvas.transform, false);
                newItem.transform.position = nextSelectItemPos.transform.position;
            }
        }

        // ��� �������� ����
        foreach (GameObject item in items)
        {
            item.transform.position = new Vector3(0, 2000, 0);
        }

        // �޴� �ݱ�
        selectItemMenu.SetActive(false);
    }


    void UpdateBoolValues(string itemName)
    {
        // ������ �����ۿ� ���� bool ���� ������Ʈ
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

    // ��ư�� �Ҵ�Ǿ� ����
    public void Fire()
    {
        //items[0]
        selectNum = 1;
        itemName.text = "ȭ����ȯ";
        itemEx.text = "�ֺ��� ������ ��ġ�� �ұ���� ��ȯ�Ͽ� ���� �ð� ���� �������� �����ϴ�.";
    }
    public void FireShot()
    {
        //items[1]
        selectNum = 2;
        itemName.text = "�����ϴ� �Ҳ�";
        itemEx.text = "���� ������ �Ҳ��� �۶߷� �뷮�� �������� �����ϴ�.";
    }
    public void HolyShiled()
    {
        //items[2]
        selectNum = 3;
        itemName.text = "���� ��ȣ��";
        itemEx.text = "�� ���� ������ �Ϻ��ϰ� ����ϴ� ���� ���и� ��ȯ�մϴ�.";
    }
    public void HolyShot()
    {
        //items[3]
        selectNum = 4;
        itemName.text = "���� ����";
        itemEx.text = "������ ������ ���� �����Ͽ� ���� �ð� ���� �������� �����ϴ�.";
    }
    public void Melee()
    {
        //items[4]
        selectNum = 5;
        itemName.text = "��ǳ�� �ϰ�";
        itemEx.text = "���� 5���� ���ݵ��� �߰������� �� �� �� �����մϴ�.";
    }
    public void Posion()
    {
        //items[5]
        selectNum = 6;
        itemName.text = "�͵�";
        itemEx.text = "���� ������ ���Ϳ��� ���� �ð� ���� ���������� �������� �����ϴ�.";
    }
    public void Rock()
    {
        //items[6]
        selectNum = 7;
        itemName.text = "�е����� ��";
        itemEx.text = "���� ���ݿ� ������ �������� �����ϴ�.";
    }
    public void Sturn()
    {
        //items[7]
        selectNum = 8;
        itemName.text = "�ð��� ���";
        itemEx.text = "���Ͱ� ���� ������ ������ �� ������ �ð��� �����մϴ�.";
    }
}
