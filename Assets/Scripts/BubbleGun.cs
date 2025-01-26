using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class BubbleGun : MonoBehaviour
{


    [Header("GunStats")]
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected int numProjectile;
    [SerializeField] private int magazineSize;
    [SerializeField] private float reloadSpeed;
    private int currentMagLeft;
    private float magSize;
    [SerializeField] int numberOfMagazines;
    [SerializeField] private bool isFullyAuto;
    

    private Coroutine currentFireTimer;


    private WaitUntil myWaitFunc;
    private bool isOnCoolDown;
    private bool isReloading;

    

    [Header("BULLET")]
    [SerializeField] protected Vector3 bulletSpreadVariance;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] private float projectileSpeed;

    [Header("OTHER")]
    [SerializeField] protected Transform shootPoint;
    [SerializeField] private SkinnedMeshRenderer shart;
    [SerializeField] private VolumeAdjuster bubbleContainer;
    
     private PlayerController playerController;

    [Header("AUDIO")]
    AudioSource audioSource;
    [SerializeField] AudioClip squirt;

    Animator anims;
    bool outOfAmmo;
    private void Awake()
    {
        anims = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        magSize = 1f / magazineSize;
        //print(magSize);
        myWaitFunc = new WaitUntil(() => !isOnCoolDown);
        currentMagLeft = magazineSize;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)StartFiring();
        else if (context.canceled)StopFiring();

    }

    public void StartFiring()
    {
        if (!isReloading)
        {
            shart.SetBlendShapeWeight(0, 100);
        }
        currentFireTimer = StartCoroutine(ReFireTimer());
    }
    public void StopFiring()
    {
        //Debug.Log("STOP");

        //TryAttack();
        shart.SetBlendShapeWeight(0, 0);
        StopCoroutine(currentFireTimer);

    }

    private void TryAttack()
    {
        Debug.Log(CanAttack());
        if (!CanAttack()) return;
        Attack();
        StartCoroutine(CoolDown());

        if (isFullyAuto) currentFireTimer = StartCoroutine(ReFireTimer());

    }

     private bool CanAttack()
    {
        
        return !isOnCoolDown && !isReloading && !outOfAmmo;
    }

    public void Attack()
    {

        //Debug.Log("pew");
        audioSource.PlayOneShot(squirt);
        for (int i = 0; i < numProjectile; i++)
        {
            currentMagLeft--;
            bubbleContainer.SetAmount((float)currentMagLeft * magSize);
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity).GetComponent<Bubble>().SetMotion(shootPoint.forward + GetDirection(),projectileSpeed);
            if (currentMagLeft <= 0)
            {
                break;
                
            }
        }
        if (currentMagLeft <= 0 && numberOfMagazines > 0)
        {
            StartCoroutine(Reload());
        }
        else if (currentMagLeft <= 0 && numberOfMagazines == 0)
        {
            outOfAmmo = true;
        }
        


    }
    private Vector3 GetDirection()
    {
        

        Vector3 direction = transform.forward;

        if (bulletSpreadVariance.x > 0)
        {
            direction += new Vector3(
                Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x),
                Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y),
                Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)
                );
            direction.Normalize();
        }
        return direction;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        anims.SetTrigger("Reload");
       // Debug.Log("start reloading");
        yield return new WaitForSeconds(reloadSpeed);
        numberOfMagazines--;
        GameManager.instance.AdjustMagazines(numberOfMagazines);
        currentMagLeft = magazineSize;
        isReloading = false;
        outOfAmmo = false;
        bubbleContainer.SetAmount((float)currentMagLeft * magSize);
    }

    private IEnumerator ReFireTimer()
    {
        
       // print("pre cooldown");
        yield return myWaitFunc;
       // print("post cooldown");

        TryAttack();
        yield return null;
    }

    private IEnumerator CoolDown()
    {
      //  Debug.Log("onCoolDown");
        isOnCoolDown = true;
        yield return new WaitForSeconds(timeBetweenAttacks);
        isOnCoolDown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("gunTrigger");
        if (other.tag == "Pickup")
        {
            string pickupType = "";   
            pickupType = other.GetComponent<PickUp>().Collect().ToString();
            

            switch (pickupType)
            {
                case "MAGSIZE":
                    magazineSize += 5;
                    magSize = 1f / magazineSize;
                    pickupType = "Bubble fluid capacity increased";
                    break;
                case "FIRERATE":
                    timeBetweenAttacks /= 1.4f;
                    pickupType = "Fire rate increased";
                    break;
                case"BULLETSPREAD":
                    bulletSpreadVariance *= 0.70f;
                    pickupType = "Accuracy increased";
                    break;
                case "HEALTHBONUS":
                    playerController.TakeDamage(-30);
                    pickupType = "Healed";
                    break;
                case "PROJECTILESPEED":
                    projectileSpeed *= 1.4f;
                    pickupType = "Bubble speed increased";
                    break;
                case "MOREAMMO":
                    numberOfMagazines+=1;
                    GameManager.instance.AdjustMagazines(numberOfMagazines);
                    if (outOfAmmo)
                    {
                        StartCoroutine(Reload());
                    }
                    pickupType = "Bubble fluid found";
                    break;
        
            }
            other.GetComponent<PickUp>().PopThisBubble();
            GameManager.instance.UpgradeCollectedDisplay(pickupType);

        }
    }

}
