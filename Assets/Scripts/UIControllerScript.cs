using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameManager gmMgr;

    [Header("Top Panel")]
    [SerializeField] Button settingsBtn;
    [SerializeField] Button gpPlayBtn;
    [SerializeField] Button gpPauseBtn;
    [SerializeField] Button gpSpeedUpBtn;

    [SerializeField] Button healthPlusBtn;
    [SerializeField] Button happyPlusBtn;
    [SerializeField] Button respectPlusBtn;

    [Header("Homepage")]
    public GameObject homepage;
    [SerializeField] Button sUIOpenBtn;
    [SerializeField] Button rUIOpenBtn;
    [SerializeField] Button bUIOpenBtn;
    [SerializeField] Button lsUIOpenBtn;
    [SerializeField] Button tUIOpenBtn;

    [SerializeField] Button balanceSheetBtn;
    [SerializeField] Button investorAdBtn;
    [SerializeField] Button dailyBonusBtn;
    [SerializeField] Button findInvestorAdBtn;

    public GameObject bSheetOpen;
    [SerializeField] GameObject investorAdOpen;
    [SerializeField] GameObject dBonusOpen;

    [Header("Daily Bonus")]
    [SerializeField] GameObject dailyBonusDay1;
    [SerializeField] GameObject dailyBonusDay2;
    [SerializeField] GameObject dailyBonusDay3;
    [SerializeField] GameObject dailyBonusDay4;
    [SerializeField] GameObject dailyBonusDay5;
    [SerializeField] GameObject dailyBonusDay6;
    [SerializeField] GameObject dailyBonusDay7;

    [Header("Health")]
    [SerializeField] Button healthAdWatchBtn;
    [SerializeField] Button healthActBtn1;
    [SerializeField] Button healthActBtn2;
    [SerializeField] Button healthActBtn3;
    [SerializeField] Button healthActBtn4;
    [SerializeField] Button healthActBtn5;
    [SerializeField] Button healthActBtn6;
    [SerializeField] Button healthActBtn7;
    [SerializeField] Button healthActBtn8;

    [Header("Happiness")]
    [SerializeField] Button happinessAdWatchBtn;
    [SerializeField] Button happinessActBtn1;
    [SerializeField] Button happinessActBtn2;
    [SerializeField] Button happinessActBtn3;
    [SerializeField] Button happinessActBtn4;
    [SerializeField] Button happinessActBtn5;
    [SerializeField] Button happinessActBtn6;
    [SerializeField] Button happinessActBtn7;
    [SerializeField] Button happinessActBtn8;

    [Header("Activity Text Health Happiness Respect")]
    public TextMeshProUGUI healthActiText;
    public TextMeshProUGUI happinessActiText;
    public TextMeshProUGUI respectActiText;

    [Header("BalanceSheet Text")]
    public TextMeshProUGUI networthValueText;
    public TextMeshProUGUI totalIncomeValueText;
    public TextMeshProUGUI totalExpenseValueText;

    public TextMeshProUGUI salaryValueText;
    public TextMeshProUGUI mgrSalaryValueText;
    public TextMeshProUGUI foodValueText;
    public TextMeshProUGUI transpValueText;
    public TextMeshProUGUI clubValueText;
    public TextMeshProUGUI hotelValueText;
    public TextMeshProUGUI educaValueText;
    public TextMeshProUGUI itElecValueText;
    public TextMeshProUGUI mediaValueText;
    public TextMeshProUGUI financeValueText;
    public TextMeshProUGUI sportValueText;
    public TextMeshProUGUI healthValueText;
    public TextMeshProUGUI ogeValueText;
    public TextMeshProUGUI defenceValueText;

    public TextMeshProUGUI prtExpValueText;
    public TextMeshProUGUI homeExpValueText;
    public TextMeshProUGUI TransExpValueText;


    [Header("Store")]
    [SerializeField] GameObject storeUI;

    [Header("Research")]
    [SerializeField] GameObject researchUI;
    [SerializeField] GameObject researchUnlock;

    [Header("Business")]
    [SerializeField] GameObject businessUI;
    [SerializeField] GameObject businessClicked;

    [Header("Lifestyle")]
    [SerializeField] GameObject lifeStyleUI;
    [SerializeField] GameObject lsWarning;

    [Header("Trade")]
    [SerializeField] GameObject tradeUI;
    [SerializeField] GameObject tBuySell;
    [SerializeField] GameObject tradeComplete;

    [Header("Tutorial")]
    [SerializeField] int tutorialStepNum;
    public GameObject overHeadPopUp;
    [SerializeField] GameObject gameTutorial;
    [SerializeField] GameObject soundManager;
    [SerializeField] AudioClip dadDialogue;
    [SerializeField] AudioClip sonDialogue;

    [Header("Cross Buttons")]
    [SerializeField] GameObject[] doubleCrossBtns;
    [SerializeField] GameObject[] crossBtns;
    [SerializeField] GameObject[] dualCrossBtns;


    private void Awake()
    {
        CallAwakeFunc();
    }

    private void Start()
    {
        CallStartFunc();
        OnUIScaleChange();
        OnGameStart();
    }

    private void CallAwakeFunc()
    {
        gmMgr = GetComponent<GameManager>();

        settingsBtn = GameObject.Find("PauseButton").GetComponent<Button>();
        gpPlayBtn = GameObject.Find("GpPlayBtn").GetComponent<Button>();
        gpPauseBtn = GameObject.Find("GpPauseBtn").GetComponent<Button>();
        gpSpeedUpBtn = GameObject.Find("GpSpeedUpBtn").GetComponent<Button>();

        healthPlusBtn = GameObject.Find("HealthPlusBtn").GetComponent<Button>();
        happyPlusBtn = GameObject.Find("HappyPlusBtn").GetComponent<Button>();
        respectPlusBtn = GameObject.Find("RespectPlusBtn").GetComponent<Button>();

        healthActiText = GameObject.Find("HealthActiText").GetComponent<TextMeshProUGUI>();
        happinessActiText = GameObject.Find("HappinessActiText").GetComponent<TextMeshProUGUI>();
        respectActiText = GameObject.Find("RespectActiText").GetComponent<TextMeshProUGUI>();

        dailyBonusDay1 = GameObject.Find("DailyBonusDay1");
        dailyBonusDay2 = GameObject.Find("DailyBonusDay2");
        dailyBonusDay3 = GameObject.Find("DailyBonusDay3");
        dailyBonusDay4 = GameObject.Find("DailyBonusDay4");
        dailyBonusDay5 = GameObject.Find("DailyBonusDay5");
        dailyBonusDay6 = GameObject.Find("DailyBonusDay6");
        dailyBonusDay7 = GameObject.Find("DailyBonusDay7");

        homepage = GameObject.Find("HomePage");
        sUIOpenBtn = GameObject.Find("StoreOpenBtn").GetComponent<Button>();
        rUIOpenBtn = GameObject.Find("ResearchOpenBtn").GetComponent<Button>();
        bUIOpenBtn = GameObject.Find("BusinessOpenBtn").GetComponent<Button>();
        lsUIOpenBtn = GameObject.Find("LifeStyleOpenBtn").GetComponent<Button>();
        tUIOpenBtn = GameObject.Find("TradeOpenBtn").GetComponent<Button>();
        balanceSheetBtn = GameObject.Find("BalanceSheet").GetComponent<Button>();
        investorAdBtn = GameObject.Find("InvestorAD").GetComponent<Button>();
        dailyBonusBtn = GameObject.Find("DailyBonus").GetComponent<Button>();
        findInvestorAdBtn = GameObject.Find("FindInvestorAdBtn").GetComponent<Button>();

        bSheetOpen = GameObject.Find("BSheetOpen");
        investorAdOpen = GameObject.Find("InvestorAdOpen");
        dBonusOpen = GameObject.Find("DailyBonusOpen");

        healthAdWatchBtn = GameObject.Find("HealthAdWatchBtn").GetComponent<Button>();
        healthActBtn1 = GameObject.Find("HealthActBtn1").GetComponent<Button>();
        healthActBtn2 = GameObject.Find("HealthActBtn2").GetComponent<Button>();
        healthActBtn3 = GameObject.Find("HealthActBtn3").GetComponent<Button>();
        healthActBtn4 = GameObject.Find("HealthActBtn4").GetComponent<Button>();
        healthActBtn5 = GameObject.Find("HealthActBtn5").GetComponent<Button>();
        healthActBtn6 = GameObject.Find("HealthActBtn6").GetComponent<Button>();
        healthActBtn7 = GameObject.Find("HealthActBtn7").GetComponent<Button>();
        healthActBtn8 = GameObject.Find("HealthActBtn8").GetComponent<Button>();

        happinessAdWatchBtn = GameObject.Find("HappyAdWatchBtn").GetComponent<Button>();
        happinessActBtn1 = GameObject.Find("HappyActBtn1").GetComponent<Button>();
        happinessActBtn2 = GameObject.Find("HappyActBtn2").GetComponent<Button>();
        happinessActBtn3 = GameObject.Find("HappyActBtn3").GetComponent<Button>();
        happinessActBtn4 = GameObject.Find("HappyActBtn4").GetComponent<Button>();
        happinessActBtn5 = GameObject.Find("HappyActBtn5").GetComponent<Button>();
        happinessActBtn6 = GameObject.Find("HappyActBtn6").GetComponent<Button>();
        happinessActBtn7 = GameObject.Find("HappyActBtn7").GetComponent<Button>();
        happinessActBtn8 = GameObject.Find("HappyActBtn8").GetComponent<Button>();

        networthValueText = GameObject.Find("NetworthValue").GetComponent<TextMeshProUGUI>();
        totalIncomeValueText = GameObject.Find("TotalIncomeValue").GetComponent<TextMeshProUGUI>();
        totalExpenseValueText = GameObject.Find("TotalExpenseValue").GetComponent<TextMeshProUGUI>();

        salaryValueText = GameObject.Find("SalaryEarnValue").GetComponent<TextMeshProUGUI>();
        mgrSalaryValueText = GameObject.Find("MgrSalaryEarnValue").GetComponent<TextMeshProUGUI>();
        foodValueText = GameObject.Find("FoodEarnValue").GetComponent<TextMeshProUGUI>();
        transpValueText = GameObject.Find("TransportEarnValue").GetComponent<TextMeshProUGUI>();
        clubValueText = GameObject.Find("ClubLeisureEarnValue").GetComponent<TextMeshProUGUI>();
        hotelValueText = GameObject.Find("HotelTourismEarnValue").GetComponent<TextMeshProUGUI>();
        educaValueText = GameObject.Find("EducationEarnValue").GetComponent<TextMeshProUGUI>();
        itElecValueText = GameObject.Find("ITElectronicsEarnVAlue").GetComponent<TextMeshProUGUI>();
        mediaValueText = GameObject.Find("MediaCultureEarnValue").GetComponent<TextMeshProUGUI>();
        financeValueText = GameObject.Find("FinanceEarnValue").GetComponent<TextMeshProUGUI>();
        sportValueText = GameObject.Find("SportsEarnValue").GetComponent<TextMeshProUGUI>();
        healthValueText = GameObject.Find("HealthEarnValue").GetComponent<TextMeshProUGUI>();
        ogeValueText = GameObject.Find("OilGasEnergyEarnValue").GetComponent<TextMeshProUGUI>();
        defenceValueText = GameObject.Find("DefenceEarnValue").GetComponent<TextMeshProUGUI>();

        prtExpValueText = GameObject.Find("PartnerExpenseTxtValue").GetComponent<TextMeshProUGUI>();
        homeExpValueText = GameObject.Find("HomeExpenseTxtValue").GetComponent<TextMeshProUGUI>();
        TransExpValueText = GameObject.Find("TransportExpenseTxtValue").GetComponent<TextMeshProUGUI>();

        storeUI = GameObject.Find("StoreUI");

        researchUI = GameObject.Find("ResearchUI");
        researchUnlock = GameObject.Find("ResearchUnlock");

        businessUI = GameObject.Find("BusinessUI");
        businessClicked = GameObject.Find("BusinessClicked");

        lifeStyleUI = GameObject.Find("LifeStyleUI");
        lsWarning = GameObject.Find("LsWarning");

        tradeUI = GameObject.Find("TradeUI");
        tBuySell = GameObject.Find("TBuySell");
        tradeComplete = GameObject.Find("TradeComplete");

        overHeadPopUp = GameObject.Find("OverHeaPopUp");
        gameTutorial = GameObject.Find("GameTutorial");

        doubleCrossBtns = GameObject.FindGameObjectsWithTag("DoubleCrossBtn");
        crossBtns = GameObject.FindGameObjectsWithTag("CrossBtn");
        dualCrossBtns = GameObject.FindGameObjectsWithTag("DualCrossBtn");

        soundManager = GameObject.Find("SoundManager");
        dadDialogue = Resources.Load<AudioClip>("AudioClips/DadDialogue");
        sonDialogue = Resources.Load<AudioClip>("AudioClips/SonDialogue");
    }

    private void CallStartFunc()
    {
        //PlayerPrefs.DeleteAll();


        settingsBtn.onClick.AddListener(() => { OnSettingsButton(); });
        gpPlayBtn.onClick.AddListener(() => { OnGpPlayButton(); });
        gpPauseBtn.onClick.AddListener(() => { OnGpPauseButton(); });
        gpSpeedUpBtn.onClick.AddListener(() => { OnGpSpeedUpBtn(); });

        healthPlusBtn.onClick.AddListener(() => { OnHealthPlusBtn(); });
        happyPlusBtn.onClick.AddListener(() => { OnHappyPlusBtn(); });
        respectPlusBtn.onClick.AddListener(() => { OnRespectPlusBtn(); });
        dailyBonusDay1.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnDailyBonusDay1Btn(); });
        dailyBonusDay2.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnDailyBonusDay2Btn(); });
        dailyBonusDay3.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnDailyBonusDay3Btn(); });
        dailyBonusDay4.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnDailyBonusDay4Btn(); });
        dailyBonusDay5.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnDailyBonusDay5Btn(); });
        dailyBonusDay6.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnDailyBonusDay6Btn(); });
        dailyBonusDay7.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnDailyBonusDay7Btn(); });

        sUIOpenBtn.onClick.AddListener(() => { OnStoreUIOpen(); });
        rUIOpenBtn.onClick.AddListener(() => { OnResearchUIOpen(); });
        bUIOpenBtn.onClick.AddListener(() => { OnBusinessUIOpen(); });
        lsUIOpenBtn.onClick.AddListener(() => { OnLifeStyleUIOpen(); });
        tUIOpenBtn.onClick.AddListener(() => { OnTradeUIOpen(); });
        balanceSheetBtn.onClick.AddListener(() => { OnBalanceSheetOpen(); });
        investorAdBtn.onClick.AddListener(() => { OnInvestorOpen(); });
        dailyBonusBtn.onClick.AddListener(() => { OnDailyBonusOpen(); });
        findInvestorAdBtn.onClick.AddListener(() =>
        {
            gmMgr.hpScript.OnAdWatchAdCountPlus();
            OnInvestorOpen();
        });

        healthAdWatchBtn.onClick.AddListener(() => { OnHealthAdWatchBtn(); });
        healthActBtn1.onClick.AddListener(() => { OnHealthActivityBtn1(); });
        healthActBtn2.onClick.AddListener(() => { OnHealthActivityBtn2(); });
        healthActBtn3.onClick.AddListener(() => { OnHealthActivityBtn3(); });
        healthActBtn4.onClick.AddListener(() => { OnHealthActivityBtn4(); });
        healthActBtn5.onClick.AddListener(() => { OnHealthActivityBtn5(); });
        healthActBtn6.onClick.AddListener(() => { OnHealthActivityBtn6(); });
        healthActBtn7.onClick.AddListener(() => { OnHealthActivityBtn7(); });
        healthActBtn8.onClick.AddListener(() => { OnHealthActivityBtn8(); });

        happinessAdWatchBtn.onClick.AddListener(() => { OnHappinessAdWatchBtn(); });
        happinessActBtn1.onClick.AddListener(() => { OnHappyActivityBtn1(); });
        happinessActBtn2.onClick.AddListener(() => { OnHappyActivityBtn2(); });
        happinessActBtn3.onClick.AddListener(() => { OnHappyActivityBtn3(); });
        happinessActBtn4.onClick.AddListener(() => { OnHappyActivityBtn4(); });
        happinessActBtn5.onClick.AddListener(() => { OnHappyActivityBtn5(); });
        happinessActBtn6.onClick.AddListener(() => { OnHappyActivityBtn6(); });
        happinessActBtn7.onClick.AddListener(() => { OnHappyActivityBtn7(); });
        happinessActBtn8.onClick.AddListener(() => { OnHappyActivityBtn8(); });

        overHeadPopUp.transform.GetChild(5).GetChild(2).GetComponent<Button>().onClick.AddListener(() => { PlayerNameSet(); });
        overHeadPopUp.transform.GetChild(4).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { NewGameBonus(); });

        // Gameover restart button function
        overHeadPopUp.transform.GetChild(8).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnGameOverRestart(); });
        overHeadPopUp.transform.GetChild(8).GetChild(2).GetComponent<Button>().onClick.AddListener(() => { OnGameOverResurrect(); });

        // Assigning Cross Button Disable Function
        for (int i = 0; i < doubleCrossBtns.Length; i++)
        {
            int a = i;
            doubleCrossBtns[a].GetComponent<Button>().onClick.AddListener(delegate
            {
                doubleCrossBtns[a].transform.parent.parent.gameObject.SetActive(false);
            });
        }
        for (int i = 0; i < crossBtns.Length; i++)
        {
            int a = i;
            crossBtns[a].GetComponent<Button>().onClick.AddListener(delegate
            {
                crossBtns[a].transform.parent.gameObject.SetActive(false);
            });
        }
        for (int i = 0; i < dualCrossBtns.Length; i++)
        {
            int a = i;
            dualCrossBtns[a].GetComponent<Button>().onClick.AddListener(delegate
            {
                dualCrossBtns[a].transform.parent.gameObject.SetActive(false);
                dualCrossBtns[a].transform.parent.parent.gameObject.SetActive(false);
            });
        }
        tBuySell.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnBuySellClose(); });
    }

    private void OnUIScaleChange()
    {
        // Homepage Home, Partner & Transport Scale
        float hpHeight = homepage.transform.GetChild(2).GetComponent<RectTransform>().rect.height;
        float hpWidth = homepage.transform.GetChild(2).GetComponent<RectTransform>().rect.width;


        homepage.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(hpWidth, hpHeight / 2);
        homepage.transform.GetChild(2).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(hpWidth / 2, hpHeight / 2);
        homepage.transform.GetChild(2).GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(hpWidth / 2, hpHeight / 2);

        // Trade Buy & Sell Scale

        float trHeight = tradeUI.GetComponent<RectTransform>().rect.height;
        float trWidth = tradeUI.GetComponent<RectTransform>().rect.width;
        trHeight -= 200;
        tradeUI.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(trWidth, trHeight / 2);
        tradeUI.transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2(trWidth, trHeight / 2);
    }

    #region Homepage UI

    #region Game Play Pause
    void OnSettingsButton()
    {
        overHeadPopUp.SetActive(true);
        overHeadPopUp.transform.GetChild(0).gameObject.SetActive(true);
    }

    void OnGpPlayButton()
    {
        gpPlayBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 110);
        gpPauseBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
        gpSpeedUpBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);

        gpPlayBtn.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(35, gpPlayBtn.transform.GetComponent<RectTransform>().anchoredPosition.y);
        gpPauseBtn.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, gpPauseBtn.transform.GetComponent<RectTransform>().anchoredPosition.y);
        gpSpeedUpBtn.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, gpSpeedUpBtn.transform.GetComponent<RectTransform>().anchoredPosition.y);

        gmMgr.hpScript.OnGameSpeedNrml();
    }

    void OnGpPauseButton()
    {
        gpPlayBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
        gpPauseBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 110);
        gpSpeedUpBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);

        gpPlayBtn.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(35, gpPlayBtn.transform.GetComponent<RectTransform>().anchoredPosition.y);
        gpPauseBtn.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, gpPauseBtn.transform.GetComponent<RectTransform>().anchoredPosition.y);
        gpSpeedUpBtn.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-35, gpSpeedUpBtn.transform.GetComponent<RectTransform>().anchoredPosition.y);

        gmMgr.hpScript.isTimeStopped = true;
    }

    void OnGpSpeedUpBtn()
    {
        gpPlayBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
        gpPauseBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
        gpSpeedUpBtn.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 110);

        gpPlayBtn.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, gpPlayBtn.transform.GetComponent<RectTransform>().anchoredPosition.y);
        gpPauseBtn.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, gpPauseBtn.transform.GetComponent<RectTransform>().anchoredPosition.y);
        gpSpeedUpBtn.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-35, gpSpeedUpBtn.transform.GetComponent<RectTransform>().anchoredPosition.y);

        gmMgr.hpScript.OnGameSpeedUp();
    }

    #endregion

    #region Activity Panel Health Happiness Respect

    void OnHealthPlusBtn()
    {
        overHeadPopUp.SetActive(true);
        overHeadPopUp.transform.GetChild(1).gameObject.SetActive(true);

        healthActBtn1.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.healthActPerct[0] + "%";
        healthActBtn2.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.healthActPerct[1] + "%";
        healthActBtn3.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.healthActPerct[2] + "%";
        healthActBtn4.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.healthActPerct[3] + "%";
        healthActBtn5.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.healthActPerct[4] + "%";
        healthActBtn6.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.healthActPerct[5] + "%";
        healthActBtn7.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.healthActPerct[6] + "%";
        healthActBtn8.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.healthActPerct[7] + "%";

        healthActBtn1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gmMgr.homePageSO.healthActCost[0];
        healthActBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.healthActCost[1];
        healthActBtn3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.healthActCost[2];
        healthActBtn4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.healthActCost[3];
        healthActBtn5.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.healthActCost[4];
        healthActBtn6.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.healthActCost[5];
        healthActBtn7.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.healthActCost[6];
        healthActBtn8.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.healthActCost[7];

        healthActiText.text = "Health: " + gmMgr.hpScript.healthPercentage + "%";
    }

    void OnHappyPlusBtn()
    {
        overHeadPopUp.SetActive(true);
        overHeadPopUp.transform.GetChild(2).gameObject.SetActive(true);

        happinessActBtn1.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.happyActPerct[0] + "%";
        happinessActBtn2.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.happyActPerct[1] + "%";
        happinessActBtn3.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.happyActPerct[2] + "%";
        happinessActBtn4.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.happyActPerct[3] + "%";
        happinessActBtn5.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.happyActPerct[4] + "%";
        happinessActBtn6.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.happyActPerct[5] + "%";
        happinessActBtn7.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.happyActPerct[6] + "%";
        happinessActBtn8.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.homePageSO.happyActPerct[7] + "%";

        happinessActBtn1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gmMgr.homePageSO.happyActCost[0];
        happinessActBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.happyActCost[1];
        happinessActBtn3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.happyActCost[2];
        happinessActBtn4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.happyActCost[3];
        happinessActBtn5.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.happyActCost[4];
        happinessActBtn6.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.happyActCost[5];
        happinessActBtn7.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.happyActCost[6];
        happinessActBtn8.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.happyActCost[7];

        happinessActiText.text = "Happiness: " + gmMgr.hpScript.happyPercentage + "%";
    }

    void OnRespectPlusBtn()
    {
        storeUI.SetActive(true);
    }

    #endregion

    private IEnumerator DisableButtonFor5Sec(Button btn)
    {
        yield return new WaitForSeconds(5);
        btn.interactable = true;
    }

    #region Health Activity Button

    public void OnHealthAdWatchBtn()
    {
        gmMgr.hpScript.healthPercentage += 20;
        gmMgr.hpScript.UpdateHeHaReBar();
        gmMgr.hpScript.OnAdWatchAdCountPlus();
    }

    public void OnHealthActivityBtn1()
    {
        gmMgr.hpScript.healthPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[0]);
        gmMgr.hpScript.UpdateHeHaReBar();

        healthActBtn1.interactable = false;
        StartCoroutine(DisableButtonFor5Sec(healthActBtn1));
    }

    public void OnHealthActivityBtn2()
    {
        string cost = healthActBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.healthPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[1]);

            // Deducting Coins
            string newCoins = healthActBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            healthActBtn2.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(healthActBtn2));
        }
    }

    public void OnHealthActivityBtn3()
    {
        string cost = healthActBtn3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.healthPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[2]);

            // Deducting Coins
            string newCoins = healthActBtn3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            healthActBtn3.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(healthActBtn3));
        }
    }

    public void OnHealthActivityBtn4()
    {
        string cost = healthActBtn4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.healthPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[3]);

            // Deducting Coins
            string newCoins = healthActBtn4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            healthActBtn4.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(healthActBtn4));
        }
    }

    public void OnHealthActivityBtn5()
    {
        string cost = healthActBtn5.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.healthPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[4]);

            // Deducting Coins
            string newCoins = healthActBtn5.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            healthActBtn5.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(healthActBtn5));
        }
    }

    public void OnHealthActivityBtn6()
    {
        string cost = healthActBtn6.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.healthPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[5]);

            // Deducting Coins
            string newCoins = healthActBtn6.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            healthActBtn6.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(healthActBtn6));
        }
    }

    public void OnHealthActivityBtn7()
    {
        string cost = healthActBtn7.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.healthPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[6]);

            // Deducting Coins
            string newCoins = healthActBtn7.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            healthActBtn7.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(healthActBtn7));
        }
    }

    public void OnHealthActivityBtn8()
    {
        string cost = healthActBtn8.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.healthPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[7]);

            // Deducting Coins
            string newCoins = healthActBtn8.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            healthActBtn8.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(healthActBtn8));
        }
    }

    #endregion

    #region Happiness Activity Button


    public void OnHappinessAdWatchBtn()
    {
        gmMgr.hpScript.happyPercentage += 20f;
        gmMgr.hpScript.UpdateHeHaReBar();
        gmMgr.hpScript.OnAdWatchAdCountPlus();
    }

    public void OnHappyActivityBtn1()
    {
        gmMgr.hpScript.happyPercentage += int.Parse(gmMgr.homePageSO.happyActPerct[0]);
        gmMgr.hpScript.UpdateHeHaReBar();

        happinessActBtn1.interactable = false;
        StartCoroutine(DisableButtonFor5Sec(happinessActBtn1));
    }

    public void OnHappyActivityBtn2()
    {
        string cost = happinessActBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.happyPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[1]);

            // Deducting Coins
            string newCoins = happinessActBtn2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            happinessActBtn2.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(happinessActBtn2));
        }
    }

    public void OnHappyActivityBtn3()
    {
        string cost = happinessActBtn3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.happyPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[2]);

            // Deducting Coins
            string newCoins = happinessActBtn3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            happinessActBtn3.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(happinessActBtn3));
        }
    }

    public void OnHappyActivityBtn4()
    {
        string cost = happinessActBtn4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.happyPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[3]);

            // Deducting Coins
            string newCoins = happinessActBtn4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            happinessActBtn4.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(happinessActBtn4));
        }
    }

    public void OnHappyActivityBtn5()
    {
        string cost = happinessActBtn5.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.happyPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[4]);

            // Deducting Coins
            string newCoins = happinessActBtn5.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            happinessActBtn5.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(happinessActBtn5));
        }
    }

    public void OnHappyActivityBtn6()
    {
        string cost = happinessActBtn6.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.happyPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[5]);

            // Deducting Coins
            string newCoins = happinessActBtn6.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            happinessActBtn6.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(happinessActBtn6));
        }
    }

    public void OnHappyActivityBtn7()
    {
        string cost = happinessActBtn7.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.happyPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[6]);

            // Deducting Coins
            string newCoins = happinessActBtn7.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            happinessActBtn7.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(happinessActBtn7));
        }
    }

    public void OnHappyActivityBtn8()
    {
        string cost = happinessActBtn8.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
        if (gmMgr.hpScript.CashCoin >= decimal.Parse(cost))
        {
            // Incresing Fillbar %
            gmMgr.hpScript.happyPercentage += int.Parse(gmMgr.homePageSO.healthActPerct[7]);

            // Deducting Coins
            string newCoins = happinessActBtn8.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(1);
            gmMgr.hpScript.CashCoin -= decimal.Parse(newCoins);
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.hpScript.UpdateCredit();

            // Disabling Button for Cool Down Period
            happinessActBtn8.interactable = false;
            StartCoroutine(DisableButtonFor5Sec(happinessActBtn8));
        }
    }

    #endregion

    #region Daily Bonus Day Buttons

    void OnDailyBonusDay1Btn()
    {
        dailyBonusDay1.transform.GetChild(1).gameObject.SetActive(false);
        dailyBonusDay1.transform.GetChild(2).gameObject.SetActive(true);

        DateTime currentDate = DateTime.Now;
        gmMgr.hpScript.dbStartDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
        string yrMnDt = currentDate.Year + ", " + currentDate.Month + "," + currentDate.Day;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("dbStartDate", yrMnDt);

        // Add Daily Bonus Amount to Cash/Coins
        decimal dailyBonus = decimal.Parse(gmMgr.homePageSO.dbAmounts[0]);
        gmMgr.hpScript.CashCoin += dailyBonus;
        gmMgr.hpScript.UpdateCredit();

        // Increment the current day
        gmMgr.hpScript.dbCollectedDay = 1;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("dbCollectedDay", gmMgr.hpScript.dbCollectedDay.ToString());
    }

    void OnDailyBonusDay2Btn()
    {
        dailyBonusDay2.transform.GetChild(1).gameObject.SetActive(false);
        dailyBonusDay2.transform.GetChild(2).gameObject.SetActive(true);

        // Add Daily Bonus Amount to Cash/Coins
        decimal dailyBonus = decimal.Parse(gmMgr.homePageSO.dbAmounts[1]);
        gmMgr.hpScript.CashCoin += dailyBonus;
        gmMgr.hpScript.UpdateCredit();

        // Increment the current day
        gmMgr.hpScript.dbCollectedDay = 2;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("dbCollectedDay", gmMgr.hpScript.dbCollectedDay.ToString());
    }

    void OnDailyBonusDay3Btn()
    {
        dailyBonusDay3.transform.GetChild(1).gameObject.SetActive(false);
        dailyBonusDay3.transform.GetChild(2).gameObject.SetActive(true);

        // Add Daily Bonus Amount to Cash/Coins
        decimal dailyBonus = decimal.Parse(gmMgr.homePageSO.dbAmounts[2]);
        gmMgr.hpScript.CashCoin += dailyBonus;
        gmMgr.hpScript.UpdateCredit();

        // Increment the current day
        gmMgr.hpScript.dbCollectedDay = 3;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("dbCollectedDay", gmMgr.hpScript.dbCollectedDay.ToString());
    }

    void OnDailyBonusDay4Btn()
    {
        dailyBonusDay4.transform.GetChild(1).gameObject.SetActive(false);
        dailyBonusDay4.transform.GetChild(2).gameObject.SetActive(true);

        // Add Daily Bonus Amount to Cash/Coins
        decimal dailyBonus = decimal.Parse(gmMgr.homePageSO.dbAmounts[3]);
        gmMgr.hpScript.CashCoin += dailyBonus;
        gmMgr.hpScript.UpdateCredit();

        // Increment the current day
        gmMgr.hpScript.dbCollectedDay = 4;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("dbCollectedDay", gmMgr.hpScript.dbCollectedDay.ToString());
    }

    void OnDailyBonusDay5Btn()
    {
        dailyBonusDay5.transform.GetChild(1).gameObject.SetActive(false);
        dailyBonusDay5.transform.GetChild(2).gameObject.SetActive(true);

        // Add Daily Bonus Amount to Cash/Coins
        decimal dailyBonus = decimal.Parse(gmMgr.homePageSO.dbAmounts[4]);
        gmMgr.hpScript.CashCoin += dailyBonus;
        gmMgr.hpScript.UpdateCredit();

        // Increment the current day
        gmMgr.hpScript.dbCollectedDay = 5;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("dbCollectedDay", gmMgr.hpScript.dbCollectedDay.ToString());
    }

    void OnDailyBonusDay6Btn()
    {
        dailyBonusDay6.transform.GetChild(1).gameObject.SetActive(false);
        dailyBonusDay6.transform.GetChild(2).gameObject.SetActive(true);

        // Add Daily Bonus Amount to Cash/Coins
        decimal dailyBonus = decimal.Parse(gmMgr.homePageSO.dbAmounts[5]);
        gmMgr.hpScript.CashCoin += dailyBonus;
        gmMgr.hpScript.UpdateCredit();

        // Increment the current day
        gmMgr.hpScript.dbCollectedDay = 6;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("dbCollectedDay", gmMgr.hpScript.dbCollectedDay.ToString());
    }

    void OnDailyBonusDay7Btn()
    {
        dailyBonusDay7.transform.GetChild(1).gameObject.SetActive(false);
        dailyBonusDay7.transform.GetChild(2).gameObject.SetActive(true);

        // Add Daily Bonus Amount to Cash/Coins
        decimal dailyBonus = decimal.Parse(gmMgr.homePageSO.dbAmounts[6]);
        gmMgr.hpScript.CashCoin += dailyBonus;
        gmMgr.hpScript.UpdateCredit();

        // Increment the current day
        gmMgr.hpScript.dbCollectedDay = 7;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("dbCollectedDay", gmMgr.hpScript.dbCollectedDay.ToString());
    }

    #endregion

    #region UI Open Btn

    void OnStoreUIOpen()
    {
        storeUI.SetActive(true);
    }
    void OnResearchUIOpen()
    {
        researchUI.SetActive(true);
    }
    void OnBusinessUIOpen()
    {
        businessUI.SetActive(true);
        gmMgr.bizScript.OnToggleTab(0);
    }
    void OnLifeStyleUIOpen()
    {
        lifeStyleUI.SetActive(true);
        gmMgr.lsScript.OnToggleTab(0);
    }
    void OnTradeUIOpen()
    {
        tradeUI.SetActive(true);
    }


    decimal businessWorth;
    decimal lifestyleWorth;
    public void OnBalanceSheetOpen()
    {
        bSheetOpen.SetActive(true);
        businessWorth = 0;
        lifestyleWorth = 0;

        // All business Revenue []
        List<decimal> allCost = new List<decimal>();
        for (int i = 0; i < gmMgr.researchSO.researchName.Length; i++)
        {
            decimal cost = (gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl1Reven[i]) * int.Parse(gmMgr.businessSO.bLvl1Owned[i])) +
                            (gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl2Reven[i]) * int.Parse(gmMgr.businessSO.bLvl2Owned[i])) +
                            (gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl3Reven[i]) * int.Parse(gmMgr.businessSO.bLvl3Owned[i]));
            allCost.Add(cost);
        }

        // Business worth
        for (int i = 0; i < gmMgr.businessSO.bLvl1Names.Length; i++)
        {
            businessWorth += (gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl1Owned[i])) * (gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl1Cost[i]));
            businessWorth += (gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl2Owned[i])) * (gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl2Cost[i]));
            businessWorth += (gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl3Owned[i])) * (gmMgr.hpScript.ConvStrgToDecimal(gmMgr.businessSO.bLvl3Cost[i]));
        }

        // Lifestyle worth
        for (int i = 0; i < gmMgr.lifeStyleSO.homeName.Length; i++)
        {
            if (gmMgr.lifeStyleSO.homeStatus[i] == "True")
            {
                lifestyleWorth += gmMgr.hpScript.ConvStrgToDecimal(gmMgr.lifeStyleSO.homeCost[i]);
            }
            if (gmMgr.lifeStyleSO.carsStatus[i] == "True")
            {
                lifestyleWorth += gmMgr.hpScript.ConvStrgToDecimal(gmMgr.lifeStyleSO.carsCost[i]);
            }
        }

        int electNum = gmMgr.elecScript.electionNum;
        string salary = gmMgr.homePageSO.electedSalary[electNum];
        decimal playerSalary = gmMgr.hpScript.ConvStrgToDecimal(salary);
        networthValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(gmMgr.hpScript.CashCoin + gmMgr.hpScript.monthlyIncome 
                                    + gmMgr.hpScript.monthlyExpense + businessWorth + lifestyleWorth + playerSalary);
        totalIncomeValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(gmMgr.hpScript.monthlyIncome + playerSalary);
        totalExpenseValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(gmMgr.hpScript.monthlyExpense);

        salaryValueText.text = "$" + salary;
        foodValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[0]);
        transpValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[1]);
        clubValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[2]);
        hotelValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[3]);
        educaValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[4]);
        itElecValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[5]);
        mediaValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[6]); ;
        financeValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[7]);
        sportValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[8]);
        healthValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[9]);
        ogeValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[10]);
        defenceValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(allCost[11]);

        gmMgr.bizScript.OnManagerSalary();
        mgrSalaryValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(gmMgr.bizScript.mgrSalary);
        prtExpValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(gmMgr.lsScript.partExpense);
        homeExpValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(gmMgr.lsScript.homeExpense);
        TransExpValueText.text = "$" + gmMgr.hpScript.ConvDecimalToStrg(gmMgr.lsScript.transExpense);
    }

    void OnInvestorOpen()
    {
        investorAdOpen.SetActive(true);
        investorAdOpen.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Get " + gmMgr.hpScript.adBonusVal + " From Angel Investor";
    }



    void OnDailyBonusOpen()
    {
        dBonusOpen.SetActive(true);

        // Check the current system date
        DateTime currentDate = DateTime.Now;

        TimeSpan timeSinceCycleStart = currentDate.Subtract(gmMgr.hpScript.dbStartDate);
        int daysSinceCycleStart = timeSinceCycleStart.Days;

        // If coming after long time or Reset Daily Bonus
        if (daysSinceCycleStart > 6)
        {
            gmMgr.hpScript.dbStartDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
            daysSinceCycleStart = 0;

            dailyBonusDay1.transform.GetChild(1).gameObject.SetActive(true);
            dailyBonusDay1.transform.GetChild(2).gameObject.SetActive(false);
            dailyBonusDay1.transform.GetChild(1).GetComponent<Button>().interactable = true;

            dailyBonusDay2.transform.GetChild(1).gameObject.SetActive(true);
            dailyBonusDay2.transform.GetChild(2).gameObject.SetActive(false);
            dailyBonusDay2.transform.GetChild(1).GetComponent<Button>().interactable = false;

            dailyBonusDay3.transform.GetChild(1).gameObject.SetActive(true);
            dailyBonusDay3.transform.GetChild(2).gameObject.SetActive(false);
            dailyBonusDay3.transform.GetChild(1).GetComponent<Button>().interactable = false;

            dailyBonusDay4.transform.GetChild(1).gameObject.SetActive(true);
            dailyBonusDay4.transform.GetChild(2).gameObject.SetActive(false);
            dailyBonusDay4.transform.GetChild(1).GetComponent<Button>().interactable = false;

            dailyBonusDay5.transform.GetChild(1).gameObject.SetActive(true);
            dailyBonusDay5.transform.GetChild(2).gameObject.SetActive(false);
            dailyBonusDay5.transform.GetChild(1).GetComponent<Button>().interactable = false;

            dailyBonusDay6.transform.GetChild(1).gameObject.SetActive(true);
            dailyBonusDay6.transform.GetChild(2).gameObject.SetActive(false);
            dailyBonusDay6.transform.GetChild(1).GetComponent<Button>().interactable = false;

            dailyBonusDay7.transform.GetChild(1).gameObject.SetActive(true);
            dailyBonusDay7.transform.GetChild(2).gameObject.SetActive(false);
            dailyBonusDay7.transform.GetChild(1).GetComponent<Button>().interactable = false;
        }

        // Calculate the current day of the bonus cycle
        int currentCycleDay = (daysSinceCycleStart % gmMgr.hpScript.numberOfDays) + 1;

        // Check if it's the last day of the cycle, if so, reset to day 1
        if (gmMgr.hpScript.dbCollectedDay > gmMgr.hpScript.numberOfDays)
        {
            gmMgr.hpScript.dbCollectedDay = 0;
        }


        if (currentCycleDay == 1)
        {
            dailyBonusDay1.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        else if (currentCycleDay == 2)
        {
            dailyBonusDay2.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        else if (currentCycleDay == 3)
        {
            dailyBonusDay3.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        else if (currentCycleDay == 4)
        {
            dailyBonusDay4.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        else if (currentCycleDay == 5)
        {
            dailyBonusDay5.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        else if (currentCycleDay == 6)
        {
            dailyBonusDay6.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }
        else if (currentCycleDay == 7)
        {
            dailyBonusDay7.transform.GetChild(1).GetComponent<Button>().interactable = true;
        }


        if (gmMgr.hpScript.dbCollectedDay == 1)
        {
            dailyBonusDay1.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay1.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (gmMgr.hpScript.dbCollectedDay == 2)
        {
            dailyBonusDay1.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay1.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay2.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay2.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (gmMgr.hpScript.dbCollectedDay == 3)
        {
            dailyBonusDay1.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay1.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay2.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay2.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay3.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay3.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (gmMgr.hpScript.dbCollectedDay == 4)
        {
            dailyBonusDay1.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay1.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay2.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay2.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay3.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay3.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay4.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay4.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (gmMgr.hpScript.dbCollectedDay == 5)
        {
            dailyBonusDay1.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay1.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay2.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay2.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay3.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay3.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay4.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay4.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay5.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay5.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (gmMgr.hpScript.dbCollectedDay == 6)
        {
            dailyBonusDay1.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay1.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay2.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay2.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay3.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay3.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay4.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay4.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay5.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay5.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay6.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay6.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (gmMgr.hpScript.dbCollectedDay == 7)
        {
            dailyBonusDay1.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay1.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay2.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay2.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay3.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay3.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay4.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay4.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay5.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay5.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay6.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay6.transform.GetChild(2).gameObject.SetActive(true);
            dailyBonusDay7.transform.GetChild(1).gameObject.SetActive(false);
            dailyBonusDay7.transform.GetChild(2).gameObject.SetActive(true);
        }


        //Amount Display
        dailyBonusDay1.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.dbAmounts[0];
        dailyBonusDay2.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.dbAmounts[1];
        dailyBonusDay3.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.dbAmounts[2];
        dailyBonusDay4.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.dbAmounts[3];
        dailyBonusDay5.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.dbAmounts[4];
        dailyBonusDay6.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.dbAmounts[5];
        dailyBonusDay7.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$" + gmMgr.homePageSO.dbAmounts[6];
    }


    #endregion

    #region UI Close Btn

    void OnBalanceSheetClose()
    {
        bSheetOpen.SetActive(false);
    }

    void OnInvestorClose()
    {
        investorAdOpen.SetActive(false);
    }

    void OnDailyBonusClose()
    {
        dBonusOpen.SetActive(false);
    }

    #endregion

    #endregion

    #region Store UI
    public void OnStoreUIClose()
    {
        storeUI.SetActive(false);
    }

    #endregion

    #region Research UI

    public void OnResearchUIClose()
    {
        researchUI.SetActive(false);
    }

    public void OnResearchUnlockClose()
    {
        researchUnlock.SetActive(false);
        gmMgr.rsrchScript.isRReqPanelopen = false;
    }

    #endregion

    #region Business UI

    public void OnBusinessUIClose()
    {
        businessUI.SetActive(false);
    }

    public void OnBusinessInsideClose()
    {
        businessClicked.SetActive(false);
    }

    #endregion

    #region LifeStyle UI

    public void OnLifeStyleUIClose()
    {
        lifeStyleUI.SetActive(false);
    }

    #endregion

    #region Trade UI

    public void OnTradeUIClose()
    {
        tradeUI.SetActive(false);
    }
    public void OnBuySellClose()
    {
        gmMgr.trdScript.isTpanelOpen = false;
        gmMgr.trdScript.isTradeExist = false;
        gmMgr.trdScript.OnIntradayToggle(false);
        tBuySell.SetActive(false);
    }

    #endregion

    #region Game Over 

    public void OnGameOverRestart()
    {
        overHeadPopUp.transform.GetChild(8).gameObject.SetActive(false);
        overHeadPopUp.SetActive(false);
        OnStoreUIClose();
        OnResearchUIClose();
        OnBusinessUIClose();
        OnLifeStyleUIClose();
        OnTradeUIClose();

        gmMgr.GetDBDataToSO();
        gmMgr.gsSaveMgrSql.CreateTable();
        gmMgr.gsSaveMgrSql.AddAllEntryToTable();
        gmMgr.gsSaveMgrSql.InitialGameDataCheck();

        gmMgr.rsrchScript.OnResearchUpdate();
        gmMgr.bizScript.OnBusinessUpdate();
        gmMgr.lsScript.OnLsPartnerUpdate();
        gmMgr.lsScript.OnLsHomeUpdate();
        gmMgr.lsScript.OnLsTransportUpdate();
        gmMgr.hpScript.UpdateCredit();
        gmMgr.hpScript.UpdateHeHaReBar();
    }

    private void OnGameOverResurrect()
    {
        overHeadPopUp.transform.GetChild(8).gameObject.SetActive(false);
        overHeadPopUp.SetActive(false);
        OnStoreUIClose();
        OnResearchUIClose();
        OnBusinessUIClose();
        OnLifeStyleUIClose();
        OnTradeUIClose();
        gmMgr.hpScript.happyPercentage = 100;
        gmMgr.hpScript.healthPercentage = 100;
        gmMgr.lsScript.currentPartner = gmMgr.lifeStyleSO.partnersName[0];
        gmMgr.lsScript.currentHome = gmMgr.lifeStyleSO.homeName[0];
        gmMgr.lsScript.currentTransport = gmMgr.lifeStyleSO.carsName[0];
        gmMgr.hpScript.UpdateCredit();
        gmMgr.hpScript.UpdateHeHaReBar();
        gmMgr.lsScript.OnLsPartnerUpdate();
        gmMgr.lsScript.OnLsHomeUpdate();
        gmMgr.lsScript.OnLsTransportUpdate();

        gmMgr.hpScript.OnAdWatchAdCountPlus();
    }

    #endregion

    #region Game Start 
    public void OnGameStart()
    {
        researchUI.SetActive(false);
        businessUI.SetActive(false);
        lsWarning.SetActive(false);
        lifeStyleUI.SetActive(false);
        tradeComplete.SetActive(false);
        tBuySell.SetActive(false);
        tradeUI.SetActive(false);
        storeUI.SetActive(false);
        overHeadPopUp.transform.GetChild(0).gameObject.SetActive(false);
        overHeadPopUp.transform.GetChild(1).gameObject.SetActive(false);
        overHeadPopUp.transform.GetChild(2).gameObject.SetActive(false);
        overHeadPopUp.transform.GetChild(3).gameObject.SetActive(false);
        overHeadPopUp.transform.GetChild(8).gameObject.SetActive(false);
        gameTutorial.SetActive(false);

        OnBalanceSheetClose();
        OnInvestorClose();
        gmMgr.elecScript.electionUI.SetActive(false);
        homepage.transform.GetChild(2).GetChild(12).gameObject.SetActive(false);
        OnDailyBonusClose();
        OnGpPlayButton();

        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            StartCoroutine(OnGameStoryStart());
        }
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            foreach (Transform t in overHeadPopUp.transform)
            {
                t.gameObject.SetActive(false);
            }
            overHeadPopUp.SetActive(false);
            StartCoroutine(waitingPeriod());
        }
    }

    private IEnumerator waitingPeriod()
    {
        yield return new WaitForSeconds(0.001f);
        homepage.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerName");
        gmMgr.hpScript.onGameOpen = true;
        OnGpPlayButton();
    }

    #endregion

    #region Tutorial

    public IEnumerator OnGameStoryStart()
    {
        gmMgr.hpScript.isTimeStopped = true;
        gmMgr.hpScript.onGameOpen = false;
        yield return new WaitForSeconds(1);
        soundManager.GetComponent<AudioSource>().PlayOneShot(dadDialogue);

        researchUI.SetActive(true);
        OnResearchUIOpen();
        yield return new WaitForSeconds(0.001f);

        businessUI.SetActive(true);
        OnBusinessUIOpen();
        yield return new WaitForSeconds(0.001f);
        gmMgr.bizScript.OnToggleTab(1);
        yield return new WaitForSeconds(0.001f);
        gmMgr.bizScript.OnToggleTab(2);
        yield return new WaitForSeconds(0.001f);

        lifeStyleUI.SetActive(true);
        OnLifeStyleUIOpen();
        yield return new WaitForSeconds(0.001f);
        gmMgr.lsScript.OnToggleTab(1);
        yield return new WaitForSeconds(0.001f);
        gmMgr.lsScript.OnToggleTab(2);
        yield return new WaitForSeconds(0.001f);
        gmMgr.lsScript.OnToggleTab(3);
        yield return new WaitForSeconds(0.001f);
        gmMgr.lsScript.OnToggleTab(4);
        yield return new WaitForSeconds(0.001f);


        yield return new WaitForSeconds(12);
        overHeadPopUp.transform.GetChild(7).gameObject.SetActive(false);
        soundManager.GetComponent<AudioSource>().PlayOneShot(sonDialogue);

        OnResearchUIClose();
        OnBusinessUIClose();
        OnLifeStyleUIClose();

        yield return new WaitForSeconds(12);
        overHeadPopUp.transform.GetChild(6).gameObject.SetActive(false);
    }

    public void PlayerNameSet()
    {
        overHeadPopUp.transform.GetChild(5).gameObject.SetActive(false);
        string playerName = overHeadPopUp.transform.GetChild(5).GetChild(1).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text;
        homepage.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = playerName;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("PlayerName", playerName);
    }

    public void NewGameBonus()
    {
        overHeadPopUp.transform.GetChild(4).gameObject.SetActive(false);
        overHeadPopUp.SetActive(false);
        string playerName = overHeadPopUp.transform.GetChild(5).GetChild(1).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text;
        PlayerPrefs.SetString("PlayerName", playerName);
        gmMgr.hpScript.CashCoin = 5000;

        gmMgr.hpScript.UpdateCredit();

        tutorialStepNum = 0;
        StartCoroutine(OnGameTutorialStart());
    }

    public IEnumerator OnGameTutorialStart()
    {
        // Initial Tutorial Setup
        gmMgr.hpScript.isTimeStopped = false;
        yield return new WaitForSeconds(1);
        gmMgr.hpScript.isTimeStopped = true;
        OnStoreUIClose();
        OnResearchUIClose();
        OnBusinessUIClose();
        OnLifeStyleUIClose();
        OnTradeUIClose();
        overHeadPopUp.SetActive(true);
        gameTutorial.SetActive(true);
        gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hello There, I will be guiding you throughout your journey.";
        gameTutorial.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
    }

    public IEnumerator TutorialNextBtn()
    {
        if (tutorialStepNum == 0)
        {
            //Debug.Log("0");
            gameTutorial.transform.GetChild(2).gameObject.SetActive(false);
            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "First we need to do some research.\nClick on RESEARCH button.";

            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(600, 250);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 0);

            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = false;

            GameObject go = homepage.transform.GetChild(1).GetChild(1).gameObject;
            Instantiate(go, homepage.transform.GetChild(1));
            yield return new WaitForSeconds(0.00001f);

            homepage.transform.GetChild(1).GetChild(5).SetParent(gameTutorial.transform.GetChild(1));
            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 1)
        {
            //Debug.Log("1");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            OnResearchUIOpen();

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Let's start the Food research.";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(150, -350);

            Transform researchContent = researchUI.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0);
            researchContent.GetComponent<ContentSizeFitter>().enabled = false;
            researchContent.GetComponent<VerticalLayoutGroup>().enabled = false;

            GameObject go = researchContent.GetChild(0).gameObject;
            Instantiate(go, researchContent);
            yield return new WaitForSeconds(0.00001f);

            researchContent.GetChild(12).SetParent(gameTutorial.transform.GetChild(1));
            researchContent.GetComponent<ContentSizeFitter>().enabled = true;
            researchContent.GetComponent<VerticalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 2)
        {
            //Debug.Log("2");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gmMgr.rsrchScript.OnResearchInitiate("Food", 0);

            GameObject go = researchUI.transform.GetChild(3).gameObject;
            Instantiate(go, researchUI.transform);
            yield return new WaitForSeconds(0.00001f);

            researchUI.transform.GetChild(4).SetParent(gameTutorial.transform.GetChild(1));
            gameTutorial.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Looks like we are missing some requirements.";

            float height = gameTutorial.GetComponent<RectTransform>().rect.height;
            height = height / 2;
            height -= 150;
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, height);

            gameTutorial.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 3)
        {
            //Debug.Log("3");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            researchUI.transform.GetChild(3).gameObject.SetActive(false);
            researchUI.SetActive(false);

            gameTutorial.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "I think we need to add some lifestyles.";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 150);

            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = false;

            GameObject go = homepage.transform.GetChild(1).GetChild(3).gameObject;
            Instantiate(go, homepage.transform.GetChild(1));
            yield return new WaitForSeconds(0.00001f);

            homepage.transform.GetChild(1).GetChild(5).SetParent(gameTutorial.transform.GetChild(1));
            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = true;
            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 4)
        {
            //Debug.Log("4");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            OnLifeStyleUIOpen();

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Select your partner.";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(150, -350);

            Transform partnerContent = lifeStyleUI.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0);
            partnerContent.GetComponent<ContentSizeFitter>().enabled = false;
            partnerContent.GetComponent<VerticalLayoutGroup>().enabled = false;

            GameObject go = partnerContent.GetChild(0).gameObject;
            Instantiate(go, partnerContent);
            yield return new WaitForSeconds(0.00001f);

            partnerContent.GetChild(13).SetParent(gameTutorial.transform.GetChild(1));
            partnerContent.GetComponent<ContentSizeFitter>().enabled = true;
            partnerContent.GetComponent<VerticalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 5)
        {
            //Debug.Log("5");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gmMgr.lsScript.OnPartnerAccept(gmMgr.lifeStyleSO.partnersName[0]);

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Now you have a partner let's get you a Home.";
            lifeStyleUI.transform.GetChild(2).GetComponent<HorizontalLayoutGroup>().enabled = false;

            GameObject go = lifeStyleUI.transform.GetChild(2).GetChild(1).gameObject;
            Instantiate(go, lifeStyleUI.transform.GetChild(2));
            yield return new WaitForSeconds(0.00001f);

            lifeStyleUI.transform.GetChild(2).GetChild(3).SetParent(gameTutorial.transform.GetChild(1));
            lifeStyleUI.transform.GetChild(2).GetComponent<HorizontalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 6)
        {
            //Debug.Log("6");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gmMgr.lsScript.OnToggleTab(1);

            Transform homeContent = lifeStyleUI.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(1);
            homeContent.GetComponent<ContentSizeFitter>().enabled = false;
            homeContent.GetComponent<VerticalLayoutGroup>().enabled = false;

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Now Buy your first home.";

            GameObject go = homeContent.GetChild(0).gameObject;
            Instantiate(go, homeContent);
            yield return new WaitForSeconds(0.00001f);

            homeContent.GetChild(13).SetParent(gameTutorial.transform.GetChild(1));
            homeContent.GetComponent<ContentSizeFitter>().enabled = true;
            homeContent.GetComponent<VerticalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 7)
        {
            //Debug.Log("7");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            Transform homeContent = lifeStyleUI.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(1);
            gmMgr.lsScript.OnHomeTransBuy(gmMgr.lifeStyleSO.homeName[0], homeContent);
            gmMgr.hpScript.UpdateCredit();

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "You should also have a ride.";
            lifeStyleUI.transform.GetChild(2).GetComponent<HorizontalLayoutGroup>().enabled = false;

            GameObject go = lifeStyleUI.transform.GetChild(2).GetChild(2).gameObject;
            Instantiate(go, lifeStyleUI.transform.GetChild(2));
            yield return new WaitForSeconds(0.00001f);

            lifeStyleUI.transform.GetChild(2).GetChild(3).SetParent(gameTutorial.transform.GetChild(1));
            lifeStyleUI.transform.GetChild(2).GetComponent<HorizontalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 8)
        {
            //Debug.Log("8");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gmMgr.lsScript.OnToggleTab(2);

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Let's buy your first car.";
            Transform carContent = lifeStyleUI.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(2);
            carContent.GetComponent<ContentSizeFitter>().enabled = false;
            carContent.GetComponent<VerticalLayoutGroup>().enabled = false;

            GameObject go = carContent.GetChild(0).gameObject;
            Instantiate(go, carContent);
            yield return new WaitForSeconds(0.00001f);

            carContent.GetChild(13).SetParent(gameTutorial.transform.GetChild(1));
            carContent.GetComponent<ContentSizeFitter>().enabled = true;
            carContent.GetComponent<VerticalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 9)
        {
            //Debug.Log("9");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            Transform carContent = lifeStyleUI.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(2);
            gmMgr.lsScript.OnHomeTransBuy(gmMgr.lifeStyleSO.carsName[0], carContent);
            gmMgr.hpScript.UpdateCredit();

            gameTutorial.transform.GetChild(2).gameObject.SetActive(true);

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Now we have required lifestyle, let's start our first research.";
        }

        if (tutorialStepNum == 10)
        {
            //Debug.Log("10");
            lifeStyleUI.SetActive(false);
            gameTutorial.transform.GetChild(2).gameObject.SetActive(false);

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Click on research.";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(600, 250);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 0);

            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = false;

            GameObject go = homepage.transform.GetChild(1).GetChild(1).gameObject;
            Instantiate(go, homepage.transform.GetChild(1));
            yield return new WaitForSeconds(0.00001f);

            homepage.transform.GetChild(1).GetChild(5).SetParent(gameTutorial.transform.GetChild(1));
            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 11)
        {
            //Debug.Log("11");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            OnResearchUIOpen();

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Let's start the Food research.";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(150, -350);

            Transform researchContent = researchUI.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0);
            researchContent.GetComponent<ContentSizeFitter>().enabled = false;
            researchContent.GetComponent<VerticalLayoutGroup>().enabled = false;

            GameObject go = researchContent.GetChild(0).gameObject;
            Instantiate(go, researchContent);
            yield return new WaitForSeconds(0.00001f);

            researchContent.GetChild(12).SetParent(gameTutorial.transform.GetChild(1));
            researchContent.GetComponent<ContentSizeFitter>().enabled = true;
            researchContent.GetComponent<VerticalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 12)
        {
            //Debug.Log("12");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gmMgr.rsrchScript.OnResearchInitiate("Food", 0);

            Transform researchContent = researchUI.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0);
            gmMgr.hpScript.isTimeStopped = false;

            GameObject go = researchUI.transform.GetChild(3).gameObject;
            Instantiate(go, researchUI.transform);
            yield return new WaitForSeconds(0.00001f);

            researchUI.transform.GetChild(4).SetParent(gameTutorial.transform.GetChild(1));
            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "All Requirement have met let's start our first research.";

            float height = gameTutorial.GetComponent<RectTransform>().rect.height;
            height = height / 2;
            height -= 150;

            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, height);

            gameTutorial.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
            gameTutorial.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(7).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 13)
        {
            //Debug.Log("13");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gmMgr.rsrchScript.OnResearchAccept();
            gmMgr.hpScript.UpdateCredit();

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "You can also speed up your research.";
            gameTutorial.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(150, -350);

            Transform researchContent = researchUI.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0);

            GameObject go = researchContent.GetChild(0).GetChild(4).gameObject;
            Instantiate(go, researchContent.GetChild(0));
            yield return new WaitForSeconds(0.00001f);

            researchContent.GetChild(0).GetChild(7).SetParent(gameTutorial.transform.GetChild(1));

            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 14)
        {
            //Debug.Log("14");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gmMgr.hpScript.isTimeStopped = true;
            gmMgr.rsrchScript.OnResearchCompletion();

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Now we can open our first business.";
            gameTutorial.transform.GetChild(2).gameObject.SetActive(true);
        }

        if (tutorialStepNum == 15)
        {
            //Debug.Log("15");
            researchUI.SetActive(false);
            gameTutorial.transform.GetChild(2).gameObject.SetActive(false);
            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Click on business.";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(600, 250);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 0);

            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = false;

            GameObject go = homepage.transform.GetChild(1).GetChild(2).gameObject;
            Instantiate(go, homepage.transform.GetChild(1));
            yield return new WaitForSeconds(0.00001f);

            homepage.transform.GetChild(1).GetChild(5).SetParent(gameTutorial.transform.GetChild(1));
            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 16)
        {
            //Debug.Log("16");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            OnBusinessUIOpen();

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Let's open our first business.";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(150, -350);

            Transform businessContent = businessUI.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0);
            businessContent.GetComponent<ContentSizeFitter>().enabled = false;
            businessContent.GetComponent<VerticalLayoutGroup>().enabled = false;

            GameObject go = businessContent.GetChild(0).gameObject;
            Instantiate(go, businessContent);
            yield return new WaitForSeconds(0.00001f);

            businessContent.GetChild(12).SetParent(gameTutorial.transform.GetChild(1));
            businessContent.GetComponent<ContentSizeFitter>().enabled = true;
            businessContent.GetComponent<VerticalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetChild(5).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 17)
        {
            //Debug.Log("17");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            Transform businessContent = businessUI.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0);
            gmMgr.bizScript.OnBusinessOpen(gmMgr.businessSO.bLvl1Names[0], businessContent.transform, 0);

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Select max qty.";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(150, -500);

            Transform buyInput = businessUI.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(1);

            GameObject go = buyInput.GetChild(2).GetChild(0).GetChild(4).gameObject;
            Instantiate(go, buyInput.GetChild(2).GetChild(0));
            yield return new WaitForSeconds(0.00001f);

            buyInput.GetChild(2).GetChild(0).GetChild(5).SetParent(gameTutorial.transform.GetChild(1));
            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 18)
        {
            //Debug.Log("18");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gmMgr.bizScript.OnBuyQtyMax();
            Transform buyInput = businessUI.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(1);

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Now click on Buy.";

            GameObject go = buyInput.GetChild(2).GetChild(1).gameObject;
            Instantiate(go, buyInput.GetChild(2));
            yield return new WaitForSeconds(0.00001f);

            buyInput.GetChild(2).GetChild(2).SetParent(gameTutorial.transform.GetChild(1));
            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 19)
        {
            //Debug.Log("19");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gmMgr.bizScript.OnBusinessBuyBtn();
            gmMgr.hpScript.UpdateCredit();

            gmMgr.hpScript.isTimeStopped = false;
            gameObject.GetComponent<LifeStyleScript>().OnLsPartnerUpdate();
            gameObject.GetComponent<LifeStyleScript>().OnLsHomeUpdate();
            gameObject.GetComponent<LifeStyleScript>().OnLsTransportUpdate();
            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Now your business will generate monthly revenue.";
            gameTutorial.transform.GetChild(2).gameObject.SetActive(true);
        }

        if (tutorialStepNum == 20)
        {
            //Debug.Log("20");
            businessUI.SetActive(false);
            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Now if you need any help you can always refer to the presidential guide.";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(600, 250);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 0);

            GameObject go = homepage.transform.GetChild(2).GetChild(6).gameObject;
            Instantiate(go, homepage.transform.GetChild(2));
            yield return new WaitForSeconds(0.00001f);

            homepage.transform.GetChild(2).GetChild(14).SetParent(gameTutorial.transform.GetChild(1));
        }

        if (tutorialStepNum == 21)
        {
            //Debug.Log("21");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            gameTutorial.transform.GetChild(2).gameObject.SetActive(false);

            gameTutorial.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Now one last thing remains Trade.\n But looks like you don't have enough credit to trade.\n Here $1000 for you";
            gameTutorial.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 0);

            Vector2 sizeDelta = gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().sizeDelta;
            sizeDelta.y = 400f;
            gameTutorial.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().sizeDelta = sizeDelta;

            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = false;

            GameObject go = homepage.transform.GetChild(1).GetChild(4).gameObject;
            Instantiate(go, homepage.transform.GetChild(1));
            yield return new WaitForSeconds(0.00001f);

            homepage.transform.GetChild(1).GetChild(5).SetParent(gameTutorial.transform.GetChild(1));
            homepage.transform.GetChild(1).GetComponent<HorizontalLayoutGroup>().enabled = true;

            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            gameTutorial.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(TutorialNextBtn()); });
        }

        if (tutorialStepNum == 22)
        {
            //Debug.Log("22");
            Destroy(gameTutorial.transform.GetChild(1).GetChild(0).gameObject);
            OnTradeUIOpen();

            gameTutorial.SetActive(false);
            overHeadPopUp.SetActive(false);

            gmMgr.hpScript.CashCoin += 1000;
            gmMgr.hpScript.UpdateCredit();
            gmMgr.hpScript.onGameOpen = true;
        }

        tutorialStepNum++;
    }

    #endregion

}