using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextCommandHandler : MonoBehaviour
{
    [SerializeField] string prefix;
    [SerializeField] CommandInvoker commandInvoker;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] TextCommand[] commands;
    [SerializeField] private TextMeshProUGUI  commandInputField;


    private void Start()
    {
        commandInvoker = FindObjectOfType<CommandInvoker>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    public void ProcessText(string inputValue)
    {
        if (!inputValue.StartsWith(prefix)) return;

        inputValue = inputValue.Remove(0, prefix.Length);

        string[] inputSplit = inputValue.Split(' ');

        string commandInput = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();
        commandInputField.text = string.Empty;
        ProcessText(commandInput, args);
    }

    public void ProcessText(string command, string[] args)
    {
        foreach (var com in commands) 
        {
            if (com.CommandWord.Equals(command, StringComparison.OrdinalIgnoreCase))
            { 
                com.SetVar(args);
                commandInvoker.ExecuteCommand(com);
                break;
            }
        }

    }
}
