using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack : Attribute {

    public int Amount;
    private Item item = null;

    public ItemStack(Entity entity, Item item, int quantity) : base(entity,null,true){
        this.Amount = quantity;
        this.item = item;
    }

    public ItemStack(Entity entity, Item item) : base(entity, null, true) {
        this.Amount = 1;
        this.item = item;
    }

    public ItemStack(Entity entity, DataPacket data) : base(entity, data, true) {

        List<string> keys = new List<string>(data.Values.Keys);

        this.Amount = int.Parse((string)data.Values["Amount"]);
        this.item = EntityFactory.Instanciate(data.DataPackets["Item"]).getAttribute<Item>();
    }

    protected override DataPacket saveVariables() {

        DataPacket data = new DataPacket();
        data.Values.Add("Amount", this.Amount + "");
        data.DataPackets.Add("Item", item.Entity.GetDataPacket());

        return data;
    }

    public override void Awake() {
        this.item = this.Entity.getAttribute<Item>();

        if (item == null) {
            throw new System.Exception("Attribute: Item was not found on " + this.entity.Name);
        }
    }

    public float getWeight() {
        return this.item.Weight * this.Amount;
    }

    public float getVolume() {
        return this.item.Volume * this.Amount;
    }

    public Item getItem() {
        return this.item;
    } 
}