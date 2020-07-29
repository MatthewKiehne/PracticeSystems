using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScript : MonoBehaviour {

    [SerializeField]
    private TextAsset text;

    public void Start() {

        Entity apple = new Entity("apple");

        DataPacket appleItemData = new DataPacket();
        appleItemData.Values.Add("Weight", .1f + "");
        appleItemData.Values.Add("Volume", 1f + "");
        Item appleItem = new Item(apple, appleItemData);
        apple.addAttribute(appleItem);

        DataPacket appleStackData = new DataPacket();
        appleStackData.Values.Add("MaxStack",  "" + 999);
        appleStackData.Values.Add("CurrentAmount", 5 + "");
        ItemStack appleItemStack = new ItemStack(apple, appleStackData);
        apple.addAttribute(appleItemStack);

        Entity player = new Entity("Player");
        DataPacket invData = new DataPacket();
        invData.Values.Add("MaxWeight", 100 + "");
        invData.Values.Add("MaxVolume", 100 + "");
        Inventory inv = new Inventory(player, invData);
        

        apple.Awake();

        Debug.Log(appleItemStack.getWeight());

        Entity apple2 = EntityFactory.Instanciate(apple.GetDataPacket());



        //Entity person = new Entity("person");
        //Inventory inv = new Inventory(person, 100, 100);
        //Obstacle obs = new Obstacle(person);

        //Debug.Log("result: " + EntityFactory.Instanciate("apple"));

    }
}