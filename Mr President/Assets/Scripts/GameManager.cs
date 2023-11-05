using UnityEngine;
using SQLite4Unity3d;

public class GameManager : MonoBehaviour
{
    [Header("SQL Managers")]
    public DatabaseServiceScript dbSerScript;
    public SQLiteConnection connection;

    [Header("Scriptable Object")]
    public HomePageSOScript homePageSO;
    public ResearchSOScript researchSO;
    public BusinessSOScript businessSO;
    public LifeStyleSOScript lifeStyleSO;
    public TradeSOScript tradeSO;

    [Header("All Script's Instance")]
    public HomePageUIScript hpScript;
    public StoreScript strScript;
    public ResearchScript rsrchScript;
    public BusinessScript bizScript;
    public LifeStyleScript lsScript;
    public TradeScript trdScript;
    public UIControllerScript uiCtrlScript;
    public ElectionScript elecScript;
    public GameObjectiveScript objScript;
    public GameSaveMangerSQL gsSaveMgrSql;
    private string dbPath;

    private void Awake()
    {
        dbSerScript = GetComponent<DatabaseServiceScript>();

        hpScript = GetComponent<HomePageUIScript>();
        strScript = GetComponent<StoreScript>();
        rsrchScript = GetComponent<ResearchScript>();
        bizScript = GetComponent<BusinessScript>();
        lsScript = GetComponent<LifeStyleScript>();
        trdScript = GetComponent<TradeScript>();
        uiCtrlScript = GetComponent<UIControllerScript>();
        elecScript = GetComponent<ElectionScript>();
        objScript = GetComponent<GameObjectiveScript>();
        gsSaveMgrSql = GetComponent<GameSaveMangerSQL>();
    }

    private void Start()
    {
#if UNITY_EDITOR
        dbPath = Application.streamingAssetsPath + "/GmResourcesDB.db";
#elif UNITY_ANDROID
        dbPath = Application.persistentDataPath + "/GmResourcesDB.db";
#endif

        homePageSO = (HomePageSOScript)Resources.Load("HomePageSO");
        researchSO = (ResearchSOScript)Resources.Load("ResearchSO");
        businessSO = (BusinessSOScript)Resources.Load("BusinessSO");
        lifeStyleSO = (LifeStyleSOScript)Resources.Load("LifeStyleSO");
        tradeSO = (TradeSOScript)Resources.Load("TradeSO");

        GetDBDataToSO();
    }

    public void GetDBDataToSO()
    {
        dbSerScript.DatabaseName = "GmResourcesDB.db";
        dbSerScript.OnDatabaseService();

        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadOnly);

        GetHpTabData();
        GetRsrchTabData();
        GetBizTabData();
        GetLsTabData();
        GetTrdTabData();
        
    }


    #region Data Retreieving Functions

    private void GetHpTabData()
    {
        for (int i = 0; i < homePageSO.dbAmounts.Length; i++)
        {
            string data = GetHpTabData("DailyBonusAmt" , (i+1).ToString());
            homePageSO.dbAmounts[i] = data;
        }

        for (int i = 0; i < homePageSO.healthActPerct.Length; i++)
        {
            string data = GetHpTabData("HealthActivityPerct" , (i+1).ToString());
            homePageSO.healthActPerct[i] = data;
        }

        for (int i = 0; i < homePageSO.happyActPerct.Length; i++)
        {
            string data = GetHpTabData("HappyActivityPerct", (i+1).ToString());
            homePageSO.happyActPerct[i] = data;
        }

        for (int i = 0; i < homePageSO.healthActCost.Length; i++)
        {
            string data = GetHpTabData("HealthActivityCost" , (i+1).ToString());
            homePageSO.healthActCost[i] = data;
        }

        for (int i = 0; i < homePageSO.happyActCost.Length; i++)
        {
            string data = GetHpTabData("HappyActivityCost", (i+1).ToString());
            homePageSO.happyActCost[i] = data;
        }

        for (int i = 0; i < homePageSO.electionNames.Length; i++)
        {
            string data = GetHpTabData("ElectionNames" , (i+1).ToString());
            homePageSO.electionNames[i] = data;
        }

        for (int i = 0; i < homePageSO.electionAgeReq.Length; i++)
        {
            string data = GetHpTabData("ElectionAgeReq" , (i+1).ToString());
            homePageSO.electionAgeReq[i] = data;
        }

        for (int i = 0; i < homePageSO.electionRespReq.Length; i++)
        {
            string data = GetHpTabData("ElectionResptReq" , (i+1).ToString());
            homePageSO.electionRespReq[i] = data;
        }

        for (int i = 0; i < homePageSO.electionCampFund.Length; i++)
        {
            string data = GetHpTabData("ElectionCampFund" , (i+1).ToString());
            homePageSO.electionCampFund[i] = data;
        }

        for (int i = 0; i < homePageSO.electionReward.Length; i++)
        {
            string data = GetHpTabData("ElectionReward" , (i+1).ToString());
            homePageSO.electionReward[i] = data;
        }

        for (int i = 0; i < homePageSO.electedSalary.Length; i++)
        {
            string data = GetHpTabData("ElectedSalary", (i + 1).ToString());
            homePageSO.electedSalary[i] = data;
        }
    }

    private void GetRsrchTabData()
    {
        for (int i = 0; i < researchSO.researchName.Length; i++)
        {
            string data = GetRsrchTabData("rsrchName", (i + 1).ToString());
            researchSO.researchName[i] = data;
        }
        
        for (int i = 0; i < researchSO.researchImgPath.Length; i++)
        {
            string data = GetRsrchTabData("rsrchImgPath", (i + 1).ToString());
            researchSO.researchImgPath[i] = data;
        }
        
        for (int i = 0; i < researchSO.researchDscpt.Length; i++)
        {
            string data = GetRsrchTabData("rsrchDscpt", (i + 1).ToString());
            researchSO.researchDscpt[i] = data;
        }
        
        for (int i = 0; i < researchSO.researchCostLvl1.Length; i++)
        {
            string data = GetRsrchTabData("rsrchCostL1", (i + 1).ToString());
            researchSO.researchCostLvl1[i] = data;
        }
        
        for (int i = 0; i < researchSO.researchCostLvl2.Length; i++)
        {
            string data = GetRsrchTabData("rsrchCostL2", (i + 1).ToString());
            researchSO.researchCostLvl2[i] = data;
        }
        
        for (int i = 0; i < researchSO.researchCostLvl3.Length; i++)
        {
            string data = GetRsrchTabData("rsrchCostL3", (i + 1).ToString());
            researchSO.researchCostLvl3[i] = data;
        }
        
        for (int i = 0; i < researchSO.r1ReqRespect.Length; i++)
        {
            string data = GetRsrchTabData("rsrchReqResptL1", (i + 1).ToString());
            researchSO.r1ReqRespect[i] = data;
        }
        
        for (int i = 0; i < researchSO.r2ReqRespect.Length; i++)
        {
            string data = GetRsrchTabData("rsrchReqResptL2", (i + 1).ToString());
            researchSO.r2ReqRespect[i] = data;
        }
        
        for (int i = 0; i < researchSO.r3ReqRespect.Length; i++)
        {
            string data = GetRsrchTabData("rsrchReqResptL3", (i + 1).ToString());
            researchSO.r3ReqRespect[i] = data;
        }
    }

    private void GetBizTabData()
    {
        for (int i = 0; i < businessSO.bLvl1Names.Length; i++)
        {
            string data = GetBizTabData("BizNameL1", (i + 1).ToString());
            businessSO.bLvl1Names[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl2Names.Length; i++)
        {
            string data = GetBizTabData("BizNameL2", (i + 1).ToString());
            businessSO.bLvl2Names[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl3Names.Length; i++)
        {
            string data = GetBizTabData("BizNameL3", (i + 1).ToString());
            businessSO.bLvl3Names[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl1ImgPath.Length; i++)
        {
            string data = GetBizTabData("BizImgPathL1", (i + 1).ToString());
            businessSO.bLvl1ImgPath[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl2ImgPath.Length; i++)
        {
            string data = GetBizTabData("BizImgPathL2", (i + 1).ToString());
            businessSO.bLvl2ImgPath[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl3ImgPath.Length; i++)
        {
            string data = GetBizTabData("BizImgPathL3", (i + 1).ToString());
            businessSO.bLvl3ImgPath[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl1Cost.Length; i++)
        {
            string data = GetBizTabData("BizCostL1", (i + 1).ToString());
            businessSO.bLvl1Cost[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl2Cost.Length; i++)
        {
            string data = GetBizTabData("BizCostL2", (i + 1).ToString());
            businessSO.bLvl2Cost[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl3Cost.Length; i++)
        {
            string data = GetBizTabData("BizCostL3", (i + 1).ToString());
            businessSO.bLvl3Cost[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl1Reven.Length; i++)
        {
            string data = GetBizTabData("BizRevenueL1", (i + 1).ToString());
            businessSO.bLvl1Reven[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl2Reven.Length; i++)
        {
            string data = GetBizTabData("BizRevenueL2", (i + 1).ToString());
            businessSO.bLvl2Reven[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl3Reven.Length; i++)
        {
            string data = GetBizTabData("BizRevenueL3", (i + 1).ToString());
            businessSO.bLvl3Reven[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl1Resale.Length; i++)
        {
            string data = GetBizTabData("BizResaleL1", (i + 1).ToString());
            businessSO.bLvl1Resale[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl2Resale.Length; i++)
        {
            string data = GetBizTabData("BizResaleL2", (i + 1).ToString());
            businessSO.bLvl2Resale[i] = data;
        }
        
        for (int i = 0; i < businessSO.bLvl3Resale.Length; i++)
        {
            string data = GetBizTabData("BizResaleL3", (i + 1).ToString());
            businessSO.bLvl3Resale[i] = data;
        }

    }

    private void GetLsTabData()
    {
        for (int i = 0; i < lifeStyleSO.respectPercent.Length; i++)
        {
            string data = GetLsTabData("LsRespect", (i + 1).ToString());
            lifeStyleSO.respectPercent[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.partnersName.Length; i++)
        {
            string data = GetLsTabData("LsPartName", (i + 1).ToString());
            lifeStyleSO.partnersName[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.homeName.Length; i++)
        {
            string data = GetLsTabData("LsHomeName", (i + 1).ToString());
            lifeStyleSO.homeName[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.carsName.Length; i++)
        {
            string data = GetLsTabData("LsTransName", (i + 1).ToString());
            lifeStyleSO.carsName[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.partnersSpriteList.Length; i++)
        {
            string data = GetLsTabData("LsPartImgPath", (i + 1).ToString());
            lifeStyleSO.partnersSpriteList[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.homeSpriteList.Length; i++)
        {
            string data = GetLsTabData("LsHomeImgPath", (i + 1).ToString());
            lifeStyleSO.homeSpriteList[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.carsSpriteList.Length; i++)
        {
            string data = GetLsTabData("LsTransImgPath", (i + 1).ToString());
            lifeStyleSO.carsSpriteList[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.partnersExpense.Length; i++)
        {
            string data = GetLsTabData("LsPartExpense", (i + 1).ToString());
            lifeStyleSO.partnersExpense[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.homeExpense.Length; i++)
        {
            string data = GetLsTabData("LsHomeExpense", (i + 1).ToString());
            lifeStyleSO.homeExpense[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.carsExpense.Length; i++)
        {
            string data = GetLsTabData("LsTransExpense", (i + 1).ToString());
            lifeStyleSO.carsExpense[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.homeCost.Length; i++)
        {
            string data = GetLsTabData("LsHomeCost", (i + 1).ToString());
            lifeStyleSO.homeCost[i] = data;
        }
        
        for (int i = 0; i < lifeStyleSO.carsCost.Length; i++)
        {
            string data = GetLsTabData("LsTransCost", (i + 1).ToString());
            lifeStyleSO.carsCost[i] = data;
        }
    }

    private void GetTrdTabData()
    {
        for (int i = 0; i < tradeSO.buyTradeName.Length; i++)
        {
            string data = GetTrdTabData("TradeName", (i + 1).ToString());
            tradeSO.buyTradeName[i] = data;
        }
        
        for (int i = 0; i < tradeSO.buyTradePrice.Length; i++)
        {
            string data = GetTrdTabData("TradePrice", (i + 1).ToString());
            tradeSO.buyTradePrice[i] = data;
        }
    }

    #endregion

    #region SQL Query

    private string GetHpTabData(string columnName, string primaryKey)
    {
        // Prepare the query
        var query = $"SELECT {columnName} FROM HomePageTable WHERE ID = '{primaryKey}'";

        // Execute the query
        var result = connection.ExecuteScalar<string>(query);

        // Return the GameState
        return result.ToString();
    }

    private string GetRsrchTabData(string columnName, string primaryKey)
    {
        // Prepare the query
        var query = $"SELECT {columnName} FROM ResearchTable WHERE ID = '{primaryKey}'";

        // Execute the query
        var result = connection.ExecuteScalar<string>(query);

        // Return the GameState
        return result.ToString();
    }

    public string GetBizTabData(string columnName, string primaryKey)
    {
        // Prepare the query
        var query = $"SELECT {columnName} FROM BusinessTable WHERE ID = '{primaryKey}'";

        // Execute the query
        var result = connection.ExecuteScalar<string>(query);

        // Return the content of the specified column
        return result;
    }

    public string GetLsTabData(string columnName, string primaryKey)
    {
        // Prepare the query
        var query = $"SELECT {columnName} FROM LifeStyleTable WHERE ID = '{primaryKey}'";

        // Execute the query
        var result = connection.ExecuteScalar<string>(query);

        // Return the content of the specified column
        return result;
    }
    
    public string GetTrdTabData(string columnName, string primaryKey)
    {
        // Prepare the query
        var query = $"SELECT {columnName} FROM TradeTable WHERE ID = '{primaryKey}'";

        // Execute the query
        var result = connection.ExecuteScalar<string>(query);

        // Return the content of the specified column
        return result;
    }

    

    #endregion
}
