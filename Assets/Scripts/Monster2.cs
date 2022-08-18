using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : MonoBehaviour
{

    Rigidbody2D rigid;
    public int nextMove;
    public int a ;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("Think", 1);

    }


    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);



        /*if(a == 1)
        {
        rigid.velocity = new Vector2(-1, rigid.velocity.y);

        }else if(nextMove == -1)
        {
            rigid.velocity = new Vector2(1, rigid.velocity.y);
        }*/
    }

    void Think()
    {
        if (a == 1)
        {
            nextMove = -1;

        }
        else if (a == -1)
        {
            nextMove = 1;
        }

        a = nextMove;
        Invoke("Think", 1);
    }


    public int hp = 100;
    public void Attacked(int damage)
    {
        hp -= damage;
    }
}
