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
    float spawnTimer;

    [SerializeField] float minScale, maxScale;

    bool hasCaputeredEnemy;
    bool hasBounced;
    private CuteCreature capturedCreature;

    private BubblePop bubblePop;
    float speedMod;

    int bubbleSizeIncreases;

   
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bubblePop = GetComponent<BubblePop>();
        transform.localScale *= Random.Range(minScale, maxScale);
        lifeTime *= Random.Range(minScale, maxScale);
        speedMod = Random.Range(minScale, maxScale)/2;
        //transform.forward = rb.velocity.normalized;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        if (rb.velocity.magnitude > 0 && timer >= varianceDelay && !hasCaputeredEnemy && !hasBounced) 
        {
            rb.velocity = (targetDir + AddNoiseOnAngle(minNoise, maxNoise)).normalized * rb.velocity.magnitude;
            timer = 0;

        }
        if(spawnTimer >= lifeTime && !hasCaputeredEnemy)
        {
            print("destroy because life time");
            bubblePop.Pop();
            Destroy(this);
        }

    }

    public void SetMotion(Vector3 dir, float startSpeed)
    {
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
            print("destroy because smaller");
            bubblePop.Pop();
            Destroy(this);
        }
        else if(bubbleSizeIncreases < 2)
        {
            bubbleSizeIncreases++;
            transform.localScale *= 2;
        }
        
    }

    private void CaptureEnemy(CuteCreature cute)
    {
        Debug.Log("bubble");
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
        capturedCreature.transform.parent = null;
        capturedCreature.GetComponent<Rigidbody>().isKinematic = false;
        print("destroy because pop timer");
        bubblePop.Pop();
        Destroy(this);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);

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
