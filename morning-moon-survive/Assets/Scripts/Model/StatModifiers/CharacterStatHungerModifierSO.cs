using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHungerModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Hunger hunger = character.GetComponent<Hunger>();
        if (hunger!=null)
            hunger.DecreaseHunger((int)val);
        
    }
}
