using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElectionScript : MonoBehaviour
{
    public GameManager gmMgr;

    public GameObject electionUI;
    [SerializeField] Button electionBtn;
    [SerializeField] TextMeshProUGUI curPosText;
    public int electionNum;

    private bool isElectionTimeOn;
    private bool isReqElectionAge;
    private bool isReqElectionRespect;
    private bool isReqElectionFund;
    private string congratsMsg;
    public float electionTimer;
    private float electionFillDur = 180;

    private Sprite crossImg;
    private Sprite tickImg;
    private Image filler;

    private void Awake()
    {
        gmMgr = GetComponent<GameManager>();
        electionUI = GameObject.Find("ElectionUI");
        electionBtn = GameObject.Find("ElectionBtn").GetComponent<Button>();
        curPosText = GameObject.Find("CurrentPositionText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        crossImg = Resources.Load<Sprite>("Sprites/Research/NO");
        tickImg = Resources.Load<Sprite>("Sprites/Research/YES");

        filler = electionUI.transform.GetChild(0).GetChild(10).GetChild(0).GetComponent<Image>();
        electionBtn.onClick.AddListener(() => { OnElectionPanelOpen(); });
        electionUI.transform.GetChild(0).GetChild(8).GetComponent<Button>().onClick.AddListener(() => { OnElectionParticipateBtn(); });
    }

    private void Update()
    {
        if (!gmMgr.hpScript.isTimeStopped && !isElectionTimeOn)
        {
            if (gmMgr.hpScript.isTimeSpeedUp)
            {
                electionTimer += Time.deltaTime * gmMgr.hpScript.speedUpNum;
            }
            else
            {
                electionTimer += Time.deltaTime;
            }
            gmMgr.gsSaveMgrSql.UpdateGtGameState("ElectionTimer", electionTimer.ToString());

            float progress = (float)electionTimer / electionFillDur;
            if (progress >= 1)
            {
                isElectionTimeOn = true;
                OnElectionTimer();
            }

            if (electionUI.activeInHierarchy)
            {
                filler.fillAmount = progress;
            }
        }
    }

    private void OnElectionPanelOpen()
    {
        electionUI.SetActive(true);
        electionUI.transform.GetChild(0).gameObject.SetActive(true);
        electionUI.transform.GetChild(1).gameObject.SetActive(false);
        electionUI.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = gmMgr.homePageSO.electionNames[electionNum];
        electionUI.transform.GetChild(0).GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text = ": " + gmMgr.homePageSO.electionAgeReq[electionNum] + " & Above";
        electionUI.transform.GetChild(0).GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text = ": " + gmMgr.homePageSO.electionRespReq[electionNum] + "% & Above";
        electionUI.transform.GetChild(0).GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text = ": " + gmMgr.homePageSO.electionCampFund[electionNum];


        OnElectionTimer();
        OnElectionReqCheck();
    }

    private void OnElectionTimer()
    {
        if (isElectionTimeOn)
        {
            electionUI.transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
            electionUI.transform.GetChild(0).GetChild(10).gameObject.SetActive(false);
            electionUI.transform.GetChild(0).GetChild(9).GetComponent<TextMeshProUGUI>().text = "Reward: +" + gmMgr.homePageSO.electionReward[electionNum] + "% Respect";
        }
        else
        {
            electionUI.transform.GetChild(0).GetChild(8).gameObject.SetActive(false);
            electionUI.transform.GetChild(0).GetChild(10).gameObject.SetActive(true);
            electionUI.transform.GetChild(0).GetChild(9).GetComponent<TextMeshProUGUI>().text = "Wait for Election Poll Time";
        }
    }

    public void OnElectionReqCheck()
    {
        isReqElectionAge = false;
        isReqElectionRespect = false;
        isReqElectionFund = false;

        electionUI.transform.GetChild(0).GetChild(5).GetChild(0).GetComponent<Image>().sprite = crossImg;
        electionUI.transform.GetChild(0).GetChild(6).GetChild(0).GetComponent<Image>().sprite = crossImg;
        electionUI.transform.GetChild(0).GetChild(7).GetChild(0).GetComponent<Image>().sprite = crossImg;

        if (int.Parse(gmMgr.homePageSO.electionAgeReq[electionNum]) <= gmMgr.hpScript.currentAge)
        {
            isReqElectionAge = true;
            electionUI.transform.GetChild(0).GetChild(5).GetChild(0).GetComponent<Image>().sprite = tickImg;
        }
        if (float.Parse(gmMgr.homePageSO.electionRespReq[electionNum]) <= gmMgr.hpScript.respectPercentage)
        {
            isReqElectionRespect = true;
            electionUI.transform.GetChild(0).GetChild(6).GetChild(0).GetComponent<Image>().sprite = tickImg;
        }
        string electionFund = gmMgr.homePageSO.electionCampFund[electionNum];
        if (gmMgr.hpScript.CashCoin >= gmMgr.hpScript.ConvStrgToDecimal(electionFund))
        {
            isReqElectionFund = true;
            electionUI.transform.GetChild(0).GetChild(7).GetChild(0).GetComponent<Image>().sprite = tickImg;
        }
    }

    private void OnElectionParticipateBtn()
    {
        if (isReqElectionAge && isReqElectionRespect && isReqElectionFund)
        {
            // Game Ends Here
            if (electionNum == 7)
            {
                gmMgr.uiCtrlScript.overHeadPopUp.SetActive(true);
                GameObject img = gmMgr.uiCtrlScript.overHeadPopUp.transform.GetChild(7).gameObject;
                img.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HomePage/PresidentElected");
                img.SetActive(true);
                img.transform.GetChild(0).gameObject.SetActive(true);
                img.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { gmMgr.uiCtrlScript.OnGameOverRestart(); Application.Quit(); } );
                return;
            }

            gmMgr.hpScript.respectPercentage += float.Parse(gmMgr.homePageSO.electionReward[electionNum]);

            string electionFund = gmMgr.homePageSO.electionCampFund[electionNum];
            gmMgr.hpScript.CashCoin -= gmMgr.hpScript.ConvStrgToDecimal(electionFund);
            gmMgr.hpScript.UpdateCredit();
            gmMgr.hpScript.UpdateHeHaReBar();
            gmMgr.bizScript.OnBusinessUpdate();

            if (electionNum == 2 || electionNum == 3)
            {
                congratsMsg = "Congratulations You Are Now Honorable Member Of\n" + gmMgr.homePageSO.electionNames[electionNum];
            }
            else
            {
                congratsMsg = "Congratulations You Are Now Honorable\n" + gmMgr.homePageSO.electionNames[electionNum];
            }
            curPosText.text = gmMgr.homePageSO.electionNames[electionNum];
            electionUI.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = congratsMsg;
            electionUI.transform.GetChild(0).gameObject.SetActive(false);
            electionUI.transform.GetChild(1).gameObject.SetActive(true);

            electionTimer = 0;
            isElectionTimeOn = false;
            electionNum++;
            gmMgr.gsSaveMgrSql.UpdateGtGameState("ElectionNum", electionNum.ToString());
        }
    }
}
