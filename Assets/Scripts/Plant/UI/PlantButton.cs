using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantButton : MonoBehaviour
{
    [Header("物品数据")] public ItemDataList_SO itemDataList_SO;

    [Header("背包数据")] public InventoryBag_SO playerBag;

    [Header("UI")]
    public GameObject PlantUI;
    public GameObject ToolUI;
    

    [Header("按钮物品ID")] public int ID;
    
    [Header("种子按钮")] 
    public GameObject PlantSoybean;
    public GameObject PlantWheat;
    public GameObject PlantCorn;
    
    public GameObject Count;
    private float PlayerCoin;
    private float PlayerScore;
    
    public void UseItem()
    {
        //TODO 交易系统
        //无交易系统，故不消耗物品，直接消耗金币
        /*
        int index = GetItemIndexInBag(ID);
        if (playerBag.itemList[index].itemAmount > 0)
        {
            int currentAmount = playerBag.itemList[index].itemAmount - 1;
            var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
            playerBag.itemList[index] = item;
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,playerBag.itemList);
        }
        else
        {
            var item = new InventoryItem { itemID = ID, itemAmount = 0 };
            playerBag.itemList[index] = item;
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,playerBag.itemList);
        }
        */
        
        //改变UI
        PlantUI.SetActive(false);
        ToolUI.SetActive(true);
        ToolUI.transform.position = new Vector3(1060, 740, 0);
    }

    public void SeedCost()
    {
        PlayerCoin = Count.transform.position.x;
        PlayerScore = Count.transform.position.y;
        if (ID == 1001 || ID == 1002)
            PlayerCoin -= 15;
        else if (ID == 1003)
            PlayerCoin -= 25;
        
        Count.transform.position = new Vector3(PlayerCoin,PlayerScore,0);
        Debug.Log("Coin"+PlayerCoin+"Score"+PlayerScore);
    }
}
