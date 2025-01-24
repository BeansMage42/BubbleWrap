using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Vector3 moveDir;
    private Vector2 look;
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private GameObject camRotPoint;
    [SerializeField] private float minYAngle, maxYAngle;

   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        
    }

    /*
     * update loop is used to check if you are looking at interactable objects. 
     * the system does not check if you are already interacting with something
     * 
     * it checks by sending out a speherecast every frame to check for objects with the interactable interface
     * sphere cast is used so the player does not have to be exact with their aim, giving a little leeway
     * this must be checked every frame so the objects highlight can be turned on and off when they are being looked at
     * 
     * Normally id use a layer mask to check for a specific things, such as interactable objects, but in this case I need walls to block interaction
     * so a layermask would actively hinder the process
     * 
     * any time a new object is focused or an objeect to focus on isnt found, it deactivates the previous objects outline
     */
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
      
        rb.velocity = transform.rotation * moveDir * moveSpeed;
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
    /*
     * 
     * This function is used to engage the interactable elements of an interactable object if the player is actively looking at one
     * the third nested if statement was a quick solution to allow a gameplay statechange, its a messy solution but I dont have enough time for a better one
     * 
     * potential fix, make a story or event manager class and use events to trigger the state changes
     */

   

}
