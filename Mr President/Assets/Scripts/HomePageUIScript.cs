using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomePageUIScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameManager gmMgr;

    [Header("Player Name")]
    public string playerName;

    [Header("Calender")]
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI monthYearText;
    public int currentDay = 1;
    public int currentMonth = 1;
    public int currentYear = 2005;
    public int currentAge = 18;
    public float calenderTimer = 0f;
    public float secondsPerDay = 2f; // Adjust this value to control the speed of date change
    public string[] monthNames = { "January", "February", "March", "April", "May", "June", "July",
                                    "August", "September", "October", "November", "December" };
    public int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    public int previousMonth = 1;

    [Header("Age")]
    private TextMeshProUGUI ageText;

    [Header("Coins")]
    public decimal CashCoin;
    public TextMeshProUGUI cashCoinsText;

    [Header("BalanceSheet")]
    private TextMeshProUGUI balanceText;
    public decimal monthlyExpense;
    public decimal monthlyIncome;
    public bool negativeCredit;

    [Header("Health, Happpiness, Respect")]
    public Image healthFillBar;
    public Image happyFillBar;
    public Image respectFillBar;

    private TextMeshProUGUI hePercentageText;
    private TextMeshProUGUI haPercentageText;
    private TextMeshProUGUI rePercentageText;

    public float healthPercentage;
    public float happyPercentage;
    public float respectPercentage;

    [Header("GamePLay Pause")]
    public bool isTimeStopped = true;
    public bool isTimeSpeedUp;
    public float speedUpNum = 5;
    public bool onGameOpen;

    [Header("Daily Bonus")]
    public int numberOfDays = 7;
    public int dbCollectedDay;
    public DateTime dbStartDate;

    public decimal adBonusVal = 1000;
    private int adPercentVal = 10;         // 10% in float


    private void Awake()
    {
        CallAwakeFunc();
    }

    private void CallAwakeFunc()
    {
        gmMgr = GetComponent<GameManager>();
        dateText = GameObject.Find("Date").GetComponent<TextMeshProUGUI>();
        monthYearText = GameObject.Find("Month_Year").GetComponent<TextMeshProUGUI>();
        ageText = GameObject.Find("AgeText").GetComponent<TextMeshProUGUI>();
        cashCoinsText = GameObject.Find("CashCoinsText").GetComponent<TextMeshProUGUI>();
        balanceText = GameObject.Find("BalanceSheetText").GetComponent<TextMeshProUGUI>();
        healthFillBar = GameObject.Find("HealthFillBar").GetComponent<Image>();
        happyFillBar = GameObject.Find("HappyFillBar").GetComponent<Image>();
        respectFillBar = GameObject.Find("RespectFillBar").GetComponent<Image>();
        hePercentageText = GameObject.Find("HealthPercent").GetComponent<TextMeshProUGUI>();
        haPercentageText = GameObject.Find("HappyPercent").GetComponent<TextMeshProUGUI>();
        rePercentageText = GameObject.Find("RespectPercent").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        isTimeStopped = true;
        onGameOpen = false;
        InvokeRepeating("HealthCalc", 110, 55); // Invoke the function to decrease Health every 45 seconds
        InvokeRepeating("HappinessCalc", 90, 50); // Invoke the function to decrease Health every 45 seconds
    }

    private void Update()
    {
        if (!isTimeStopped)
        {
            // Update and display the Date, Month & Year
            UpdateCalenderUI();

            // Checking if any of the Health, happiness & respect is zero
            GameOverCheck();
        }
    }

    public void OnGameSpeedNrml()
    {
        isTimeSpeedUp = false;
        isTimeStopped = false;
        secondsPerDay = 2f;
    }

    public void OnGameSpeedUp()
    {
        isTimeStopped = false;
        if (!isTimeSpeedUp)
        {
            secondsPerDay = secondsPerDay/speedUpNum;
            isTimeSpeedUp = true;
        }
    }

    private void GameOverCheck()
    {
        if (healthFillBar.fillAmount == 0 || happyFillBar.fillAmount == 0)
        {
            gmMgr.uiCtrlScript.overHeadPopUp.SetActive(true);
            gmMgr.uiCtrlScript.overHeadPopUp.transform.GetChild(8).gameObject.SetActive(true);
            isTimeStopped = true;
        }
    }

    public void UpdateCredit()
    {
        if (CashCoin < 0)
        {
            decimal cash = (decimal)Mathf.Abs((float)CashCoin);
            cashCoinsText.text = "Credit: -$" + ConvDecimalToStrg(cash);
        }
        else
        {
            cashCoinsText.text = "Credit: $" + ConvDecimalToStrg(CashCoin);
        }
        gmMgr.gsSaveMgrSql.UpdateGtGameState("CashCoin", ConvDecimalToStrg(CashCoin));


        // Updating Price on other panels if open
        if (onGameOpen)
        {
            if (gmMgr.bizScript.businessClicked.activeInHierarchy)
            {
                gmMgr.bizScript.OnMaxBuyUpdate();
            }
            if (gmMgr.rsrchScript.researchUnlock.activeInHierarchy)
            {
                gmMgr.rsrchScript.OnResearchReqCheck();
            }
            if (gmMgr.uiCtrlScript.bSheetOpen.activeInHierarchy)
            {
                gmMgr.uiCtrlScript.OnBalanceSheetOpen();
            }
            if (gmMgr.elecScript.electionUI.activeInHierarchy)
            {
                gmMgr.elecScript.OnElectionReqCheck();
            }
        }
    }

    public void UpdateIncomExpen()
    {
        if (monthlyIncome >= monthlyExpense)
        {
            decimal difference = monthlyIncome - monthlyExpense;
            string diffInStrg = ConvDecimalToStrg(difference);
            balanceText.text = "+$" + diffInStrg + "/Month";
            balanceText.color = new Color32(0, 125, 15, 255);
            negativeCredit = false;
        }
        else
        {
            decimal difference = monthlyExpense - monthlyIncome;
            string diffInStrg = ConvDecimalToStrg(difference);
            balanceText.text = "-$" + diffInStrg + "/Month";
            balanceText.color = new Color32(255, 0, 0, 255);
            negativeCredit = true;
        }
    }

    private void UpdateCalenderUI()
    {
        calenderTimer += Time.deltaTime;

        // Update the UI with the new date
        dateText.text = currentDay.ToString();
        monthYearText.text = monthNames[currentMonth - 1] + "\n" + currentYear.ToString();
        ageText.text = "Age: " + currentAge + "y; " + (currentMonth - 1) + "m";

        if (calenderTimer >= secondsPerDay)
        {
            calenderTimer = 0f;

            // Increment the date
            currentDay++;
            gmMgr.gsSaveMgrSql.UpdateGtGameState("currentDay", currentDay.ToString());

            if (currentDay > daysInMonth[currentMonth - 1])
            {
                currentDay = 1;

                previousMonth = currentMonth;
                currentMonth++;
                gmMgr.gsSaveMgrSql.UpdateGtGameState("currentDay", currentDay.ToString());
                gmMgr.gsSaveMgrSql.UpdateGtGameState("currentMonth", currentMonth.ToString());

                if (currentMonth > 12)
                {
                    currentMonth = 1;
                    currentYear++;
                    currentAge++;
                    gmMgr.gsSaveMgrSql.UpdateGtGameState("currentMonth", currentMonth.ToString());
                    gmMgr.gsSaveMgrSql.UpdateGtGameState("currentYear", currentYear.ToString());
                    gmMgr.gsSaveMgrSql.UpdateGtGameState("currentAge", currentAge.ToString());
                }

                if (previousMonth != currentMonth)
                {
                    int electNum = gmMgr.elecScript.electionNum;
                    string salary = gmMgr.homePageSO.electedSalary[electNum];
                    CashCoin += ConvStrgToDecimal(salary);

                    gmMgr.bizScript.OnManagerSalary();
                    CashCoin -= monthlyExpense;
                    CashCoin -= gmMgr.bizScript.mgrSalary;
                    UpdateCredit();
                    gmMgr.lsScript.OnPartnerExpenseIncrease();
                    gmMgr.lsScript.OnHomeExpenseIncrease();
                    gmMgr.lsScript.OnTransportExpenseIncrease();
                    UpdateIncomExpen();
                }
            }
        }

    }

    public void UpdateHeHaReBar()
    {
        healthPercentage = Mathf.RoundToInt(healthPercentage);
        happyPercentage = Mathf.RoundToInt(happyPercentage);
        respectPercentage = Mathf.Round(respectPercentage * 100f) / 100f;

        // If more than 100
        if (healthPercentage > 100)
        {
            healthPercentage = 100;
        }
        if (happyPercentage > 100)
        {
            happyPercentage = 100;
        }
        if (respectPercentage > 100)
        {
            respectPercentage = 100;
        }

        // If Less then 0
        if (healthPercentage <= 0)
        {
            healthPercentage = 0;
        }
        if (happyPercentage <= 0)
        {
            happyPercentage = 0;
        }
        if (respectPercentage <= 0)
        {
            respectPercentage = 0;
        }

        healthFillBar.fillAmount = healthPercentage / 100;
        happyFillBar.fillAmount = happyPercentage / 100;
        respectFillBar.fillAmount = respectPercentage / 100;

        hePercentageText.text = healthPercentage + "%";
        haPercentageText.text = happyPercentage + "%";
        rePercentageText.text = respectPercentage + "%";

        gmMgr.gsSaveMgrSql.UpdateGtGameState("healthPercentage", healthPercentage.ToString());
        gmMgr.gsSaveMgrSql.UpdateGtGameState("happyPercentage", happyPercentage.ToString());
        gmMgr.gsSaveMgrSql.UpdateGtGameState("respectPercentage", respectPercentage.ToString());

        if (gmMgr.uiCtrlScript.overHeadPopUp.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            gmMgr.uiCtrlScript.healthActiText.text = "Health: " + healthPercentage + "%";
        }
        if (gmMgr.uiCtrlScript.overHeadPopUp.transform.GetChild(2).gameObject.activeInHierarchy)
        {
            gmMgr.uiCtrlScript.happinessActiText.text = "Happiness: " + happyPercentage + "%";
        }
        if (gmMgr.rsrchScript.researchUnlock.activeInHierarchy)
        {
            gmMgr.rsrchScript.OnResearchReqCheck();
        }
        if (gmMgr.elecScript.electionUI.activeInHierarchy)
        {
            gmMgr.elecScript.OnElectionReqCheck();
        }
    }

    public void HealthCalc()
    {
        if (!isTimeStopped)
        {
            // If expense is more then greater depleting rate
            if (negativeCredit)
            {
                healthPercentage -= 10;
                return;
            }

            // Health
            if (healthPercentage >= 97)
            {
                healthPercentage -= 1;
            }
            else if (healthPercentage >= 93)
            {
                healthPercentage -= 2;
            }
            else if (healthPercentage >= 85)
            {
                healthPercentage -= 3;
            }
            else if (healthPercentage >= 70)
            {
                healthPercentage -= 4;
            }
            else if (healthPercentage >= 50)
            {
                healthPercentage -= 5;
            }
            else if (healthPercentage < 50)
            {
                healthPercentage -= 6;
            }
            healthPercentage = Mathf.Round(healthPercentage * 10f) / 10f;
            gmMgr.gsSaveMgrSql.UpdateGtGameState("healthPercentage", healthPercentage.ToString());

            UpdateHeHaReBar();
        }
    }

    public void HappinessCalc()
    {
        if (!isTimeStopped)
        {
            // If expense is more then greater depleting rate
            if (negativeCredit)
            {
                happyPercentage -= 10;
                return;
            }

            // Happiness
            if (happyPercentage >= 97)
            {
                happyPercentage -= 1.5f;
            }
            else if (happyPercentage >= 93)
            {
                happyPercentage -= 2.5f;
            }
            else if (happyPercentage >= 85)
            {
                happyPercentage -= 3.5f;
            }
            else if (happyPercentage >= 70)
            {
                happyPercentage -= 4.5f;
            }
            else if (happyPercentage >= 50)
            {
                happyPercentage -= 6;
            }
            else if (happyPercentage < 50)
            {
                happyPercentage -= 7;
            }
            happyPercentage = Mathf.Round(happyPercentage * 10f) / 10f;
            gmMgr.gsSaveMgrSql.UpdateGtGameState("happyPercentage", happyPercentage.ToString());

            UpdateHeHaReBar();
        }
    }

    public decimal ConvStrgToDecimal(string input)
    {
        char lastChar = input[input.Length - 1];
        decimal result = 0;

        if (lastChar == 'K')
        {
            input = input.Substring(0, input.Length - 1);
            decimal floatValue = decimal.Parse(input);
            result = floatValue * 1000;
        }
        else if (lastChar == 'M')
        {
            input = input.Substring(0, input.Length - 1);
            decimal floatValue = decimal.Parse(input);
            result = floatValue * 1000000;
        }
        else if (lastChar == 'B')
        {
            input = input.Substring(0, input.Length - 1);
            decimal floatValue = decimal.Parse(input);
            result = floatValue * 1000000000;
        }
        else if (lastChar == 'T')
        {
            input = input.Substring(0, input.Length - 1);
            decimal floatValue = decimal.Parse(input);
            result = floatValue * 1000000000000;
        }
        else if (lastChar == 'Q')
        {
            input = input.Substring(0, input.Length - 1);
            decimal floatValue = decimal.Parse(input);
            result = floatValue * 1000000000000000;
        }
        else if (lastChar == 'P')
        {
            input = input.Substring(0, input.Length - 1);
            decimal floatValue = decimal.Parse(input);
            result = floatValue * 1000000000000000000;
        }
        else
        {
            result = decimal.Parse(input);
        }

        return result;
    }

    public string ConvDecimalToStrg(decimal input)
    {
        string result = "";

        if (input > 1000000000000000000)
        {
            input = input / 1000000000000000;
            if (input < 9)
            {
                result = input.ToString("F2") + "S";
            }
            else if (input < 99)
            {
                result = input.ToString("F1") + "S";
            }
            else
            {
                input = (int)input;
                result = input.ToString() + "S";
            }
        }
        else if (input > 1000000000000000)
        {
            input = input / 1000000000000000;
            if (input < 9)
            {
                result = input.ToString("F2") + "Q";
            }
            else if (input < 99)
            {
                result = input.ToString("F1") + "Q";
            }
            else
            {
                input = (int)input;
                result = input.ToString() + "Q";
            }
        }
        else if (input > 1000000000000)
        {
            input = input / 1000000000000;
            if (input < 9)
            {
                result = input.ToString("F2") + "T";
            }
            else if (input < 99)
            {
                result = input.ToString("F1") + "T";
            }
            else
            {
                input = (int)input;
                result = input.ToString() + "T";
            }
        }
        else if (input > 1000000000)
        {
            input = input / 1000000000;
            if (input < 9)
            {
                result = input.ToString("F2") + "B";
            }
            else if (input < 99)
            {
                result = input.ToString("F1") + "B";
            }
            else
            {
                input = (int)input;
                result = input.ToString() + "B";
            }
        }
        else if (input > 1000000)
        {
            input = input / 1000000;
            if (input < 9)
            {
                result = input.ToString("F2") + "M";
            }
            else if (input < 99)
            {
                result = input.ToString("F1") + "M";
            }
            else
            {
                input = (int)input;
                result = input.ToString() + "M";
            }
        }
        else if (input > 10000)
        {
            input = input / 1000;
            if (input < 9)
            {
                result = input.ToString("F2") + "K";
            }
            else if (input < 99)
            {
                result = input.ToString("F1") + "K";
            }
            else
            {
                input = (int)input;
                result = input.ToString() + "K";
            }
        }
        else
        {
            if (input < 9)
            {
                result = input.ToString("F2");
            }
            else if (input < 99)
            {
                result = input.ToString("F1");
            }
            else
            {
                input = (int)input;
                result = input.ToString();
            }
        }

        return result;
    }

    public void OnAdWatchAdCountPlus()
    {
        CashCoin += adBonusVal;
        // Calculate 10% of the original number.
        decimal tenPercent = decimal.Truncate(adBonusVal / adPercentVal);

        adBonusVal += tenPercent;
        gmMgr.gsSaveMgrSql.UpdateGtGameState("AdBonusVal", ConvDecimalToStrg(adBonusVal));
        UpdateCredit();
    }
}
