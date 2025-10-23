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

    [Header("Keybinding UI")]
    [SerializeField] GameObject bindingObject;
    [SerializeField] Transform bindingParent;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {

                Destroy(gameObject);
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
        bindingParent.gameObject.SetActive(false);
        
    }

    public TextMeshProUGUI PopulateBinding(string bindingName, string key, InputHandler inputHandler)
    {
        GameObject text = Instantiate(bindingObject, bindingParent.GetChild(0));
        text.GetComponent<PlayerInputButton>().inputHandler = inputHandler;
        text.GetComponent<PlayerInputButton>().inputName = bindingName;
        text.GetComponent<TextMeshProUGUI>().text = bindingName;
        TextMeshProUGUI keytext = text.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        keytext.text = key;
        return keytext;
    }
    public void UpdateKeyText(TextMeshProUGUI textToUpdate, string newBinding)
    {
        textToUpdate.text = newBinding;
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
            bindingParent.gameObject.SetActive(false);
            killCountText.text = "Bunnies slaughtered: " + numCuteKilled;
            timeSurvived.text = "Time survived: " + (maxTime - timerAmount) + "s";
        
    }

    public void ToggleBindingMenu(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            bindingParent.gameObject.SetActive(!bindingParent.gameObject.activeSelf);
            if (bindingParent.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
