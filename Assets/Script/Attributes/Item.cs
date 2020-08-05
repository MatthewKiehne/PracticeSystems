using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : Attribute {

    private float weight;
    private float volume;

    public Item(Entity entity, float weight, float volume) : base(entity, null, false) {
        this.weight = weight;
        this.volume = volume;
    }

    public Item(Entity entity, DataPacket data) : base(entity, data,false) {

        //Debug.Log(data.Values["Weight"].GetType());
        this.weight = float.Parse((string)data.Values["Weight"]);
        this.volume = float.Parse((string)data.Values["Volume"]);
    }


    protected override DataPacket saveVariables() {


        DataPacket data = new DataPacket();
        data.Values.Add("Weight", this.weight + "");
        data.Values.Add("Volume", this.volume + "");

        return data;
    }

    public float Weight {
        get { return this.weight; }
    }

    public float Volume {
        get { return this.volume; }
    }
}