using SQLite4Unity3d;
using UnityEngine;

public class AllTableClass: MonoBehaviour
{
    // Here All the Class Represents Table of the Database
}

public class GmSaveGeneralTable
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Property { get; set; }
    public string GameState { get; set; }
}

public class GmSaveResearchTable
{
    [PrimaryKey, AutoIncrement]
    public int ResearchNum { get; set; }
    public string Level { get; set; }
}

public class GmSaveBusinessTable
{
    [PrimaryKey, AutoIncrement]
    public int BusinessNum { get; set; }
    public string bOwnedLvl1 { get; set; }
    public string bOwnedLvl2 { get; set; }
    public string bOwnedLvl3 { get; set; }
    public string bStatusLvl1 { get; set; }
    public string bStatusLvl2 { get; set; }
    public string bStatusLvl3 { get; set; }
    public string bManagerLvl1 { get; set; }
    public string bManagerLvl2 { get; set; }
    public string bManagerLvl3 { get; set; }
    public string bRewardLvl1 { get; set; }
    public string bRewardLvl2 { get; set; }
    public string bRewardLvl3 { get; set; }
    public string bTimerLvl1 { get; set; }
    public string bTimerLvl2 { get; set; }
    public string bTimerLvl3 { get; set; }
}

public class GmSaveLifeStyleTable
{
    [PrimaryKey, AutoIncrement]
    public int LifeStyleNum { get; set; }
    public string PartnerExpense { get; set; }
    public string HomeExpense { get; set; }
    public string TransportExpense { get; set; }
    public string HomeStatus { get; set; }
    public string TransportStatus { get; set; }
}

public class GmSaveTradeTable
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string TradeName { get; set; }
    public string Qty { get; set; }
    public string BuyPrice { get; set; }
}