using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHungerModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Satiety hunger = character.GetComponent<Satiety>();
        if (hunger!=null)
            hunger.IncreaseSatiety((float)val);
        
    }
}
