using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameManager gmMgr;

    [Header("For Populate")]
    [SerializeField] private GameObject researchCardPrefab;
    [SerializeField] GameObject researchContent;
    public GameObject researchUnlock;
    [SerializeField] private Button rAcceptBtn;

    [Header("Research Requirement")]
    [SerializeField] private GameObject rReqList;
    public bool isRReqPanelopen;
    private string reqPHT;
    [SerializeField] private bool[] reqBoolArray = { false, false, false, false, false, false, false, false };

    [Header("For Current Research Tracking")]
    public string selectRechName;
    public int selectRechLevel;
    public int selectRechIndex;

    public string activeRechName;
    public int activeRechLevel;
    public int activeRechIndex;

    [SerializeField] private bool isReqFulfilled;
    public bool isAnyReseaGoingOn;
    public Image progressBarImg = null;
    public float researchTimer;
    public float fillDuration = 180f; // 3 minutes in seconds

    string unlockBusinessName;

    private void Awake()
    {
        CallAwakeFunc();
    }

    private void CallAwakeFunc()
    {
        gmMgr = GetComponent<GameManager>();

        researchCardPrefab = Resources.Load<GameObject>("Prefabs/ResearchCard");
        researchContent = GameObject.Find("ResearchContent");
        researchUnlock = GameObject.Find("ResearchUnlock");
        rAcceptBtn = GameObject.Find("RAcceptBtn").GetComponent<Button>();

        rReqList = GameObject.Find("RRequirementList");
    }

    private void Start()
    {
        PopulateRData();
        GetReschLsReq();
        researchUnlock.SetActive(false);
    }

    private void Update()
    {
        ResearchProgressBar();
    }

    private void ResearchProgressBar()
    {
        if (!gmMgr.hpScript.isTimeStopped)
        {
            if (progressBarImg != null)
            {
                if (gmMgr.hpScript.isTimeSpeedUp)
                {
                    researchTimer += Time.deltaTime * gmMgr.hpScript.speedUpNum;
                }
                else
                {
                    researchTimer += Time.deltaTime;
                }
                float progress = researchTimer / fillDuration;
                progressBarImg.fillAmount = progress;

                if (progress >= 1)
                {
                    OnResearchCompletion();
                }
                gmMgr.gsSaveMgrSql.UpdateGtGameState("researchTimer", researchTimer.ToString());
            }
        }
    }

    private void PopulateRData()
    {
        for (int i = 0; i < gmMgr.researchSO.researchName.Length; i++)
        {
            GameObject A1 = Instantiate(researchCardPrefab, researchContent.transform);
            A1.name = gmMgr.researchSO.researchName[i];
            int rIndex = i;
            int rLevel = int.Parse(gmMgr.researchSO.researchLevel[i]);
            A1.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(gmMgr.researchSO.researchImgPath[i]);
            A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gmMgr.researchSO.researchName[i];
            if (int.Parse(gmMgr.researchSO.researchLevel[i]) > 2)
            {
                A1.transform.GetChild(3).gameObject.SetActive(false);
                A1.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Complete";
                A1.transform.GetChild(4).GetComponent<Button>().interactable = false;
            }
            // Time: always 3 months
            A1.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => { OnResearchInitiate(A1.name, rIndex); });
        }
    }

    private void GetReschLsReq()
    {
        int index1 = 0;
        int index2 = 0;
        int index3 = 0;

        int pIndex = 0;
        int hIndex = 0;
        int tIndex = 0;

        for (int i = 0; i < 36; i++)
        {
            if (i <= 3)
            {
                string reqPHT = gmMgr.lifeStyleSO.partnersName[pIndex] + "," + gmMgr.lifeStyleSO.homeName[hIndex] + "," + gmMgr.lifeStyleSO.carsName[tIndex];
                gmMgr.researchSO.r1Req[index1] = reqPHT;
                gmMgr.researchSO.reschLvlSequence[i] = "R" + (index1 + 1) + "L1";
                index1++;
            }
            else if (i > 3 && i <= 11)
            {
                if (i % 2 == 0)
                {
                    string reqPHT = gmMgr.lifeStyleSO.partnersName[pIndex] + "," + gmMgr.lifeStyleSO.homeName[hIndex] + "," + gmMgr.lifeStyleSO.carsName[tIndex];
                    gmMgr.researchSO.r2Req[index2] = reqPHT;
                    gmMgr.researchSO.reschLvlSequence[i] = "R" + (index2 + 1) + "L2";
                    index2++;
                }
                else
                {
                    string reqPHT = gmMgr.lifeStyleSO.partnersName[pIndex] + "," + gmMgr.lifeStyleSO.homeName[hIndex] + "," + gmMgr.lifeStyleSO.carsName[tIndex];
                    gmMgr.researchSO.r1Req[index1] = reqPHT;
                    gmMgr.researchSO.reschLvlSequence[i] = "R" + (index1 + 1) + "L1";
                    index1++;
                }
            }
            else if (i > 11 && i <= 23)
            {
                if (i % 3 == 0)
                {
                    string reqPHT = gmMgr.lifeStyleSO.partnersName[pIndex] + "," + gmMgr.lifeStyleSO.homeName[hIndex] + "," + gmMgr.lifeStyleSO.carsName[tIndex];
                    gmMgr.researchSO.r3Req[index3] = reqPHT;
                    gmMgr.researchSO.reschLvlSequence[i] = "R" + (index3 + 1) + "L3";
                    index3++;
                }
                else if ((i - 1) % 3 == 0)
                {
                    string reqPHT = gmMgr.lifeStyleSO.partnersName[pIndex] + "," + gmMgr.lifeStyleSO.homeName[hIndex] + "," + gmMgr.lifeStyleSO.carsName[tIndex];
                    gmMgr.researchSO.r2Req[index2] = reqPHT;
                    gmMgr.researchSO.reschLvlSequence[i] = "R" + (index2 + 1) + "L2";
                    index2++;
                }
                else
                {
                    string reqPHT = gmMgr.lifeStyleSO.partnersName[pIndex] + "," + gmMgr.lifeStyleSO.homeName[hIndex] + "," + gmMgr.lifeStyleSO.carsName[tIndex];
                    gmMgr.researchSO.r1Req[index1] = reqPHT;
                    gmMgr.researchSO.reschLvlSequence[i] = "R" + (index1 + 1) + "L1";
                    index1++;
                }
            }
            else if (i > 23 && i <= 31)
            {
                if (i % 2 == 0)
                {
                    string reqPHT = gmMgr.lifeStyleSO.partnersName[pIndex] + "," + gmMgr.lifeStyleSO.homeName[hIndex] + "," + gmMgr.lifeStyleSO.carsName[tIndex];
                    gmMgr.researchSO.r3Req[index3] = reqPHT;
                    gmMgr.researchSO.reschLvlSequence[i] = "R" + (index3 + 1) + "L3";
                    index3++;
                }
                else
                {
                    string reqPHT = gmMgr.lifeStyleSO.partnersName[pIndex] + "," + gmMgr.lifeStyleSO.homeName[hIndex] + "," + gmMgr.lifeStyleSO.carsName[tIndex];
                    gmMgr.researchSO.r2Req[index2] = reqPHT;
                    gmMgr.researchSO.reschLvlSequence[i] = "R" + (index2 + 1) + "L2";
                    index2++;
                }
            }
            else
            {
                if (i == 35)
                {
                    tIndex++;
                }
                string reqPHT = gmMgr.lifeStyleSO.partnersName[pIndex] + "," + gmMgr.lifeStyleSO.homeName[hIndex] + "," + gmMgr.lifeStyleSO.carsName[tIndex];
                gmMgr.researchSO.r3Req[index3] = reqPHT;
                gmMgr.researchSO.reschLvlSequence[i] = "R" + (index3 + 1) + "L3";
                index3++;
            }

            if (i % 3 == 0)
            {
                pIndex++;
            }
            else if (i % 3 == 1)
            {
                hIndex++;
            }
            else if (i % 3 == 2)
            {
                tIndex++;
            }
        }
    }

    public void OnResearchUpdate()
    {
        for (int i = 0; i < researchContent.transform.childCount; i++)
        {
            if (int.Parse(gmMgr.researchSO.researchLevel[i]) == 1)
            {
                researchContent.transform.GetChild(i).GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Business/1star");
            }
            else if (int.Parse(gmMgr.researchSO.researchLevel[i]) == 2)
            {
                researchContent.transform.GetChild(i).GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Business/1star");
                researchContent.transform.GetChild(i).GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Business/1star");
            }
            if (int.Parse(gmMgr.researchSO.researchLevel[i]) > 2)
            {
                researchContent.transform.GetChild(i).GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Business/1star");
                researchContent.transform.GetChild(i).GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Business/1star");
                researchContent.transform.GetChild(i).GetChild(2).GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Business/1star");
                researchContent.transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
                researchContent.transform.GetChild(i).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Complete";
                researchContent.transform.GetChild(i).GetChild(4).GetComponent<Button>().interactable = false;
            }
        }

        if (isAnyReseaGoingOn && activeRechName != "")
        {
            researchContent.transform.GetChild(activeRechIndex).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Speed Up";
            researchContent.transform.GetChild(activeRechIndex).GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
            researchContent.transform.GetChild(activeRechIndex).GetChild(4).GetComponent<Button>().onClick.AddListener(() => { OnResearchSpeedUp(); });
            researchContent.transform.GetChild(activeRechIndex).GetChild(5).gameObject.SetActive(true);
            progressBarImg = researchContent.transform.GetChild(activeRechIndex).GetChild(5).GetChild(0).GetComponent<Image>();
        }
    }

    public void OnResearchInitiate(string rName, int rIndex)
    {
        researchUnlock.SetActive(true);
        isRReqPanelopen = true;
        selectRechName = rName;
        selectRechIndex = rIndex;
        selectRechLevel = int.Parse(gmMgr.researchSO.researchLevel[rIndex]);

        researchUnlock.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = researchContent.transform.GetChild(rIndex).GetChild(0).GetComponent<Image>().sprite;
        researchUnlock.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = selectRechName;
        if (selectRechLevel == 0)
        {
            unlockBusinessName = gmMgr.businessSO.bLvl1Names[rIndex];
        }
        else if (selectRechLevel == 1)
        {
            unlockBusinessName = gmMgr.businessSO.bLvl2Names[rIndex];
        }
        else if (selectRechLevel == 2)
        {
            unlockBusinessName = gmMgr.businessSO.bLvl3Names[rIndex];
        }
        researchUnlock.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = gmMgr.researchSO.researchDscpt[rIndex] + "\n\nUnlocks Business:\n" + unlockBusinessName;
        researchUnlock.transform.GetChild(0).GetChild(7).GetComponent<Button>().onClick.AddListener(() => { OnResearchAccept(); });


        // Assigning Research condition
        AssignResearchReq();

        // Health & Happiness is fixed need 80% or above
        // Assigning Respect Requirement
        // Assigning LifeStyle Requirement
        if (selectRechLevel == 0)
        {
            reqPHT = gmMgr.researchSO.r1Req[selectRechIndex];
            rReqList.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Respect: " + gmMgr.researchSO.r1ReqRespect[selectRechIndex] + "%";
        }
        else if (selectRechLevel == 1)
        {
            reqPHT = gmMgr.researchSO.r2Req[selectRechIndex];
            rReqList.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Respect: " + gmMgr.researchSO.r2ReqRespect[selectRechIndex] + "%";
        }
        else if (selectRechLevel == 2)
        {
            reqPHT = gmMgr.researchSO.r3Req[selectRechIndex];
            rReqList.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Respect: " + gmMgr.researchSO.r3ReqRespect[selectRechIndex] + "%";
        }
        string[] words = reqPHT.Split(',');
        rReqList.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Partner: " + words[0];
        rReqList.transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Home: " + words[1];
        rReqList.transform.GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Transport: " + words[2];


        //Assigning Credit Requirement
        if (selectRechLevel == 0)
        {
            string cost = gmMgr.researchSO.researchCostLvl1[selectRechIndex];
            if (cost.Substring(cost.Length - 1) == "K")
            {
                if (cost[0].ToString() == "0")
                {
                    cost = (float.Parse(cost.Substring(0, cost.Length - 1)) * 1000).ToString();
                }
            }
            rReqList.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            "Credit: $" + cost;
        }
        else if (selectRechLevel == 1)
        {
            rReqList.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            "Credit: $" + gmMgr.researchSO.researchCostLvl2[selectRechIndex];
        }
        else
        {
            rReqList.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            "Credit: $" + gmMgr.researchSO.researchCostLvl3[selectRechIndex];
        }

        OnResearchReqCheck();
    }

    private void AssignResearchReq()
    {
        rReqList.transform.GetChild(0).gameObject.SetActive(true);
        if (selectRechLevel == 0)
        {
            if (selectRechIndex <= 3)
            {
                if (selectRechIndex == 0)
                {
                    rReqList.transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    rReqList.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        "Research: " + gmMgr.researchSO.researchName[selectRechIndex - 1] + " Level 1";
                }
            }
            else
            {
                rReqList.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        "Research: " + gmMgr.researchSO.researchName[selectRechIndex - 4] + " Level 2";
            }
        }
        else if (selectRechLevel == 1)
        {
            if (selectRechIndex <= 3)
            {
                rReqList.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        "Research: " + gmMgr.researchSO.researchName[selectRechIndex + 3] + " Level 1";
            }
            else
            {
                rReqList.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        "Research: " + gmMgr.researchSO.researchName[selectRechIndex - 4] + " Level 3";
            }
        }
        else if (selectRechLevel == 2)
        {
            if (selectRechIndex <= 4)
            {
                rReqList.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        "Research: " + gmMgr.researchSO.researchName[selectRechIndex + 7] + " Level 1";
            }
            if (selectRechIndex > 4 && selectRechIndex <= 8)
            {
                rReqList.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        "Research: " + gmMgr.researchSO.researchName[selectRechIndex + 3] + " Level 2";
            }
            else if (selectRechIndex >= 9)
            {
                rReqList.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        "Research: " + gmMgr.researchSO.researchName[selectRechIndex - 1] + " Level 3";
            }
        }
    }

    public void OnResearchReqCheck()
    {
        // Reuirement Check
        //if (isRReqPanelopen)
        if (researchUnlock.activeInHierarchy)
        {
            // Assigning false & bool false to list
            for (int i = 0; i < reqBoolArray.Length; i++)
            {
                rReqList.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/NO");
                reqBoolArray[i] = false;
            }

            // Research requirement check
            if (selectRechLevel == 0 && selectRechIndex == 0)
            {
                // skip research Check it's Deactive
                reqBoolArray[0] = true;
            }
            else
            {
                string reqRName = rReqList.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(10);
                string reqLvlNum = (reqRName[reqRName.Length - 1]).ToString();
                reqRName = reqRName.Substring(0, reqRName.Length - 8);

                for (int i = 0; i < gmMgr.researchSO.researchName.Length; i++)
                {
                    if (gmMgr.researchSO.researchName[i] == reqRName)
                    {
                        if (gmMgr.researchSO.researchLevel[i] == reqLvlNum)
                        {
                            rReqList.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                            reqBoolArray[0] = true;
                        }
                    }
                }
            }

            // Health fixed 80 & above
            if (gmMgr.hpScript.healthFillBar.fillAmount * 100 >= 80)
            {
                rReqList.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                reqBoolArray[1] = true;
            }

            //Happiness fixed 80 & above
            if (gmMgr.hpScript.happyFillBar.fillAmount * 100 >= 80)
            {
                rReqList.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                reqBoolArray[2] = true;
            }

            // Respect
            if (selectRechLevel == 0)
            {
                if (gmMgr.hpScript.respectFillBar.fillAmount * 100 >= float.Parse(gmMgr.researchSO.r1ReqRespect[selectRechIndex]))
                {
                    rReqList.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                    reqBoolArray[3] = true;
                }
            }
            else if (selectRechLevel == 1)
            {
                if (gmMgr.hpScript.respectFillBar.fillAmount * 100 >= float.Parse(gmMgr.researchSO.r2ReqRespect[selectRechIndex]))
                {
                    rReqList.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                    reqBoolArray[3] = true;
                }
            }
            else if (selectRechLevel == 2)
            {
                if (gmMgr.hpScript.respectFillBar.fillAmount * 100 >= float.Parse(gmMgr.researchSO.r3ReqRespect[selectRechIndex]))
                {
                    rReqList.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                    reqBoolArray[3] = true;
                }
            }

            // Partner
            string reqPartName = rReqList.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(9);
            for (int i = 0; i < gmMgr.lifeStyleSO.partnersName.Length; i++)
            {
                if (gmMgr.lifeStyleSO.partnersName[i] == gmMgr.lsScript.currentPartner)
                {
                    for (int a = 0; a < gmMgr.lifeStyleSO.partnersName.Length; a++)
                    {
                        if (gmMgr.lifeStyleSO.partnersName[a] == reqPartName)
                        {
                            if (i >= a)
                            {
                                rReqList.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                                reqBoolArray[4] = true;
                            }
                        }
                    }
                }
            }

            // Home
            string reqHomeName = rReqList.transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(6);
            for (int i = 0; i < gmMgr.lifeStyleSO.homeName.Length; i++)
            {
                if (gmMgr.lifeStyleSO.homeName[i] == gmMgr.lsScript.currentHome)
                {
                    for (int a = 0; a < gmMgr.lifeStyleSO.homeName.Length; a++)
                    {
                        if (gmMgr.lifeStyleSO.homeName[a] == reqHomeName)
                        {
                            if (i >= a)
                            {
                                rReqList.transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                                reqBoolArray[5] = true;
                            }
                        }
                    }
                }
            }

            //Transport
            string reqTransName = rReqList.transform.GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(11);
            for (int i = 0; i < gmMgr.lifeStyleSO.carsName.Length; i++)
            {
                if (gmMgr.lifeStyleSO.carsName[i] == gmMgr.lsScript.currentTransport)
                {
                    for (int a = 0; a < gmMgr.lifeStyleSO.carsName.Length; a++)
                    {
                        if (gmMgr.lifeStyleSO.carsName[a] == reqTransName)
                        {
                            if (i >= a)
                            {
                                rReqList.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                                reqBoolArray[6] = true;
                            }
                        }
                    }
                }
            }

            // Credit
            string reqCreditAmount = rReqList.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(9);
            decimal reqCreditInDeci = gmMgr.hpScript.ConvStrgToDecimal(reqCreditAmount);

            if (gmMgr.hpScript.CashCoin > reqCreditInDeci)
            {
                rReqList.transform.GetChild(7).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Research/YES");
                reqBoolArray[7] = true;
            }

            CheckIfAllReqAreTrue();
            bool CheckIfAllReqAreTrue()
            {
                for (int i = 1; i < reqBoolArray.Length; i++)
                {
                    if (reqBoolArray[i] != true)
                    {
                        return isReqFulfilled = false;
                    }
                }

                return isReqFulfilled = true;
            }
        }
    }

    public void OnResearchAccept()
    {
        if (isReqFulfilled)
        {
            if (!isAnyReseaGoingOn)
            {
                isAnyReseaGoingOn = true;
                isReqFulfilled = false;

                activeRechName = selectRechName;
                activeRechLevel = selectRechLevel;
                activeRechIndex = selectRechIndex;
                gmMgr.gsSaveMgrSql.UpdateGtGameState("activeRechName", activeRechName);
                gmMgr.gsSaveMgrSql.UpdateGtGameState("activeRechLevel", activeRechLevel.ToString());
                gmMgr.gsSaveMgrSql.UpdateGtGameState("activeRechIndex", activeRechIndex.ToString());
                gmMgr.gsSaveMgrSql.UpdateGtGameState("isAnyReseaGoingOn", isAnyReseaGoingOn.ToString());

                string reqCreditAmount = rReqList.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text.Substring(9);
                decimal reqCreditInDeci = gmMgr.hpScript.ConvStrgToDecimal(reqCreditAmount);

                gmMgr.hpScript.CashCoin -= reqCreditInDeci;
                gmMgr.hpScript.UpdateCredit();

                researchContent.transform.GetChild(activeRechIndex).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Speed Up";
                researchContent.transform.GetChild(activeRechIndex).GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
                researchContent.transform.GetChild(activeRechIndex).GetChild(4).GetComponent<Button>().onClick.AddListener(() => { OnResearchSpeedUp(); });
                researchContent.transform.GetChild(activeRechIndex).GetChild(5).gameObject.SetActive(true);
                progressBarImg = researchContent.transform.GetChild(activeRechIndex).GetChild(5).GetChild(0).GetComponent<Image>();
                progressBarImg.fillAmount = 0;
                researchTimer = 0f;
                researchUnlock.SetActive(false);
            }
        }
    }

    public void OnResearchCompletion()
    {
        isAnyReseaGoingOn = false;
        progressBarImg = null;
        activeRechName = "";

        gmMgr.gsSaveMgrSql.UpdateGtGameState("activeRechName", "");
        gmMgr.gsSaveMgrSql.UpdateGtGameState("isAnyReseaGoingOn", isAnyReseaGoingOn.ToString());

        researchContent.transform.GetChild(activeRechIndex).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Research";
        researchContent.transform.GetChild(activeRechIndex).GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
        researchContent.transform.GetChild(activeRechIndex).GetChild(4).GetComponent<Button>().onClick.AddListener(() => { OnResearchInitiate(activeRechName, activeRechIndex); });
        researchContent.transform.GetChild(activeRechIndex).GetChild(5).gameObject.SetActive(false);

        // Updating Business Status
        int lvlNum = int.Parse(gmMgr.researchSO.researchLevel[activeRechIndex]);
        if (lvlNum == 0)
        {
            gmMgr.businessSO.bLvl1Status[activeRechIndex] = "True";
            gmMgr.gsSaveMgrSql.UpdateBusinessData(activeRechIndex + 1, "bStatusLvl1", "True");
        }
        else if (lvlNum == 1)
        {
            gmMgr.businessSO.bLvl2Status[activeRechIndex] = "True";
            gmMgr.gsSaveMgrSql.UpdateBusinessData(activeRechIndex + 1, "bStatusLvl2", "True");
        }
        else
        {
            gmMgr.businessSO.bLvl3Status[activeRechIndex] = "True";
            gmMgr.gsSaveMgrSql.UpdateBusinessData(activeRechIndex + 1, "bStatusLvl3", "True");
        }
        lvlNum++;

        // Updating Research Level 
        gmMgr.researchSO.researchLevel[activeRechIndex] = lvlNum.ToString();
        gmMgr.gsSaveMgrSql.UpdateResearchLvl(activeRechIndex + 1, lvlNum.ToString());

        OnResearchUpdate();
        gmMgr.bizScript.OnRcomBupdate();
        gmMgr.hpScript.respectPercentage += 0.25f;
        gmMgr.hpScript.UpdateHeHaReBar();
    }

    public void OnResearchSpeedUp()
    {
        if (researchTimer < 60)
        {
            researchTimer = 60;
        }
        else if (researchTimer > 60 && researchTimer < 120)
        {
            researchTimer = 120;
        }
        else if (researchTimer > 120)
        {
            researchTimer = 180;
        }

        //gmMgr.hpScript.OnAdWatchAdCountPlus();
    }

}
