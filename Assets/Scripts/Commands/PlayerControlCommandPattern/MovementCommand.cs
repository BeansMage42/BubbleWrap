using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementCommand : BaseCommand
{
    public KeyCode boundKey;
    protected CollideAndSlideController CollideAndSlideController;
    public override void Execute()
    {
        CollideAndSlideController = GameManager.instance.GetPlayer();
    }
}
