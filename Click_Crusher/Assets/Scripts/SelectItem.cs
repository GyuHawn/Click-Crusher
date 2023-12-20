using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

                // �ߺ� üũ
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
            // �̹� 4���� �������� ���õ� ����� ó��
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

            // ���õ� �������� selectItemPos1,2,3,4�� �̵�
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
            // ���õ��� ���� �����۸� (0, 2000) ��ġ�� �̵�
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
