using UnityEngine;

[CreateAssetMenu(menuName = "SOFiles/ResearchSO")]
public class ResearchSOScript : ScriptableObject
{
    [Header("Research General Data")]
    public string[] researchName = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    public string[] researchImgPath = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    public string[] researchLevel = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    public string[] researchDscpt = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };



    public string[] reschLvlSequence = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                         "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                         "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    [Header("Research Cost Data")]
    public string[] researchCostLvl1 = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    public string[] researchCostLvl2 = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    public string[] researchCostLvl3 = {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };


    [Header("Research Required Respect")]

    public string[] r1ReqRespect = {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] r2ReqRespect = {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] r3ReqRespect = {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    
    [Header("Research Requirement Data")]

    public string[] r1Req = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] r2Req = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] r3Req = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };


    public string[] rsrchReqL1 = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] rsrchReqL2 = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] rsrchReqL3 = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

}
