using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bubble : MonoBehaviour
{
    [SerializeField] float minNoise;
    [SerializeField] float maxNoise;
    Rigidbody rb;
    Vector3 targetDir;
    [SerializeField] private float varianceDelay;
    private float timer;

    [SerializeField]float spawnDelayBeforeDestroyable;
    [SerializeField] float lifeTime;
    private float life;
    float spawnTimer;

    [SerializeField] float minScale, maxScale;

    bool hasCaputeredEnemy;
    bool hasBounced;
    private CuteCreature capturedCreature;

    public BubblePop bubblePop;
    float speedMod;

    int bubbleSizeIncreases;

    bool isActive = false;
    public GameObjectPool pool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bubblePop = GetComponent<BubblePop>();
        bubblePop.popped += () => pool.ReturnToPool(gameObject);
    }
    private void Activate()
    {
       
        transform.localScale = new Vector3 (1.5f, 1.5f,1.5f) * Random.Range(minScale, maxScale);
        life = lifeTime * Random.Range(minScale, maxScale);
        spawnTimer = 0;
        timer = 0;
        speedMod = Random.Range(minScale, maxScale) / 2;
        isActive = true;
        hasCaputeredEnemy = false;
        hasBounced = false;
        capturedCreature = null;

    }
    private void OnEnable()
    {
        Activate();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        timer += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        if (rb.velocity.magnitude > 0 && timer >= varianceDelay && !hasBounced) 
        {
            rb.velocity = (targetDir + AddNoiseOnAngle(minNoise, maxNoise)).normalized * rb.velocity.magnitude;
            timer = 0;

        }
        if(spawnTimer >= life)
        {
           // print("destroy because life time");
            isActive = false;
            bubblePop.Pop();
            life = lifeTime;
            
        }

    }
    public void SetMotion(Vector3 dir, float startSpeed)
    {
        //Activate();
        targetDir = dir;
        rb.velocity = dir * startSpeed;
    }

    Vector3 AddNoiseOnAngle(float min, float max)
    {
       // Debug.Log("add wiggle");
        // Find random angle between min & max inclusive
        float xNoise = Random.Range(min, max);
        float yNoise = Random.Range(min, max);
        float zNoise = Random.Range(min, max);

        // Convert Angle to Vector3
        Vector3 noise = new Vector3(
          Mathf.Sin(2 * Mathf.PI * xNoise / 360),
          Mathf.Sin(2 * Mathf.PI * yNoise / 360),
          Mathf.Sin(2 * Mathf.PI * zNoise / 360)
        );
        return noise;
    }

    private void PosChecks(Collider col)
    {
        if(spawnTimer >= spawnDelayBeforeDestroyable && hasCaputeredEnemy) return; 
        if(col.transform.localScale.magnitude > transform.localScale.magnitude)
        {
           // print("destroy because smaller");
            bubblePop.Pop();
            pool.ReturnToPool(gameObject);
        }
        else if(bubbleSizeIncreases < 2)
        {
            bubbleSizeIncreases++;
            transform.localScale *= 2;
        }
        
    }

    private void CaptureEnemy(CuteCreature cute)
    {
       // Debug.Log("bubble");
        if (capturedCreature != null) return;
        isActive = false;
        capturedCreature = cute;
        capturedCreature.Bubble();
        
        
        capturedCreature.transform.parent = transform;
        capturedCreature.transform.position = transform.position;
        rb.velocity = Vector3.up * 4;
        StartCoroutine(PopTimer());
    }

    private IEnumerator PopTimer()
    {
        yield return new WaitForSeconds(5f);
        if (capturedCreature != null)
        {
            capturedCreature.transform.parent = null;
            capturedCreature.GetComponent<Rigidbody>().isKinematic = false;
           // print("destroy because pop timer");
        }
        bubblePop.Pop();
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
       // print(other.gameObject.name);

        if (!hasCaputeredEnemy)
        {
            switch (other.tag)
            {
                case "Bubble":
                    PosChecks(other);
                    
                    break;
                case "Cute":

                    if (!other.gameObject.GetComponentInParent<CuteCreature>().IsBubbled())
                    {
                        hasCaputeredEnemy = true;
                        CaptureEnemy(other.gameObject.GetComponentInParent<CuteCreature>());
                    }
                    break;
                case "Obstacle":
                case "Ground":
                    hasBounced = true;
                    rb.AddForce((Vector3.up *3) + (targetDir * 2),ForceMode.Impulse);

                    break;
                



            }
        }

    }
}
