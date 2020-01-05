using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public enum TileType { Empty, Floor };

    TileType type = TileType.Empty;

    LooseObject looseObject;
    InstalledObject installedObject;

    World world;

    int x;
    int y;

    public Tile(World world, int x, int y)
    {
        Assert.ASSERT_INTS_LESS_THEN_ZERO(x, y);
        Assert.ASSERT_REF_IS_NULL(world);

        this.world = world;
        this.x = x;
        this.y = y;
    }
}
