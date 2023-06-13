using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "RotEB/Item", order = 0)]
public class Item : ScriptableObject {
    public enum Type {
        Passive,
        Active,
    }

    public string itemName;
    public string itemDescription;
    public float itemSpeed;
    public float itemPower;
    public float itemAccuracy;
    public AbstractItemAbility itemAbility;
    public bool hasAbility;
    public Type itemType;

    public Item(string itemName, string itemDescription, float itemSpeed, float itemPower, float itemAccuracy, Type itemType) {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemSpeed = itemSpeed;
        this.itemPower = itemPower;
        this.itemAccuracy = itemAccuracy;
        this.itemType = itemType;
        this.hasAbility = false;
    }

    public Item(float itemSpeed, float itemPower, float itemAccuracy, Type itemType) {
        this.itemSpeed = itemSpeed;
        this.itemPower = itemPower;
        this.itemAccuracy = itemAccuracy;
        this.itemType = itemType;
        this.hasAbility = false;
    }

    public Item(string itemName, string itemDescription, float itemSpeed, float itemPower, float itemAccuracy, Type itemType, AbstractItemAbility itemAbility) {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemSpeed = itemSpeed;
        this.itemPower = itemPower;
        this.itemAccuracy = itemAccuracy;
        this.itemType = itemType;
        this.itemAbility = itemAbility;
        this.hasAbility = true;
    }
}
