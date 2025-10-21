using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetSceneCommand : BaseCommand
{
    public override void Execute()
    {
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }

    
}
