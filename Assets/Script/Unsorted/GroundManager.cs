using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundManager : MonoBehaviour {

    [SerializeField]
    Tilemap groundMap;

    public void makeMap(int width, int height, string pathToTile) {

        Tile tile = Resources.Load<Tile>(pathToTile);

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                groundMap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}