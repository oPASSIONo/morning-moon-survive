using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatStaminaModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Stamina stamina = character.GetComponent<Stamina>();
        if (stamina!=null)
            stamina.IncreaseStamina((int)val);
    }
}
