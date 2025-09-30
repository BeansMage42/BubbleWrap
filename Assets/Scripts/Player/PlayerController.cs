using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDir;
    private Vector2 look;
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private GameObject camRotPoint;
    [SerializeField] private float minYAngle, maxYAngle;
    [SerializeField] private float maxSprintMod;
    private float currentSprintMod = 1;

    [SerializeField] float maxHealth;
    private float maxH;
    float currentHealth;


    [SerializeField] private Volume globalVolume;
    private Vignette jelly;


    bool isGrounded = true;
    [SerializeField] float jumpForce;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        maxH = 1 / maxHealth;
        globalVolume.profile.TryGet(out jelly);
    }
    
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
    
        rb.MovePosition( transform.position + (transform.rotation * moveDir * moveSpeed * currentSprintMod));

    }

    public void MoveDir(InputAction.CallbackContext context)
    {
        
        moveDir = context.ReadValue<Vector3>().normalized;
    }
    /*
     * The look function takes the players mouse delta and translates it to camera rotation
     * 
     * this function is ignored if the player is interacting with an NPC or is in a dialogue event, preventing them from moving the camera around while reading or pressing buttons
     * 
     * The camera is a child of the camRotPoint object, and that object is a child of the players global transform object. This allows the up/down movements to be independent of the body movements
     * this is useful for implimenting animations and proper models as it would allow the player to look up without their whole body tilting upwards
     * 
     */
    public void Look(InputAction.CallbackContext context)
    {
        
        Vector2 mouseDir = context.ReadValue<Vector2>() * rotSpeed;
        
        Quaternion yMove = Quaternion.Euler(-mouseDir.y, 0, 0);// the y has to be inverted because up is actually negative on the x axis, which is the axis of rotation but mouse delta treats up as positive and down as negative as it is tracking position on the cartesean plane
        Quaternion zMove = Quaternion.Euler(0, mouseDir.x, 0);
        
        camRotPoint.transform.rotation = camRotPoint.transform.rotation * yMove;


        //this section performs the clamping
        float Angle = camRotPoint.transform.eulerAngles.x;
        Angle = LockRotation(Angle, minYAngle, maxYAngle);
        //factors in parents rotation to convert the rotation from local to global space to prevent errors
        camRotPoint.transform.rotation = transform.rotation * Quaternion.Euler(Angle, 0, 0);

        transform.rotation = transform.rotation * zMove;//rotates the full body
    }

    /*
     * Angles are weird and cant be clamped using the usual methods
     * 
     * first this function has to scale all the angles so that they are all within the same number of rotations otherwise the clamping wont work
     * then it clamps it with the newly scaled angles
     * 
     */
    private float LockRotation(float Angle, float min, float max)
    {
        if (Angle < 90 || Angle > 270)
        {
            if (Angle > 180) Angle -= 360;
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        Angle = Mathf.Clamp(Angle, min, max);
        if (Angle < 0) Angle += 360;
        return Angle;
    }
    
    public void SprintToggle(InputAction.CallbackContext context)
    {
        if (context.started)
        { 
            currentSprintMod = maxSprintMod;
            Camera.main.fieldOfView = 80;

        }
        if (context.canceled)
        {
            currentSprintMod = 1;
            Camera.main.fieldOfView = 60;
        }

    }
   
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
       // jelly.intensity = new ClampedFloatParameter(currentHealth * maxH, 0, 0.7f);
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
        UIManager.instance.AdjustHealth(currentHealth/maxHealth);
        if(currentHealth <= 0)
        {
            GameManager.instance.PlayerDied();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(transform.up*jumpForce, ForceMode.Impulse);
        }
    }

}
