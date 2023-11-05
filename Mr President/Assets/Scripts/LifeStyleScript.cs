using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeStyleScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameManager gmMgr;
    [SerializeField] private GameObject lsCard;         // LS Prefab

    [Header("For Populating")]
    [SerializeField] GameObject homePage;
    [SerializeField] GameObject[] lsTabBtns;
    [SerializeField] GameObject lsViewPort;
    [SerializeField] GameObject lsScroll;
    [SerializeField] GameObject[] lsContent;
    [SerializeField] GameObject tabHeader;
    [SerializeField] GameObject lsWarning;
    [SerializeField] Button aysAcceptBtn;
    [SerializeField] Button aysCancelBtn;
    
    private int lsNum = 3;

    public string currentPartner;
    public string currentHome;
    public string currentTransport;

    public decimal partExpense;
    public decimal homeExpense;
    public decimal transExpense;

    [SerializeField] private int lsExpensePerct = 3;

    private void Awake()
    {
        CallAwakeFunc();
    }

    private void Start()
    {
        PopulateLSData();
    }
    private void CallAwakeFunc()
    {
        gmMgr = GetComponent<GameManager>();
        lsCard = Resources.Load<GameObject>("Prefabs/LifeStyleCard");

        homePage = GameObject.Find("HomePage");
        lsViewPort = GameObject.Find("lsViewport").gameObject;
        lsScroll = GameObject.Find("lsScrollView");
        tabHeader = GameObject.Find("lsHeaderList");
        lsWarning = GameObject.Find("LsWarning");
        aysAcceptBtn = GameObject.Find("aysAcceptBtn").GetComponent<Button>();
        aysCancelBtn = GameObject.Find("aysCancelBtn").GetComponent<Button>();
        lsContent = new GameObject[lsNum];
        for (int i = 0; i < lsNum; i++)
        {
            lsContent[i] = lsViewPort.transform.GetChild(i).gameObject;
        }

        lsTabBtns = new GameObject[lsNum];
        for (int i = 0; i < lsNum; i++)
        {
            lsTabBtns[i] = tabHeader.transform.GetChild(i).gameObject;
            int buttonNumber = i;
            lsTabBtns[i].GetComponent<Button>().onClick.AddListener(() => { OnToggleTab(buttonNumber); });
        }
    }

    private void PopulateLSData()
    {
        int pArray = gmMgr.lifeStyleSO.partnersName.Length;
        int hArray = gmMgr.lifeStyleSO.homeName.Length;
        int cArray = gmMgr.lifeStyleSO.carsName.Length;

        PopulateContent(pArray, lsContent[0].transform, gmMgr.lifeStyleSO.partnersName, gmMgr.lifeStyleSO.partnersSpriteList, gmMgr.lifeStyleSO.homeCost, gmMgr.lifeStyleSO.partnersExpense);
        PopulateContent(hArray, lsContent[1].transform, gmMgr.lifeStyleSO.homeName, gmMgr.lifeStyleSO.homeSpriteList, gmMgr.lifeStyleSO.homeCost, gmMgr.lifeStyleSO.homeExpense);
        PopulateContent(cArray, lsContent[2].transform, gmMgr.lifeStyleSO.carsName, gmMgr.lifeStyleSO.carsSpriteList, gmMgr.lifeStyleSO.carsCost, gmMgr.lifeStyleSO.carsExpense);
        lsContent[0].gameObject.SetActive(true);
    }

    private void PopulateContent(int arr, Transform t, string[] name, string[] spriteArr, string[] cost, string[] expense)
    {
        for (int i = 0; i < arr; i++)
        {
            GameObject A1 = Instantiate(lsCard, t.transform);
            A1.name = name[i].ToString();
            A1.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(spriteArr[i]);
            A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name[i].ToString();
            if (t.name == "PartnerContent")
            {
                A1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Expense: $" + expense[i] + "/Month";
                A1.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ACCEPT";
                A1.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => { OnPartnerAccept(A1.name); });
            }
            else
            {
                A1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Cost: $" + cost[i] + "\nExpense: $" + expense[i] + "/Month";
                A1.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "BUY";
                A1.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => { OnHomeTransBuy(A1.name, t.transform); });
            }
            A1.transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + gmMgr.lifeStyleSO.respectPercent[i] + "%";
        }
    }

    public void OnLsPartnerUpdate()
    {
        for (int i = 0; i < lsContent[0].transform.childCount; i++)
        {
            if (lsContent[0].transform.GetChild(i).name == currentPartner)
            {
                lsContent[0].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Expense: $" + gmMgr.lifeStyleSO.partnersExpense[i] + "/Month";
                lsContent[0].transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
                lsContent[0].transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                homePage.transform.GetChild(2).GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>(gmMgr.lifeStyleSO.partnersSpriteList[i]);

                string expense = lsContent[0].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(10);
                string expense2 = expense.Substring(0, expense.Length - 6);
                partExpense = gmMgr.hpScript.ConvStrgToDecimal(expense2);
            }
            else
            {
                lsContent[0].transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                lsContent[0].transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
            }
        }

        // Expense Calculate
        OnMonthlyExpenseUpdate();
        gmMgr.hpScript.UpdateIncomExpen();
    }

    public void OnLsHomeUpdate()
    {
        for (int i = 0; i < lsContent[1].transform.childCount; i++)
        {
            if (lsContent[1].transform.GetChild(i).name == currentHome)
            {
                lsContent[1].transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
                lsContent[1].transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                homePage.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(gmMgr.lifeStyleSO.homeSpriteList[i]);
                lsContent[1].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Expense: $" + gmMgr.lifeStyleSO.homeExpense[i] + "/Month";
                string expense = lsContent[1].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(10);
                string expense2 = expense.Substring(0, expense.Length - 6);
                homeExpense = gmMgr.hpScript.ConvStrgToDecimal(expense2);

            }
            else if (gmMgr.lifeStyleSO.homeStatus[i] == "True")
            {
                string name = gmMgr.lifeStyleSO.homeName[i];
                lsContent[1].transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                lsContent[1].transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                lsContent[1].transform.GetChild(i).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ACCEPT";
                lsContent[1].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                            "Expense: $" + gmMgr.lifeStyleSO.homeExpense[i] + "/Month";
                lsContent[1].transform.GetChild(i).GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                lsContent[1].transform.GetChild(i).GetChild(3).GetComponent<Button>().onClick.AddListener(() => { OnHomeAccept(name); });
            }
            else
            {
                lsContent[1].transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                lsContent[1].transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
            }
        }
        // Expense Calculate
        OnMonthlyExpenseUpdate();
        gmMgr.hpScript.UpdateIncomExpen();
    }

    public void OnLsTransportUpdate()
    {
        for (int i = 0; i < lsContent[2].transform.childCount; i++)
        {
            if (lsContent[2].transform.GetChild(i).name == currentTransport)
            {
                lsContent[2].transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
                lsContent[2].transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                homePage.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(gmMgr.lifeStyleSO.carsSpriteList[i]);
                lsContent[2].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Expense: $" + gmMgr.lifeStyleSO.carsExpense[i] + "/Month";
                string expense = lsContent[2].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(10);
                string expense2 = expense.Substring(0, expense.Length - 6);
                transExpense = gmMgr.hpScript.ConvStrgToDecimal(expense2);
            }
            else if (gmMgr.lifeStyleSO.carsStatus[i] == "True")
            {
                string name = gmMgr.lifeStyleSO.carsName[i];
                lsContent[2].transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                lsContent[2].transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                lsContent[2].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Expense: $" + gmMgr.lifeStyleSO.carsExpense[i] + "/Month";
                lsContent[2].transform.GetChild(i).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ACCEPT";
                lsContent[2].transform.GetChild(i).GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                lsContent[2].transform.GetChild(i).GetChild(3).GetComponent<Button>().onClick.AddListener(() => { OnTransportAccept(name); });
            }
            else
            {
                lsContent[2].transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                lsContent[2].transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
            }
        }
        // Expense Calculate
        OnMonthlyExpenseUpdate();
        gmMgr.hpScript.UpdateIncomExpen();

    }

    public void OnHomeTransBuy(string lsName, Transform transf)
    {
        // Home 
        if (transf.name == lsContent[1].name)
        {
            for (int i = 0; i < lsContent[1].transform.childCount; i++)
            {
                if (lsContent[1].transform.GetChild(i).name == lsName)
                {
                    string cost = gmMgr.lifeStyleSO.homeCost[i];
                    decimal costInDeci = gmMgr.hpScript.ConvStrgToDecimal(cost);
                    if (gmMgr.hpScript.CashCoin >= costInDeci)
                    {
                        currentHome = lsName;
                        gmMgr.lifeStyleSO.homeStatus[i] = "True";
                        gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.lifeStyleSO.respectPercent[i]);
                        gmMgr.gsSaveMgrSql.UpdateLifeStyleStatus(i + 1, "HomeStatus", "True");
                        gmMgr.gsSaveMgrSql.UpdateGtGameState("currentHome", lsName);
                        gmMgr.hpScript.CashCoin -= costInDeci;
                        gmMgr.hpScript.UpdateCredit();
                        gmMgr.hpScript.UpdateHeHaReBar();

                        lsContent[1].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Expense: $" + gmMgr.lifeStyleSO.homeExpense[i] + "/Month";
                    }
                }
            }
            OnLsHomeUpdate();
        }
        // Transport
        else if (transf.name == lsContent[2].name)
        {
            for (int i = 0; i < lsContent[2].transform.childCount; i++)
            {
                if (lsContent[2].transform.GetChild(i).name == lsName)
                {
                    string cost = gmMgr.lifeStyleSO.carsCost[i];
                    decimal costInDeci = gmMgr.hpScript.ConvStrgToDecimal(cost);
                    if (gmMgr.hpScript.CashCoin >= costInDeci)
                    {
                        currentTransport = lsName;
                        gmMgr.lifeStyleSO.carsStatus[i] = "True";
                        gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.lifeStyleSO.respectPercent[i]);
                        gmMgr.gsSaveMgrSql.UpdateLifeStyleStatus(i + 1, "TransportStatus", "True");
                        gmMgr.gsSaveMgrSql.UpdateGtGameState("currentTransport", lsName);
                        gmMgr.hpScript.CashCoin -= costInDeci;
                        gmMgr.hpScript.UpdateCredit();
                        gmMgr.hpScript.UpdateHeHaReBar();

                        lsContent[2].transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Expense: $" + gmMgr.lifeStyleSO.carsExpense[i] + "/Month";
                    }
                }
            }
            OnLsTransportUpdate();
        }
    }

    public void OnPartnerAccept(string lsName)
    {
        for (int i = 0; i < lsContent[0].transform.childCount; i++)
        {
            if (gmMgr.lifeStyleSO.partnersName[i] == lsName)
            {
                if (currentPartner == "")
                {
                    currentPartner = lsName;
                    gmMgr.gsSaveMgrSql.UpdateGtGameState("currentPartner", lsName);
                    gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.lifeStyleSO.respectPercent[i]);
                    gmMgr.hpScript.UpdateHeHaReBar();
                }
                else
                {
                    for (int a = 0; a < gmMgr.lifeStyleSO.partnersName.Length; a++)
                    {
                        if (gmMgr.lifeStyleSO.partnersName[a] == currentPartner)
                        {
                            if (i < a)
                            {
                                int aa = a;
                                int ii = i;

                                lsWarning.SetActive(true);
                                aysAcceptBtn.onClick.RemoveAllListeners();
                                aysCancelBtn.onClick.RemoveAllListeners();

                                aysCancelBtn.onClick.AddListener(() => { lsWarning.SetActive(false); });
                                aysAcceptBtn.onClick.AddListener(() =>
                                {
                                    lsWarning.SetActive(false);
                                    currentPartner = lsName;
                                    gmMgr.gsSaveMgrSql.UpdateGtGameState("currentPartner", lsName);
                                    gmMgr.hpScript.happyPercentage -= 25;
                                    gmMgr.hpScript.respectPercentage -= float.Parse(gmMgr.lifeStyleSO.respectPercent[aa]);        // Remove Current Home's Respect
                                    gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.lifeStyleSO.respectPercent[ii]);        // Assign Selected Home's Respect
                                    gmMgr.hpScript.UpdateHeHaReBar();
                                    OnLsPartnerUpdate();
                                });
                            }
                            else
                            {
                                currentPartner = lsName;
                                gmMgr.gsSaveMgrSql.UpdateGtGameState("currentPartner", lsName);
                                gmMgr.hpScript.respectPercentage -= float.Parse(gmMgr.lifeStyleSO.respectPercent[a]);        // Remove Current Home's Respect
                                gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.lifeStyleSO.respectPercent[i]);        // Assign Selected Home's Respect
                                gmMgr.hpScript.UpdateHeHaReBar();
                            }
                        }
                    }
                }
            }
        }
        OnLsPartnerUpdate();
    }

    public void OnHomeAccept(string lsName)
    {
        for (int i = 0; i < gmMgr.lifeStyleSO.homeName.Length; i++)
        {
            if (gmMgr.lifeStyleSO.homeName[i] == lsName)
            {
                for (int a = 0; a < gmMgr.lifeStyleSO.homeName.Length; a++)
                {
                    if (gmMgr.lifeStyleSO.homeName[a] == currentHome)
                    {
                        if (i < a)
                        {
                            int aa = a;
                            int ii = i;

                            lsWarning.SetActive(true);
                            aysAcceptBtn.onClick.RemoveAllListeners();
                            aysCancelBtn.onClick.RemoveAllListeners();

                            aysCancelBtn.onClick.AddListener(() => { lsWarning.SetActive(false); });
                            aysAcceptBtn.onClick.AddListener(() =>
                            {
                                lsWarning.SetActive(false);
                                currentHome = lsName;
                                gmMgr.gsSaveMgrSql.UpdateGtGameState("currentHome", lsName);
                                gmMgr.hpScript.happyPercentage -= 25;
                                gmMgr.hpScript.respectPercentage -= float.Parse(gmMgr.lifeStyleSO.respectPercent[aa]);        // Remove Current Home's Respect
                                gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.lifeStyleSO.respectPercent[ii]);        // Assign Selected Home's Respect
                                gmMgr.hpScript.UpdateHeHaReBar();
                                OnLsHomeUpdate();
                            });
                        }
                        else
                        {
                            currentHome = lsName;
                            gmMgr.gsSaveMgrSql.UpdateGtGameState("currentHome", lsName);
                            gmMgr.hpScript.respectPercentage -= float.Parse(gmMgr.lifeStyleSO.respectPercent[a]);        // Remove Current Home's Respect
                            gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.lifeStyleSO.respectPercent[i]);        // Assign Selected Home's Respect
                            gmMgr.hpScript.UpdateHeHaReBar();
                        }
                    }
                }
            }
        }
        OnLsHomeUpdate();
    }

    public void OnTransportAccept(string lsName)
    {
        for (int i = 0; i < gmMgr.lifeStyleSO.carsName.Length; i++)
        {
            if (gmMgr.lifeStyleSO.carsName[i] == lsName)
            {
                for (int a = 0; a < gmMgr.lifeStyleSO.carsName.Length; a++)
                {
                    if (gmMgr.lifeStyleSO.carsName[a] == currentTransport)
                    {
                        if (i < a)
                        {
                            int aa = a;
                            int ii = i;

                            lsWarning.SetActive(true);
                            aysAcceptBtn.onClick.RemoveAllListeners();
                            aysCancelBtn.onClick.RemoveAllListeners();

                            aysCancelBtn.onClick.AddListener(() => { lsWarning.SetActive(false); });
                            aysAcceptBtn.onClick.AddListener(() =>
                            {
                                lsWarning.SetActive(false);
                                currentTransport = lsName;
                                gmMgr.gsSaveMgrSql.UpdateGtGameState("currentTransport", lsName);
                                gmMgr.hpScript.happyPercentage -= 25;
                                gmMgr.hpScript.respectPercentage -= float.Parse(gmMgr.lifeStyleSO.respectPercent[aa]);        // Remove Current Home's Respect
                                gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.lifeStyleSO.respectPercent[ii]);        // Assign Selected Home's Respect
                                gmMgr.hpScript.UpdateHeHaReBar();
                                OnLsTransportUpdate();
                            });
                        }
                        else
                        {
                            currentTransport = lsName;
                            gmMgr.gsSaveMgrSql.UpdateGtGameState("currentTransport", lsName);
                            gmMgr.hpScript.respectPercentage -= float.Parse(gmMgr.lifeStyleSO.respectPercent[a]);        // Remove Current Home's Respect
                            gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.lifeStyleSO.respectPercent[i]);        // Assign Selected Home's Respect
                            gmMgr.hpScript.UpdateHeHaReBar();
                        }
                    }
                }
            }
        }
        OnLsTransportUpdate();
    }

    public void OnToggleTab(int index)
    {
        for (int i = 0; i < lsTabBtns.Length; i++)
        {
            if (index == i)
            {
                lsScroll.GetComponent<ScrollRect>().content = lsContent[i].gameObject.GetComponent<RectTransform>();
                lsContent[i].SetActive(true);
                lsTabBtns[i].transform.GetComponent<Image>().color = new Color32(0, 235, 25, 255);
            }
            else
            {
                lsContent[i].SetActive(false);
                lsTabBtns[i].transform.GetComponent<Image>().color = new Color32(255, 235, 255, 255);
            }
        }
        //lsScroll.transform.GetChild(1).GetComponent<Scrollbar>().value = 1;
    }

    public void OnPartnerExpenseIncrease()
    {
        string currentPrtExpense;

        for (int i = 0; i < gmMgr.lifeStyleSO.partnersName.Length; i++)
        {
            if (gmMgr.lifeStyleSO.partnersName[i] == currentPartner)
            {
                currentPrtExpense = gmMgr.lifeStyleSO.partnersExpense[i];
                decimal partExpInDeci = gmMgr.hpScript.ConvStrgToDecimal(currentPrtExpense);
                decimal expToAdd = Math.Round(partExpInDeci * (lsExpensePerct / 100m));
                partExpInDeci += expToAdd;
                string partExpInStrg = gmMgr.hpScript.ConvDecimalToStrg(partExpInDeci);
                gmMgr.lifeStyleSO.partnersExpense[i] = partExpInStrg;
                gmMgr.gsSaveMgrSql.UpdateLifeStyleStatus(i + 1, "PartnerExpense", partExpInStrg);
                partExpense = partExpInDeci;
                OnLsPartnerUpdate();
                return;
            }
        }

        OnMonthlyExpenseUpdate();
    }

    public void OnHomeExpenseIncrease()
    {
        string currentHomeExpense;

        for (int i = 0; i < gmMgr.lifeStyleSO.homeName.Length; i++)
        {
            if (gmMgr.lifeStyleSO.homeName[i] == currentHome)
            {
                currentHomeExpense = gmMgr.lifeStyleSO.homeExpense[i];
                decimal homeExpInDeci = gmMgr.hpScript.ConvStrgToDecimal(currentHomeExpense);
                decimal expToAdd = Math.Round(homeExpInDeci * (lsExpensePerct / 100m));
                homeExpInDeci += expToAdd;
                string homeExpInStrg = gmMgr.hpScript.ConvDecimalToStrg(homeExpInDeci);
                gmMgr.lifeStyleSO.homeExpense[i] = homeExpInStrg;
                gmMgr.gsSaveMgrSql.UpdateLifeStyleStatus(i + 1, "HomeExpense", homeExpInStrg);
                homeExpense = homeExpInDeci;
                OnLsHomeUpdate();
                return;
            }
        }

        OnMonthlyExpenseUpdate();
    }

    public void OnTransportExpenseIncrease()
    {
        string currentCarExpense;

        for (int i = 0; i < gmMgr.lifeStyleSO.carsName.Length; i++)
        {
            if (gmMgr.lifeStyleSO.carsName[i] == currentTransport)
            {
                currentCarExpense = gmMgr.lifeStyleSO.carsExpense[i];
                decimal transExpInDeci = gmMgr.hpScript.ConvStrgToDecimal(currentCarExpense);
                decimal expToAdd = Math.Round(transExpInDeci * (lsExpensePerct / 100m));
                transExpInDeci += expToAdd;
                string transExpInStrg = gmMgr.hpScript.ConvDecimalToStrg(transExpInDeci);
                gmMgr.lifeStyleSO.carsExpense[i] = transExpInStrg;
                gmMgr.gsSaveMgrSql.UpdateLifeStyleStatus(i + 1, "TransportExpense", transExpInStrg);
                transExpense = transExpInDeci;
                OnLsTransportUpdate();
                return;
            }
        }
        OnMonthlyExpenseUpdate();
    }

    public void OnMonthlyExpenseUpdate()
    {
        gmMgr.bizScript.OnManagerSalary();
        gmMgr.hpScript.monthlyExpense = partExpense + homeExpense + transExpense + gmMgr.bizScript.mgrSalary;
    }
}
