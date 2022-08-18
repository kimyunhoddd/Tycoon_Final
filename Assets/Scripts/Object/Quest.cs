using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Quest : MonoBehaviour
{
    public Text quest1;
    private Text buttonTxt;
    public Text quest1_into_background;

    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickquest1Bts()
    {
        /*  ����Ʈ ���� text �������� �Լ� */
        quest1 = GameObject.Find("Canvas/QuestMenu/Viewport/QuestType/quest1/Text").GetComponent<Text>();

        // �ܼ� â�� �ش� text�� ������ ���
        //print(text1.text);

        // ��ư ���� �ؽ�Ʈ '����'�� '�����Ϸ�'�� �ٲ�
        buttonTxt = GameObject.Find("Canvas/QuestMenu/Viewport/QuestType/quest1/accept/Text").GetComponent<Text>();
        buttonTxt.text = "�����Ϸ�";

        // ����Ʈ ui�� ����� ��Ӱ� �ٲ�
        GameObject normalization = GameObject.Find("Canvas/QuestMenu/Viewport/QuestType/quest1");
        Color color = normalization.GetComponent<Image>().color;
        color.r = 0.59f; color.g = 0.59f; color.b = 0.59f; color.a = 0.59f;
        normalization.GetComponent<Image>().color = color;

        // ��ư�� �� �� Ŭ���ϸ� �ٽ� Ŭ���� �� ������ Interactable�� false�� �ٲ�
        btn = GameObject.Find("Canvas/QuestMenu/Viewport/QuestType/quest1/accept").GetComponent<Button>();
        btn.interactable = false;

        // ù ��° ����Ʈ ������ ȭ�鿡 ���
        quest1_into_background = GameObject.Find("Canvas/background/backgroundText").GetComponent<Text>();
        quest1_into_background.text = "����Ʈ : " + quest1.text;

    }
}
