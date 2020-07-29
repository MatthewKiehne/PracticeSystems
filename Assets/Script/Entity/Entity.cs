using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity {

    List<Attribute> attributes = new List<Attribute>();

    private string name;

    public Entity(string name) {
        this.name = name;
    }

    public DataPacket GetDataPacket() {

        DataPacket data = new DataPacket();

        data.Values.Add("Name", this.name);
        
        foreach(Attribute at in this.attributes) {
            data.DataPackets.Add(at.GetType().ToString(), at.GetDataPacket());
        }

        return data;
    }

    public void Awake() {
        //gets called once all the attributes are added to this Entity

        foreach(Attribute at in this.attributes) {
            at.Awake();
        }
    }

    private DataPacket[] getAttributeData() {

        DataPacket[] result = new DataPacket[this.attributes.Count];

        for(int i = 0; i < this.attributes.Count; i++) {
            result[i] = this.attributes[i].GetDataPacket();
        }

        return result;
    }

    public T getAttribute<T>() where T : Attribute{

        T result = null;
        int counter = 0;

        while(result == null && counter < this.attributes.Count) {

            if(this.attributes[counter].GetType() == typeof(T)) {
                result = (T)this.attributes[counter];
            }

            counter++;
        }

        return result;
    }

    public T addAttribute<T>(T attribute) where T : Attribute {

        T result = this.getAttribute<T>();

        if(result == null) {
            this.attributes.Add(attribute);
            result = attribute;
        }

        return result;
    }

    public string Name {
        get {
            return this.name;
        }
    }

    public int AttributeCount {
        get {
            return this.attributes.Count;
        }
    }

    public Attribute this[int index] {
        get => this.attributes[index];
    }   
}