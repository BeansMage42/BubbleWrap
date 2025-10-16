using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InputHandler : MonoBehaviour
{

    private Dictionary <KeyCode, string> boundKeys = new Dictionary <KeyCode,string> ();
    private Dictionary<string, TextMeshProUGUI > bindingDisplay = new Dictionary <string, TextMeshProUGUI> ();
    [SerializeField] CommandInvoker commandInvoker;
    private bool isRebinding = false;
    private KeyCode rebindingKey;
   // [SerializeField] PlayerController playerController;
    [SerializeField] CollideAndSlideController playerController;

    private void Start()
    {
        boundKeys.Add(KeyCode.W, "Forward");
        boundKeys.Add(KeyCode.S, "Back");
        boundKeys.Add(KeyCode.A, "Left");
        boundKeys.Add(KeyCode.D, "Right");
       // boundKeys.Add(KeyCode.R, "Reload");
        boundKeys.Add(KeyCode.Space, "Jump");
        foreach (KeyCode key in boundKeys.Keys) 
        {
            bindingDisplay.Add(boundKeys[key], UIManager.instance.PopulateBinding(boundKeys[key], key.ToString(), this));
        }

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
        else
        {
            foreach (var key in boundKeys.Keys)
            {
                // Debug.Log(boundKeys[key]);
                if (Input.GetKeyDown(key))
                {
                    Debug.Log("found key");

                    commandInvoker.ExecuteCommand(CreateCommand(key));
                    
                    
                }else if (Input.GetKeyUp(key))
                {
                    Debug.Log("release key");
                    commandInvoker.ReverseCommand(CreateCommand(key));
                }
            }
        }
    
    }

    private BaseCommand CreateCommand (KeyCode commandKey)
    {
        BaseCommand newCommand = new FWDCommand(playerController);
        switch (boundKeys[commandKey])
        {
            case "Forward":
                newCommand = new FWDCommand(playerController);
                break;
            case "Back":
                newCommand = new BCKCommand(playerController);
                break;
            case "Left":
                newCommand = new LFTCommand(playerController);
                break;
            case "Right":
                newCommand = new RGTCommand(playerController);
                break;
            case "Reload":
                newCommand = new ReloadCommand(playerController);
                break;
            case "Jump":
                newCommand = new JumpCommand(playerController);
                break;

        }
        return newCommand;
    }

    public void StartRebinding(string keyToRebind)
    {
        Debug.Log("start rebinding");
        foreach (var key in boundKeys.Keys) 
        {
            if (boundKeys[key] == keyToRebind)
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
            UIManager.instance.UpdateKeyText(bindingDisplay[boundKeys[newKeycode]], newKeycode.ToString());
        }
        else
        {
            Debug.Log("choose new key");
        }
    }
}
