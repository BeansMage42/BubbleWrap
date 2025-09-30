using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public void ResetGame()
    {
        print("Load");
        FindObjectOfType<MusicPlayer>().SwapBack();
        SceneManager.LoadScene("PG_SampleScene");
    }
}
