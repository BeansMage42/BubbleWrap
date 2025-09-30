using UnityEngine;

public class VolumeAdjuster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    MeshRenderer objectRenderer;
    [SerializeField] private Transform lid;
    [SerializeField] private Transform bottom;
    private Material liquidMat;

    [SerializeField] private float amount;
    [SerializeField] private float volume;
    
    private MaterialPropertyBlock propBlock;
    
    void Start()
    {
        objectRenderer = GetComponent<MeshRenderer>();
        liquidMat = objectRenderer.material;
        propBlock = new MaterialPropertyBlock();
        liquidMat.SetFloat("_Amount", amount);
        //Mathf.Sin(pulse * time)
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localLid = lid.position - bottom.position;
        localLid = Vector3.Normalize(localLid);
        
        float adjustAmount = Vector3.Dot(localLid, Vector3.up) / volume;
        adjustAmount += amount;
        //liquidMat.SetFloat("_Amount", adjustAmount);
        objectRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_Amount", amount); 
        objectRenderer.SetPropertyBlock(propBlock);
    }
    
    public void SetAmount(float fluid)
    { 
        fluid *= 2;
        fluid -= 1;
        amount = fluid;
    }
}
