using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack : Attribute {

    private int maxStack;
    private int currentAmount;

    private Item item = null;

    public ItemStack(Entity entity, DataPacket data) : base(entity, data, true) {

        this.maxStack = int.Parse(data.Values["MaxStack"]);
        this.currentAmount = int.Parse(data.Values["CurrentAmount"]);
    }

    protected override DataPacket saveVariables() {

        DataPacket data = new DataPacket();
        data.Values.Add("MaxStack", this.maxStack + "");
        data.Values.Add("CurrentAmount", this.currentAmount + "");

        return data;
    }

    public override void Awake() {
        this.item = this.Entity.getAttribute<Item>();

        if (item == null) {
            throw new System.Exception("Attribute: Item was not found on " + this.entity.Name);
        }
    }

    public float getWeight() {
        return this.item.Weight * this.currentAmount;
    }

    public float getVolume() {
        return this.item.Volume * this.currentAmount;
    }

    public Item getItem() {
        return this.item;
    }

    
}