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
        /* ĳ������ ������ ���� ������ �����ϸ� ������ġ�� Ŀ�� ������Ʈ���� ���� �ܰ��� ����� Ǭ ����� �����ִ� �ڵ�
        // ui �̹��� ���� 255�� �ǵ����� �ڵ�
        GameObject normalization = GameObject.Find("Canvas/SandwichMenu/Viewport/SandwichType/Sandwich2");
        Color color = normalization.GetComponent<Image>().color;
        color.r = 1f; color.g = 1f; color.b = 1f; color.a = 1f;
        normalization.GetComponent<Image>().color = color;

        // �ڹ��� �̹��� ����
        GameObject locks = GameObject.Find("Canvas/SandwichMenu/Viewport/SandwichType/Sandwich2/lock");
        locks.SetActive(false);
        */

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
            if (hit.transform.gameObject.tag == "Sandwich")
            {
                // null ���� �ƴ϶��
                if (hit.collider != null)
                {
                    // �޴� â�� Ŵ
                    sandwichMenu.SetActive(true);

                }
            }
        }
    }

    public void SandwichUnlocked(int level)
    {
        /* ĳ������ ������ ���� ������ �����ϸ� ������ġ�� Ŀ�� ������Ʈ���� ���� �ܰ��� ����� Ǭ ����� �����ִ� �ڵ� */
        // ui �̹��� ���� 255�� �ǵ����� �ڵ�
        Transform normalization = GameObject.Find("Canvas").transform.Find("SandwichMenu/Viewport/SandwichType/Sandwich" + level);
        Color color = normalization.GetComponent<Image>().color;
        color.r = 1f; color.g = 1f; color.b = 1f; color.a = 1f;
        normalization.GetComponent<Image>().color = color;

        // �ڹ��� �̹��� ����
        GameObject.Find("Canvas").transform.Find("SandwichMenu/Viewport/SandwichType/Sandwich" + level + "/lock").gameObject.SetActive(false);
    }
}
