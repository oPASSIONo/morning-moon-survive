using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TreeEnviroment))]
public class TreeDebug : Editor
{
   private List<Vector3> removedTreePositions = new List<Vector3>();
   public GameObject treePrefab;
   
   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();
      
      treePrefab = (GameObject)EditorGUILayout.ObjectField("Tree Prefab", treePrefab, typeof(GameObject), false);
      
      if (GUILayout.Button("Save Removed Tree Position"))
      {
         GameObject treeParent = GameObject.Find("Tree_PineTree");

         if (treeParent != null)
         {
            removedTreePositions.Clear();
            foreach (Transform child in treeParent.transform)
            {
               if (!child.gameObject.activeInHierarchy)
               {
                  removedTreePositions.Add(child.position);
               }
            }
            Debug.Log($"Saved {removedTreePositions.Count} removed tree positions.");
         }
         else
         {
            Debug.LogWarning("Parent object named 'Tree' not found in the scene!");
         }
      }
      
      if (GUILayout.Button("Respawn Trees"))
      {
         if (treePrefab == null)
         {
            Debug.LogError("Tree Prefab is not assigned!");
            return;
         }

         GameObject PinetreeParent = GameObject.Find("Tree_PineTree");

         if (PinetreeParent == null)
         {
            PinetreeParent = new GameObject("Tree_PineTree");
         }

         foreach (Vector3 position in removedTreePositions)
         {
            // สร้างต้นไม้ใหม่เฉพาะในตำแหน่งที่บันทึกไว้
            GameObject newTree = PrefabUtility.InstantiatePrefab(treePrefab) as GameObject;
            newTree.transform.position = position;
            newTree.transform.parent = PinetreeParent.transform;
         }

         Debug.Log($"Respawned {removedTreePositions.Count} trees.");
         removedTreePositions.Clear();
      }

      if (GUILayout.Button("Amount of PineTree"))
      {
         GameObject PinetreeParent = GameObject.Find("PineTree");

         if (PinetreeParent != null)
         {
            int childCount = PinetreeParent.transform.childCount;
            Debug.Log($"Amount of PineTree: {childCount}");
         }
         else
         {
            Debug.LogWarning("Parent object named 'PineTree' not found in the scene!");
         }
      }

      if (GUILayout.Button("Amount of RoundTree"))
      {
         GameObject RoundtreeParent = GameObject.Find("RoundTree");

         if (RoundtreeParent != null)
         {
            int childCount = RoundtreeParent.transform.childCount;
            Debug.Log($"Amount of RoundTree: {childCount}");
         }
         else
         {
            Debug.LogWarning("Parent object named 'RoundTree' not found in the scene!");
         }
      }

      if (GUILayout.Button("Amount of TreeStump"))
      {
         GameObject treeSlumpParent = GameObject.Find("TreeSlump");

         if (treeSlumpParent != null)
         {
            int childCount = treeSlumpParent.transform.childCount;
            Debug.Log($"Amount of TreeStump: {childCount}");
         }
         else
         {
            Debug.LogWarning("Parent object named 'TreeStump' not found in the scene!");
         }
      }
   }
}
