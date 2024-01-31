using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameObjectiveScript : MonoBehaviour
{
    public GameManager gmMgr;
    [SerializeField] private Button pGuideBtn;
    [SerializeField] private GameObject presidentGuide;
    [SerializeField] private GameObject objectivePrefab;
    [SerializeField] private Transform objectiveListContent;

    private void Awake()
    {
        gmMgr = GetComponent<GameManager>();
        pGuideBtn = GameObject.Find("PresidentialGuide").GetComponent<Button>();
        presidentGuide = GameObject.Find("PGuideOpen");
        objectiveListContent = GameObject.Find("ObjectiveListContent").transform;
        objectivePrefab = Resources.Load<GameObject>("Prefabs/ObjectiveCard");
    }

    private void Start()
    {
        pGuideBtn.onClick.AddListener(() => { OnPresidentGuideOpen(); });
    }

    void OnPresidentGuideOpen()
    {
        presidentGuide.SetActive(true);

        // Destroying previous objective
        if (objectiveListContent.transform.childCount != 0)
        {
            for (int i = objectiveListContent.childCount - 1; i >= 0; i--)
            {
                Transform child = objectiveListContent.GetChild(i);
                Destroy(child.gameObject);
            }
        }

        // Assigning Business Objective
        for (int i = 0; i < gmMgr.researchSO.reschLvlSequence.Length; i++)
        {
            string research = gmMgr.researchSO.reschLvlSequence[i];
            int researchIndex = int.Parse(research.Substring(1, research.Length - 3));
            int researchLevel = int.Parse(research[research.Length - 1].ToString());
            researchIndex -= 1;
            researchLevel -= 1;

            if (researchLevel == 0)
            {
                int rewardLvl = int.Parse(gmMgr.businessSO.bLvl1Reward[researchIndex]);
                if (rewardLvl == 20)
                {
                    GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                    A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                        "Own 100 " + gmMgr.businessSO.bLvl1Names[researchIndex];
                    A1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+0.75% Respect";
                }
                else if (rewardLvl == 10)
                {
                    GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                    A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                        "Own 25 " + gmMgr.businessSO.bLvl1Names[researchIndex];
                    A1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+0.5% Respect";
                }
                else if (rewardLvl == 0)
                {
                    GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                    A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                        "Own 10 " + gmMgr.businessSO.bLvl1Names[researchIndex];
                    A1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+0.25% Respect";
                }
            }
            else if (researchLevel == 1)
            {
                int rewardLvl = int.Parse(gmMgr.businessSO.bLvl1Reward[researchIndex]);
                if (rewardLvl == 20)
                {
                    GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                    A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                        "Own 100 " + gmMgr.businessSO.bLvl2Names[researchIndex];
                    A1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+0.75% Respect";
                }
                else if (rewardLvl == 10)
                {
                    GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                    A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                        "Own 25 " + gmMgr.businessSO.bLvl2Names[researchIndex];
                    A1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+0.5% Respect";
                }
                else if (rewardLvl == 0)
                {
                    GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                    A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                        "Own 10 " + gmMgr.businessSO.bLvl2Names[researchIndex];
                    A1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+0.25% Respect";
                }
            }
            else if (researchLevel == 2)
            {
                int rewardLvl = int.Parse(gmMgr.businessSO.bLvl1Reward[researchIndex]);
                if (rewardLvl == 20)
                {
                    GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                    A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                        "Own 100 " + gmMgr.businessSO.bLvl3Names[researchIndex];
                    A1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+0.75% Respect";
                }
                else if (rewardLvl == 10)
                {
                    GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                    A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                        "Own 25  " + gmMgr.businessSO.bLvl3Names[researchIndex];
                    A1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+0.5% Respect";
                }
                else if (rewardLvl == 0)
                {
                    GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                    A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                        "Own 10 " + gmMgr.businessSO.bLvl3Names[researchIndex];
                    A1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+0.25% Respect";
                }
            }

            // Assigning Research Objective
            if (int.Parse(gmMgr.researchSO.researchLevel[researchIndex]) <= researchLevel)
            {
                Destroy(objectiveListContent.GetChild(objectiveListContent.childCount - 1).gameObject);
                GameObject A1 = Instantiate(objectivePrefab, objectiveListContent);
                A1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                    "Complete " + gmMgr.researchSO.researchName[researchIndex] + " Reseach Level " + (researchLevel + 1);
                A1.transform.GetChild(2).gameObject.SetActive(false);
                A1.transform.SetAsFirstSibling();

                GameObject A2 = Instantiate(objectivePrefab, objectiveListContent);
                A2.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                    "Participate in " + gmMgr.homePageSO.electionNames[gmMgr.elecScript.electionNum] + " Election";
                A2.transform.GetChild(2).gameObject.SetActive(false);
                A2.transform.SetAsFirstSibling();
                break;
            }
        }
    }

}
