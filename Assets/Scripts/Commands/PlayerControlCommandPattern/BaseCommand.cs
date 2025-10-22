using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseCommand: ScriptableObject
{
    [SerializeField] private string commandWord = string.Empty;
    public string CommandWord => commandWord;
    public abstract void Execute();
    public abstract void Undo();
}
