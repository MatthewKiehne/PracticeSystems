using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeDataPacket {

    public Dictionary<string, object> Values;

    public AttributeDataPacket() {
        this.Values = new Dictionary<string, object>();
    }
}