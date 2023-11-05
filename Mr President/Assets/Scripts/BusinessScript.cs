using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class BusinessScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameManager gmMgr;

    [Header("For Populate")]
    [SerializeField] private GameObject businessCardPrefab;
    [SerializeField] GameObject[] busiTabBtns;
    [SerializeField] GameObject businViewPort;
    [SerializeField] GameObject busiScroll;
    [SerializeField] GameObject[] busiContent;
    [SerializeField] GameObject tabHeader;

    [Header("For Buying")]
    public GameObject businessClicked;
    [SerializeField] private GameObject managerPanel;
    [SerializeField] private Button bBuyMinus;
    [SerializeField] private Button bBuyPlus;
    [SerializeField] private Button bBuyMax;
    [SerializeField] private TMP_InputField bBuyInField;
    [SerializeField] private Button busBuyBtn;

    [Header("For Selling")]
    [SerializeField] private Button bSellMinus;
    [SerializeField] private Button bSellPlus;
    [SerializeField] private Button bSellMax;
    [SerializeField] private TMP_InputField bSellInField;
    [SerializeField] private Button busSellBtn;

    [Header("Business Manager")]
    [SerializeField] Button bmbAcceptBtn;
    [SerializeField] Button bmbCancelBtn;

    [Header("For Current B Tracking")]
    public Transform selectTransform;
    public string selectBname;
    public int selectBindex;
    public int selectBlevel;
    public int maxBuySellLimit = 100;
    public int maxBuyLimit = 100;

    public int selectBuyQty;
    public int selectMaxBuy;
    public decimal selectBizCost;
    
    public int selectSellQty;
    public int selectMaxSell;
    public int selectOwnQty;
    public decimal selectResale;

    private string[] resaleArr;
    private string[] revenueArr;
    private int ij;
    private int busiNum = 3;
    private bool isCoinsReadyToCollect;
    public int fillDuration = 60;

    private void Awake()
    {
        CallAwakeFunc();
    }

    private void Start()
    {
        CallStartFunc();
        OnManagerSalary();

        for (int i = 0; i < gmMgr.businessSO.bizRevenuelist1.Length; i++)
        {
            gmMgr.businessSO.bizRevenuelist1[i] = new List<string>();
        }
        for (int i = 0; i < gmMgr.businessSO.bizRevenuelist2.Length; i++)
        {
            gmMgr.businessSO.bizRevenuelist2[i] = new List<string>();
        }
        for (int i = 0; i < gmMgr.businessSO.bizRevenuelist3.Length; i++)
        {
            gmMgr.businessSO.bizRevenuelist3[i] = new List<string>();
        }
    }

    private void Update()
    {
        if (!gmMgr.hpScript.isTimeStopped)
        {
            BusinessTimer();
        }
    }

    private void CallAwakeFunc()
    {
        gmMgr = GetComponent<GameManager>();
        businessCardPrefab = Resources.Load<GameObject>("Prefabs/BusinessCard");

        businViewPort = GameObject.Find("BViewport").gameObject;
        busiScroll = GameObject.Find("BScrollView");
        tabHeader = GameObject.Find("BHeaderList");
        busiContent = new GameObject[busiNum];
        for (int i = 0; i < busiNum; i++)
        {
            busiContent[i] = businViewPort.transform.GetChild(i).gameObject;
        }

        busiTabBtns = new GameObject[busiNum];
        for (int i = 0; i < busiNum; i++)
        {
            busiTabBtns[i] = tabHeader.transform.GetChild(i).gameObject;
            int buttonNumber = i;
            busiTabBtns[i].GetComponent<Button>().onClick.AddListener(() => { OnToggleTab(buttonNumber); });
        }

        businessClicked = GameObject.Find("BusinessClicked");
        managerPanel = GameObject.Find("BManagerPanel");
        bBuyMinus = GameObject.Find("BbuyMinus").GetComponent<Button>();
        bBuyPlus = GameObject.Find("BbuyPlus").GetComponent<Button>();
        bBuyMax = GameObject.Find("BbuyMax").GetComponent<Button>();
        bBuyInField = GameObject.Find("bBuyInField").GetComponent<TMP_InputField>();

        bSellMinus = GameObject.Find("BsellMinus").GetComponent<Button>();
        bSellPlus = GameObject.Find("BsellPlus").GetComponent<Button>();
        bSellMax = GameObject.Find("BsellMax").GetComponent<Button>();
        bSellInField = GameObject.Find("bSellInField").GetComponent<TMP_InputField>();

        bmbAcceptBtn = GameObject.Find("bmbAcceptBtn").GetComponent<Button>();
        bmbCancelBtn = GameObject.Find("bmbCancelBtn").GetComponent<Button>();

        busBuyBtn = GameObject.Find("BbuyButton").GetComponent<Button>();
        busSellBtn = GameObject.Find("BsellButton").GetComponent<Button>();
    }

    private void CallStartFunc()
    {
        bBuyMinus.onClick.AddListener(() => { OnBuyQtyMnius(); });
        bBuyPlus.onClick.AddListener(() => { OnBuyQtyPlus(); });
        bBuyMax.onClick.AddListener(() => { OnBuyQtyMax(); });
        bSellMinus.onClick.AddListener(() => { OnSellQtyMnius(); });
        bSellPlus.onClick.AddListener(() => { OnSellQtyPlus(); });
        bSellMax.onClick.AddListener(() => { OnSellQtyMax(); });
        busBuyBtn.onClick.AddListener(() => { OnBusinessBuyBtn(); });
        busSellBtn.onClick.AddListener(() => { OnBusinessSellBtn(); });

        bBuyInField.GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { CheckBuyLimit(bBuyInField); });
        bSellInField.GetComponent<TMP_InputField>().onEndEdit.AddListener(delegate { CheckSellLimit(bSellInField); });

        businessClicked.SetActive(false);
        managerPanel.SetActive(false);
        PopulateBData();
    }

    float timer;
    private void BusinessTimer()
    {
        isCoinsReadyToCollect = false;

        for (int i = 0; i < gmMgr.businessSO.bLvl1Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl1Status[i] == "True" && int.Parse(gmMgr.businessSO.bLvl1Owned[i]) > 0)
            {
                timer = float.Parse(gmMgr.businessSO.bLvl1Timer[i]);
                string mgrStatus = gmMgr.businessSO.bLvl1Manager[i];
                float progress = (float)timer / fillDuration;
                if (progress >= 1)
                {
                    if (mgrStatus == "True")
                    {
                        OnBusinessRevenueCollect(1, i);
                        timer = 0;
                    }
                    else
                    {
                        isCoinsReadyToCollect = true;
                        gmMgr.businessSO.bLvl1Timer[i] = fillDuration.ToString();
                    }
                }

                if (gmMgr.hpScript.isTimeSpeedUp)
                {
                    timer += Time.deltaTime * gmMgr.hpScript.speedUpNum;
                }
                else
                {
                    timer += Time.deltaTime;
                }

                gmMgr.businessSO.bLvl1Timer[i] = timer.ToString();
                gmMgr.gsSaveMgrSql.UpdateBusinessData(i + 1, "bTimerLvl1", timer.ToString());
            }
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl2Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl2Status[i] == "True" && int.Parse(gmMgr.businessSO.bLvl2Owned[i]) > 0)
            {
                timer = float.Parse(gmMgr.businessSO.bLvl2Timer[i]);
                string mgrStatus = gmMgr.businessSO.bLvl2Manager[i];
                float progress = (float)timer / fillDuration;
                if (progress >= 1)
                {
                    if (mgrStatus == "True")
                    {
                        OnBusinessRevenueCollect(2, i);
                        timer = 0;
                    }
                    else
                    {
                        isCoinsReadyToCollect = true;
                        gmMgr.businessSO.bLvl2Timer[i] = fillDuration.ToString();
                    }
                }

                if (gmMgr.hpScript.isTimeSpeedUp)
                {
                    timer += Time.deltaTime * gmMgr.hpScript.speedUpNum;
                }
                else
                {
                    timer += Time.deltaTime;
                }

                gmMgr.businessSO.bLvl2Timer[i] = timer.ToString();
                gmMgr.gsSaveMgrSql.UpdateBusinessData(i + 1, "bTimerLvl2", timer.ToString());
            }
        }
        for (int i = 0; i < gmMgr.businessSO.bLvl3Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl3Status[i] == "True" && int.Parse(gmMgr.businessSO.bLvl3Owned[i]) > 0)
            {
                timer = float.Parse(gmMgr.businessSO.bLvl3Timer[i]);
                string mgrStatus = gmMgr.businessSO.bLvl3Manager[i];
                float progress = (float)timer / fillDuration;
                if (progress >= 1)
                {
                    if (mgrStatus == "True")
                    {
                        OnBusinessRevenueCollect(3, i);
                        timer = 0;
                    }
                    else
                    {
                        isCoinsReadyToCollect = true;
                        gmMgr.businessSO.bLvl3Timer[i] = fillDuration.ToString();
                    }
                }

                if (gmMgr.hpScript.isTimeSpeedUp)
                {
                    timer += Time.deltaTime * gmMgr.hpScript.speedUpNum;
                }
                else
                {
                    timer += Time.deltaTime;
                }

                gmMgr.businessSO.bLvl3Timer[i] = timer.ToString();
                gmMgr.gsSaveMgrSql.UpdateBusinessData(i + 1, "bTimerLvl3", timer.ToString());
            }
        }

        // Coins Collect Notification
        if (isCoinsReadyToCollect)
        {
            gmMgr.uiCtrlScript.homepage.transform.GetChild(1).GetChild(2).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            gmMgr.uiCtrlScript.homepage.transform.GetChild(1).GetChild(2).GetChild(1).gameObject.SetActive(false);
        }
    }

    void PopulateBData()
    {
        int lvlArr1 = gmMgr.businessSO.bLvl1Names.Length;
        int lvlArr2 = gmMgr.businessSO.bLvl2Names.Length;
        int lvlArr3 = gmMgr.businessSO.bLvl3Names.Length;

        PopulateContent(0, lvlArr1, busiContent[0].transform, gmMgr.businessSO.bLvl1Names, gmMgr.businessSO.bLvl1ImgPath, gmMgr.businessSO.bLvl1Cost, gmMgr.businessSO.bLvl1Reven, gmMgr.businessSO.bLvl1Owned);
        PopulateContent(1, lvlArr2, busiContent[1].transform, gmMgr.businessSO.bLvl2Names, gmMgr.businessSO.bLvl2ImgPath, gmMgr.businessSO.bLvl2Cost, gmMgr.businessSO.bLvl2Reven, gmMgr.businessSO.bLvl2Owned);
        PopulateContent(2, lvlArr3, busiContent[2].transform, gmMgr.businessSO.bLvl3Names, gmMgr.businessSO.bLvl3ImgPath, gmMgr.businessSO.bLvl3Cost, gmMgr.businessSO.bLvl3Reven, gmMgr.businessSO.bLvl3Owned);
        busiContent[0].gameObject.SetActive(true);
    }

    void PopulateContent(int lvlNum, int arr, Transform t, string[] nameArr, string[] spriteArr, string[] costArr, string[] revenArr, string[] ownArr)
    {
        for (int i = 0; i < arr; i++)
        {
            GameObject A1 = Instantiate(businessCardPrefab, t.transform);
            A1.name = nameArr[i].ToString();

            if (lvlNum == 0)
            {
                if (gmMgr.businessSO.bLvl1Status[i] == "False")
                {
                    PopulateLocked();
                }
                else
                {
                    PopulateUnlocked();
                }
            }
            else if (lvlNum == 1)
            {
                if (gmMgr.businessSO.bLvl2Status[i] == "False")
                {
                    PopulateLocked();
                }
                else
                {
                    PopulateUnlocked();
                }
            }
            else if (lvlNum == 2)
            {
                if (gmMgr.businessSO.bLvl3Status[i] == "False")
                {
                    PopulateLocked();
                }
                else
                {
                    PopulateUnlocked();
                }
            }

            void PopulateLocked()
            {
                A1.GetComponent<Image>().color = new Color32(255, 255, 255, 175);
                A1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(spriteArr[i]);
                A1.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 175);
                A1.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = nameArr[i].ToString();
                A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 175);
                A1.transform.GetChild(2).gameObject.SetActive(false);
                A1.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Required Research:\n" + gmMgr.researchSO.researchName[i] + " Level " + (lvlNum + 1);
                A1.transform.GetChild(4).gameObject.SetActive(false);
                A1.transform.GetChild(5).gameObject.SetActive(false);
            }

            void PopulateUnlocked()
            {
                A1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(spriteArr[i]);
                A1.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = nameArr[i].ToString();
                A1.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "Cost/Unit: $" + costArr[i];
                A1.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = "Revenue: $" + revenArr[i] + "/Month";
                A1.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = "Own: " + ownArr[i];
                A1.transform.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(() => { OnBusinessOpen(A1.name, t, lvlNum); });
            }
        }
        t.gameObject.SetActive(false);
    }

    public void OnBusinessOpen(string buttonName, Transform t, int lvlNum)
    {
        businessClicked.SetActive(true);

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).name == buttonName)
            {
                ij = i;
            }
        }

        Transform selectedBcard = t.Find(buttonName);
        string bCost = selectedBcard.GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(12);
        decimal businessCost = gmMgr.hpScript.ConvStrgToDecimal(bCost);
        int businessOwnNum = int.Parse((selectedBcard.GetChild(4).GetComponent<TextMeshProUGUI>().text).Substring(5));

        selectBname = buttonName;
        selectTransform = t;
        selectBindex = ij;
        selectBlevel = lvlNum;
        selectBuyQty = 1;
        selectOwnQty = businessOwnNum;
        selectBizCost = businessCost;
        selectSellQty = selectOwnQty;
        selectMaxSell = selectOwnQty;

        OnMaxBuyUpdate();

        if (selectMaxSell > maxBuySellLimit) { selectMaxSell = maxBuySellLimit; }

        if (lvlNum == 0)
        {
            resaleArr = gmMgr.businessSO.bLvl1Resale;
            revenueArr = gmMgr.businessSO.bLvl1Reven;
        }
        else if (lvlNum == 1)
        {
            resaleArr = gmMgr.businessSO.bLvl2Resale;
            revenueArr = gmMgr.businessSO.bLvl2Reven;
        }
        else
        {
            resaleArr = gmMgr.businessSO.bLvl3Resale;
            revenueArr = gmMgr.businessSO.bLvl3Reven;
        }

        businessClicked.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = selectedBcard.GetChild(0).GetComponent<Image>().sprite;
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = selectBname;
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = 
            "Cost/Unit: $" + gmMgr.hpScript.ConvDecimalToStrg(selectBizCost);
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text =
            "Revenue: $" + revenueArr[ij] + "/Month";
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text =
            "Investment: $" + gmMgr.hpScript.ConvDecimalToStrg(selectBizCost * selectBuyQty);
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Owned: " + businessOwnNum;


        bBuyInField.text = selectBuyQty.ToString();
        bSellInField.text = selectSellQty.ToString();


        if (businessOwnNum == 0)
        {
            businessClicked.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Resale: ~";
        }
        else
        {
            businessClicked.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Resale: $" +
            gmMgr.hpScript.ConvDecimalToStrg(gmMgr.hpScript.ConvStrgToDecimal(resaleArr[ij]) * businessOwnNum);
            selectResale = gmMgr.hpScript.ConvStrgToDecimal(resaleArr[ij]);
        }

    }

    public void OnMaxBuyUpdate()
    {
        selectMaxBuy = (int)decimal.Truncate(gmMgr.hpScript.CashCoin / selectBizCost);
        if (selectMaxBuy > maxBuySellLimit) { selectMaxBuy = maxBuySellLimit; }
        if (selectOwnQty + selectMaxBuy >= maxBuyLimit) { selectMaxBuy = maxBuyLimit - selectOwnQty; }
        if (gmMgr.hpScript.CashCoin <= 0 || selectMaxBuy == 0) { selectBuyQty = 0; selectMaxBuy = 0; }
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Max Buy: " + selectMaxBuy;
    }

    //decimal allReven;
    public void OnBusinessUpdate()
    {
        OnRcomBupdate();
        gmMgr.hpScript.monthlyIncome = 0;
        int preSelectLvl = selectBlevel;
        //allReven = 0;
        OnToggleTab(0);
        for (int i = 0; i < gmMgr.businessSO.bLvl1Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl1Status[i] == "True")
            {

                if (int.Parse(gmMgr.businessSO.bLvl1Owned[i]) > 0)
                {
                    busiContent[0].transform.GetChild(i).GetChild(6).gameObject.SetActive(true);
                    busiContent[0].transform.GetChild(i).GetChild(7).gameObject.SetActive(true);
                    busiContent[0].transform.GetChild(i).GetChild(8).gameObject.SetActive(true);
                }
                gmMgr.hpScript.monthlyIncome += gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl1Reven[i]) * int.Parse(gmMgr.businessSO.bLvl1Owned[i]);

                if (int.Parse(gmMgr.businessSO.bLvl1Owned[i]) > 0 && gmMgr.businessSO.bLvl1Manager[i] == "True")
                {
                    busiContent[0].transform.GetChild(i).GetChild(7).gameObject.SetActive(true);
                    busiContent[0].transform.GetChild(i).GetChild(7).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Business/ManagerActive");
                    busiContent[0].transform.GetChild(i).GetChild(7).GetComponent<Button>().interactable = false;
                    busiContent[0].transform.GetChild(i).GetChild(8).gameObject.SetActive(false);
                }

                //OnNewRevnGenerate(1, i);
                //for (int a = 0; a < gmMgr.businessSO.bizRevenuelist1[i].Count; a++)
                //{
                //    string reven1 = gmMgr.businessSO.bizRevenuelist1[i][a];
                //    allReven += gmMgr.hpScript.ConvStrgToDecimal(reven1);
                //}
            }
        }

        OnToggleTab(1);
        for (int i = 0; i < gmMgr.businessSO.bLvl2Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl2Status[i] == "True")
            {

                if (int.Parse(gmMgr.businessSO.bLvl2Owned[i]) > 0)
                {
                    busiContent[1].transform.GetChild(i).GetChild(6).gameObject.SetActive(true);
                    busiContent[1].transform.GetChild(i).GetChild(7).gameObject.SetActive(true);
                    busiContent[1].transform.GetChild(i).GetChild(8).gameObject.SetActive(true);
                }
                gmMgr.hpScript.monthlyIncome += gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl2Reven[i]) * int.Parse(gmMgr.businessSO.bLvl2Owned[i]);

                if (int.Parse(gmMgr.businessSO.bLvl2Owned[i]) > 0 && gmMgr.businessSO.bLvl2Manager[i] == "True")
                {
                    busiContent[1].transform.GetChild(i).GetChild(7).gameObject.SetActive(true);
                    busiContent[1].transform.GetChild(i).GetChild(7).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Business/ManagerActive");
                    busiContent[0].transform.GetChild(i).GetChild(7).GetComponent<Button>().interactable = false;
                    busiContent[1].transform.GetChild(i).GetChild(8).gameObject.SetActive(false);
                }

                //OnNewRevnGenerate(2, i);
                //for (int a = 0; a < gmMgr.businessSO.bizRevenuelist2[i].Count; a++)
                //{
                //    string reven1 = gmMgr.businessSO.bizRevenuelist2[i][a];
                //    allReven += gmMgr.hpScript.ConvStrgToDecimal(reven1);
                //}
            }
        }

        OnToggleTab(2);
        for (int i = 0; i < gmMgr.businessSO.bLvl3Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl3Status[i] == "True")
            {

                if (int.Parse(gmMgr.businessSO.bLvl3Owned[i]) > 0)
                {
                    busiContent[2].transform.GetChild(i).GetChild(6).gameObject.SetActive(true);
                    busiContent[2].transform.GetChild(i).GetChild(7).gameObject.SetActive(true);
                    busiContent[2].transform.GetChild(i).GetChild(8).gameObject.SetActive(true);
                }
                gmMgr.hpScript.monthlyIncome += gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl3Reven[i]) * int.Parse(gmMgr.businessSO.bLvl3Owned[i]);

                if (int.Parse(gmMgr.businessSO.bLvl3Owned[i]) > 0 && gmMgr.businessSO.bLvl3Manager[i] == "True")
                {
                    busiContent[2].transform.GetChild(i).GetChild(7).gameObject.SetActive(true);
                    busiContent[2].transform.GetChild(i).GetChild(7).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Business/ManagerActive");
                    busiContent[0].transform.GetChild(i).GetChild(7).GetComponent<Button>().interactable = false;
                    busiContent[2].transform.GetChild(i).GetChild(8).gameObject.SetActive(false);
                }

                //OnNewRevnGenerate(3, i);
                //for (int a = 0; a < gmMgr.businessSO.bizRevenuelist3[i].Count; a++)
                //{
                //    string reven1 = gmMgr.businessSO.bizRevenuelist3[i][a];
                //    allReven += gmMgr.hpScript.ConvStrgToDecimal(reven1);
                //}
            }
        }
        OnToggleTab(preSelectLvl);

        //gmMgr.hpScript.monthlyIncome += allReven;
    }



    decimal managerCost;
    int managerLvl;
    string textMsg;
    int managerMultiplier = 19;
    public void OnBusinessManagerOpen(string lvl, int index)
    {
        managerPanel.SetActive(true);

        if (lvl == "Level 1")
        {
            managerLvl = 1;
            managerCost = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl1Cost[index]) * managerMultiplier;
            textMsg = "Buy Manager for your Business\nOnly $" + gmMgr.hpScript.ConvDecimalToStrg(managerCost);
            
        }
        else if (lvl == "Level 2")
        {
            managerLvl = 2;
            managerCost = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl2Cost[index]) * managerMultiplier;
            textMsg = "Buy Manager for your Business\nOnly $" + gmMgr.hpScript.ConvDecimalToStrg(managerCost);
        }
        else if (lvl == "Level 3")
        {
            managerLvl = 3;
            managerCost = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl3Cost[index]) * managerMultiplier;
            textMsg = "Buy Manager for your Business\nOnly $" + gmMgr.hpScript.ConvDecimalToStrg(managerCost);
        }

        bmbAcceptBtn.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = textMsg;
        bmbAcceptBtn.onClick.RemoveAllListeners();
        bmbCancelBtn.onClick.RemoveAllListeners();

        bmbCancelBtn.onClick.AddListener(() => { managerPanel.SetActive(false); });
        bmbAcceptBtn.onClick.AddListener(() => { OnManagerAcceptBtn(managerLvl, index, managerCost); });

        void OnManagerAcceptBtn(int lvl, int index, decimal cost)
        {
            if (gmMgr.hpScript.CashCoin >= cost)
            {
                managerPanel.SetActive(false);
                gmMgr.hpScript.CashCoin -= cost;
                gmMgr.hpScript.UpdateCredit();
                if (lvl == 1)
                {
                    gmMgr.businessSO.bLvl1Manager[index] = "True";
                    gmMgr.gsSaveMgrSql.UpdateBusinessData(index + 1, "bManagerLvl1", "True");
                }
                else if (lvl == 2)
                {
                    gmMgr.businessSO.bLvl2Manager[index] = "True";
                    gmMgr.gsSaveMgrSql.UpdateBusinessData(index + 1, "bManagerLvl2", "True");
                }
                else if (lvl == 3)
                {
                    gmMgr.businessSO.bLvl3Manager[index] = "True";
                    gmMgr.gsSaveMgrSql.UpdateBusinessData(index + 1, "bManagerLvl3", "True");
                }

                OnBusinessUpdate();
                gmMgr.lsScript.OnMonthlyExpenseUpdate();
            }
        }
    }



    decimal collectRevenue;
    public void OnBusinessRevenueCollect(int lvl, int Index)
    {
        if (lvl == 1)
        {
            collectRevenue = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl1Reven[Index]) * int.Parse(gmMgr.businessSO.bLvl1Owned[Index]);
        }
        else if (lvl == 2)
        {
            collectRevenue = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl2Reven[Index]) * int.Parse(gmMgr.businessSO.bLvl2Owned[Index]);
        }
        else if (lvl == 3)
        {
            collectRevenue = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl3Reven[Index]) * int.Parse(gmMgr.businessSO.bLvl3Owned[Index]);
        }

        gmMgr.hpScript.CashCoin += collectRevenue;
        gmMgr.hpScript.UpdateCredit();
    }

    //private System.Random randomGenerator = new System.Random();
    //float collectRevenue;
    //List<string> bizRevenuelist = new List<string>();
    //public void OnBusinessRevenueCollect(int lvl, int Index)
    //{
    //    OnNewRevnGenerate(lvl, Index);

    //    for (int i = 0; i < bizRevenuelist.Count; i++)
    //    {
    //        gmMgr.hpScript.CashCoin += gmMgr.hpScript.ConvStrgToDecimal(bizRevenuelist[i]);
    //    }

    //    gmMgr.hpScript.UpdateCredit();
    //}

    //private void OnNewRevnGenerate(int lvl, int Index)
    //{
    //    if (lvl == 1)
    //    {
    //        int bizOwnQty = int.Parse(gmMgr.businessSO.bLvl1Owned[Index]);
    //        decimal bRevenue = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl1Reven[Index]);
    //        decimal range = decimal.Truncate(bRevenue / 4);
    //        string rangeInStrg = gmMgr.hpScript.ConvDecimalToStrg(range);
    //        float revenue;

    //        // Get the last letter of the string
    //        string lastLetter = rangeInStrg[rangeInStrg.Length - 1].ToString();
    //        if (lastLetter == "K" || lastLetter == "M" || lastLetter == "B" || lastLetter == "T" || lastLetter == "Q" || lastLetter == "P" || lastLetter == "S")
    //        {
    //            rangeInStrg = rangeInStrg.Substring(0, rangeInStrg.Length - 1);
    //            revenue = float.Parse(rangeInStrg);
    //        }
    //        else
    //        {
    //            revenue = float.Parse(rangeInStrg);
    //        }

    //        bizRevenuelist.Clear();
    //        for (int i = 0; i < bizOwnQty; i++)
    //        {
    //            // Generate a random value between 0 and 1
    //            double randomValue = randomGenerator.NextDouble();

    //            // Check if the random value falls within the 70% range
    //            if (randomValue < 0.7)
    //            {
    //                // First number with 70% chance
    //                collectRevenue = UnityEngine.Random.Range(revenue * 2, revenue * 3);
    //                string result = floatToDecimal(collectRevenue);
    //                //Debug.Log(result);
    //                bizRevenuelist.Add(result);
    //            }
    //            else
    //            {
    //                // Second number with 30% chance
    //                collectRevenue = UnityEngine.Random.Range(revenue * 3, revenue * 4);
    //                string result = floatToDecimal(collectRevenue);
    //                //Debug.Log(result);
    //                bizRevenuelist.Add(result);
    //            }
    //        }

    //        gmMgr.businessSO.bizRevenuelist1[Index] = bizRevenuelist;
    //        //for (int i = 0; i < bizRevenuelist.Count; i++)
    //        //{
    //        //    Debug.Log(bizRevenuelist[i]);
    //        //}

    //        string floatToDecimal(float cRevenue)
    //        {
    //            string finalResult;
    //            if (lastLetter == "K" || lastLetter == "M" || lastLetter == "B" || lastLetter == "T" || lastLetter == "Q" || lastLetter == "P" || lastLetter == "S")
    //            {
    //                finalResult = cRevenue + lastLetter;
    //            }
    //            else
    //            {
    //                finalResult = cRevenue.ToString();
    //            }
    //            decimal result = gmMgr.hpScript.ConvStrgToDecimal(finalResult);
    //            return (gmMgr.hpScript.ConvDecimalToStrg(result));
    //        }
    //    }
    //    else if (lvl == 2)
    //    {
    //        int bizOwnQty = int.Parse(gmMgr.businessSO.bLvl2Owned[Index]);
    //        decimal bRevenue = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl2Reven[Index]);
    //        decimal range = decimal.Truncate(bRevenue / 4);
    //        string rangeInStrg = gmMgr.hpScript.ConvDecimalToStrg(range);
    //        float revenue;

    //        // Get the last letter of the string
    //        string lastLetter = rangeInStrg[rangeInStrg.Length - 1].ToString();
    //        if (lastLetter == "K" || lastLetter == "M" || lastLetter == "B" || lastLetter == "T" || lastLetter == "Q" || lastLetter == "P" || lastLetter == "S")
    //        {
    //            rangeInStrg = rangeInStrg.Substring(0, rangeInStrg.Length - 1);
    //            revenue = float.Parse(rangeInStrg);
    //        }
    //        else
    //        {
    //            revenue = float.Parse(rangeInStrg);
    //        }

    //        bizRevenuelist.Clear();
    //        for (int i = 0; i < bizOwnQty; i++)
    //        {
    //            // Generate a random value between 0 and 1
    //            double randomValue = randomGenerator.NextDouble();

    //            // Check if the random value falls within the 70% range
    //            if (randomValue < 0.7)
    //            {
    //                // First number with 70% chance
    //                collectRevenue = UnityEngine.Random.Range(revenue * 2, revenue * 3);
    //                string result = floatToDecimal(collectRevenue);
    //                //Debug.Log(result);
    //                bizRevenuelist.Add(result);
    //            }
    //            else
    //            {
    //                // Second number with 30% chance
    //                collectRevenue = UnityEngine.Random.Range(revenue * 3, revenue * 4);
    //                string result = floatToDecimal(collectRevenue);
    //                //Debug.Log(result);
    //                bizRevenuelist.Add(result);
    //            }
    //        }

    //        gmMgr.businessSO.bizRevenuelist2[Index] = bizRevenuelist;
    //        //for (int i = 0; i < bizRevenuelist.Count; i++)
    //        //{
    //        //    Debug.Log(bizRevenuelist[i]);
    //        //}

    //        string floatToDecimal(float cRevenue)
    //        {
    //            string finalResult;
    //            if (lastLetter == "K" || lastLetter == "M" || lastLetter == "B" || lastLetter == "T" || lastLetter == "Q" || lastLetter == "P" || lastLetter == "S")
    //            {
    //                finalResult = cRevenue + lastLetter;
    //            }
    //            else
    //            {
    //                finalResult = cRevenue.ToString();
    //            }
    //            decimal result = gmMgr.hpScript.ConvStrgToDecimal(finalResult);
    //            return (gmMgr.hpScript.ConvDecimalToStrg(result));
    //        }
    //    }
    //    else if (lvl == 3)
    //    {
    //        int bizOwnQty = int.Parse(gmMgr.businessSO.bLvl3Owned[Index]);
    //        decimal bRevenue = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl3Reven[Index]);
    //        decimal range = decimal.Truncate(bRevenue / 4);
    //        string rangeInStrg = gmMgr.hpScript.ConvDecimalToStrg(range);
    //        float revenue;

    //        // Get the last letter of the string
    //        string lastLetter = rangeInStrg[rangeInStrg.Length - 1].ToString();
    //        if (lastLetter == "K" || lastLetter == "M" || lastLetter == "B" || lastLetter == "T" || lastLetter == "Q" || lastLetter == "P" || lastLetter == "S")
    //        {
    //            rangeInStrg = rangeInStrg.Substring(0, rangeInStrg.Length - 1);
    //            revenue = float.Parse(rangeInStrg);
    //        }
    //        else
    //        {
    //            revenue = float.Parse(rangeInStrg);
    //        }

    //        bizRevenuelist.Clear();
    //        for (int i = 0; i < bizOwnQty; i++)
    //        {
    //            // Generate a random value between 0 and 1
    //            double randomValue = randomGenerator.NextDouble();

    //            // Check if the random value falls within the 70% range
    //            if (randomValue < 0.7)
    //            {
    //                // First number with 70% chance
    //                collectRevenue = UnityEngine.Random.Range(revenue * 2, revenue * 3);
    //                string result = floatToDecimal(collectRevenue);
    //                //Debug.Log(result);
    //                bizRevenuelist.Add(result);
    //            }
    //            else
    //            {
    //                // Second number with 30% chance
    //                collectRevenue = UnityEngine.Random.Range(revenue * 3, revenue * 4);
    //                string result = floatToDecimal(collectRevenue);
    //                //Debug.Log(result);
    //                bizRevenuelist.Add(result);
    //            }
    //        }

    //        gmMgr.businessSO.bizRevenuelist3[Index] = bizRevenuelist;
    //        //for (int i = 0; i < bizRevenuelist.Count; i++)
    //        //{
    //        //    Debug.Log(bizRevenuelist[i]);
    //        //}

    //        string floatToDecimal(float cRevenue)
    //        {
    //            string finalResult;
    //            if (lastLetter == "K" || lastLetter == "M" || lastLetter == "B" || lastLetter == "T" || lastLetter == "Q" || lastLetter == "P" || lastLetter == "S")
    //            {
    //                finalResult = cRevenue + lastLetter;
    //            }
    //            else
    //            {
    //                finalResult = cRevenue.ToString();
    //            }
    //            decimal result = gmMgr.hpScript.ConvStrgToDecimal(finalResult);
    //            return (gmMgr.hpScript.ConvDecimalToStrg(result));
    //        }
    //    }
    //}



    public decimal mgrSalary;
    [SerializeField] private int salaryDivider = 20;
    public void OnManagerSalary()
    {
        mgrSalary = 0;
        for (int i = 0; i < gmMgr.businessSO.bLvl1Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl1Status[i] == "True")
            {
                if (int.Parse(gmMgr.businessSO.bLvl1Owned[i]) > 0 && gmMgr.businessSO.bLvl1Manager[i] == "True")
                {
                    decimal cost = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl1Cost[i]) * managerMultiplier;
                    mgrSalary += decimal.Truncate(cost / salaryDivider);
                }
            }
        }

        for (int i = 0; i < gmMgr.businessSO.bLvl2Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl2Status[i] == "True")
            {
                if (int.Parse(gmMgr.businessSO.bLvl2Owned[i]) > 0 && gmMgr.businessSO.bLvl2Manager[i] == "True")
                {
                    decimal cost = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl2Cost[i]) * managerMultiplier;
                    mgrSalary += decimal.Truncate(cost / salaryDivider);
                }
            }
        }

        for (int i = 0; i < gmMgr.businessSO.bLvl3Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl3Status[i] == "True")
            {
                if (int.Parse(gmMgr.businessSO.bLvl3Owned[i]) > 0 && gmMgr.businessSO.bLvl3Manager[i] == "True")
                {
                    decimal cost = gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl3Cost[i]) * managerMultiplier;
                    mgrSalary += decimal.Truncate(cost / salaryDivider);
                }
            }
        }
    }

    public void OnToggleTab(int index)
    {
        selectBlevel = index;
        for (int i = 0; i < busiTabBtns.Length; i++)
        {
            if (index == i)
            {
                busiScroll.GetComponent<ScrollRect>().content = busiContent[i].gameObject.GetComponent<RectTransform>();
                busiContent[i].SetActive(true);
                busiTabBtns[i].transform.GetComponent<Image>().color = new Color32(0, 235, 25, 255);
            }
            else
            {
                busiContent[i].SetActive(false);
                busiTabBtns[i].transform.GetComponent<Image>().color = new Color32(255, 235, 255, 255);
            }
        }
        //busiScroll.transform.GetChild(1).GetComponent<Scrollbar>().value = 1;
    }

    public void OnRcomBupdate()
    {
        int preSelectLvl = selectBlevel;
        OnToggleTab(0);
        for (int i = 0; i < gmMgr.businessSO.bLvl1Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl1Status[i] == "True")
            {
                int ij = i;
                busiContent[0].transform.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                busiContent[0].transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                busiContent[0].transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(gmMgr.businessSO.bLvl1ImgPath[i]);
                busiContent[0].transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
                busiContent[0].transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                busiContent[0].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Cost/Unit: $" + gmMgr.businessSO.bLvl1Cost[i];
                busiContent[0].transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                busiContent[0].transform.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Revenue: $" + gmMgr.businessSO.bLvl1Reven[i] + "/Month";
                busiContent[0].transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                busiContent[0].transform.GetChild(i).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Own: " + gmMgr.businessSO.bLvl1Owned[i];
                busiContent[0].transform.GetChild(i).GetChild(5).gameObject.SetActive(true);
                string bName = gmMgr.businessSO.bLvl1Names[i];
                busiContent[0].transform.GetChild(i).GetChild(5).GetComponent<Button>().onClick.AddListener(() => { OnBusinessOpen(bName, busiContent[0].transform, 0); });
                busiContent[0].transform.GetChild(i).GetChild(7).GetComponent<Button>().onClick.AddListener(() => { OnBusinessManagerOpen("Level 1", ij); });
            }
        }
        OnToggleTab(1);
        for (int i = 0; i < gmMgr.businessSO.bLvl2Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl2Status[i] == "True")
            {
                int ij = i;
                string bName = gmMgr.businessSO.bLvl2Names[i];
                busiContent[1].transform.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                busiContent[1].transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                busiContent[1].transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(gmMgr.businessSO.bLvl2ImgPath[i]);
                busiContent[1].transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
                busiContent[1].transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                busiContent[1].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Cost/Unit: $" + gmMgr.businessSO.bLvl2Cost[i];
                busiContent[1].transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                busiContent[1].transform.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Revenue: $" + gmMgr.businessSO.bLvl2Reven[i] + "/Month";
                busiContent[1].transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                busiContent[1].transform.GetChild(i).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Own: " + gmMgr.businessSO.bLvl2Owned[i];
                busiContent[1].transform.GetChild(i).GetChild(5).gameObject.SetActive(true);
                busiContent[1].transform.GetChild(i).GetChild(5).GetComponent<Button>().onClick.AddListener(() => { OnBusinessOpen(bName, busiContent[1].transform, 1); });
                busiContent[1].transform.GetChild(i).GetChild(7).GetComponent<Button>().onClick.AddListener(() => { OnBusinessManagerOpen("Level 2", ij); });
            }
        }
        OnToggleTab(2);
        for (int i = 0; i < gmMgr.businessSO.bLvl3Names.Length; i++)
        {
            if (gmMgr.businessSO.bLvl3Status[i] == "True")
            {
                int ij = i;
                string bName = gmMgr.businessSO.bLvl3Names[i];
                busiContent[2].transform.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                busiContent[2].transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                busiContent[2].transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(gmMgr.businessSO.bLvl3ImgPath[i]);
                busiContent[2].transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
                busiContent[2].transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                busiContent[2].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Cost/Unit: $" + gmMgr.businessSO.bLvl3Cost[i];
                busiContent[2].transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                busiContent[2].transform.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Revenue: $" + gmMgr.businessSO.bLvl3Reven[i] + "/Month";
                busiContent[2].transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                busiContent[2].transform.GetChild(i).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Own: " + gmMgr.businessSO.bLvl3Owned[i];
                busiContent[2].transform.GetChild(i).GetChild(5).gameObject.SetActive(true);
                busiContent[2].transform.GetChild(i).GetChild(5).GetComponent<Button>().onClick.AddListener(() => { OnBusinessOpen(bName, busiContent[2].transform, 2); });
                busiContent[2].transform.GetChild(i).GetChild(7).GetComponent<Button>().onClick.AddListener(() => { OnBusinessManagerOpen("Level 3", ij); });
            }
        }
        OnToggleTab(preSelectLvl);
    }


    #region Buy Business Btns

    public void OnBuyQtyMnius()
    {
        selectBuyQty -= 1;
        if (selectBuyQty < 0)
        {
            selectBuyQty = 0;
        }
        bBuyInField.text = selectBuyQty.ToString();
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = 
            "Investment: $" + gmMgr.hpScript.ConvDecimalToStrg(selectBuyQty * selectBizCost);
    }

    public void OnBuyQtyPlus()
    {
        selectBuyQty += 1;
        if (selectBuyQty > selectMaxBuy)
        {
            selectBuyQty = selectMaxBuy;
        }
        bBuyInField.text = selectBuyQty.ToString();
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = 
            "Investment: $" + gmMgr.hpScript.ConvDecimalToStrg(selectBuyQty * selectBizCost);
    }

    public void OnBuyQtyMax()
    {
        selectBuyQty = selectMaxBuy;
        bBuyInField.text = selectMaxBuy.ToString();
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = 
            "Investment: $" + gmMgr.hpScript.ConvDecimalToStrg(selectBuyQty * selectBizCost);
    }

    void CheckBuyLimit(TMP_InputField inFi)
    {
        int qtyVal = int.Parse(inFi.text);

        if (qtyVal < 0)
        {
            inFi.text = "0";
            selectBuyQty = 0;
        }
        if (qtyVal > selectMaxBuy)
        {
            inFi.text = selectMaxBuy.ToString();
            selectBuyQty = selectMaxBuy;
        }
        selectBuyQty = qtyVal;
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = 
            "Investment: $" + gmMgr.hpScript.ConvDecimalToStrg(selectBuyQty * selectBizCost);
    }

    public void OnBusinessBuyBtn()
    {
        if (selectBuyQty > selectMaxBuy) { selectBuyQty = selectMaxBuy; }
        if (selectBlevel == 0)
        {
            int newQty = selectOwnQty + selectBuyQty;
            string qtyInStrg = newQty.ToString();
            gmMgr.businessSO.bLvl1Owned[selectBindex] = qtyInStrg;
            gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bOwnedLvl1", qtyInStrg);

            if (int.Parse(gmMgr.businessSO.bLvl1Reward[selectBindex]) == 0 && newQty >= 10)
            {
                gmMgr.hpScript.respectPercentage += 0.25f;
                gmMgr.hpScript.UpdateHeHaReBar();
                gmMgr.businessSO.bLvl1Reward[selectBindex] = "10";
                gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bRewardLvl1", "10");
            }
            if (int.Parse(gmMgr.businessSO.bLvl1Reward[selectBindex]) == 10 && newQty >= 20)
            {
                gmMgr.hpScript.respectPercentage += 0.5f;
                gmMgr.hpScript.UpdateHeHaReBar();
                gmMgr.businessSO.bLvl1Reward[selectBindex] = "20";
                gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bRewardLvl1", "20");
            }
            if (int.Parse(gmMgr.businessSO.bLvl1Reward[selectBindex]) == 20 && newQty >= 50)
            {
                gmMgr.hpScript.respectPercentage += 0.75f;
                gmMgr.hpScript.UpdateHeHaReBar();
                gmMgr.businessSO.bLvl1Reward[selectBindex] = "50";
                gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bRewardLvl1", "50");
            }
        }
        else if (selectBlevel == 1)
        {
            int newQty = selectOwnQty + selectBuyQty;
            string qtyInStrg = newQty.ToString();
            gmMgr.businessSO.bLvl2Owned[selectBindex] = qtyInStrg;
            gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bOwnedLvl2", qtyInStrg);

            if (int.Parse(gmMgr.businessSO.bLvl2Reward[selectBindex]) == 0 && newQty >= 10)
            {
                gmMgr.hpScript.respectPercentage += 0.25f;
                gmMgr.hpScript.UpdateHeHaReBar();
                gmMgr.businessSO.bLvl2Reward[selectBindex] = "10";
                gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bRewardLvl2", "10");
            }
            if (int.Parse(gmMgr.businessSO.bLvl2Reward[selectBindex]) == 10 && newQty >= 20)
            {
                gmMgr.hpScript.respectPercentage += 0.5f;
                gmMgr.hpScript.UpdateHeHaReBar();
                gmMgr.businessSO.bLvl2Reward[selectBindex] = "20";
                gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bRewardLvl2", "20");
            }
            if (int.Parse(gmMgr.businessSO.bLvl2Reward[selectBindex]) == 20 && newQty >= 50)
            {
                gmMgr.hpScript.respectPercentage += 0.75f;
                gmMgr.hpScript.UpdateHeHaReBar();
                gmMgr.businessSO.bLvl2Reward[selectBindex] = "50";
                gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bRewardLvl2", "50");
            }
        }
        else
        {
            int newQty = selectOwnQty + selectBuyQty;
            string qtyInStrg = newQty.ToString();
            gmMgr.businessSO.bLvl3Owned[selectBindex] = qtyInStrg;
            gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bOwnedLvl3", qtyInStrg);

            if (int.Parse(gmMgr.businessSO.bLvl3Reward[selectBindex]) == 0 && newQty >= 10)
            {
                gmMgr.hpScript.respectPercentage += 0.25f;
                gmMgr.hpScript.UpdateHeHaReBar();
                gmMgr.businessSO.bLvl3Reward[selectBindex] = "10";
                gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bRewardLvl3", "10");
            }
            if (int.Parse(gmMgr.businessSO.bLvl3Reward[selectBindex]) == 10 && newQty >= 20)
            {
                gmMgr.hpScript.respectPercentage += 0.5f;
                gmMgr.hpScript.UpdateHeHaReBar();
                gmMgr.businessSO.bLvl3Reward[selectBindex] = "20";
                gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bRewardLvl3", "20");
            }
            if (int.Parse(gmMgr.businessSO.bLvl3Reward[selectBindex]) == 20 && newQty >= 50)
            {
                gmMgr.hpScript.respectPercentage += 0.75f;
                gmMgr.hpScript.UpdateHeHaReBar();
                gmMgr.businessSO.bLvl3Reward[selectBindex] = "50";
                gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bRewardLvl3", "50");
            }
        }

        gmMgr.hpScript.CashCoin -= selectBuyQty * selectBizCost;
        OnBusinessUpdate();
        gmMgr.hpScript.UpdateCredit();
        gmMgr.hpScript.UpdateIncomExpen();
        gmMgr.uiCtrlScript.OnBusinessInsideClose();
    }

    #endregion


    #region Sell Business Btns

    public void OnSellQtyMnius()
    {

        selectSellQty -= 1;
        if (selectSellQty < 0)
        {
            selectSellQty = 0;
        }
        bSellInField.text = selectSellQty.ToString();
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = 
            "Resale: $" + gmMgr.hpScript.ConvDecimalToStrg(selectResale * selectSellQty);
    }

    public void OnSellQtyPlus()
    {
        selectSellQty += 1;
        if (selectSellQty > selectMaxSell)
        {
            selectSellQty = selectMaxSell;
        }
        bSellInField.text = selectSellQty.ToString();
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = 
            "Resale: $" + gmMgr.hpScript.ConvDecimalToStrg(selectResale * selectSellQty);
    }

    public void OnSellQtyMax()
    {
        selectSellQty = selectMaxSell;
        bSellInField.text = selectMaxSell.ToString();
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = 
            "Resale: $" + gmMgr.hpScript.ConvDecimalToStrg(selectResale * selectSellQty);
    }
    void CheckSellLimit(TMP_InputField inFi)
    {
        int qtyVal = int.Parse(inFi.text);

        if (qtyVal < 0)
        {
            inFi.text = "0";
            selectSellQty = 0;
        }
        if (qtyVal > selectMaxSell)
        {
            inFi.text = selectMaxSell.ToString();
            selectSellQty = selectMaxSell;
        }
        selectSellQty = qtyVal;
        businessClicked.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = 
            "Resale: $" + gmMgr.hpScript.ConvDecimalToStrg(selectResale * selectSellQty);
    }

    public void OnBusinessSellBtn()
    {
        if (selectSellQty > selectMaxSell) { selectSellQty = selectMaxSell; }
        if (selectBlevel == 0)
        {
            gmMgr.businessSO.bLvl1Owned[selectBindex] = (selectOwnQty - selectSellQty).ToString();
            gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bOwnedLvl1", ((selectOwnQty - selectSellQty).ToString()));
        }
        else if (selectBlevel == 1)
        {
            gmMgr.businessSO.bLvl2Owned[selectBindex] = (selectOwnQty - selectSellQty).ToString();
            gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bOwnedLvl2", ((selectOwnQty - selectSellQty).ToString()));
        }
        else
        {
            gmMgr.businessSO.bLvl3Owned[selectBindex] = (selectOwnQty - selectSellQty).ToString();
            gmMgr.gsSaveMgrSql.UpdateBusinessData(selectBindex + 1, "bOwnedLvl3", ((selectOwnQty - selectSellQty).ToString()));
        }

        gmMgr.hpScript.CashCoin += selectSellQty * selectResale;
        OnBusinessUpdate();
        gmMgr.hpScript.UpdateCredit();
        gmMgr.hpScript.UpdateIncomExpen();
        gmMgr.uiCtrlScript.OnBusinessInsideClose(); 
    }


    #endregion


}
