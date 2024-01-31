using UnityEngine;

[CreateAssetMenu(menuName = "SOFiles/TradeSO")]
public class TradeSOScript : ScriptableObject
{
    public string[] buyTradeName = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"};

    public string[] buyTradePrice = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"};

    public string[] boughtTradeName;
    public string[] boughtTradeQty;
    public string[] boughtTradePrice;
}
