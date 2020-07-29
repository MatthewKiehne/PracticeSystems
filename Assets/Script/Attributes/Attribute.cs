using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Attribute {

    protected Entity entity;
    private bool duplicate;

    public Attribute(Entity entity, DataPacket data, bool duplicate) {
        this.entity = entity;
        this.duplicate = duplicate;
    }

    public DataPacket GetDataPacket() {
        //gets the datapacket to save to json or to make a new 
        
        DataPacket data = this.saveVariables();
        //data.Values.Add("Attribute_Type", this.GetType().ToString());
        return data;
    }

    protected abstract DataPacket saveVariables();

    public virtual void update(float time) {
        //used for anytime the attribute does anything over time
    }

    public virtual void Awake() {
        //Is called once after all the Attributes are added to the Entity
    }

    public Entity Entity {
        get {
            return this.entity;
        }
    }

    public bool Duplicate {
        get {
            return this.duplicate;
        }
    }
}