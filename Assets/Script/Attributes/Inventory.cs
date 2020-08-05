using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Attribute {

    public List<ItemStack> Items;

    private float maxWeight;
    private float maxVolume;

    public Inventory(Entity entity, float maxWeight, float maxVolume) : base(entity, null, true) {
        this.maxVolume = maxVolume;
        this.maxWeight = maxWeight;
        this.Items = new List<ItemStack>();
    }

    public Inventory(Entity entity, DataPacket data) : base(entity, data, true) {

        this.Items = new List<ItemStack>();

        this.maxWeight = float.Parse((string)data.Values["MaxWeight"]);
        this.maxVolume = float.Parse((string)data.Values["MaxVolume"]);

        List<string> entityName = new List<string>(data.DataPackets.Keys);
        foreach (string name in entityName) {
            Entity ent = new Entity(name);
            ItemStack stack = new ItemStack(ent, data.DataPackets[name].DataPackets["ItemStack"]);
            ent.addAttribute(stack);

            this.addItem(stack);
        }
    }

    public ItemStack addItem(ItemStack stack) {
        //return a stack of items that can not fit into the inventory

        ItemStack result = stack;

        float usedVolume = this.usedVolume();
        float usedWeight = this.usedWeight();

        if (stack.getVolume() + usedVolume > this.maxVolume ||
            stack.getWeight() + usedWeight > this.maxWeight) {

            float availableVolume = this.maxWeight - usedVolume;
            float availableWeight = this.maxWeight - usedWeight;

            int minAmount = (int)(availableVolume / stack.getItem().Volume);

            if (minAmount > (int)(availableWeight / stack.getItem().Weight)) {
                minAmount = (int)(availableWeight / stack.getItem().Weight);
            }

            Entity itemEnt = EntityFactory.Instanciate(stack.getItem().Entity.Name);

            Entity stackEnt = new Entity(stack.Entity.Name);
            ItemStack s = new ItemStack(stackEnt, stackEnt.getAttribute<Item>(), minAmount);
            stackEnt.addAttribute(s);

            this.Items.Add(s);

            result.Amount -= minAmount;

        } else {
            this.Items.Add(stack);
            result = null;
        }

        return result;
    }

    public ItemStack addItem(Item item) {
        //adds the item to the inventory

        Entity ent = new Entity(item.Entity.Name + " Stack");
        ItemStack stack = new ItemStack(ent, item);
        ent.addAttribute(stack);

        return this.addItem(stack);
    }

    public float usedVolume() {
        float sum = 0;

        foreach (ItemStack stack in this.Items) {
            sum += stack.getVolume();
        }
        return sum;
    }
    public float usedWeight() {
        float sum = 0;

        foreach (ItemStack stack in this.Items) {
            sum += stack.getWeight();
        }
        return sum;
    }

    protected override DataPacket saveVariables() {
        DataPacket data = new DataPacket();

        data.Values.Add("MaxWeight", this.maxWeight + "");
        data.Values.Add("MaxVolume", this.maxVolume + "");

        foreach (ItemStack stack in this.Items) {
            data.DataPackets.Add(stack.Entity.Name, stack.Entity.GetDataPacket());
        }

        return data;
    }
}