using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Collections.Generic;
using System.Threading.Tasks;


public class Monster_damage : MonoBehaviour
{
    Animation ani;  
    public float StartHealth;
    public float Health;

    public GameObject Damagetext;
    public GameObject TextPos;  //텍스트 생성될 위치

    public GameObject HealthBar;

    public void GetDamage(int damage)
    {
        GameObject dmgText = Instantiate(Damagetext, TextPos.transform.position, Quaternion.identity);
        dmgText.GetComponent<Text>().text = damage.ToString();
        Health -= damage;
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;

     //   dmgText.transform.position = dapos.position;
        Destroy(dmgText, 1f);
    }



    public void Update()
    {
        if (gameObject != null)
        {
            if (Health <= 0)
            {
                //die();
                // Invoke("Fall", 0.2f);
                //Destroy(this.gameObject,1f);
               // this.gameObject.tag = "aa";
                gameObject.SetActive(false);
                Invoke("Alive", 5);
            }

        }
    }
    void Alive()
    {
        Health = StartHealth;
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
        gameObject.SetActive(true);
        CancelInvoke("Alive");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "spsp")
        {
             GetDamage(other.GetComponent<Damageee>().Damage);
          //  Debug.Log("HIt!");
            
        }
        
    }
}



   


   


    
