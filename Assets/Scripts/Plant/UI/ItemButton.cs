using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [Header("物品数据")] public ItemDataList_SO itemDataList_SO;

    [Header("背包数据")] public InventoryBag_SO playerBag;
    
    [Header("按钮物品ID")] public int ID;
    
    [Header("按钮")]
    public GameObject Fertilizer;
    public GameObject Insecticide;

    [Header("肥力，健康度")] 
    public GameObject PlantUIFertility;
    public GameObject ToolUIFertility;
    public GameObject PlantUIHealth;
    public GameObject ToolUIHealth;

    public GameObject Count;
    private float PlayerCoin;
    private float PlayerScore;

    public void UseItem()
    {
        PlayerCoin = Count.transform.position.x;
        PlayerScore = Count.transform.position.y;
        
        int index = GetItemIndexInBag(ID);
        if (playerBag.itemList[index].itemAmount > 0)
        {
            //TODO 交易系统
            //无交易系统，故不消耗物品，直接消耗金币
            /*
            int currentAmount = playerBag.itemList[index].itemAmount - 1;
            var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
            playerBag.itemList[index] = item;
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,playerBag.itemList);
            */
            //Fertility+0.1
            if (ID == 1004)
            {
                PlayerCoin -= 50;
                float currentFertility = PlantUIFertility.GetComponent<Slider>().value;
                if (currentFertility < 0.90f)
                {
                    PlantUIFertility.GetComponent<Slider>().value = currentFertility + 0.1f;
                    ToolUIFertility.GetComponent<Slider>().value = currentFertility + 0.1f;
                }
                else if (currentFertility >= 0.90f)
                {
                    PlantUIFertility.GetComponent<Slider>().value = 1.0f;
                    ToolUIFertility.GetComponent<Slider>().value = 1.0f;
                }
            }
            //Health+0.1
            else if (ID == 1005)
            {
                PlayerCoin -= 50;
                float currentHealth = PlantUIHealth.GetComponent<Slider>().value;
                if (currentHealth < 0.90f)
                {
                    PlantUIHealth.GetComponent<Slider>().value = currentHealth + 0.1f;
                    ToolUIHealth.GetComponent<Slider>().value = currentHealth + 0.1f;
                }
                else if (currentHealth >= 0.90f)
                {
                    PlantUIHealth.GetComponent<Slider>().value = 1.0f;
                    ToolUIHealth.GetComponent<Slider>().value = 1.0f;
                }
                //Debug.Log("Health"+currentHealth);
            }
            
            Count.transform.position = new Vector3(PlayerCoin,PlayerScore,0);
            Debug.Log("Coin"+PlayerCoin+"Score"+PlayerScore);
        }
        
        
    }
    
    
    private int GetItemIndexInBag(int id)
    {
        for (int i = 0; i < playerBag.itemList.Count; i++)
        {
            if (playerBag.itemList[i].itemID == ID)
            {
                return i;
            }
        }
        return -1;
    }
}
