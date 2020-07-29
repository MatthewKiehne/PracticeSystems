using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DataPacket {

    public Dictionary<string, string> Values;
    public Dictionary<string, DataPacket> DataPackets;

    public DataPacket() {
        this.Values = new Dictionary<string, string>();
        this.DataPackets = new Dictionary<string, DataPacket>();
    }
}