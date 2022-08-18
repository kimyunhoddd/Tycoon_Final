using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aass : MonoBehaviour
{
    public GameObject hudDamageText;
    public Transform hudPos;

    public void dmgpos()
    {
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPos.position; // 표시될 위치
    }

    
}
