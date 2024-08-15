using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAI : EnemyAI
{
    private float attack1Range = 3f;
    


    protected override IEnumerator AttackMove1() //Tackle
    {
        if(!IsPlayerInRange(attack1Range))
        {
            agent.speed = moveSpeed * 5f;
            enemyAnimation.SetFloat("Speed",2);
            while (!IsPlayerInRange(attack1Range))
            {
                agent.SetDestination(player.transform.position);
                yield return null;
            }
            float speedLerpTime = 0.2f; 
            float elapsedTime = 0f;
            while (elapsedTime < speedLerpTime)
            {
                agent.speed = Mathf.Lerp(moveSpeed * 5f, moveSpeed, elapsedTime / speedLerpTime);
                enemyAnimation.SetFloat("Speed", Mathf.Lerp(5f, 1f, elapsedTime / speedLerpTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        isModelUsingAction = true;
        agent.isStopped = true;
        lookAtPlayer = false;
        enemyAnimation.CrossFade(animHash_Attack1,0);
        AnimatorStateInfo stateInfo = enemyAnimation.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length+ 0.5f);
        enemyAnimation.CrossFade(animHash_Idle,0.2f);
        isAttack = false;
        isModelRunning = false;
        isModelUsingAction = false;
        enemyCombat.SetMoveset(1);
    }
}
