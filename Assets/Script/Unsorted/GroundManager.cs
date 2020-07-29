using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundManager : MonoBehaviour {

    [SerializeField]
    Tilemap groundMap;

    [SerializeField]
    Tile groundTile;

    [SerializeField]
    private int width = 128;

    [SerializeField]
    private int height = 128;

    private Ground[,] ground;

    private void Start() {

        this.ground = new Ground[this.width,this.height];

        for(int x = 0; x < this.width; x++) {
            for(int y = 0; y < this.height; y++) {

                this.ground[x, y] = new Ground();
                groundMap.SetTile(new Vector3Int(x, y, 0), groundTile);
            }
        }
    }
}