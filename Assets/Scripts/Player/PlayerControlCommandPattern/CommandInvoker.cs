using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExecuteCommand(BaseCommand command)
    {
        Debug.Log("recieved command");
        command.Execute();
    }
}
