using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global 
{
    public const string PLAYER_ITEM_TO_SAVE_FILE = "/player_inv.save";
    public static string PLAYER_ITEM_TO_SAVE_PATH = string.Concat(Application.persistentDataPath, PLAYER_ITEM_TO_SAVE_FILE);

    /* Inventory system */
    public const int SLOTS_COUNT = 34;
    public const int EQUIP_SLOTS_COUNT = 5;
    public const int DEFAULT_ID = -1;
}
