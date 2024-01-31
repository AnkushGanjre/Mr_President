using UnityEngine;

[CreateAssetMenu(menuName = "SOFiles/LifeStyleSO")]
public class LifeStyleSOScript : ScriptableObject
{
    public string[] respectPercent = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    [Header("Partners")]
    public string[] partnersName = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] partnersSpriteList = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] partnersExpense = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    [Header("Homes")]
    public string[] homeName = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] homeSpriteList = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] homeExpense = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] homeCost = {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] homeStatus = { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };

    [Header("Transport")]
    public string[] carsName = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] carsSpriteList = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] carsExpense = {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] carsCost = {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    public string[] carsStatus = { "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False", "False" };
}