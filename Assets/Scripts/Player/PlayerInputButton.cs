using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputButton : MonoBehaviour
{
    public string inputName;
    public InputHandler inputHandler;
    // Start is called before the first frame update
    public void ButtonPressed()
    {
        inputHandler.StartRebinding(inputName);
    }
}
