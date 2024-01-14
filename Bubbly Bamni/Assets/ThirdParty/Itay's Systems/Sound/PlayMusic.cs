using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] SoundScriptableObject atticMusic;
    [SerializeField] SoundScriptableObject episodeMusic;
    private SoundScriptableObject currentMusic;
    private SoundManager soundManager;
    private float musicTimer, musicDuration;
    private ShowManager showManager;

    void Awake()
    {
        currentMusic = atticMusic;
        soundManager = FindObjectOfType<SoundManager>();
        showManager = FindObjectOfType<ShowManager>();
        PlayNewMusic();
        showManager.onStartEpisode += SwitchToEpisodeMusic;
        showManager.onEndEpisode += SwitchToAtticMusic;
    }

    private void Update()
    {
        musicTimer += Time.deltaTime;
        if (musicTimer >= musicDuration) PlayNewMusic();
    }

    private void SwitchToEpisodeMusic()
    {
        currentMusic = episodeMusic;
        PlayNewMusic();
    }

    private void SwitchToAtticMusic()
    {
        currentMusic = atticMusic;
        PlayNewMusic();
    }

    private void PlayNewMusic()
    {
        Debug.Log("Plays!!");
        soundManager.PlaySound(currentMusic);
        musicDuration = soundManager.GetSoundAudioSource(currentMusic).clip.length;
        musicTimer = 0f;
    }
}
