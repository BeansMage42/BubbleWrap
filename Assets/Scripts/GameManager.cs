using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public PlayerController playerController;

    private List<CuteCreature> cuteCreatures = new List<CuteCreature>();

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCreature(CuteCreature creature)
    {
        cuteCreatures.Add(creature);
    }
    public void RemoveCreature(CuteCreature creature) 
    { 
        cuteCreatures.Remove(creature);
    }


    public void ActivateSleeperAgent()
    {
        if (cuteCreatures.Count > 0)
        {
            foreach (var creature in cuteCreatures)
            {
                creature.aggressive = true;
            }
        }

    }
}
