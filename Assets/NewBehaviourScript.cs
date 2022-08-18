using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{


    void Abcd()
    {
        Invoke("Abcd", 2);
        Debug.Log("attack!!");
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Invoke("Abcd", 2);
        if (other.tag == "Player")
        {
           
        }
    }
}
