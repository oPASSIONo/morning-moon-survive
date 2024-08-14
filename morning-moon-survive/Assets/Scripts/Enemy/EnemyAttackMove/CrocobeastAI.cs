using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocobeastAI : EnemyAI
{

    private EnemyAI enemyAI;

    private float attack1Range = 8f;
    private float attack2Range = 6f;
    private float attack3Range = 5f;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    
    protected override IEnumerator AttackMove1() //TailSlap
    {
        if(!IsPlayerInRange(attack1Range))
        {
            agent.speed = moveSpeed * 5f;
            enemyAnimation.SetFloat("Speed",5);
            while (!IsPlayerInRange(attack1Range))
            {
                agent.SetDestination(player.transform.position);
                yield return null;
            }
            float speedLerpTime = 0.2f; // Adjust as needed
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
    
    protected override IEnumerator AttackMove2() //Stomp
    {
        if (!IsPlayerInRange(attack2Range))
        {
            agent.speed = moveSpeed * 5f;
            enemyAnimation.SetFloat("Speed",5);
            while (!IsPlayerInRange(attack2Range))
            {
                agent.SetDestination(player.transform.position);
                yield return null;
            }
            
            float speedLerpTime = 0.2f; // Adjust as needed
            float elapsedTime = 0f;
            while (elapsedTime < speedLerpTime)
            {
                agent.speed = Mathf.Lerp(moveSpeed * 5f, moveSpeed, elapsedTime / speedLerpTime);
                enemyAnimation.SetFloat("Speed", Mathf.Lerp(5f, 1f, elapsedTime / speedLerpTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            /*agent.speed = moveSpeed;
            enemyAnimation.SetFloat("Speed",1);*/
        }
        
        isModelUsingAction = true;
        agent.isStopped = true;
        lookAtPlayer = false;
        enemyAnimation.CrossFade(animHash_Attack2,0);
        AnimatorStateInfo stateInfo = enemyAnimation.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.5f);
        enemyAnimation.CrossFade(animHash_Idle,0f);
        isAttack = false;
        isModelRunning = false;
        isModelUsingAction = false;
        enemyCombat.SetMoveset(2);
    }

    /*protected override IEnumerator AttackMove3() //Charge
    {
        yield return new WaitForSeconds(1.0f);
    }*/
}
