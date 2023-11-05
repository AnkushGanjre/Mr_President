using System;
using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;


public class GameSaveMangerSQL : MonoBehaviour
{
    public GameManager gmMgr;
    public SQLiteConnection connection;
    string dbPath;

    private string hpTable = "GmSaveGeneralTable";


    private void Awake()
    {
        gmMgr = GetComponent<GameManager>();
    }

    private void Start()
    {
#if UNITY_EDITOR
        dbPath = Application.streamingAssetsPath + "/GmSaveDataBase.db";
#elif UNITY_ANDROID
        dbPath = Application.persistentDataPath + "/GmSaveDataBase.db";
#endif

        // To get SQL Database from persistentDataPath to streamingAssetsPath
        gmMgr.dbSerScript.DatabaseName = "GmSaveDataBase.db";
        gmMgr.dbSerScript.OnDatabaseService();

        //CreateTable();
        //AddAllEntryToTable();
        InitialGameDataCheck();
    }

    public void InitialGameDataCheck()
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        bool hpTabExist = gmMgr.dbSerScript.TableExists(hpTable, connection);

        if (hpTabExist)
        {
            //Debug.Log("Table exists in the database, Fetching Data.");
            GetBoughtTradeData();
            GetLifeStyleData();
            GetBusinessData();
            GetResearchData();
            GetGtDataToGame();
        }
        else
        {
            //Debug.Log("Table does not exist in the database, Creating one.");
            CreateTable();
            AddAllEntryToTable();
        }


        // Closing the connection to the database
        connection.Close();
    }
    public void CreateTable()
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        connection.DropTable<GmSaveGeneralTable>();
        connection.CreateTable<GmSaveGeneralTable>();

        connection.DropTable<GmSaveResearchTable>();
        connection.CreateTable<GmSaveResearchTable>();

        connection.DropTable<GmSaveBusinessTable>();
        connection.CreateTable<GmSaveBusinessTable>();

        connection.DropTable<GmSaveLifeStyleTable>();
        connection.CreateTable<GmSaveLifeStyleTable>();

        connection.DropTable<GmSaveTradeTable>();
        connection.CreateTable<GmSaveTradeTable>();


        // Closing the connection to the database
        connection.Close();
    }

    public void AddAllEntryToTable()
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        GmSaveGeneralTable[] gmSaveGenTab = new[]
        {
            new GmSaveGeneralTable
            {
                Property = "PlayerName",
                GameState = ""
            },
            new GmSaveGeneralTable
            {
                Property = "currentDay",
                GameState = "1"
            },
            new GmSaveGeneralTable
            {
                Property = "currentMonth",
                GameState = "1"
            },
            new GmSaveGeneralTable
            {
                Property = "currentYear",
                GameState = "2005"
            },
            new GmSaveGeneralTable
            {
                Property = "currentAge",
                GameState = "18"
            },
            new GmSaveGeneralTable
            {
                Property = "CashCoin",
                GameState = "5000"
            },
            new GmSaveGeneralTable
            {
                Property = "healthPercentage",
                GameState = "100"
            },
            new GmSaveGeneralTable
            {
                Property = "happyPercentage",
                GameState = "100"
            },
            new GmSaveGeneralTable
            {
                Property = "respectPercentage",
                GameState = "10"
            },
            new GmSaveGeneralTable
            {
                Property = "dbCollectedDay",
                GameState = "0"
            },
            new GmSaveGeneralTable
            {
                Property = "dbStartDate",
                GameState = "2005, 01, 01"
            },
            new GmSaveGeneralTable
            {
                Property = "ElectionNum",
                GameState = "0"
            },
            new GmSaveGeneralTable
            {
                Property = "ElectionTimer",
                GameState = "0"
            },
            new GmSaveGeneralTable
            {
                Property = "currentPartner",
                GameState = ""
            },
            new GmSaveGeneralTable
            {
                Property = "currentHome",
                GameState = ""
            },
            new GmSaveGeneralTable
            {
                Property = "currentTransport",
                GameState = ""
            },
            new GmSaveGeneralTable
            {
                Property = "activeRechName",
                GameState = ""
            },
            new GmSaveGeneralTable
            {
                Property = "activeRechLevel",
                GameState = "0"
            },
            new GmSaveGeneralTable
            {
                Property = "activeRechIndex",
                GameState = "0"
            },
            new GmSaveGeneralTable
            {
                Property = "isAnyReseaGoingOn",
                GameState = "False"
            },
            new GmSaveGeneralTable
            {
                Property = "researchTimer",
                GameState = "0"
            },
            new GmSaveGeneralTable
            {
                Property = "AdBonusVal",
                GameState = "1000"
            }
        };
        ToAddGenData(gmSaveGenTab);

        GmSaveResearchTable[] researchTab = new[]
        {
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            },
            new GmSaveResearchTable
            {
                Level = "0"
            }
        };
        ToAddRechData(researchTab);

        GmSaveBusinessTable[] businessTab = new[]
        {
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
            new GmSaveBusinessTable
            {
                bOwnedLvl1 = "0",
                bOwnedLvl2 = "0",
                bOwnedLvl3 = "0",
                bStatusLvl1 = "False",
                bStatusLvl2 = "False",
                bStatusLvl3 = "False",
                bManagerLvl1 = "False",
                bManagerLvl2 = "False",
                bManagerLvl3 = "False",
                bRewardLvl1 = "0",
                bRewardLvl2 = "0",
                bRewardLvl3 = "0",
                bTimerLvl1 = "0",
                bTimerLvl2 = "0",
                bTimerLvl3 = "0"
            },
        };
        ToAddBizData(businessTab);

        GmSaveLifeStyleTable[] lifestyleTab = new[]
        {
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[0],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[0],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[0],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[1],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[1],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[1],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[2],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[2],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[2],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[3],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[3],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[3],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[4],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[4],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[4],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[5],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[5],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[5],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[6],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[6],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[6],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[7],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[7],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[7],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[8],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[8],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[8],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[9],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[9],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[9],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[10],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[10],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[10],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[11],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[11],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[11],
                HomeStatus = "False",
                TransportStatus = "False"
            },
            new GmSaveLifeStyleTable
            {
                PartnerExpense = gmMgr.lifeStyleSO.partnersExpense[12],
                HomeExpense = gmMgr.lifeStyleSO.homeExpense[12],
                TransportExpense = gmMgr.lifeStyleSO.carsExpense[12],
                HomeStatus = "False",
                TransportStatus = "False"
            }
        };
        ToAddLsData(lifestyleTab);

        // Closing the connection to the database
        connection.Close();
    }

    #region Fetching Function

    public void GetGtDataToGame()
    {
        string playerName = GetGtGameState("PlayerName");
        gmMgr.hpScript.playerName = playerName;

        string currentday = GetGtGameState("currentDay");
        gmMgr.hpScript.currentDay = int.Parse(currentday);

        string currentMonth = GetGtGameState("currentMonth");
        gmMgr.hpScript.currentMonth = int.Parse(currentMonth);

        string currentYear = GetGtGameState("currentYear");
        gmMgr.hpScript.currentYear = int.Parse(currentYear);

        string currentAge = GetGtGameState("currentAge");
        gmMgr.hpScript.currentAge = int.Parse(currentAge);

        string cashCoin = GetGtGameState("CashCoin");
        gmMgr.hpScript.CashCoin = gmMgr.hpScript.ConvStrgToDecimal(cashCoin);

        string healthPercentage = GetGtGameState("healthPercentage");
        gmMgr.hpScript.healthPercentage = float.Parse(healthPercentage);

        string happyPercentage = GetGtGameState("happyPercentage");
        gmMgr.hpScript.happyPercentage = float.Parse(happyPercentage);

        string respectPercentage = GetGtGameState("respectPercentage");
        gmMgr.hpScript.respectPercentage = float.Parse(respectPercentage);

        string dbCollectedDay = GetGtGameState("dbCollectedDay");
        gmMgr.hpScript.dbCollectedDay = int.Parse(dbCollectedDay);

        string dbStartDate = GetGtGameState("dbStartDate");
        string[] words = dbStartDate.Split(',');
        gmMgr.hpScript.dbStartDate = new DateTime(int.Parse(words[0]), int.Parse(words[1]), int.Parse(words[2]));

        string electionNum = GetGtGameState("ElectionNum");
        gmMgr.elecScript.electionNum = int.Parse(electionNum);
        
        string electionTimer = GetGtGameState("ElectionTimer");
        gmMgr.elecScript.electionTimer = float.Parse(electionTimer);

        string currentPartner = GetGtGameState("currentPartner");
        gmMgr.lsScript.currentPartner = currentPartner;

        string currentHome = GetGtGameState("currentHome");
        gmMgr.lsScript.currentHome = currentHome;

        string currentTransport = GetGtGameState("currentTransport");
        gmMgr.lsScript.currentTransport = currentTransport;

        string activeRechName = GetGtGameState("activeRechName");
        gmMgr.rsrchScript.activeRechName = activeRechName;

        string activeRechLevel = GetGtGameState("activeRechLevel");
        gmMgr.rsrchScript.activeRechLevel = int.Parse(activeRechLevel);

        string activeRechIndex = GetGtGameState("activeRechIndex");
        gmMgr.rsrchScript.activeRechIndex = int.Parse(activeRechIndex);

        string isAnyReseaGoingOn = GetGtGameState("isAnyReseaGoingOn");
        gmMgr.rsrchScript.isAnyReseaGoingOn = bool.Parse(isAnyReseaGoingOn);

        string researchTimer = GetGtGameState("researchTimer");
        gmMgr.rsrchScript.researchTimer = float.Parse(researchTimer);

        string adBonusVal = GetGtGameState("AdBonusVal");
        gmMgr.hpScript.adBonusVal = gmMgr.hpScript.ConvStrgToDecimal(adBonusVal);

        gmMgr.rsrchScript.OnResearchUpdate();
        gmMgr.bizScript.OnBusinessUpdate();
        gmMgr.lsScript.OnLsPartnerUpdate();
        gmMgr.lsScript.OnLsHomeUpdate();
        gmMgr.lsScript.OnLsTransportUpdate();
        gmMgr.hpScript.UpdateCredit();
        gmMgr.hpScript.UpdateHeHaReBar();
        gmMgr.uiCtrlScript.OnGameStart();
    }

    public void GetResearchData()
    {
        for (int i = 0; i < gmMgr.researchSO.researchLevel.Length; i++)
        {
            string rechLvl = GetResearchLevel(i + 1);
            gmMgr.researchSO.researchLevel[i] = rechLvl;
        }
    }

    public void GetBusinessData()
    {
        // Business Owned Data
        for (int i = 0; i < gmMgr.businessSO.bLvl1Owned.Length; i++)
        {
            int ij = i + 1;
            string bOwnNum = GetBusinessData(ij, "bOwnedLvl1");
            gmMgr.businessSO.bLvl1Owned[i] = bOwnNum;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl2Owned.Length; i++)
        {
            int ij = i + 1;
            string bOwnNum = GetBusinessData(ij, "bOwnedLvl2");
            gmMgr.businessSO.bLvl2Owned[i] = bOwnNum;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl3Owned.Length; i++)
        {
            int ij = i + 1;
            string bOwnNum = GetBusinessData(ij, "bOwnedLvl3");
            gmMgr.businessSO.bLvl3Owned[i] = bOwnNum;
        }

        // Business Status Data
        for (int i = 0; i < gmMgr.businessSO.bLvl1Status.Length; i++)
        {
            int ij = i + 1;
            string bStatus = GetBusinessData(ij, "bStatusLvl1");
            gmMgr.businessSO.bLvl1Status[i] = bStatus;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl2Status.Length; i++)
        {
            int ij = i + 1;
            string bStatus = GetBusinessData(ij, "bStatusLvl2");
            gmMgr.businessSO.bLvl2Status[i] = bStatus;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl3Status.Length; i++)
        {
            int ij = i + 1;
            string bStatus = GetBusinessData(ij, "bStatusLvl3");
            gmMgr.businessSO.bLvl3Status[i] = bStatus;
        }

        // Business Manager Data
        for (int i = 0; i < gmMgr.businessSO.bLvl1Manager.Length; i++)
        {
            int ij = i + 1;
            string bManager = GetBusinessData(ij, "bManagerLvl1");
            gmMgr.businessSO.bLvl1Manager[i] = bManager;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl2Manager.Length; i++)
        {
            int ij = i + 1;
            string bManager = GetBusinessData(ij, "bManagerLvl2");
            gmMgr.businessSO.bLvl2Manager[i] = bManager;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl3Manager.Length; i++)
        {
            int ij = i + 1;
            string bManager = GetBusinessData(ij, "bManagerLvl3");
            gmMgr.businessSO.bLvl3Manager[i] = bManager;
        }
        
        // Business Reward Data
        for (int i = 0; i < gmMgr.businessSO.bLvl1Reward.Length; i++)
        {
            int ij = i + 1;
            string bReward = GetBusinessData(ij, "bRewardLvl1");
            gmMgr.businessSO.bLvl1Reward[i] = bReward;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl2Reward.Length; i++)
        {
            int ij = i + 1;
            string bReward = GetBusinessData(ij, "bRewardLvl2");
            gmMgr.businessSO.bLvl2Reward[i] = bReward;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl3Reward.Length; i++)
        {
            int ij = i + 1;
            string bReward = GetBusinessData(ij, "bRewardLvl3");
            gmMgr.businessSO.bLvl3Reward[i] = bReward;
        }
        
        // Business Timer Data
        for (int i = 0; i < gmMgr.businessSO.bLvl1Timer.Length; i++)
        {
            int ij = i + 1;
            string bReward = GetBusinessData(ij, "bTimerLvl1");
            gmMgr.businessSO.bLvl1Timer[i] = bReward;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl2Timer.Length; i++)
        {
            int ij = i + 1;
            string bReward = GetBusinessData(ij, "bTimerLvl2");
            gmMgr.businessSO.bLvl2Timer[i] = bReward;
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl3Timer.Length; i++)
        {
            int ij = i + 1;
            string bReward = GetBusinessData(ij, "bTimerLvl3");
            gmMgr.businessSO.bLvl3Timer[i] = bReward;
        }
    }

    public void GetLifeStyleData()
    {
        // Partner Expense
        for (int i = 0; i < gmMgr.lifeStyleSO.partnersExpense.Length; i++)
        {
            int ij = i + 1;
            string pExpense = GetLifeStyleStatus(ij, "PartnerExpense");
            gmMgr.lifeStyleSO.partnersExpense[i] = pExpense;
        }
        // Home Expense
        for (int i = 0; i < gmMgr.lifeStyleSO.homeExpense.Length; i++)
        {
            int ij = i + 1;
            string hExpense = GetLifeStyleStatus(ij, "HomeExpense");
            gmMgr.lifeStyleSO.homeExpense[i] = hExpense;
        }
        // Transport Expense
        for (int i = 0; i < gmMgr.lifeStyleSO.carsExpense.Length; i++)
        {
            int ij = i + 1;
            string tExpense = GetLifeStyleStatus(ij, "TransportExpense");
            gmMgr.lifeStyleSO.carsExpense[i] = tExpense;
        }
        // Home Status
        for (int i = 0; i < gmMgr.lifeStyleSO.homeStatus.Length; i++)
        {
            int ij = i + 1;
            string bStatus = GetLifeStyleStatus(ij, "HomeStatus");
            gmMgr.lifeStyleSO.homeStatus[i] = bStatus;
        }
        // Transport Status
        for (int i = 0; i < gmMgr.lifeStyleSO.carsStatus.Length; i++)
        {
            int ij = i + 1;
            string bStatus = GetLifeStyleStatus(ij, "TransportStatus");
            gmMgr.lifeStyleSO.carsStatus[i] = bStatus;
        }
    }

    public void GetBoughtTradeData()
    {
        GetBoughtTradeData(out gmMgr.tradeSO.boughtTradeName, out gmMgr.tradeSO.boughtTradeQty, out gmMgr.tradeSO.boughtTradePrice);
        gmMgr.trdScript.UpdateBoughttrade();
    }

    #endregion


    #region SQL Query

    #region HomePage Table Query

    public int ToAddGenData(GmSaveGeneralTable[] generalData)
    {
        return connection.InsertAll(generalData);
    }

    public string GetGtGameState(string property)
    {
        // Prepare the query
        var query = $"SELECT GameState FROM GmSaveGeneralTable WHERE Property = '{property}'";

        // Execute the query
        var result = connection.ExecuteScalar<string>(query);

        // Return the GameState
        return result.ToString();
    }

    public void UpdateGtGameState(string property, string newGameState)
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // Prepare the query
        var query = $"UPDATE GmSaveGeneralTable SET GameState = '{newGameState}' WHERE Property = '{property}'";

        // Execute the query
        connection.Execute(query);

        // Closing the connection to the database
        connection.Close();
    }

    #endregion

    #region Research Table Query

    public int ToAddRechData(GmSaveResearchTable[] researchData)
    {
        return connection.InsertAll(researchData);
    }

    public string GetResearchLevel(int rechLvl)
    {
        // Prepare the query
        var query = $"SELECT Level FROM GmSaveResearchTable WHERE ResearchNum = '{rechLvl}'";

        // Execute the query
        var result = connection.ExecuteScalar<string>(query);

        // Return the GameState
        return result.ToString();
    }

    public void UpdateResearchLvl(int rechLvl, string updatedLevel)
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // Prepare the query
        var query = $"UPDATE GmSaveResearchTable SET Level = '{updatedLevel}' WHERE ResearchNum = '{rechLvl}'";

        // Execute the query
        connection.Execute(query);

        // Closing the connection to the database
        connection.Close();
    }

    #endregion

    #region Business Table Query

    public int ToAddBizData(GmSaveBusinessTable[] businessData)
    {
        return connection.InsertAll(businessData);
    }

    public string GetBusinessData(int bNum, string business)
    {
        // Prepare the query
        var query = $"SELECT {business} FROM GmSaveBusinessTable WHERE BusinessNum = {bNum}";

        // Execute the query
        var result = connection.ExecuteScalar<string>(query);

        // Return the content of the specified column
        return result;
    }

    public void UpdateBusinessData(int bNum, string business, string newData)
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // Prepare the query
        var query = $"UPDATE GmSaveBusinessTable SET {business} = '{newData}' WHERE BusinessNum = {bNum}";

        // Execute the query
        connection.Execute(query);

        // Closing the connection to the database
        connection.Close();
    }

    #endregion

    #region Lifestyle Table Query

    public int ToAddLsData(GmSaveLifeStyleTable[] lifestyleData)
    {
        return connection.InsertAll(lifestyleData);
    }

    public string GetLifeStyleStatus(int lsNum, string lifeStyle)
    {
        // Prepare the query
        var query = $"SELECT {lifeStyle} FROM GmSaveLifeStyleTable WHERE LifeStyleNum = {lsNum}";

        // Execute the query
        var result = connection.ExecuteScalar<string>(query);

        // Return the content of the specified column
        return result;
    }

    public void UpdateLifeStyleStatus(int lsNum, string lifeStyle, string newData)
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // Prepare the query
        var query = $"UPDATE GmSaveLifeStyleTable SET {lifeStyle} = '{newData}' WHERE LifeStyleNum = {lsNum}";

        // Execute the query
        connection.Execute(query);

        // Closing the connection to the database
        connection.Close();
    }

    #endregion

    #region Trade Table Query

    public int ToAddBoughtTrade(GmSaveTradeTable gsTradeTab)
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        return connection.Insert(gsTradeTab);
    }

    public void ToDeleteTradeSold(string tName)
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // Prepare the query
        var query = $"DELETE FROM GmSaveTradeTable WHERE TradeName = '{tName}'";

        // Execute the query
        connection.Execute(query);

        // Closing the connection to the database
        connection.Close();
    }

    public void UpdateTradeQtyPrice(string tName, string newQty, string newPrice)
    {
        // Open a connection to the database
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // Prepare the query
        var query = $"UPDATE GmSaveTradeTable SET Qty = '{newQty}', BuyPrice = '{newPrice}' WHERE TradeName = '{tName}'";

        // Execute the query
        connection.Execute(query);

        // Closing the connection to the database
        connection.Close();
    }

    public void GetBoughtTradeData(out string[] tradeNames, out string[] quantities, out string[] buyPrices)
    {
        // Retrieve all the data from the table
        var query = "SELECT * FROM GmSaveTradeTable";
        List<GmSaveTradeTable> tradeDataList = connection.Query<GmSaveTradeTable>(query);

        // Lists to store the data for each entry
        List<string> tradeNameList = new List<string>();
        List<string> qtyList = new List<string>();
        List<string> buyPriceList = new List<string>();

        foreach (GmSaveTradeTable tradeData in tradeDataList)
        {
            string tradeName = tradeData.TradeName;
            string qty = tradeData.Qty;
            string buyPrice = tradeData.BuyPrice;

            // Add the current entry's data to the respective lists
            tradeNameList.Add(tradeName);
            qtyList.Add(qty);
            buyPriceList.Add(buyPrice);
        }

        // Convert the lists to arrays
        tradeNames = tradeNameList.ToArray();
        quantities = qtyList.ToArray();
        buyPrices = buyPriceList.ToArray();
    }

    #endregion

    #endregion

}
