using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource musicSource;
    public static MusicPlayer instance;
    [SerializeField]private AudioClip calmMusic;
    [SerializeField] private AudioClip fightMusic;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {

                Destroy(gameObject);
            }
        }
        else
        {
            instance = this;
        }
        //playerController = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        SwapBack();
    }

    public void SwapTracks()
    {
        musicSource.clip = fightMusic;
        musicSource.volume = 0.20f;
        musicSource.Play();

    }
    public void SwapBack()
    {
        musicSource.clip = calmMusic;
        musicSource.volume = 0.5f;
        musicSource.Play();
    }


}
