using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TradeScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameManager gmMgr;

    [Header("For Populating")]
    [SerializeField] GameObject stockScrollStrip;
    [SerializeField] GameObject tradeBuyContent;
    [SerializeField] GameObject tradeSellContent;
    [SerializeField] private GameObject tradeBuyPrefab;
    [SerializeField] private GameObject tradeSellPrefab;
    [SerializeField] private GameObject buySell;
    [SerializeField] private GameObject buySellSlider;
    [SerializeField] private GameObject tradeComplete;
    [SerializeField] private GameObject intradayToggle;
    [SerializeField] private GameObject longtermToggle;
    private Toggle intraTgg;
    private Toggle longtermTgg;
    [SerializeField] Button tMinusBtn;
    [SerializeField] Button tPlusBtn;
    [SerializeField] Button tMaxBtn;
    [SerializeField] GameObject inputQty;

    public bool isTpanelOpen;
    public string currentTradeName;
    private int maxQty;
    private int currentQty;
    private int tradePrice;
    public bool isBuying;
    private string tPnL;
    public bool isTradeExist;

    [SerializeField] private bool isReqCreditAvail;

    private string scrollingText;
    private int[] traOldPrice = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public int minTradeBuyQty = 1;
    public int maxTradeBuyQty = 16;

    public Image fillImage;
    private float timer;
    public float callInterval = 10f; // Time interval in seconds between function calls

    private void Awake()
    {
        CallAwakeFunc();
    }

    private void Start()
    {
        tMinusBtn.onClick.AddListener(() => { OnQtyMinus(); });
        tPlusBtn.onClick.AddListener(() => { OnQtyPlus(); });
        tMaxBtn.onClick.AddListener(() => { OnMaxButton(); });

        PopulateTradeData();
    }

    private void CallAwakeFunc()
    {
        gmMgr = GetComponent<GameManager>();

        stockScrollStrip = GameObject.Find("StockScrollStrip");
        tradeBuyContent = GameObject.Find("TBuyContent");
        tradeSellContent = GameObject.Find("TSellContent");
        tradeBuyPrefab = Resources.Load<GameObject>("Prefabs/TradeBuyCard");
        tradeSellPrefab = Resources.Load<GameObject>("Prefabs/TradeSellCard");
        buySell = GameObject.Find("TBuySell");
        buySellSlider = GameObject.Find("BuySellSlider");
        tradeComplete = GameObject.Find("TradeComplete");
        intradayToggle = GameObject.Find("IntradayToggle");
        longtermToggle = GameObject.Find("LongTermToggle");

        intraTgg = intradayToggle.GetComponent<Toggle>();
        longtermTgg = longtermToggle.GetComponent<Toggle>();
        intraTgg.onValueChanged.AddListener(new UnityAction<bool>(OnIntradayToggle));
        longtermTgg.onValueChanged.AddListener(new UnityAction<bool>(OnLongTermToggle));

        tMinusBtn = GameObject.Find("TradeMinus").GetComponent<Button>();
        tPlusBtn = GameObject.Find("TradePlus").GetComponent<Button>();
        tMaxBtn = GameObject.Find("TradeMax").GetComponent<Button>();
        inputQty = GameObject.Find("InputBOXXX");

        fillImage = GameObject.Find("trFillerImage").GetComponent<Image>();
    }

    private void Update()
    {
        if (!gmMgr.hpScript.isTimeStopped && !isTpanelOpen)
        {
            // Increment the timer with Time.deltaTime
            timer += Time.deltaTime;

            // Check if it's time to call the function
            if (timer >= callInterval)
            {
                // Call the function you want to execute
                TPriceUpdating();

                // Reset the timer for the next call
                timer = 0f;
            }
            fillImage.fillAmount = timer / callInterval;
        }
    }

    private void PopulateTradeData()
    {
        for (int i = 0; i < gmMgr.tradeSO.buyTradeName.Length; i++)
        {
            GameObject A1 = Instantiate(tradeBuyPrefab, tradeBuyContent.transform);
            A1.name = gmMgr.tradeSO.buyTradeName[i];
            A1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gmMgr.tradeSO.buyTradeName[i];
            A1.transform.GetComponent<Button>().onClick.AddListener(() => { OnBuyTradeInitiate(A1.name); });
        }
    }

    public void TPriceUpdating()
    {
        if (!isTpanelOpen)
        {
            scrollingText = "";
            for (int i = 0; i < gmMgr.tradeSO.buyTradeName.Length; i++)
            {
                // Buy Side Update
                int tQty = Random.Range(minTradeBuyQty, maxTradeBuyQty);
                int range1 = int.Parse(gmMgr.tradeSO.buyTradePrice[i * 2]);
                int range2 = int.Parse(gmMgr.tradeSO.buyTradePrice[(i * 2) + 1]);
                int tPrice = Random.Range(range1, range2);

                tradeBuyContent.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = tQty.ToString();
                tradeBuyContent.transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "$" + tPrice.ToString();

                // Stock Scroll Update
                string proNloss;
                if (tPrice > traOldPrice[i])
                {
                    proNloss = "+" + (tPrice - traOldPrice[i]);
                }
                else
                {
                    proNloss = (tPrice - traOldPrice[i]).ToString();
                }
                scrollingText += gmMgr.tradeSO.buyTradeName[i] + " $" + tPrice + ", " + proNloss + ";" + "       ";
                traOldPrice[i] = tPrice;

                // Sell Side update
                for (int a = 0; a < tradeSellContent.transform.childCount; a++)
                {
                    string tName = (tradeSellContent.transform.GetChild(a).name).Substring(4);
                    int qty = int.Parse(tradeSellContent.transform.GetChild(a).GetChild(1).GetComponent<TextMeshProUGUI>().text);
                    if (tName == gmMgr.tradeSO.buyTradeName[i])
                    {
                        string tSPrice = tradeSellContent.transform.GetChild(a).GetChild(2).GetComponent<TextMeshProUGUI>().text;
                        int SbuyPrice = int.Parse(tSPrice.Substring(1));
                        string pnL;
                        if (SbuyPrice > tPrice)
                        {
                            pnL = "-" + (SbuyPrice - tPrice) * qty;
                        }
                        else
                        {
                            pnL = "+" + (tPrice - SbuyPrice) * qty;
                        }
                        tradeSellContent.transform.GetChild(a).GetChild(3).GetComponent<TextMeshProUGUI>().text = pnL;
                    }
                }
            }
            stockScrollStrip.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = scrollingText;

        }
    }


    // Buy Trade
    public void OnBuyTradeInitiate(string tradeName)
    {
        isTpanelOpen = true;
        buySell.SetActive(true);
        OnIntradayToggle(true);
        buySell.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color32(70, 175, 70, 255);
        buySellSlider.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Trade/BuyBubble");
        tradeComplete.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Trade/Bought");
        buySellSlider.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Swipe to Buy";

        buySell.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = tradeName;

        currentTradeName = tradeName;
        GameObject tradeCard = GameObject.Find(tradeName);
        string tQty = tradeCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        string tPrice = tradeCard.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;

        if (int.Parse(tQty) == 0)
        {
            isBuying = true;
            buySellSlider.transform.parent.gameObject.SetActive(false);
            maxQty = 0;
            currentQty = 0;
            inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentQty.ToString();
            buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Margin Required:\n" + "$0";
        }
        else
        {
            buySellSlider.transform.parent.gameObject.SetActive(true);
            isBuying = true;
            currentQty = 1;
            maxQty = int.Parse(tQty);
            string trPrice = tPrice.Substring(1);
            tradePrice = int.Parse(trPrice);

            inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentQty.ToString();
            buySell.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Current Price:\n" + "$" + tradePrice;
            buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Margin Required:\n" + "$" + tradePrice;

            if (gmMgr.hpScript.CashCoin >= tradePrice)
            {
                isReqCreditAvail = true;
            }
            else
            {
                isReqCreditAvail = false;
            }

            // checking if buying same trade
            foreach (Transform t in tradeSellContent.transform)
            {
                if (t.GetChild(0).GetComponent<TextMeshProUGUI>().text == tradeName)
                {
                    isTradeExist = true;
                }
            }
        }
    }

    // Sell Trade
    public void OnSellTradeInitiate(string tradeName)
    {
        isTpanelOpen = true;
        buySell.SetActive(true);
        buySellSlider.transform.parent.gameObject.SetActive(true);
        OnIntradayToggle(true);
        buySell.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color32(230, 65, 85, 255);
        buySellSlider.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Trade/SellBubble");
        tradeComplete.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Trade/Sold");
        buySellSlider.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Swipe to Sell";

        GameObject S1 = GameObject.Find(tradeName);
        string sTradeName = tradeName.Substring(4);
        string sTprice = S1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;
        string sTPnL = S1.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text;

        isBuying = false;
        currentTradeName = S1.name;
        currentQty = int.Parse(S1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        maxQty = currentQty;
        tPnL = sTPnL;

        buySell.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = sTradeName;
        inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentQty.ToString();
        buySell.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Buying Price:\n" + sTprice;
        buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "P/L:\n" + sTPnL;
    }

    public void OnIntradayToggle(bool boolu)
    {
        boolu = intraTgg.isOn;
        if (boolu)
        {
            longtermTgg.isOn = false;
            longtermTgg.interactable = true;
            intraTgg.interactable = false;
        }
    }

    public void OnLongTermToggle(bool boolu)
    {
        boolu = longtermTgg.isOn;
        if (boolu)
        {
            intraTgg.isOn = false;
            intraTgg.interactable = true;
            longtermTgg.interactable = false;
        }
    }

    public void OnQtyMinus()
    {
        // Getting Qty Directly from Text file.
        string qty = inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        int newQty = int.Parse(qty);
        if (newQty == 0)
        {
            buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Margin Required:\n" + "$0";
        }
        else
        {
            newQty -= 1;
            if (newQty <= 1)
            {
                newQty = 1;
            }

            inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newQty.ToString();
            currentQty = newQty;
            int newPrice = newQty * tradePrice;

            if (isBuying)
            {
                buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Margin Required:\n" + "$" + newPrice;
            }
            else
            {
                if (tPnL == "~")
                {
                    buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "P/L:\n" + "~";
                }
                else
                {
                    int newPnL = (int.Parse(tPnL.Substring(1))) / maxQty;
                    char symbl = tPnL.Substring(0, 1)[0];
                    buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "P/L:\n" + symbl + newQty * newPnL;
                }
            }

            if (gmMgr.hpScript.CashCoin >= newPrice)
            {
                isReqCreditAvail = true;
            }
            else
            {
                isReqCreditAvail = false;
            }
        }
    }

    public void OnQtyPlus()
    {
        string qty = inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        int newQty = int.Parse(qty);
        newQty += 1;
        if (newQty >= maxQty)
        {
            newQty = maxQty;
        }
        inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newQty.ToString();
        currentQty = newQty;
        int newPrice = newQty * tradePrice;
        if (isBuying)
        {
            buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Margin Required:\n" + "$" + newPrice;
        }
        else
        {
            if (tPnL == "~")
            {
                buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "P/L:\n" + "~";
            }
            else
            {
                int newPnL = (int.Parse(tPnL.Substring(1))) / maxQty;
                char symbl = tPnL.Substring(0, 1)[0];
                buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "P/L:\n" + symbl + newQty * newPnL;
            }
        }

        if (gmMgr.hpScript.CashCoin >= newPrice)
        {
            isReqCreditAvail = true;
        }
        else
        {
            isReqCreditAvail = false;
        }
    }

    public void OnMaxButton()
    {
        string qty = inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        int newQty = int.Parse(qty);
        newQty = maxQty;
        inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newQty.ToString();
        currentQty = newQty;
        int newPrice = maxQty * tradePrice;
        if (isBuying)
        {
            buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Margin Required:\n" + "$" + newPrice;
        }
        else
        {
            if (tPnL == "~")
            {
                buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "P/L:\n" + "~";
            }
            else
            {
                int newPnL = (int.Parse(tPnL.Substring(1))) / maxQty;
                char symbl = tPnL.Substring(0, 1)[0];
                buySell.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "P/L:\n" + symbl + newQty * newPnL;
            }
        }

        if (gmMgr.hpScript.CashCoin >= newPrice)
        {
            isReqCreditAvail = true;
        }
        else
        {
            isReqCreditAvail = false;
        }
    }

    public void onQtyEnter()
    {

    }

    public void OnBoughtSoldEvent()
    {
        if (isBuying)
        {
            if (!isReqCreditAvail)
            {
                StartCoroutine(TradeUnsuccessful());
            }
            else
            {
                StartCoroutine(TradeSuccessful());
            }
        }
        else
        {
            StartCoroutine(TradeSuccessful());
        }
    }

    private IEnumerator TradeUnsuccessful()
    {
        yield return new WaitForSeconds(0.25f);
        buySellSlider.SetActive(false);
        buySell.transform.GetChild(0).GetChild(2).GetChild(6).GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        buySellSlider.SetActive(true);
        buySell.transform.GetChild(0).GetChild(2).GetChild(6).GetChild(2).gameObject.SetActive(false);
        Vector3 pos = buySellSlider.transform.GetChild(0).localPosition;
        buySellSlider.transform.GetChild(0).localPosition = new Vector3(-225, pos.y, pos.z);
    }

    private IEnumerator TradeSuccessful()
    {
        yield return new WaitForSeconds(0.25f);
        buySellSlider.SetActive(false);
        tradeComplete.SetActive(true);
        yield return new WaitForSeconds(1f);
        buySell.SetActive(false);

        tradeComplete.SetActive(false);
        buySellSlider.SetActive(true);
        Vector3 pos = buySellSlider.transform.GetChild(0).localPosition;
        buySellSlider.transform.GetChild(0).localPosition = new Vector3(-225, pos.y, pos.z);

        isTpanelOpen = false;

        // Adding sellCard Prefab to sell side
        if (isBuying)
        {
            if (isTradeExist)
            {
                isTradeExist = false;
                GameObject go = GameObject.Find("Sell" + currentTradeName);
                int oldBuyPrice = int.Parse(go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(1));
                int exitingQty = int.Parse(go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
                int newQty = exitingQty + currentQty;
                go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = newQty.ToString();

                string oldQty = GameObject.Find(currentTradeName).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
                int remQty = int.Parse(oldQty) - currentQty;
                GameObject.Find(currentTradeName).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = remQty.ToString();
                int totalPrice = (oldBuyPrice * exitingQty) + (tradePrice * currentQty);
                int totalQty = exitingQty + currentQty;
                int avgPrice = totalPrice / totalQty;
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "$" + avgPrice;

                // Updating Database
                gmMgr.gsSaveMgrSql.UpdateTradeQtyPrice(currentTradeName, newQty.ToString(), avgPrice.ToString());
            }
            else
            {
                GameObject A1 = Instantiate(tradeSellPrefab, tradeSellContent.transform);
                A1.name = "Sell" + currentTradeName;
                A1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentTradeName;
                A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentQty.ToString();
                A1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "$" + tradePrice;
                A1.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "~";

                A1.transform.GetComponent<Button>().onClick.AddListener(() => { OnSellTradeInitiate(A1.name); });

                string oldQty = GameObject.Find(currentTradeName).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
                int remQty = int.Parse(oldQty) - currentQty;
                GameObject.Find(currentTradeName).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = remQty.ToString();


                // Adding trade to Database
                GmSaveTradeTable gsTradeTab = new GmSaveTradeTable
                {
                    TradeName = currentTradeName,
                    Qty = currentQty.ToString(),
                    BuyPrice = tradePrice.ToString()
                };
                gmMgr.gsSaveMgrSql.ToAddBoughtTrade(gsTradeTab);
            }

            // Subtracting trade Price from game coins
            gmMgr.hpScript.CashCoin -= tradePrice * currentQty;
        }
        if (!isBuying)
        {
            GameObject S1 = GameObject.Find(currentTradeName);
            string profit = S1.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text;
            int boughtPrice = int.Parse((buySell.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text).Substring(15));
            int boughtQty = int.Parse(S1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
            int selectQty = int.Parse(inputQty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            if (profit == "~")
            {
                gmMgr.hpScript.CashCoin += (boughtPrice * selectQty);
            }
            else
            {
                //Add overAllProfit to bank
                int overAllProfit = int.Parse(profit.Substring(1));
                char symbl = profit.Substring(0, 1)[0];
                if (symbl.ToString() == "+")
                {
                    //Debug.Log("Over All Profit: +" + overAllProfit);
                    gmMgr.hpScript.CashCoin += (boughtPrice * selectQty) + overAllProfit;
                }
                else
                {
                    //Debug.Log("Over All Loss: -" + overAllProfit);
                    gmMgr.hpScript.CashCoin += (boughtPrice * selectQty) - overAllProfit;
                }
            }
            if (selectQty < boughtQty)
            {
                S1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (boughtQty - selectQty).ToString();
                gmMgr.gsSaveMgrSql.UpdateTradeQtyPrice(currentTradeName.Substring(4), ((boughtQty - selectQty).ToString()), boughtPrice.ToString());
            }
            else if (selectQty == boughtQty)
            {
                Destroy(S1);
                gmMgr.gsSaveMgrSql.ToDeleteTradeSold(currentTradeName.Substring(4));
            }
        }
        gmMgr.hpScript.UpdateCredit();
    }

    public void UpdateBoughttrade()
    {
        for (int i = 0; i < gmMgr.tradeSO.boughtTradeName.Length; i++)
        {
            GameObject A1 = Instantiate(tradeSellPrefab, tradeSellContent.transform);
            A1.name = "Sell" + gmMgr.tradeSO.boughtTradeName[i];
            A1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gmMgr.tradeSO.boughtTradeName[i];
            A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gmMgr.tradeSO.boughtTradeQty[i];
            A1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.tradeSO.boughtTradePrice[i];
            A1.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "~";

            A1.transform.GetComponent<Button>().onClick.AddListener(() => { OnSellTradeInitiate(A1.name); });
        }
        TPriceUpdating();
    }
}
