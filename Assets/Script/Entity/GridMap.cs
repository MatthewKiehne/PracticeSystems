using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap {

    private Ground[,] ground;
    private string pathToTile;

    public GridMap(int width, int height, string tilePath) {
        this.ground = new Ground[width, height];
        this.pathToTile = tilePath;
    }

    #region gridManipulation
    public void addEntity(int x, int y, Entity entity) {
        //adds an entity to the ground tile

        if (this.ground[x, y] == null) {
            Entity groundEnt = EntityFactory.Instanciate("Ground");
            this.ground[x, y] = groundEnt.getAttribute<Ground>();
        }

        this.ground[x, y].onGround.Add(entity);
    }
    public void addEntity(Vector2Int position, Entity entity) {
        this.addEntity(position.x, position.y, entity);
    }

    public bool removeEntity(int x, int y, Entity entity) {
        //removes the entity from the ground tile
        bool result = false;

        if (this.ground[x, y] != null) {
            result = this.ground[x, y].onGround.Remove(entity);
        }

        return result;
    }
    public bool removeEntity(Vector2Int position, Entity entity) {
        return this.removeEntity(position.x, position.y, entity);
    }

    public Entity[] atPosition(int x, int y) {
        //returns all the entities at the position on the ground

        Entity[] result = new Entity[0];

        if (this.ground[x, y] != null) {

            result = this.ground[x, y].onGround.ToArray();

        }
        return result;
    }

    public Entity[] atPosition(Vector2Int pos) {
        return this.atPosition(pos.x, pos.y);
    }

    #endregion gridManipulation

    public string PathToTile { get { return this.pathToTile; } }
    public int Width { get { return this.ground.GetLength(0); } }
    public int Height { get { return this.ground.GetLength(1); } }
}