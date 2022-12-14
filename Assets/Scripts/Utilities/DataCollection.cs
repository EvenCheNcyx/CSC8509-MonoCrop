using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    public int itemID;

    public string itemName;

    public ItemType itemType;

    public Sprite itemIcon;
    public Sprite ItemOnWorldSprite;

    public string itemDescription;

    public int itemUseRadius;

    public bool canPickedup;
    public bool canDropped;
    public bool canCarried;

    public int itemPrice;
    [Range(0, 1)] public float sellPercentage;
}

[System.Serializable]
public struct InventoryItem
{
    public int itemID;
    public int itemAmount;
    
}

[System.Serializable]
public class SerializableVector3
{
    public float x,y,z;

    public SerializableVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z; 
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public Vector2Int ToVector2Int()
    {
        return new Vector2Int((int)x,(int) y);
    }
}


[System.Serializable]
public class SceneItem
{
    public int itemID;
    public SerializableVector3 position;
}

[System.Serializable]
public class TileProperty
{
    public Vector2Int tileCoordinate;

    public GridType gridType;

    public bool boolTypeValue;
}