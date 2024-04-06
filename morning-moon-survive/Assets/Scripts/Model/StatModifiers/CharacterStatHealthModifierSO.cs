using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float var)
    {
        /*Health health = character.GetComponent<Health>();
        if (health!=null)
            health.AddHealth((int)val);*/
    }
}
