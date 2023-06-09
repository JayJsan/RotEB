using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerItemManager))]
public class ItemDebugEditor : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerItemManager itemManager = (PlayerItemManager)target;

        if (GUILayout.Button("Generate Random Equipped Item")) {
            itemManager.GenerateRandomEquippedItem();
        }
    }
}