using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject StartPanel; //StartPanel ����
    public GameObject IntroPanel; //IntroPanel ����


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayTime(2)); //Coroutine�Լ��� ����Ͽ� ������ ���� 2�� �ش�
    }


    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time); //2�ʸ� ��ٸ� ��

        IntroPanel.SetActive(false); //SetActive �Լ��� ����Ͽ� IntroPanel ��Ȱ��ȭ
        StartPanel.SetActive(true); //StartPanel�� Ȱ��ȭ
    }

    public void GoGameScene()
    {
        SceneManager.LoadScene(1); //LoadScene�Լ��� ����Ͽ� ���Ӿ��� �ҷ��´�. ���Ӿ��� �ε����� 0
    }

    // Update is called once per frame
    void Update()
    {

    }
}