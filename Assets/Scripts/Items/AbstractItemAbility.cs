using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Taken and modified from https://youtu.be/ry4I6QyPw4E
public abstract class AbstractItemAbility : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float activeTime;

    public abstract void Activate(GameObject parent);
}
