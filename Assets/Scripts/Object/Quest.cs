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
        /*  퀘스트 내용 text 가져오는 함수 */
        quest1 = GameObject.Find("Canvas/QuestMenu/Viewport/QuestType/quest1/Text").GetComponent<Text>();

        // 콘솔 창에 해당 text의 내용을 띄움
        //print(text1.text);

        // 버튼 내의 텍스트 '수락'을 '수락완료'로 바꿈
        buttonTxt = GameObject.Find("Canvas/QuestMenu/Viewport/QuestType/quest1/accept/Text").GetComponent<Text>();
        buttonTxt.text = "수락완료";

        // 퀘스트 ui의 배경을 어둡게 바꿈
        GameObject normalization = GameObject.Find("Canvas/QuestMenu/Viewport/QuestType/quest1");
        Color color = normalization.GetComponent<Image>().color;
        color.r = 0.59f; color.g = 0.59f; color.b = 0.59f; color.a = 0.59f;
        normalization.GetComponent<Image>().color = color;

        // 버튼을 한 번 클릭하면 다시 클릭할 수 없도록 Interactable을 false로 바꿈
        btn = GameObject.Find("Canvas/QuestMenu/Viewport/QuestType/quest1/accept").GetComponent<Button>();
        btn.interactable = false;

        // 첫 번째 퀘스트 내용을 화면에 띄움
        quest1_into_background = GameObject.Find("Canvas/background/backgroundText").GetComponent<Text>();
        quest1_into_background.text = "퀘스트 : " + quest1.text;

    }
}
