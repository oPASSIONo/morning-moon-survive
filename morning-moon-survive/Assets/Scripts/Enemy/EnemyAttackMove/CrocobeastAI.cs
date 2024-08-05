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
        anim.Play("Attack1");
        yield return new WaitForSeconds(anim["Attack1"].length);
        anim.CrossFade("Idle");
        isAttack = false;
        enemyCombat.SetMoveset(1);
    }
    
    protected override IEnumerator AttackMove2() //Stomp
    {
        isAttack = true;
        anim.Play("Attack2");
        yield return new WaitForSeconds(anim["Attack2"].length);
        anim.CrossFade("Idle");
        isAttack = false;
        enemyCombat.SetMoveset(2);
    }

    /*protected override IEnumerator AttackMove3() //Charge
    {
        yield return new WaitForSeconds(1.0f);
    }*/
}
