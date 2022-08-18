using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject StartPanel; //StartPanel 선언
    public GameObject IntroPanel; //IntroPanel 선언


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayTime(2)); //Coroutine함수를 사용하여 딜레이 값을 2초 준다
    }


    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time); //2초를 기다른 후

        IntroPanel.SetActive(false); //SetActive 함수를 사용하여 IntroPanel 비활성화
        StartPanel.SetActive(true); //StartPanel은 활성화
    }

    public void GoGameScene()
    {
        SceneManager.LoadScene(1); //LoadScene함수를 사용하여 게임씬을 불러온다. 게임씬의 인덱스가 0
    }

    // Update is called once per frame
    void Update()
    {

    }
}