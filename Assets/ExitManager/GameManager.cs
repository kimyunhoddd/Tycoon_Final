using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject sandwichMenu;
    public GameObject coffeeMenu;
    public GameObject computerMenu;
    public GameObject npcMenu;
    public GameObject questMenu;
    public GameObject MenuSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
