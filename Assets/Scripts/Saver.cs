using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public GameObject playerObj;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void GameSave()
    {
        player = playerObj.GetComponent<PlayerObj>().player;
        int boolean;

        PlayerPrefs.SetInt("user", 1);
        PlayerPrefs.SetString("Stroage_character_name", playerObj.name);
        PlayerPrefs.SetFloat("Storage_fatigue", player.fatigue); //피로도
        PlayerPrefs.SetFloat("Storage_speed", player.speed); //스피드

        PlayerPrefs.SetInt("Storage_level", player.Lv); //레벨
        PlayerPrefs.SetInt("Storage_MaxFG", player.MAXFG); //최대피로도
        PlayerPrefs.SetInt("Storage_MaxHP", player.MAXHP); //최대체력
        PlayerPrefs.SetInt("Storage_MaxEXP", player.MAXEXP);
        PlayerPrefs.SetInt("Storage_hp", player.hp); //체력
        PlayerPrefs.SetInt("Storage_hlv", player.hlv); //체력
        PlayerPrefs.SetInt("Storage_power", player.power); //power
        PlayerPrefs.SetInt("Storage_exp", player.exp); //경험치
        PlayerPrefs.SetInt("Storage_gold", player.gold); //골드

        boolean = (player.hasQuest) ? 1 : 0;
        PlayerPrefs.SetInt("Storage_has_quest", boolean);
        boolean = (player.hasTest) ? 1 : 0;
        PlayerPrefs.SetInt("Storage_has_Test", boolean);
        PlayerPrefs.SetInt("Storage_quest_target", player.Quest[0]);
        PlayerPrefs.SetInt("Storage_quest_cnt", player.Quest[1]);

        PlayerPrefs.Save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
