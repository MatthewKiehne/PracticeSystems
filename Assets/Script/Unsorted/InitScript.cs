using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class InitScript : MonoBehaviour {

    [SerializeField]
    private GroundManager groundManager;

    public void Start() {

        GridMap map = new GridMap(100, 100,"tempGround");
        Entity player = this.makePlayer();
        player.addAttribute(new Visible(player, "player"));
        map.addEntity(0, 0, player);

        Position pos = new Position(player, new Vector2Int(3,9));
        string posJson = JsonConvert.SerializeObject(pos.GetDataPacket());
        Debug.Log(posJson);
        DataPacket posData = JsonConvert.DeserializeObject<DataPacket>(posJson);
        Position newCol = new Position(player, posData);
        Debug.Log(newCol.position);

        groundManager.makeMap(map.Width, map.Height, map.PathToTile);

        Entity ground = EntityFactory.Instanciate("Ground");
        Position testPos = new Position(ground, new Vector2Int(0, 0));
        testPos.position = new Vector2Int(1, 1);
        ground.addAttribute(testPos);
    }

    public Entity makePlayer() {
        //does some stuff with the player
        //really put here just to store

        Entity apple = new Entity("Apple");
        Item appleItem = new Item(apple, .01f, .01f);
        apple.addAttribute(appleItem);

        Entity appleStack = new Entity("Apple Stack");
        ItemStack stack = new ItemStack(appleStack, appleItem, 5);
        appleStack.addAttribute(stack);

        Entity player = new Entity("Player");
        Inventory inv = new Inventory(player, 100f, 50f);
        player.addAttribute(inv);

        inv.addItem(stack);

        return player;
    }

    public void saveEntity(Entity entity) {

        string entityJson = JsonConvert.SerializeObject(entity.GetDataPacket());
        string path = Application.dataPath + "/Saves/test.json";

        try {
            // Check if file already exists. If yes, delete it.     
            if (File.Exists(path)) {
                File.Delete(path);
            }

            // Create a new file     
            using (FileStream fs = File.Create(path)) {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes(entityJson);
                fs.Write(title, 0, title.Length);
            }

        } catch (Exception Ex) {
            Console.WriteLine(Ex.ToString());
        }
    }
}