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
    //public PlayerController playerController;
    public CollideAndSlideController playerController;

    // private List<CuteCreature> cuteCreatures = new List<CuteCreature>();
    private GameObjectPool bunnyPool;

    private float timerAmount = 0;

    [SerializeField] private HeavyMetalStarts moodSetter;

    [Header("GameRules")] [SerializeField] private Transform spawnPos;
    [SerializeField] int maxCuteCreatures;
    private int activeCreatures;
    [SerializeField] float maxSpawnDistanceFromCenter;
    [SerializeField] float timeBetweenSpawns;
     float respawnTimer;


    [SerializeField] GameObject bunnyPrefab;
    [SerializeField] CreatureFactory creatureFactory;

    private int numCuteKilled;

    /*bool kingDead;
    bool gameActive = true;
    bool trackSwitched;
    private bool timerOn;
    public bool isPlayerDead;*/



    public GameState currentState;
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
        //playerController = FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.OnPlayerDeath += PlayerDied;
        playerController = FindObjectOfType<CollideAndSlideController>();
        moodSetter = FindObjectOfType<HeavyMetalStarts>();
        /*trackSwitched = false;
        isPlayerDead = false;
        kingDead = false;
        timerOn = false;
        gameActive = true;*/
        currentState = GameState.GameStart;
        timerAmount = 0;
        respawnTimer = 0;
        activeCreatures = 10;
        bunnyPool = new GameObjectPool(bunnyPrefab, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != GameState.GameSwitch) return;
        if (timerAmount > -1 )
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
        
        if(activeCreatures < maxCuteCreatures)
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
        activeCreatures++;
        print("spawn");
        Vector3 spawnpoint = spawnPos.position + UnityEngine.Random.insideUnitSphere * Random.Range(1,maxSpawnDistanceFromCenter);
       // print("spawn");
        spawnpoint.y = 1;
        GameObject newCreature = null;
        if (bunnyPool.pool.Count > 0)
        {
            newCreature = bunnyPool.GetPoolObject();
            newCreature.transform.position = spawnpoint;
            newCreature.SetActive(true);
        }
        else
        {
          newCreature =  creatureFactory.SpawnICreature(spawnpoint);
          newCreature.SetActive(true);
        }
        CuteCreature cuteCreature;
        if (TryGetComponent<CuteCreature>(out cuteCreature))
        {
            cuteCreature.aggressive = true;
        }
    }
    public void addCreature(CuteCreature creature)
    {
        
    }
    public void RemoveCreature(CuteCreature creature) 
    {
       bunnyPool.ReturnToPool(creature.gameObject);
        activeCreatures--;
        if (currentState == GameState.GameSwitch)
        {
            numCuteKilled++;
        }
    }

    public CollideAndSlideController GetPlayer()
    {
        if(playerController == null)
        {
            playerController = FindObjectOfType<CollideAndSlideController>();
        }
        return playerController;
    }
    public void ActivateSleeperAgent()
    {
        if(currentState != GameState.GameStart) return;
        currentState = GameState.GameSwitch;
        UIManager.instance.ActivateUI();
        //timerOn = true;
        moodSetter.ChangeMood();
            FindAnyObjectByType<MusicPlayer>().SwapTracks();
        
            foreach (var creature in bunnyPool.pool)
            {
                if(creature == null) continue;
                creature.SetActive(true);
                creature.GetComponent<CuteCreature>().aggressive = true;
            }
        

    }

    public void PlayerSurvived()
    {
        EndGame("You Survived!");
    }
    public void PlayerDied()
    {
        //isPlayerDead = true;
        EndGame("You Died!");
    }

    private void EndGame(string winlose)
    {
        if (currentState == GameState.GameSwitch)
        {
            currentState = GameState.GameEnd;
            Cursor.lockState = CursorLockMode.None;
            //gameActive = false;
            playerController.gameObject.GetComponent<PlayerInput>().actions.FindActionMap("GameMode").Disable();
            UIManager.instance.ConfigureDeathScreen(winlose, numCuteKilled);
        }
    }

    public void KillAllCreatures()
    {
        int i = bunnyPool.pool.Count;

        for(int x = 0; x < i -1; x++)
        {
            bunnyPool.pool[i].GetComponent<CuteCreature>().TakeDamage();
        }
    }
    
}

public enum GameState
{
    MainMenu,
    GameStart,
    GameSwitch,
    GameEnd

}