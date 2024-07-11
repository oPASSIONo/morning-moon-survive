using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private float baseDamage = 10;

    private void OnTriggerEnter(Collider other)
    {
        DamageZone damageZone = other.GetComponent<DamageZone>();
        
        if (damageZone != null)
        {
            damageZone.ApplyDamage(baseDamage);
        }
    }
}