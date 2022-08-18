using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sandwich : MonoBehaviour
{

    public GameObject sandwichMenu;

    // Start is called before the first frame update
    void Start()
    {
        /* 캐릭터의 레벨이 일정 레벨에 도달하면 샌드위치와 커피 오브젝트에서 다음 단계의 잠금을 푼 모습을 보여주는 코드
        // ui 이미지 투명도 255로 되돌리는 코드
        GameObject normalization = GameObject.Find("Canvas/SandwichMenu/Viewport/SandwichType/Sandwich2");
        Color color = normalization.GetComponent<Image>().color;
        color.r = 1f; color.g = 1f; color.b = 1f; color.a = 1f;
        normalization.GetComponent<Image>().color = color;

        // 자물쇠 이미지 숨김
        GameObject locks = GameObject.Find("Canvas/SandwichMenu/Viewport/SandwichType/Sandwich2/lock");
        locks.SetActive(false);
        */

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))       // 마우스 클릭시
        {
            // 마우스로 클릭한 좌표값을 가져옴
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 해당 좌표에 있는 오브젝트를 찾음
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            //태그가 "computer"인 오브젝트 클릭시
            if (hit.transform.gameObject.tag == "Sandwich")
            {
                // null 값이 아니라면
                if (hit.collider != null)
                {
                    // 메뉴 창을 킴
                    sandwichMenu.SetActive(true);

                }
            }
        }
    }

    public void SandwichUnlocked(int level)
    {
        /* 캐릭터의 레벨이 일정 레벨에 도달하면 샌드위치와 커피 오브젝트에서 다음 단계의 잠금을 푼 모습을 보여주는 코드 */
        // ui 이미지 투명도 255로 되돌리는 코드
        Transform normalization = GameObject.Find("Canvas").transform.Find("SandwichMenu/Viewport/SandwichType/Sandwich" + level);
        Color color = normalization.GetComponent<Image>().color;
        color.r = 1f; color.g = 1f; color.b = 1f; color.a = 1f;
        normalization.GetComponent<Image>().color = color;

        // 자물쇠 이미지 숨김
        GameObject.Find("Canvas").transform.Find("SandwichMenu/Viewport/SandwichType/Sandwich" + level + "/lock").gameObject.SetActive(false);
    }
}
