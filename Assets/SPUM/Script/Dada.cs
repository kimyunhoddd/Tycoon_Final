using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dada : MonoBehaviour
{
    GameObject a;
    public void Damage12()
    {
        // Invoke("Abcd", 2);
        if (a != null)
        {
           // a = GameObject.Find("Monster");
            a.GetComponent<Monster_damage>().GetDamage(15);
            Debug.Log("Hit!!");
        }

    }




}
    
      
