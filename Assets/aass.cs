using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aass : MonoBehaviour
{
    public GameObject hudDamageText;
    public Transform hudPos;

    public void dmgpos()
    {
        GameObject hudText = Instantiate(hudDamageText); // ������ �ؽ�Ʈ ������Ʈ
        hudText.transform.position = hudPos.position; // ǥ�õ� ��ġ
    }

    
}
