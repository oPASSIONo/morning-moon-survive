using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocobeastAI : EnemyAI
{

    private EnemyAI enemyAI;
    private EnemyCombat enemyCombat;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyCombat = GetComponent<EnemyCombat>();
    }

    
    protected override IEnumerator AttackMove1() //TailSlap
    {
        isAttack = true;
        enemyAnimation.CrossFade(animHash_Attack1,0);
        AnimatorStateInfo stateInfo = enemyAnimation.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length+ 0.5f);
        enemyAnimation.CrossFade(animHash_Idle,0.2f);
        /*anim.Play("Attack1");
        yield return new WaitForSeconds(anim["Attack1"].length);
        anim.CrossFade("Idle");*/
        isAttack = false;
        enemyCombat.SetMoveset(1);
    }
    
    protected override IEnumerator AttackMove2() //Stomp
    {
        isAttack = true;
        enemyAnimation.CrossFade(animHash_Attack2,0);
        AnimatorStateInfo stateInfo = enemyAnimation.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.5f);
        enemyAnimation.CrossFade(animHash_Idle,0.2f);
        /*anim.Play("Attack2");
        yield return new WaitForSeconds(anim["Attack2"].length);
        anim.CrossFade("Idle");*/
        isAttack = false;
        enemyCombat.SetMoveset(2);
    }

    /*protected override IEnumerator AttackMove3() //Charge
    {
        yield return new WaitForSeconds(1.0f);
    }*/
}
