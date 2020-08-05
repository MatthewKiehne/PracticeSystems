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
        //used primarily to load Entities from a save file ;

        Entity result = null;
        InitDictionaries();

        Entity ent = null;

        string name = (string)data.Values["Name"];

        if (Entities.ContainsKey(name)) {

            ent = Entities[name];

        } else {
            //load the blueprint into memory
            //then pass the data in again

            ent = Instanciate(name);
        }

        if (isEntityFlyWeight(ent)) {

            result = ent;

        } else {
            //copy all the attributes that are flyweight 
            //and make new attributes that are not based on the json

            result = new Entity(ent.Name);

            for (int i = 0; i < ent.AttributeCount; i++) {

                Attribute att = ent[i];

                if (att.NewInstance) {

                    DataPacket attributeData = data.DataPackets[att.GetType().ToString()];

                    att = (Attribute)Activator.CreateInstance(
                        att.GetType(),
                        new object[] { result, attributeData });
                }

                result.addAttribute(att);
            }

            result.Awake();
        }



        return result;
    }

    public static Entity Instanciate(string name) {
        //returns an new Entity based on the name
        //if it does not exist, it reads the json blueprint for the Entity

        Entity result = null;
        InitDictionaries();

        if (Entities.ContainsKey(name)) {

            Entity ent = Entities[name];

            if (isEntityFlyWeight(ent)) {

                result = ent;

            } else {
                //copy all the attributes that are flyweight 
                //and make new attributes that are not based on the json

                result = new Entity(ent.Name);

                for (int i = 0; i < ent.AttributeCount; i++) {

                    Attribute att = ent[i];
                    if (att.NewInstance) {

                        att = (Attribute)Activator.CreateInstance(
                            att.GetType(),
                            new object[] { result, att.GetDataPacket() });
                    }

                    result.addAttribute(att);
                }

                result.Awake();
            }

        } else {
            //make make the entity from json
            //save it to the directory
            //call the Instacne funciton (this function) again and set it to result

            TextAsset entityRecipe = Resources.Load<TextAsset>(name);
            DataPacket entityData = JsonConvert.DeserializeObject<DataPacket>(entityRecipe.text);

            Entity entity = new Entity((string)entityData.Values["Name"]);
            List<string> keys = new List<string>(entityData.DataPackets.Keys);

            foreach (string key in keys) {

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

        while (counter < entity.AttributeCount && result) {

            if (entity[counter].NewInstance) {
                result = false;
            }

            counter++;
        }

        return result;
    }
}