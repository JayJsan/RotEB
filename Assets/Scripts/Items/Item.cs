using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "RotEB/Item", order = 0)]
public class Item : ScriptableObject {
    public Item(string itemName, string itemDescription, float itemSpeed, float itemPower, float itemAccuracy) {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemSpeed = itemSpeed;
        this.itemPower = itemPower;
        this.itemAccuracy = itemAccuracy;
    }

    public Item(float itemSpeed, float itemPower, float itemAccuracy) {
        this.itemSpeed = itemSpeed;
        this.itemPower = itemPower;
        this.itemAccuracy = itemAccuracy;
    }

    public string itemName;
    public string itemDescription;
    public float itemSpeed;
    public float itemPower;
    public float itemAccuracy;
}
