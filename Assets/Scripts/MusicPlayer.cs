using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource musicSource;
    [SerializeField]private AudioClip calmMusic;
    [SerializeField] private AudioClip fightMusic;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        musicSource = GetComponent<AudioSource>();
    }

    public void SwapTracks()
    {
        musicSource.clip = fightMusic;
        musicSource.volume = 0.20f;
        musicSource.Play();

    }


}
