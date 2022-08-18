using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
   
    
    public Player GameLoad()
    {
            Debug.Log("데이터를 불러옵니다.");
            
            float fatigue = PlayerPrefs.GetFloat("Storage_fatigue");
            float speed = PlayerPrefs.GetFloat("Storage_speed");

            int lv = PlayerPrefs.GetInt("Storage_level");
            int MAXFG = PlayerPrefs.GetInt("Storage_MaxFG");
            int MAXHP = PlayerPrefs.GetInt("Storage_MaxHP");
            int MAXEXP = PlayerPrefs.GetInt("Storage_MaxEXP");
            int hp = PlayerPrefs.GetInt("Storage_hp");
            int hlv = PlayerPrefs.GetInt("Storage_hlv");
            int exp = PlayerPrefs.GetInt("Storage_exp");
            int gold = PlayerPrefs.GetInt("Storage_gold");
            int power = PlayerPrefs.GetInt("Storage_power");
            bool hasQuest = PlayerPrefs.GetInt("Storage_has_quest") == 1 ? true : false;
            bool hasTest = PlayerPrefs.GetInt("Storage_has_test") == 1 ? true : false;
            int[] quest = new int[] { PlayerPrefs.GetInt("Storage_quest_target"), PlayerPrefs.GetInt("Storage_quest_cnt") };
        Debug.Log("save result: " + exp);
            return new Player(lv, hlv, MAXEXP, MAXFG, MAXHP, hp, power, exp, gold, fatigue, speed, hasQuest, hasTest, quest);
    }
}
