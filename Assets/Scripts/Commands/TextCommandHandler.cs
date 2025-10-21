using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextCommandHandler : MonoBehaviour
{
    [SerializeField] string prefix;
    [SerializeField] CommandInvoker commandInvoker;
    [SerializeField] PlayerHealth playerHealth;
    
    public void ProcessCommand(string inputValue)
    {
        if (!inputValue.StartsWith(prefix)) return;

        inputValue = inputValue.Remove(0, prefix.Length);

        string[] inputSplit = inputValue.Split(' ');

        string commandInput = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();
        ProcessCommand(commandInput, args);
    }

    public void ProcessCommand(string command, string[] args)
    {
        switch (command) 
        {
            case "GameEnd":
                if (args[0].Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    commandInvoker.ExecuteCommand(new GameEndCommand());
                }
                else if (args[0].Equals("false", StringComparison.OrdinalIgnoreCase)) 
                { 
                    commandInvoker.ReverseCommand(new GameEndCommand());
                }
                    break;
            case "ChangeHealth":
                commandInvoker.ExecuteCommand(new ChangeHealthCommand(playerHealth, int.Parse(args[0])));
                break;
            case "StartCombat":
                commandInvoker.ExecuteCommand(new StartCombatCommand());
                break;
            case "Reset":
                commandInvoker.ExecuteCommand(new ResetSceneCommand());
                break;
        
        }

    }
}
