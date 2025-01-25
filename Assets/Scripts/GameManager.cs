using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public PlayerController playerController;

    private List<CuteCreature> cuteCreatures = new List<CuteCreature>();

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerAmount;
    

    private PlayerController PlayerController;
    [Header("Upgradetext")]
    [SerializeField]private GameObject upgradeText;
    Vector3 upgradeTextStartPos;
    [SerializeField]float upgradeTextFloatSpeed;
    [SerializeField] float upgradeTextFadeSpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (timerAmount > 0)
        {
            // Subtract elapsed time every frame
            timerAmount -= Time.deltaTime;

            // Divide the time by 60
            float minutes = Mathf.FloorToInt(timerAmount / 60);

            // Returns the remainder
            float seconds = Mathf.FloorToInt(timerAmount % 60);

            // Set the text string
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = "Time's up";
            timerAmount = 0;
        }
    }

    public void addCreature(CuteCreature creature)
    {
        cuteCreatures.Add(creature);
    }
    public void RemoveCreature(CuteCreature creature) 
    { 
        cuteCreatures.Remove(creature);
    }

    public PlayerController GetPlayer()
    {
        return playerController;
    }
    public void ActivateSleeperAgent()
    {
        print("sleepers activated");
        if (cuteCreatures.Count > 0)
        {
            foreach (var creature in cuteCreatures)
            {
                creature.aggressive = true;
            }
        }

    }

    public void UpgradeCollectedDisplay(string upgradeType)
    {

        Instantiate(upgradeText, mainCanvas.transform).GetComponent<FloatingText>().CreateText(upgradeType,upgradeTextFloatSpeed,upgradeTextFadeSpeed);

    }
    
}
