using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player
{
    public int[][] Price = { new int[] { 600, 1000, 1400, 1600 }, new int[] { 800, 1500, 2000, 2500 } };   //������ġ, Ŀ�� ����
    public int[][] Recover = { new int[] { 80, 160, 320, 560 }, new int[] { 40, 80, 160, 280 } };       //������ġ, Ŀ�� ȸ����
    public int[][] Gold = { new int[] { 50, 70, 100 } };
    public int[][] Experience = { new int[] { 3, 5, 7 } };

    public int Lv, MAXEXP, hlv;   //����, �ִ� ü��, �ִ� �Ƿε�, �ִ� ����ġ
    public int MAXHP, MAXFG;
    public int hp, power, exp, gold;       //ü��, ���ݷ�, ����ġ, ���
    public float fatigue, speed;           //�Ƿε�, �ӵ�
    public bool hasQuest, hasTest;         //����Ʈ �� ���� ����
    public int[] Quest;                  //����Ʈ ����

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

    //ĳ���� ���� �Լ���
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
            Debug.Log("���� ���� ���: " + gold);
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
