using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : Attribute {

    [Flags]
    public enum CollisionState {
        None = 0b_0000_0000,  // 0
        High = 0b_0000_0001,  // 1
        Low  = 0b_0000_0010,  // 2
    }
    private CollisionState state = CollisionState.Low;

    public Collision(Entity entity, DataPacket data) : base(entity, data, false) {
        this.state = (CollisionState)Convert.ToInt32(data.Values["State"]);
        Debug.Log(this.state);
    }

    public Collision(Entity entity, CollisionState state) : base(entity,null,false){
        this.state = state;
    }

    protected override DataPacket saveVariables() {
        DataPacket data = new DataPacket();
        data.Values.Add("State", this.state);
        return data;
    }

    public CollisionState State { get { return this.state; } }
}