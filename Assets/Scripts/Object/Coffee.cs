using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{

    public GameObject coffeeMenu;
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (hit.transform.gameObject.tag == "Coffee")
            {
                // null 값이 아니라면
                if (hit.collider != null)
                {
                    // 메뉴 창을 킴
                    coffeeMenu.SetActive(true);
                    

                }
            }
        }
    }
}
