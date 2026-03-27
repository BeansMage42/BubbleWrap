using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] private Transform leader;
    Agent [ ] agents;
    // Start is called before the first frame update
    void Start()
    {
        agents = GetComponentsInChildren<Agent>();
        
        for (int i = 0; i < agents.Length; i++)
        {
            agents[i].SetLeader(leader);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(leader == null)
        {
            leader = agents.First().transform;
        }
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].enabled)
                agents[i].CalculateMovement();
        }
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].enabled)
                agents[i].UpdateMovement();
        }
    }
}
