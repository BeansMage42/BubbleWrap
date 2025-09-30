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

    private float timerAmount = 0;
    private bool timerOn;

    [SerializeField] private HeavyMetalStarts moodSetter;

    [Header("GameRules")] [SerializeField] private Transform spawnPos;
    [SerializeField] int maxCuteCreatures;
    [SerializeField] float maxSpawnDistanceFromCenter;
    [SerializeField] float timeBetweenSpawns;
     float respawnTimer;
    bool kingDead;


    [SerializeField] GameObject bunnyPrefab;

    private int numCuteKilled;

    bool gameActive = true;

    bool trackSwitched;

    public bool isPlayerDead;
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
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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
           UIManager.instance.UpdateTimer(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
        else if (timerOn)
        {
            UIManager.instance.UpdateTimer( "Time's up");
            timerAmount = 0;
            EndGame("You Survived!");
        }
        
        if(cuteCreatures.Count < maxCuteCreatures && kingDead)
        {
            respawnTimer += Time.deltaTime;
            if (timeBetweenSpawns > 1)
                timeBetweenSpawns -= Time.deltaTime * 0.01f;
            if (respawnTimer >= (timeBetweenSpawns))
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
        UIManager.instance.ActivateUI();
        timerOn = true;
        moodSetter.ChangeMood();
        if (!trackSwitched)
        {
            trackSwitched = true;
            FindAnyObjectByType<MusicPlayer>().SwapTracks();
        }
       
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

    public void PlayerSurvived()
    {
        EndGame("You Survived!");
    }
    public void PlayerDied()
    {
        isPlayerDead = true;
        EndGame("You Died!");
    }

    private void EndGame(string winlose)
    {
        if (gameActive)
        {
            Cursor.lockState = CursorLockMode.None;
            gameActive = false;
            playerController.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("GameMode").Disable();
            UIManager.instance.ConfigureDeathScreen(winlose, numCuteKilled);
        }
    }
    
}
