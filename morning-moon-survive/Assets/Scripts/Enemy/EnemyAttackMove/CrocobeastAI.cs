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
        anim.Play("Attack1");
        yield return new WaitForSeconds(anim["Attack1"].length);
        anim.CrossFade("Idle");
        enemyCombat.SetMoveset(1);
    }
    
   /* public override IEnumerator AttackMove2() //Charge
    {
        yield return new WaitForSeconds(1.0f);
    }

    public override IEnumerator AttackMove3() //Stomp
    {
        yield return new WaitForSeconds(1.0f);
    }*/
}
