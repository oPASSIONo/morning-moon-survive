using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreeEnviroment))]
public class TreeDebug : Editor
{
   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();

      //TreeEnviroment tree = (TreeEnviroment) target;
      
      if (GUILayout.Button("Amount of Tree"))
      {
         GameObject treeParent = GameObject.Find("PineTree");

         if (treeParent != null)
         {
            int childCount = treeParent.transform.childCount;
            Debug.Log($"Amount of Tree: {childCount}");
         }
         else
         {
            Debug.LogWarning("Parent object named 'PineTree' not found in the scene!");
         }
      }
   }
}
