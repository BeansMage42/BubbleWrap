using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Main Canvases")]
    [SerializeField] private Canvas mainCanvas;
    [Header("Game Timer")]
    [SerializeField] private TextMeshProUGUI timerText;
    private float timerAmount = 0;
    private float maxTime;
    private bool timerOn;

    [SerializeField] private HeavyMetalStarts moodSetter;

    private PlayerController PlayerController;
    [Header("Upgradetext")]
    [SerializeField] private GameObject upgradeText;
    Vector3 upgradeTextStartPos;
    [SerializeField] float upgradeTextFloatSpeed;
    [SerializeField] float upgradeTextFadeSpeed;

    [Header("Healthbar")]
    [SerializeField] Image healthFill;

    [SerializeField] private GameObject healthBar;

    [Header("ENDGAME")]
    [SerializeField] GameObject endScreen;
    [SerializeField] TextMeshProUGUI winloseText;
    [SerializeField] TextMeshProUGUI killCountText;
    [SerializeField] TextMeshProUGUI timeSurvived;

    [Header("Goals")]
    [SerializeField] TextMeshProUGUI goalText;

    [Header("Gun UI")]
    [SerializeField] TextMeshProUGUI magazinesCount;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {

                Destroy(this);
            }
        }
        else
        {
            instance = this;
            
        }

    }
    void Start()
    {
        endScreen.SetActive(false);
        maxTime = timerAmount;
        healthFill.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    public void ActivateUI()
    {
        healthFill.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        goalText.text = "Objective:\n-Survive";
    }
    
    public void UpdateTimer(string time)
    {
        timerText.text = time;
    }

    public void UpgradeCollectedDisplay(string upgradeType)
    {

        Instantiate(upgradeText, mainCanvas.transform).GetComponent<FloatingText>().CreateText(upgradeType, upgradeTextFloatSpeed, upgradeTextFadeSpeed);

    }

    public void AdjustHealth(float healthRatio)
    {
         healthFill.fillAmount = healthRatio;
    }

    public void AdjustMagazines(float magazinesRatio)
    {
         magazinesCount.text = "Bubble bottles left: " + magazinesRatio.ToString();
    }


    public void ConfigureDeathScreen(string winlose, int numCuteKilled)
    {
            endScreen.SetActive(true);

            winloseText.text = winlose;

            killCountText.text = "Bunnies slaughtered: " + numCuteKilled;
            timeSurvived.text = "Time survived: " + (maxTime - timerAmount) + "s";
        
    }
}
