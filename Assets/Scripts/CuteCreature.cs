using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using static UnityEngine.UI.Image;

public class CuteCreature : MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshAgent ai;
    private TempGore gore;
    private Vector3 currentTargetPoint;
    [SerializeField]private float distance;
    public bool aggressive;
    [SerializeField]private bool chasingPlayer;

    bool isBubbled;

    private PlayerController playerController;
    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        StartCoroutine(WalkToMotion());
        playerController = GameManager.instance.playerController;
        GameManager.instance.addCreature(this);
        gore = GetComponent<TempGore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chasingPlayer && !isBubbled) 
        {
            ai.SetDestination(playerController.transform.position);
        }
    }

    private IEnumerator WalkToMotion()
    {
        
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += transform.position;
        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);

        
        SetTarget(navHit.position);
        Debug.Log("start moving to destination");
        yield return new WaitUntil(() => ai.remainingDistance <= ai.stoppingDistance);
        StartCoroutine(Wait());
        
        
        //State = defaultState;
    }
    private IEnumerator Wait()
    {
        print("waiting");
        yield return new WaitForSeconds(2f);
        
        StartCoroutine(WalkToMotion());
    }

    private void SetTarget(Vector3 point)
    {
        ai.SetDestination(point);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (aggressive && other.tag == "Player") 
        {
        
            StopAllCoroutines();
            chasingPlayer = true;
        
        }
    }

    public void TakeDamage()
    {
        if (!aggressive)
        {
            GameManager.instance.ActivateSleeperAgent();
        }
        Die();
    }

    private void Die()
    {
        GameManager.instance.RemoveCreature(this);
        gore.Pop();
    }

    public void Bubble()
    {
        
        GetComponent<Rigidbody>().isKinematic = true;
        StopAllCoroutines();
        ai.enabled = false;
        isBubbled = true;

    }
    public bool IsBubbled()
    {
        return isBubbled;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground" && isBubbled)
        {
            TakeDamage();
        }
    }

}
