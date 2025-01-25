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
    private CuteCreature capturedCreature;

    private BubblePop bubblePop;

   
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bubblePop = GetComponent<BubblePop>();
        transform.localScale *= Random.Range(minScale, maxScale);
        lifeTime *= Random.Range(minScale, maxScale);
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
        if (rb.velocity.magnitude > 0 && timer >= varianceDelay && !hasCaputeredEnemy) 
        {
            rb.velocity = (targetDir + AddNoiseOnAngle(minNoise, maxNoise)).normalized * rb.velocity.magnitude;
            timer = 0;

        }
        if(spawnTimer >= lifeTime && !hasCaputeredEnemy)
        {
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
            bubblePop.Pop();
            Destroy(this);
        }
        else if (col.transform.position.y > transform.position.y)
        {
           // print("destroy");
           bubblePop.Pop();
           Destroy(this);
        }
        else if (col.transform.position.y < transform.position.y)
        {
            transform.localScale *= 2; 
        }
        else if(Vector3.Dot(transform.position, col.transform.position) > 0)
        {
            transform.localScale *= 2;
        }
        else
        {
           // print("destroy");
           bubblePop.Pop();
           Destroy(this);
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
                    Destroy(this);
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
                    bubblePop.Pop();
                    Destroy(this);
                    break;
                



            }
        }

    }
}
