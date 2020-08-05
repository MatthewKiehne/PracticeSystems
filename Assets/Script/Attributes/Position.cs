using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : Attribute {

    public Vector2Int position;

    public Position(Entity entity, Vector2Int position) : base(entity, null, true) {
        this.position = position;
    }

    public Position(Entity entity, DataPacket packet) : base(entity, null, true) {
        this.position = new Vector2Int(
        Convert.ToInt32(packet.Values["X"]),
        Convert.ToInt32(packet.Values["Y"]));

    }

    protected override DataPacket saveVariables() {
        DataPacket data = new DataPacket();
        data.Values.Add("X", this.position.x);
        data.Values.Add("Y", this.position.y);
        return data;
    }
}