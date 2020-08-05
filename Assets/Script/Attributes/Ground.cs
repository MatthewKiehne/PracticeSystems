using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Attribute {
    //the ground tile for each of the spaces on the GroundMap

    public List<Entity> onGround = new List<Entity>();

    public Ground(Entity ent) : base(ent, null, true) {

    }

    public Ground(Entity ent, DataPacket packet) : base(ent, packet, true) {

    }

    protected override DataPacket saveVariables() {

        DataPacket data = new DataPacket();
        foreach (Entity tempEnt in onGround) {
            data.DataPackets.Add(tempEnt.Name, tempEnt.GetDataPacket());
        }
        return data;
    }
}