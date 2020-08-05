using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : Attribute {

    private string pathString = null;

    public Visible(Entity entity, DataPacket packet) : base(entity, packet, false) {
        this.pathString = (string)packet.Values["Path"];
    }

    public Visible(Entity entity, string pathToSprite) : base(entity, null, false) {
        this.pathString = pathToSprite;
    }

    protected override DataPacket saveVariables() {
        DataPacket data = new DataPacket();
        data.Values.Add("Path", this.pathString);
        return data;
    }

    public string SpritePath {
        get {
            return this.pathString;
        } 
    }
}