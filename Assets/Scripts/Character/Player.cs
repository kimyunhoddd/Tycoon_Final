using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player
{
    public int[][] Price = { new int[] { 600, 1000, 1400, 1600 }, new int[] { 800, 1500, 2000, 2500 } };   //샌드위치, 커피 가격
    public int[][] Recover = { new int[] { 80, 160, 320, 560 }, new int[] { 40, 80, 160, 280 } };       //샌드위치, 커피 회복량
    public int[][] Gold = { new int[] { 50, 70, 100 } };
    public int[][] Experience = { new int[] { 3, 5, 7 } };

    public int Lv, MAXEXP, hlv;   //레벨, 최대 체력, 최대 피로도, 최대 경험치
    public int MAXHP, MAXFG;
    public int hp, power, exp, gold;       //체력, 공격력, 경험치, 골드
    public float fatigue, speed;           //피로도, 속도
    public bool hasQuest, hasTest;         //퀘스트 및 시험 유무
    public int[] Quest;                  //퀘스트 내용

    public Player()
    {

    }
    public Player(int lv, int hlv, int MAXEXP, int MAXFG, int MAXHP, int hp, int power, int exp, int gold, float fatigue, float speed, bool hasQuest, bool hasTest, int[] Quest)
    {
        this.Lv = lv;
        this.hlv = hlv;
        this.MAXEXP = MAXEXP;
        this.MAXFG = MAXFG;
        this.MAXHP = MAXHP;
        this.hp = hp;
        this.power = power;
        this.exp = exp;
        this.gold = gold;
        this.fatigue = fatigue;
        this.speed = speed;
        this.hasQuest = hasQuest;
        this.hasTest = hasTest;
        this.Quest = Quest;
    }

    //캐릭터 관련 함수들
    void LevelUp()
    {
        hp = MAXHP += 100 * Lv;
        fatigue = MAXFG += 50 * Lv;
        Lv++;
    }
    public void alive()
    {
        hp = (int)(MAXHP * 0.2);
        fatigue = MAXFG * 0.8f;
    }
    public int getLv() { return Lv; }
    public void GainFatigue(float fatigue)
    {
        this.fatigue += fatigue * Lv;
    }
    public bool CanHunt() { return hp > MAXHP / 5 * 1 && fatigue < MAXFG / 5 * 4f; }
    public void SetHuntLv()
    {
        if ((float)exp / MAXEXP > 0.6f) hlv = 3;
        else if ((float)exp / MAXEXP > 0.3f) hlv = 2;
        else hlv = 1;
    }
    public int GetHLV() { SetHuntLv(); return hlv; }
    void SpendGold(int gold) { this.gold -= gold; }
    public void GetGold(int gold) { this.gold += gold; }
    public void KillMoster()
    {
        this.gold += Gold[Lv - 1][hlv - 1];
        this.exp += Experience[Lv - 1][hlv - 1];

    }
    public void Attacked(int damage)
    {
        hp -= damage;
    }
    public bool Recovery(int hf)
    {
        if (gold >= Price[hf][Lv - 1])
        {
            if (hf == 0) hp += Recover[hf][Lv - 1];
            else fatigue -= Recover[hf][Lv - 1];
            SpendGold(Price[hf][Lv - 1]);
            Debug.Log("현재 소지 골드: " + gold);
            return true;
        }
        else return false;
    }
    public void setQuest(bool hasQuest, int[] Quest)
    {
        this.hasQuest = hasQuest;
        this.Quest = Quest;
    }
    public string getQuest()
    {
        return Quest[0] + ", " + Quest[1];
    }

}
