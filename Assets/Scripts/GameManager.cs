using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerController;

    private List<CuteCreature> cuteCreatures = new List<CuteCreature>();

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private TextMeshProUGUI timerText;
    private float timerAmount = 0;
    private float maxTime;
    private bool timerOn;

    [SerializeField] private HeavyMetalStarts moodSetter;

    private PlayerController PlayerController;
    [Header("Upgradetext")]
    [SerializeField]private GameObject upgradeText;
    Vector3 upgradeTextStartPos;
    [SerializeField]float upgradeTextFloatSpeed;
    [SerializeField] float upgradeTextFadeSpeed;

    [Header("Healthbar")]
    [SerializeField] Image healthFill;

    [SerializeField] private GameObject healthBar;

    [Header("GameRules")] [SerializeField] private Transform spawnPos;
    [SerializeField] int maxCuteCreatures;
    [SerializeField] float maxSpawnDistanceFromCenter;
    [SerializeField] float timeBetweenSpawns;
     float respawnTimer;
    bool kingDead;


    [SerializeField] GameObject bunnyPrefab;

    private int numCuteKilled;

    [Header("ENDGAME")]
    [SerializeField] GameObject endScreen;
    [SerializeField] TextMeshProUGUI winloseText;
    [SerializeField] TextMeshProUGUI killCountText;
    [SerializeField] TextMeshProUGUI timeSurvived;
    bool gameActive = true;

    bool trackSwitched;

    [SerializeField] TextMeshProUGUI goalText;
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

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        endScreen.SetActive(false);
        maxTime = timerAmount;
        healthFill.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameActive) return;
        if (timerAmount > -1 && timerOn)
        {
            // Subtract elapsed time every frame
            timerAmount += Time.deltaTime;

            // Divide the time by 60
            float minutes = Mathf.FloorToInt(timerAmount / 60);

            // Returns the remainder
            float seconds = Mathf.FloorToInt(timerAmount % 60);

            // Set the text string
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (timerOn)
        {
            timerText.text = "Time's up";
            timerAmount = 0;
            EndGame("You Survived!");
        }
        
        if(cuteCreatures.Count < maxCuteCreatures && kingDead)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= timeBetweenSpawns)
            {
                respawnTimer = 0;
                SpawnCreature();
            }
        }


    }

    private void SpawnCreature()
    {
        print("spawn");
        Vector3 spawnpoint = spawnPos.position + UnityEngine.Random.insideUnitSphere * Random.Range(1,maxSpawnDistanceFromCenter);
       // print("spawn");
        spawnpoint.y = 1;
        CuteCreature newCreature = Instantiate(bunnyPrefab, spawnpoint, Quaternion.identity).GetComponent<CuteCreature>();
        newCreature.aggressive = true;
        
        
    }
    public void addCreature(CuteCreature creature)
    {
        cuteCreatures.Add(creature);
    }
    public void RemoveCreature(CuteCreature creature) 
    {
       // if (cuteCreatures.Contains(creature)) { Debug.Log("contains"); }
        cuteCreatures.Remove(creature);
        if (gameActive)
        {
            numCuteKilled++;
        }
    }

    public PlayerController GetPlayer()
    {
        return playerController;
    }
    public void ActivateSleeperAgent()
    {
        healthFill.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        timerOn = true;
        moodSetter.ChangeMood();
        if (!trackSwitched)
        {
            trackSwitched = true;
            FindAnyObjectByType<MusicPlayer>().SwapTracks();
        }
        goalText.text = "Objective:\n-Survive";
       // print("sleepers activated");
        if (cuteCreatures.Count > 0)
        {
            kingDead = true;
            foreach (var creature in cuteCreatures)
            {
                creature.gameObject.SetActive(true);
                creature.aggressive = true;
            }
        }

    }

    

    public void UpgradeCollectedDisplay(string upgradeType)
    {

        Instantiate(upgradeText, mainCanvas.transform).GetComponent<FloatingText>().CreateText(upgradeType,upgradeTextFloatSpeed,upgradeTextFadeSpeed);

    }

    public void AdjustHealth(float healthRatio)
    {
        if(gameActive) healthFill.fillAmount = healthRatio;
    }

    public void AdjustMagazines(float magazinesRatio)
    {
        if(gameActive) magazinesCount.text = "Bubble bottles left: " + magazinesRatio.ToString();
    }

    public void PlayerDied()
    {
        EndGame("You Died!");
    }

    private void EndGame(string winlose)
    {
        if (gameActive)
        {
            Cursor.lockState = CursorLockMode.Confined;
            gameActive = false;
            playerController.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("GameMode").Disable();
            endScreen.SetActive(true);

            winloseText.text = winlose;

            killCountText.text = "Bunnies slaughtered: " + numCuteKilled;
            timeSurvived.text = "Time survived: " + (maxTime - timerAmount) + "s";
        }
    }
    
}
