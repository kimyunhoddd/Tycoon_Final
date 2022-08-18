using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDie : StateMachineBehaviour
{
    float timepassde;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.transform.GetChild(0).gameObject);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timepassde += Time.deltaTime;
        if (timepassde >= 5)
        {
            Destroy(animator.gameObject);
        }
    }


}
