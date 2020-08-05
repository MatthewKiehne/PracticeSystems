using System.Collections.Generic;
using System;

[Serializable]
public class DataPacket {

    public Dictionary<string, object> Values;
    public Dictionary<string, DataPacket> DataPackets;

    public DataPacket() {
        this.Values = new Dictionary<string, object>();
        this.DataPackets = new Dictionary<string, DataPacket>();
    }
}