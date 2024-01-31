using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SOFiles/BusinessSO")]
public class BusinessSOScript : ScriptableObject
{
    [Header("Business Names")]
    public string[] bLvl1Names = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl2Names = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl3Names = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    [Header("Business Images")]
    public string[] bLvl1ImgPath = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl2ImgPath = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl3ImgPath = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    [Header("Business Status")]
    public string[] bLvl1Status = { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
    public string[] bLvl2Status = { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
    public string[] bLvl3Status = { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };

    [Header("Business Cost")]
    public string[] bLvl1Cost = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl2Cost = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl3Cost = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    [Header("Business Revenue")]
    public string[] bLvl1Reven = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl2Reven = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl3Reven = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    [Header("Business Owned")]
    public string[] bLvl1Owned = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl2Owned = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl3Owned = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    
    [Header("Business Timers")]
    public string[] bLvl1Timer = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl2Timer = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl3Timer = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    
    [Header("Business Manager Level")]
    public string[] bLvl1Manager = { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
    public string[] bLvl2Manager = { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
    public string[] bLvl3Manager = { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };

    [Header("Business Resale")]
    public string[] bLvl1Resale = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl2Resale = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl3Resale = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    [Header("Business Reward Collected")]
    public string[] bLvl1Reward = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl2Reward = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] bLvl3Reward = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    public List<string>[] bizRevenuelist1 = new List<string>[12];
    public List<string>[] bizRevenuelist2 = new List<string>[12];
    public List<string>[] bizRevenuelist3 = new List<string>[12];
}
