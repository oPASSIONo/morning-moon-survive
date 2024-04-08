using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    
    /*public override void AffectCharacter(GameObject character, float val)
    {
        HealthController health = character.GetComponent<HealthController>();
        if (health!=null)
            health.RestoreHealth((int)val);
    }*/
    public override void AffectCharacter(GameObject character, float val)
    {
        Health health = character.GetComponent<Health>();
        if (health!=null)
            health.AddHealth((int)val);
    }
}
