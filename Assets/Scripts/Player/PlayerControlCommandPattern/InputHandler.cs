using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    private Dictionary <KeyCode, string> boundKeys = new Dictionary <KeyCode,string> ();
    [SerializeField] CommandInvoker commandInvoker;
    private bool isRebinding = false;
    private KeyCode rebindingKey;
   // [SerializeField]  PlayerController playerController;

    private void Start()
    {
        boundKeys.Add(KeyCode.W, "FWD");
        boundKeys.Add(KeyCode.S, "BCK");
        boundKeys.Add(KeyCode.A, "LFT");
        boundKeys.Add(KeyCode.D, "RGT");

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
                    BaseCommand newCommand = new FWDCommand();
                    switch (boundKeys[key])
                    {
                        case "FWD":
                            newCommand = new FWDCommand();
                            break;
                        case "BCK":
                            newCommand = new BCKCommand();
                            break;
                        case "LFT":
                            newCommand = new LFTCommand();
                            break;
                        case "RGT":
                            newCommand = new RGTCommand();
                            break;

                    }
                    commandInvoker.ExecuteCommand(newCommand);
                }

            }
        }
    
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
        }
        else
        {
            Debug.Log("choose new key");
        }
    }
}
