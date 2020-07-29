using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityFactory {

    private static Dictionary<string, Entity> Entities;

    private static void InitDictionaries() {

        if (Entities == null) {
            Entities = new Dictionary<string, Entity>();
        }
    }

    public static Entity Instanciate(DataPacket data) {
        //returns an Entity based on the data passed in
        //used primarily to load Entities from a save file 

        Debug.Log("Inst(data)");

        Entity result = null;
        InitDictionaries();

        string name = data.Values["Name"];

        if (Entities.ContainsKey(name)) {

            Debug.Log("Inst(data) -> Found Key");

            Entity ent = Entities[name];

            if (isEntityFlyWeight(ent)) {

                Debug.Log("Inst(data) -> is flyweight");

                result = ent;

            } else {
                //copy all the attributes that are flyweight 
                //and make new attributes that are not based on the json

                Debug.Log("Inst(data) -> not flyweight");

                result = new Entity(ent.Name);

                for (int i = 0; i < ent.AttributeCount; i++) {

                    Attribute att = ent[i];
                    
                    if (att.Duplicate) {

                        DataPacket attributeData = data.DataPackets[att.GetType().ToString()];

                        att = (Attribute)Activator.CreateInstance(
                            att.GetType(),
                            new object[] { result, attributeData });
                    }

                    Debug.Log("Inst(data) -> copy " +  att.GetType().ToString() + 
                        " " + (att.GetHashCode() == ent[i].GetHashCode()));

                    result.addAttribute(att);
                }

                result.Awake();
            }

        } else {
            //load the blueprint into memory
            //then pass the data in again

            Debug.Log("Inst(data) -> miss key");

            Instanciate(name);
            result = Instanciate(data);
        }

        return result;
    }

    public static Entity Instanciate(string name) {
        //returns an new Entity based on the name
        //if it does not exist, it reads the json blueprint for the Entity

        Debug.Log("Inst(name)");

        Entity result = null;
        InitDictionaries();

        if (Entities.ContainsKey(name)) {

            Debug.Log("Inst(name) -> found key");

            Entity ent = Entities[name];

            if (isEntityFlyWeight(ent)) {

                Debug.Log("Inst(name) -> is flyweight");

                result = ent;

            } else {
                //copy all the attributes that are flyweight 
                //and make new attributes that are not based on the json

                Debug.Log("Inst(name) -> not flyweight");

                result = new Entity(ent.Name);

                for(int i = 0; i < ent.AttributeCount; i++) {

                    Attribute att = ent[i];
                    if (att.Duplicate) {

                        att = (Attribute)Activator.CreateInstance(
                            att.GetType(),
                            new object[] { result, att.GetDataPacket() });
                    }

                    Debug.Log("Inst(data) -> copy " + att.GetType().ToString() +
                        " " + (att.GetHashCode() == ent[i].GetHashCode()));

                    result.addAttribute(att);
                }

                result.Awake();
            }

        } else {
            //make make the entity from json
            //save it to the directory
            //call the Instacne funciton (this function) again and set it to result

            Debug.Log("Inst(data) -> load blueprint");

            TextAsset entityRecipe = Resources.Load<TextAsset>(name);
            DataPacket entityData = JsonConvert.DeserializeObject<DataPacket>(entityRecipe.text);

            Entity entity = new Entity((string)entityData.Values["Name"]);
            List<string> keys = new List<string>(entityData.DataPackets.Keys);

            foreach(string key in keys) {

                DataPacket attributeData = entityData.DataPackets[key];
                
                Attribute att = (Attribute)Activator.CreateInstance(
                            Type.GetType(key),
                            new object[] { entity, attributeData });

                entity.addAttribute(att);
            }

            entity.Awake();
            Entities.Add(name, entity);
            result = EntityFactory.Instanciate(name);
        }

        return result;
    }

    private static bool isEntityFlyWeight(Entity entity) {
        //returns if all the components are flyweight 
        //and thus the entire Entity can just be coppied

        bool result = true;
        int counter = 0;

        while(counter < entity.AttributeCount && result) {

            if (!entity[counter].Duplicate) {
                result = false;
            }

            counter++;
        }

        return result;
    }
}