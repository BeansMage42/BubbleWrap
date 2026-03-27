using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using UnityEngine.Animations;
using static Unity.VisualScripting.Member;

public class CuteCreature : MonoBehaviour, ICreature
{
    // Start is called before the first frame update

    private NavMeshAgent ai;
    private TempGore gore;
    private Vector3 currentTargetPoint;
    [SerializeField]private float distance;
    public bool aggressive;
    [SerializeField]private bool chasingPlayer;

    [SerializeField] bool isBubbled;

    private PlayerHealth playerController;

    [SerializeField] GameObject pickUpPrefab;
    [SerializeField] PickUpFactory pickUpFactory;
    [SerializeField] bool isKing;

    private Coroutine waitRoutine;
    private Coroutine walkRoutine;

    [SerializeField] private float damage;
    [SerializeField] private float attackDelay;
    private float attackTimer;
    bool canAttack;

    [SerializeField] Animator anims;
    AudioSource source;
    [SerializeField] AudioClip goreSound;
    [SerializeField] AudioClip stabSound;

    public bool explode;
    public bool Boid;
    [SerializeField] float waitTime;
    void Start()
    {
        source = GetComponent<AudioSource>();
        anims = GetComponentInChildren<Animator>();
        ai = GetComponent<NavMeshAgent>();
        if(!Boid)StartCoroutine(WanderToMotion());
        playerController = FindAnyObjectByType<PlayerHealth>();
        if(GameManager.instance != null)GameManager.instance.addCreature(this);
        gore = GetComponent<TempGore>();
        attackTimer = attackDelay / 2f;
        if (!isKing && !isBubbled && !aggressive)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBubbled) return;
       if(!Boid) anims.SetFloat("Speed", ai.velocity.magnitude);
        else
        {
            anims.SetFloat("Speed", 1f);
        }
        if (chasingPlayer && !Boid) 
        {
            
            // print("chasing behaviour");
            ai.SetDestination(playerController.transform.position);

            if (ai.remainingDistance <= (ai.stoppingDistance+2))
            {
                ai.isStopped = true;
                attackTimer += Time.deltaTime;
                if(attackTimer >= attackDelay)
                {
                    attackTimer = 0;
                    Attack();
                }
            }
            else
            {
                ai.isStopped = false;
            }
        }

        if (explode == true)
        {
            TakeDamage();
            explode = false;
        }
    }

    private void Attack()
    {
       // print("attack");
       if (!isBubbled && !Boid && Vector3.Distance(playerController.gameObject.transform.position,gameObject.transform.position)<= ai.stoppingDistance + 2)
       {
           source.clip = stabSound;
           source.Play();
           playerController.TakeDamage(damage);
       }
       else if(!isBubbled && Vector3.Distance(playerController.gameObject.transform.position,gameObject.transform.position) <= 1)
        {

        }
    }
    

    private IEnumerator WanderToMotion()
    {
        
       
        SetTarget(CreatePosition());
       // Debug.Log("start moving to destination");
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
      // print("current pos: " + transform.position + " SamplePos: " + navHit.position);
       

            
        return navHit.position;
    }
    private IEnumerator Wait()
    {
        
       // print("waiting");
        yield return new WaitForSeconds(waitTime);
       // print("wait finished");
        walkRoutine = StartCoroutine(WanderToMotion());
    }

    private void SetTarget(Vector3 point)
    {
        ai.SetDestination(point);
        ai.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBubbled || Boid) return;
        if (aggressive && other.CompareTag("Player")) 
        {
            ai.isStopped = false ;
            StopAllCoroutines();
            chasingPlayer = true;
            ai.speed = 7;
           // print("chasing player");
        }
    }

    public void TakeDamage()
    {
        if (isKing)
        {
            GameManager.instance.ActivateSleeperAgent();
        }
        
        StopAllCoroutines();
        Die();
    }

    private void Die()
    {
       // print("die");
       
        //int chance = (int)Random.Range(0, 3);
        if (pickUpPrefab != null)
        {
          
                pickUpFactory.SpawnIPickUp(transform.position);
               
        }
       
        gore.Pop();
        ai.enabled = true;
        isBubbled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        if(isKing)Destroy(gameObject);
        if(!isKing && GameManager.instance != null)GameManager.instance.RemoveCreature(this);
    }

    public void Bubble()
    {
        
        GetComponent<Rigidbody>().isKinematic = true;
        anims.SetFloat("Speed", 0);
        StopAllCoroutines();
        if(!Boid) ai.enabled = false;
        isBubbled = true;

    }
    public bool IsBubbled()
    {
        return isBubbled;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isBubbled) return;
        if(collision.collider.CompareTag("Ground") )
        {
            source.clip = goreSound;
            AudioSource.PlayClipAtPoint(goreSound, transform.position,30f);
            TakeDamage();
        }
    }

    public void Initialize()
    {
        
    }
}
