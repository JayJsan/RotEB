using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerItemManager))]
public class ItemDebugEditor : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerItemManager itemManager = (PlayerItemManager)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Random Equipped Item")) {
            itemManager.GenerateRandomEquippedItem();
        }

        if (GUILayout.Button("Equip Dash Item")) {

        }

        if (GUILayout.Button("Remove All Passive Items")) {
            itemManager.UnequipAllPassiveItems();
        }

        if (GUILayout.Button("Remove Active Item")) {
            itemManager.UnequipCurrentActiveItem();
        }

        GUILayout.EndHorizontal();
    }
}