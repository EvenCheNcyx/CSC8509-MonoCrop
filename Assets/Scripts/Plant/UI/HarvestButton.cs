using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestButton : MonoBehaviour
{
    [Header("物品数据")] public ItemDataList_SO itemDataList_SO;

    [Header("背包数据")] public InventoryBag_SO playerBag;
    
    [Header("UI")]
    public GameObject PlantUI;
    public GameObject ToolUI;

    [Header("判断用Gameobject")]
    public GameObject isPlanted;
    public GameObject PlantDone;
    public GameObject PlantDead;
    public GameObject SoybeanisPlanted;
    public GameObject WheatisPlanted;
    public GameObject CornisPlanted;

    [Header("肥力，健康度")] 
    public GameObject PlantUIFertility;
    public GameObject ToolUIFertility;
    public GameObject PlantUIHealth;
    public GameObject ToolUIHealth;

    public GameObject Count;
    private float PlayerCoin;
    private float PlayerScore;
    
    private float Fertility, Health;

    public void Harvest()
    {
        PlayerCoin = Count.transform.position.x;
        PlayerScore = Count.transform.position.y;
        Fertility = PlantUIFertility.GetComponent<Slider>().value;
        Health = PlantUIHealth.GetComponent<Slider>().value;
        
        if (PlantDone.activeSelf)
        {
            int Amount = CalculateAmount(Fertility,Health);
            //soybean
            if (SoybeanisPlanted.activeSelf)
            {
                HarvestToBag(1006,Amount);
                PlayerCoin += (Amount * 4);
                PlayerScore += (Amount * 20);
                
                PlantUIFertility.GetComponent<Slider>().value = Fertility + 0.1f;
                ToolUIFertility.GetComponent<Slider>().value = Fertility + 0.1f;
                
                Count.transform.position = new Vector3(PlayerCoin,PlayerScore,0);
            }
            //Wheat
            else if (WheatisPlanted.activeSelf)
            {
                HarvestToBag(1007,Amount);
                
                PlayerCoin += (Amount * 4);
                PlayerScore += (Amount * 20);
                
                PlantUIHealth.GetComponent<Slider>().value = Health + 0.1f;
                ToolUIHealth.GetComponent<Slider>().value = Health + 0.1f;
                
                Count.transform.position = new Vector3(PlayerCoin,PlayerScore,0);
            }
            //Corn
            else if (CornisPlanted.activeSelf)
            {
                HarvestToBag(1008,Amount);

                PlayerCoin += (Amount * 6);
                PlayerScore += (Amount * 30);
                
                PlantUIFertility.GetComponent<Slider>().value = Fertility - 0.2f;
                ToolUIFertility.GetComponent<Slider>().value = Fertility - 0.2f;
                PlantUIHealth.GetComponent<Slider>().value = Health - 0.2f;
                ToolUIHealth.GetComponent<Slider>().value = Health - 0.2f;
                
                Count.transform.position = new Vector3(PlayerCoin,PlayerScore,0);
            }
            
        }
        else if(PlantDead.activeSelf)
        {
            //Soybean
            if (SoybeanisPlanted.activeSelf)
            {
                PlantUIFertility.GetComponent<Slider>().value = Fertility + 0.1f;
                ToolUIFertility.GetComponent<Slider>().value = Fertility + 0.1f;
                PlantDead.SetActive(false);
            }
            //Wheat
            else if (WheatisPlanted.activeSelf)
            {
                PlantUIHealth.GetComponent<Slider>().value = Health + 0.1f;
                ToolUIHealth.GetComponent<Slider>().value = Health + 0.1f;
                PlantDead.SetActive(false);
            }
            //Corn
            else if (CornisPlanted.activeSelf)
            {
                PlantUIFertility.GetComponent<Slider>().value = Fertility - 0.15f;
                ToolUIFertility.GetComponent<Slider>().value = Fertility - 0.15f;
                PlantUIHealth.GetComponent<Slider>().value = Health - 0.15f;
                ToolUIHealth.GetComponent<Slider>().value = Health - 0.15f;
                PlantDead.SetActive(false);
            }
        }
        
        PlantUI.SetActive(true);
        ToolUI.SetActive(false);
    }
    
    private int GetItemIndexInBag(int ID)
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

    /// <summary>
    /// 收获作物并更新背包UI
    /// </summary>
    /// <param name="ID">作物ID</param>
    /// <param name="Amount">收获数量</param>
    private void HarvestToBag(int ID,int Amount)
    {
        int index = GetItemIndexInBag(ID);
        int currentAmount = playerBag.itemList[index].itemAmount + Amount;
        var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
        playerBag.itemList[index] = item;
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,playerBag.itemList);
    }
    
    /// <summary>
    /// 根据肥力和健康度计算收货数量
    /// </summary>
    /// <param name="Fertility">肥力</param>
    /// <param name="Health">健康度</param>
    /// <returns>收获数int</returns>
    private int CalculateAmount(float fertility, float health)
    {
        int Amount = 36;
        if (fertility < 0.66 && fertility > 0.33)
        {
            Amount -= 6;
        }
        else if (fertility < 0.33)
            Amount -= 12;

        if (health < 0.66 && health > 0.33)
        {
            Amount -= 6;
        }
        else if (health < 0.33)
            Amount -= 12;

        Debug.Log("Production amount" + Amount);
        return Amount;
    }
    
}
