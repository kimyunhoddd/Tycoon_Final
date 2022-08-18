using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{

    public GameObject computerMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))       // ���콺 Ŭ����
        {
            // ���콺�� Ŭ���� ��ǥ���� ������
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // �ش� ��ǥ�� �ִ� ������Ʈ�� ã��
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            //�±װ� "computer"�� ������Ʈ Ŭ����
            if (hit.transform.gameObject.tag == "Computer")
            {
                // null ���� �ƴ϶��
                if (hit.collider != null)
                {
                    // �޴� â�� Ŵ
                    computerMenu.SetActive(true);


                }
            }
        }
    }
}
