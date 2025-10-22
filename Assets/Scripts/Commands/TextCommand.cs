using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TextCommand : BaseCommand
{
    public abstract void SetVar(string[] args);
   
}
