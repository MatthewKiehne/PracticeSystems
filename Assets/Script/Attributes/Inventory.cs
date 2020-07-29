using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Attribute {

    public List<Item> Items;

    private float maxWeight;
    private float maxVolume;

    public Inventory(Entity entity, DataPacket data) : base(entity, data, true) {

        this.Items = new List<Item>();

        this.maxWeight = float.Parse(data.Values["MaxWeight"]);
        this.maxVolume = float.Parse(data.Values["MaxVolume"]);

        List<string> itemNames = new List<string>(data.DataPackets.Keys);
        foreach( string name in itemNames) {
            Entity ent = EntityFactory.Instanciate(data.DataPackets[name]);
            Item item = ent.getAttribute<Item>();
            this.Items.Add(item);
        }
    }

    protected override DataPacket saveVariables() {
        DataPacket data = new DataPacket();

        data.Values.Add("MaxWeight", this.maxWeight + "");
        data.Values.Add("MaxVolume", this.maxVolume + "");

        foreach (Item item in this.Items) {
            data.DataPackets.Add(item.Entity.Name, item.Entity.GetDataPacket());
        }

        return data;
    }
}