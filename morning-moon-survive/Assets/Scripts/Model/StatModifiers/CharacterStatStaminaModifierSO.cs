using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatStaminaModifierSO : CharacterStatModifierSO
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void AffectCharacter(GameObject character, float val)
    {
        Stamina stamina = character.GetComponent<Stamina>();
        if (stamina!=null)
            stamina.IncreaseStamina((int)val);
    }
}
