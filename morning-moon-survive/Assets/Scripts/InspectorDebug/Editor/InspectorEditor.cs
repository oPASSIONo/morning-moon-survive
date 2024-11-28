using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class InspectorEditor : Editor
{
   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();

      Player player = (Player) target;
      
      if (GUILayout.Button("Regen Player HP"))
      {
         player.SetHP(player.MaxHP);
      }
      if (GUILayout.Button("Regen Player Satiety"))
      {
         player.SetSatiety(player.MaxSatiety);
      }
      if (GUILayout.Button("Regen Player Stamina"))
      {
         player.SetStamina(player.MaxStamina);
      }
      
   }
}
