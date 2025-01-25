using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class CuteCreature : MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshAgent ai;
    private TempGore gore;
    private Vector3 currentTargetPoint;
    [SerializeField]private float distance;
    public bool aggressive;
    [SerializeField]private bool chasingPlayer;

    [SerializeField] bool isBubbled;

    private PlayerController playerController;

    [SerializeField] GameObject pickUpPrefab;
    [SerializeField] bool isKing;

    private Coroutine waitRoutine;
    private Coroutine walkRoutine;
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
            print("chasing behaviour");
            ai.SetDestination(playerController.transform.position);
        }
    }

    private IEnumerator WalkToMotion()
    {
        
       
        SetTarget(CreatePosition());
        Debug.Log("start moving to destination");
        yield return new WaitUntil(() => ai.remainingDistance <= ai.stoppingDistance && !ai.pathPending);
        ai.isStopped = true;
        ai.destination = transform.position;
        waitRoutine = StartCoroutine(Wait());
        
        
        //State = defaultState;
    }

    private Vector3 CreatePosition()
    {

        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += transform.position;
        randomDirection.y = 0;
        NavMeshHit navHit;

       if(! NavMesh.SamplePosition(randomDirection, out navHit, distance, ai.areaMask))
        {
            Debug.LogWarning("couldnt find position");
        }
       print("current pos: " + transform.position + " SamplePos: " + navHit.position);
       

            
        return navHit.position;
    }
    private IEnumerator Wait()
    {
        
        print("waiting");
        yield return new WaitForSeconds(2f);
        print("wait finished");
        walkRoutine = StartCoroutine(WalkToMotion());
    }

    private void SetTarget(Vector3 point)
    {
        ai.SetDestination(point);
        ai.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (aggressive && other.tag == "Player" && !isBubbled) 
        {
            ai.isStopped = false ;
            StopAllCoroutines();
            chasingPlayer = true;
            print("chasing player");
        }
    }

    public void TakeDamage()
    {
        if (isKing)
        {
            print("isKing");
            GameManager.instance.ActivateSleeperAgent();
        }
        Die();
    }

    private void Die()
    {
        print("die");
        GameManager.instance.RemoveCreature(this);
        int chance = (int)Random.Range(0, 3);
        if (pickUpPrefab != null)
        {
            print("has prize");
            if (chance > 1)
            {
                print("spawn prize");
                Instantiate(pickUpPrefab, transform.position + Vector3.up, Quaternion.identity);
            }
        }
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
