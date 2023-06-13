using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // RESERACH - 13/06/23
    // MIGRATE BALL CONTROL INTO THIS
    private enum MouseButton {
        Left,
        Right,
    }
    private enum Action {
        Idle,
        ActivateAbility,
        // ControlBall or something
    }
    // Start is called before the first frame update
    private KeyCode abilityKey = KeyCode.E;
    private MouseButton abilityButton = MouseButton.Right;
    void Awake() {

    }

    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        DetectUserInput();
    }
            
    private void DetectUserInput() {
        switch (DetectKeyboardInput()) {
            default:
                // do nothing
                break;

            case Action.Idle:
                // do nothing
                break;
            
            case Action.ActivateAbility:
                // activate ability
                Debug.Log("Keyboard Key Ability Pressed!");
                PlayerItemManager.Instance.ActivateItemAbility();
                break;

        }

        switch (DetectMouseInput()) {
            default:
                // do nothing
                break;

            case Action.Idle:
                // do nothing
                break;
            
            case Action.ActivateAbility:
                // activate ability
                Debug.Log("Mouse Key Ability Pressed!");
                PlayerItemManager.Instance.ActivateItemAbility();
                break;
        }
    }

    private Action DetectKeyboardInput() {
        if (Input.GetKeyDown(abilityKey)) {
            return Action.ActivateAbility;
        }
        return Action.Idle;
    }
    
    private Action DetectMouseInput() {
        if (Input.GetMouseButtonDown((int)abilityButton)) {
            return Action.ActivateAbility;
        }
        return Action.Idle;
    }
}
