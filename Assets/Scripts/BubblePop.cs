using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePop : MonoBehaviour
{
    [SerializeField] private bool play;
    private Material mat;

    private ParticleSystem ps;

    private float popAmount;
    private bool pop;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void Pop()
    {
        pop = true;
        ps.Play();
        play = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (pop)
        {
            if (popAmount < 1)
            {
                popAmount += Time.deltaTime * 3;
                mat.SetFloat("_DissolveAmount", popAmount);
            }
            else
            {
                Destroy(GetComponent<MeshRenderer>());
                Destroy(GetComponent<SphereCollider>());
                pop = false;
            }
        }

        if (ps.isStopped && play)
        {
            Destroy(gameObject);
        }
    }
}
