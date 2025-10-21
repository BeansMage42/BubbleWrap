using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "Reset Scene", menuName = "Commands/Text Commands/Reset Scene", order = 1)]
public class ResetSceneCommand : TextCommand
{
    public override void SetVar(string[] args)
    {
       // throw new System.NotImplementedException();
    }
    public override void Execute()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public override void Undo()
    {
        throw new System.NotImplementedException();
    }

    
}
