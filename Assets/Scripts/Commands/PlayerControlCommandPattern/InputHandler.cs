using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InputHandler : MonoBehaviour
{

    private Dictionary<KeyCode, MovementCommand> boundKeys = new Dictionary<KeyCode, MovementCommand>();
    private Dictionary<string, TextMeshProUGUI> bindingDisplay = new Dictionary<string, TextMeshProUGUI>();

    [SerializeField] CommandInvoker commandInvoker;
    private bool isRebinding = false;
    private KeyCode rebindingKey;
    public bool paused = false;
    [SerializeField] private MovementCommand[] commandList;
   // [SerializeField] PlayerController playerController;
    [SerializeField] CollideAndSlideController playerController;

    private void Awake()
    {
        
        foreach (MovementCommand command in commandList) 
        {
            boundKeys.Add(command.boundKey,command);
            bindingDisplay.Add(command.CommandWord, UIManager.instance.PopulateBinding(command.CommandWord,command.boundKey.ToString(), this));
        }

       /* foreach (KeyCode key in boundKeys.Keys) 
        {
            bindingDisplay.Add(boundKeys[key], UIManager.instance.PopulateBinding(boundKeys[key], key.ToString(), this));
        }*/

    }

    void Update()
    {
        if (isRebinding)
        {

            if (Input.anyKeyDown)
            {
                foreach (var item in Enum.GetValues(typeof(KeyCode))) {
                    var key = (KeyCode)item;
                    if (Input.GetKeyDown(key))
                    {
                        RebindKey(rebindingKey, key);
                        break;
                    }
                }
            }
        }
        else if(!paused)
        {
            foreach (var key in boundKeys.Keys)
            {
                // Debug.Log(boundKeys[key]);
                if (Input.GetKeyDown(key))
                {
                    Debug.Log("found key");

                    commandInvoker.ExecuteCommand(/*CreateCommand(key)*/ boundKeys[key]);
                    
                    
                }else if (Input.GetKeyUp(key))
                {
                    Debug.Log("release key");
                    commandInvoker.ReverseCommand(/*CreateCommand(key)*/ boundKeys[key]);
                }
            }
        } 

    
    }
    public void StartRebinding(string keyToRebind)
    {
        Debug.Log("start rebinding");
        foreach (var key in boundKeys.Keys) 
        {
            if (boundKeys[key].CommandWord == keyToRebind)
            {
                rebindingKey = key;
                break;
            }
        
        }
        
        isRebinding = true;
    }

    private void RebindKey(KeyCode oldKeycode, KeyCode newKeycode)
    {
        if (!boundKeys.ContainsKey(newKeycode))
        {
            Debug.Log("Rebound " + boundKeys[oldKeycode] + " from " + (int)oldKeycode + " to " + (int)newKeycode);
            boundKeys.Add(newKeycode, boundKeys[oldKeycode]);
            boundKeys.Remove(oldKeycode);
            isRebinding = false;
            UIManager.instance.UpdateKeyText(bindingDisplay[boundKeys[newKeycode].CommandWord], newKeycode.ToString());
        }
        else
        {
            Debug.Log("choose new key");
        }
    }

    public void TogglePause()
    {
        paused = !paused;
    }
}
