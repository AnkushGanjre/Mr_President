using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SOFiles/HomePageSO")]
public class HomePageSOScript : ScriptableObject
{
    public string[] conversionSymbl = { "K", "M", "B", "T", "Q", "P", "S" };

    public string[] dbAmounts = { "0", "0", "0", "0", "0", "0", "0" };

    public string[] healthActPerct = { "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] happyActPerct = { "0", "0", "0", "0", "0", "0", "0", "0" };

    public string[] healthActCost = { "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] happyActCost = { "0", "0", "0", "0", "0", "0", "0", "0" };

    public string[] electionNames = { "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] electionAgeReq = { "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] electionRespReq = { "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] electionCampFund = { "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] electionReward = { "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] electedSalary = { "0", "0", "0", "0", "0", "0", "0", "0" };
}
