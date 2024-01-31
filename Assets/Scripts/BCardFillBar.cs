using UnityEngine;
using UnityEngine.UI;

public class BCardFillBar : MonoBehaviour
{
    public GameManager gmMgr;
    [SerializeField] private GameObject gameManager;
    private Image image;

    float businessTimer;
    string managerLvl;
    int bLevel;
    int BIndex;
    Button collectbtn;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        gmMgr = gameManager.GetComponent<GameManager>();
        image = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        collectbtn = image.transform.parent.parent.GetChild(8).GetComponent<Button>();
        collectbtn.interactable = false;
        collectbtn.onClick.AddListener(() => { OnCollectBtn(); });
        GetData();
    }

    private void Update()
    {
        if (!gmMgr.hpScript.isTimeStopped)
        {
            if (bLevel == 1)
            {
                managerLvl = gmMgr.businessSO.bLvl1Manager[BIndex];
                businessTimer = float.Parse(gmMgr.businessSO.bLvl1Timer[BIndex]);
            }
            else if (bLevel == 2)
            {
                managerLvl = gmMgr.businessSO.bLvl2Manager[BIndex];
                businessTimer = float.Parse(gmMgr.businessSO.bLvl2Timer[BIndex]);
            }
            else if (bLevel == 3)
            {
                managerLvl = gmMgr.businessSO.bLvl3Manager[BIndex];
                businessTimer = float.Parse(gmMgr.businessSO.bLvl3Timer[BIndex]);
            }

            float progress = (float)businessTimer / gmMgr.bizScript.fillDuration;
            image.fillAmount = progress;

            if (progress >= 1)
            {
                if (bLevel == 1 && managerLvl == "False")
                {
                    collectbtn.interactable = true;
                }
                else if (bLevel == 2 && managerLvl == "False")
                {
                    collectbtn.interactable = true;
                }
                else if (bLevel == 3 && managerLvl == "False")
                {
                    collectbtn.interactable = true;
                }
            }
        }
    }

    private void GetData()
    {
        Transform content = image.transform.parent.parent.parent;
        string business = image.transform.parent.parent.name;
        if (content.name == "BusinessContent1")
        {
            bLevel = 1;

            for (int i = 0; i < content.childCount; i++)
            {
                if (content.GetChild(i).name == business)
                {
                    BIndex = i;
                }
            }
        }
        else if (content.name == "BusinessContent2")
        {
            bLevel = 2;

            for (int i = 0; i < content.childCount; i++)
            {
                if (content.GetChild(i).name == business)
                {
                    BIndex = i;
                }
            }
        }
        else if (content.name == "BusinessContent3")
        {
            bLevel = 3;

            for (int i = 0; i < content.childCount; i++)
            {
                if (content.GetChild(i).name == business)
                {
                    BIndex = i;
                }
            }
        }
        businessTimer = 0;
    }

    private void OnCollectBtn()
    {
        collectbtn.interactable = false;

        if (bLevel == 1)
        {
            gmMgr.businessSO.bLvl1Timer[BIndex] = "0";
        }
        else if (bLevel == 2)
        {
            gmMgr.businessSO.bLvl2Timer[BIndex] = "0";
        }
        else if (bLevel == 3)
        {
            gmMgr.businessSO.bLvl3Timer[BIndex] = "0";
        }

        gmMgr.bizScript.OnBusinessRevenueCollect(bLevel, BIndex);
    }
}