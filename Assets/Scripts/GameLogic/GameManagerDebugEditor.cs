using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerDebugEditor : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Clear Level and Goto Shop")) {
            EnemyManager.Instance.ClearAllEnemies();
            gameManager.UpdateGameState(StateType.SHOP_MENU);
        }

        GUILayout.EndHorizontal();
    }
}