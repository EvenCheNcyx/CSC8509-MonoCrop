using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{
    [Header("物品数据")]
    public ItemDataList_SO itemDataList_SO;

    [Header("背包数据")] 
    public InventoryBag_SO playerBag;

    [Header("UI")]
    public GameObject PlantUI;
    public GameObject ToolUI;
    public GameObject GameEnd;

    [Header("作物表现")] 
    public GameObject Soybean_Begin;
    public GameObject Soybean_Done;
    public GameObject Soybean_Dead;
    public GameObject Soybean_Grow;
    public GameObject Soybean_Winter;
    
    public GameObject Wheat_Begin;
    public GameObject Wheat_Done;
    public GameObject Wheat_Dead;
    public GameObject Wheat_Grow;
    public GameObject Wheat_Winter;
    
    public GameObject Corn_Begin;
    public GameObject Corn_Done;
    public GameObject Corn_Dead;
    public GameObject Corn_Grow;
    public GameObject Corn_Winter;
    
    [Header("按钮")]
    public GameObject PlantSoybean;
    public GameObject PlantWheat;
    public GameObject PlantCorn;
    public GameObject Fertilizer;
    public GameObject Insecticide;
    public GameObject Harvest;
    
    [Header("肥力，健康度")] 
    public GameObject Fertility;
    public GameObject Health;

    [Header("种植判定")] 
    public GameObject isPlanted;
    public GameObject SoybeanisPlanted;
    public GameObject WheatisPlanted;
    public GameObject CornisPlanted;
    public GameObject PlantDone;
    public GameObject PlantDead;

    [Header("金币得分")]
    public GameObject Count;
    public GameObject isSubmit;
    
    private int GrowTime;
    private Season currentSeason;
    private int currentMonth;
    private int currentYear;
    
    private float PlayerCoin;
    private float PlayerScore;

    //isPlanted == true,ToolUI setactive,isPlanted == false,PlantUI setactive

    //plantui-0，则检测是否拥有背包种子激活对应按钮
    
    //点击按钮，背包内对应种子数量-1，begin setactive，isPlanted set true
    
    //TODO 计算种植周期和初始化
    
    //TODO toolUI，根据点选按钮更改肥力和健康值 

    private void OnEnable()
    {
        EventHandler.GameDateEvent += OnGameDateEvent;
    }
    
    private void OnDisable()
    {
        EventHandler.GameDateEvent -= OnGameDateEvent;
    }

    private void OnGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        currentSeason = season;
        currentYear = year;
        currentMonth = month;
        if (isPlanted.activeSelf == true)
        {
            if(currentSeason != Season.Winter)
                GrowTime--;
        }
        
        Debug.Log(currentSeason+"季节"+GrowTime+"成熟倒计时");
    }

    private void Update()
    {
        PlayerCoin = Count.transform.position.x;
        PlayerScore = Count.transform.position.y;
        //更新UI按钮可用性
        if (PlantUI.activeSelf || ToolUI.activeSelf)
        {
            for (int i = 1001; i < 1006; i++)
            {
                ButtonTest(i);
            }
        }
        
        //作物完成、死亡状态更新
        if(isPlanted.activeSelf)
            UpdatePlantState();
        
        //计算作物成熟时间并更新Object
        
        if (isPlanted.activeSelf)
        {
            PlantGrow();
        }
        else if (isPlanted.activeSelf == false)
        {
            CalculateGrowTime();
        }

        //每年12月重置任务
        if (currentMonth == 12)
        {
            isSubmit.SetActive(false);
        }
        //每年春完成任务
        if(isSubmit.activeSelf == false)
            QuestInYear();
        
        //Count.transform.position = new Vector3(PlayerCoin,PlayerScore,0);
        //Debug.Log("Coin"+PlayerCoin+"Score"+PlayerScore);
    }

    private void QuestInYear()
    {
        int n = currentYear - 2022;
        
        //每年3月交40n金币，获得80n得分
        if (currentMonth == 3 && PlayerCoin > 40 * n)
        {
            PlayerCoin -= 40 * n;
            PlayerScore += 80 * n;
            Count.transform.position = new Vector3(PlayerCoin,PlayerScore,0);
            Debug.Log("Mission Finished");
            Debug.Log("Coin"+PlayerCoin+"Score"+PlayerScore);
            isSubmit.SetActive(true);
        }
        //游戏结束
        else if(currentMonth == 3 && PlayerCoin < 40 * n)
        {
            Time.timeScale = 0;
            GameEnd.SetActive(true);
        }
    }
    
    private void UpdatePlantState()
    {
        if (GrowTime == 0)
        {
            PlantDone.SetActive(true);
        }
        else if (GrowTime < -2)
        {
            PlantDone.SetActive(false);
            PlantDead.SetActive(true);
        }
    }

    //设置生长时间
    private void CalculateGrowTime()
    {
        if (SoybeanisPlanted.activeSelf)
        {
            GrowTime = 3;
            isPlanted.SetActive(true);
        }
        else if (WheatisPlanted.activeSelf)
        {
            GrowTime = 3;
            isPlanted.SetActive(true);
        }
        else if (CornisPlanted.activeSelf)
        {
            GrowTime = 6;
            isPlanted.SetActive(true);
        }
    }

    //作物生长
    private void PlantGrow()
    {
        if (SoybeanisPlanted.activeSelf)
        {
            SoybeanPlant();
        }
        else if (WheatisPlanted.activeSelf)
        {
            WheatPlant();
        }
        else if (CornisPlanted.activeSelf)
        {
            CornPlant();
        }
    }
    
    private void SoybeanPlant()
    {
        if (currentSeason == Season.Winter && GrowTime > 0)
        {
            Soybean_Begin.SetActive(false);
            Soybean_Grow.SetActive(false);
            Soybean_Winter.SetActive(true);
        }
        else if (currentSeason != Season.Winter && 3 > GrowTime && GrowTime > 0)
        {
            Soybean_Begin.SetActive(false);
            Soybean_Grow.SetActive(true);
            Soybean_Winter.SetActive(false);
        }
        else if (GrowTime == 0)
        {
            Soybean_Begin.SetActive(false);
            Soybean_Grow.SetActive(false);
            Soybean_Winter.SetActive(false);
            Soybean_Done.SetActive(true);
        }
        else if(GrowTime < -2)
        {
            Soybean_Done.SetActive(false);
            Soybean_Dead.SetActive(true);
        }
    }
    
    private void WheatPlant()
    {
        if (currentSeason == Season.Winter && GrowTime > 0)
        {
            Wheat_Begin.SetActive(false);
            Wheat_Grow.SetActive(false);
            Wheat_Winter.SetActive(true);
        }
        else if (currentSeason != Season.Winter && 2 > GrowTime && GrowTime > 0)
        {
            Wheat_Begin.SetActive(false);
            Wheat_Grow.SetActive(true);
            Wheat_Winter.SetActive(false);
        }
        else if (GrowTime == 0)
        {
            Wheat_Begin.SetActive(false);
            Wheat_Grow.SetActive(false);
            Wheat_Winter.SetActive(false);
            Wheat_Done.SetActive(true);
        }
        else if(GrowTime < -2)
        {
            Wheat_Done.SetActive(false);
            Wheat_Dead.SetActive(true);
        }
    }

    private void CornPlant()
    {
        if (currentSeason == Season.Winter && GrowTime > 0)
        {
            Corn_Begin.SetActive(false);
            Corn_Grow.SetActive(false);
            Corn_Winter.SetActive(true);
        }
        else if (currentSeason != Season.Winter && 4 > GrowTime && GrowTime > 0)
        {
            Corn_Begin.SetActive(false);
            Corn_Grow.SetActive(true);
            Corn_Winter.SetActive(false);
        }
        else if (GrowTime == 0)
        {
            Corn_Begin.SetActive(false);
            Corn_Grow.SetActive(false);
            Corn_Winter.SetActive(false);
            Corn_Done.SetActive(true);
        }
        else if(GrowTime < -2)
        {
            Corn_Done.SetActive(false);
            Corn_Dead.SetActive(true);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (SoybeanisPlanted.activeSelf == false && WheatisPlanted.activeSelf == false && CornisPlanted.activeSelf == false)
        {
            PlantUI.SetActive(true);
            PlantUI.transform.position = new Vector3(1060, 740, 0);
        }
        else
        {
            ToolUI.SetActive(true);
            ToolUI.transform.position = new Vector3(1060, 740, 0);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlantUI.SetActive(false);
        ToolUI.SetActive(false);
    }
    
    //根据物品ID获取是否存在和背包index
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

    //检测背包是否有物品以激活对应物品按钮
    private void ButtonTest(int ID)
    {
        int index = GetItemIndexInBag(ID);
        if (index == -1 || playerBag.itemList[index].itemAmount<=0)
        {
            if(ID == 1001)
                PlantSoybean.GetComponent<Button>().interactable = false;
            else if(ID == 1002)
                PlantWheat.GetComponent<Button>().interactable = false;
            else if(ID == 1003)
                PlantCorn.GetComponent<Button>().interactable = false;
            else if (ID == 1004)
                Fertilizer.GetComponent<Button>().interactable = false;
            else if (ID == 1005)
                Insecticide.GetComponent<Button>().interactable = false;
        }
        else
        {
            if (ID == 1001)
            {
                if(currentSeason != Season.Winter && PlayerCoin > 15) 
                    PlantSoybean.GetComponent<Button>().interactable = true;
                else
                    PlantSoybean.GetComponent<Button>().interactable = false;
                
                //Debug.Log("Soybean"+currentSeason);
            }
            else if (ID == 1002)
            {
                if(currentSeason != Season.Winter && PlayerCoin > 15) 
                    PlantWheat.GetComponent<Button>().interactable = true;
                else
                    PlantWheat.GetComponent<Button>().interactable = false;
                
                //Debug.Log("Wheat"+currentSeason);
            }
            else if (ID == 1003)
            {
                if(currentSeason != Season.Summer && currentSeason != Season.Winter && PlayerCoin > 25)
                    PlantCorn.GetComponent<Button>().interactable = true;
                else
                    PlantCorn.GetComponent<Button>().interactable = false;

                //Debug.Log("Corn"+currentSeason);
            }
            else if (ID == 1004)
                if(Fertility.GetComponent<Slider>().value < 1.0f && PlayerCoin > 50)
                    Fertilizer.GetComponent<Button>().interactable = true;
                else
                    Fertilizer.GetComponent<Button>().interactable = false;
            else if (ID == 1005)
                if(Health.GetComponent<Slider>().value < 1.0f && PlayerCoin > 50) 
                    Insecticide.GetComponent<Button>().interactable = true;
                else
                    Insecticide.GetComponent<Button>().interactable = false;
                
            
            //收割按钮状态
            if (PlantDone.activeSelf || PlantDead.activeSelf)
            {
                Harvest.GetComponent<Button>().interactable = true;
            }
            else
            {
                Harvest.GetComponent<Button>().interactable = false;
            }
            

        }
        
        //Debug.Log("index"+index+"amount"+playerBag.itemList[index].itemAmount);
        
        //Debug.Log(isPlanted.activeSelf+"是否设置完成种植周期");
        //Debug.Log(SoybeanisPlanted.activeSelf+"是否种植大豆");
        //Debug.Log(WheatisPlanted.activeSelf+"是否种植小麦");
        //Debug.Log(CornisPlanted.activeSelf+"是否种植玉米");
        //Debug.Log(PlantDone.activeSelf+"是否成熟");
        //Debug.Log(PlantDead.activeSelf+"是否腐败");
    }
    
    
}
