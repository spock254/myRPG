using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    Tile[,] tiles;
    int wight;
    int height;

    public World(int wight = 100, int height = 100)
    {
        Assert.ASSERT_INTS_LESS_THEN_ZERO(wight, height);

        this.wight = wight;
        this.height = height;

        tiles = new Tile[wight, height];

        for (int i = 0; i < wight; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i, j] = new Tile(this, i, j);
            }
        }

        Debug.Log(wight + " " + height);
    }

    public Tile GetTileAt(int x, int y) 
    {
        Assert.ASSERT_INTS_LESS_THEN_ZERO(x, y);

        return tiles[x, y];
    }
}
