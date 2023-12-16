using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] SoundScriptableObject musicSounds;
    private SoundManager soundManager;
    private float musicTimer, musicDuration;

    void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        PlayNewMusic();
    }

    private void Update()
    {
        musicTimer += Time.deltaTime;
        if (musicTimer >= musicDuration) PlayNewMusic();
    }

    private void PlayNewMusic()
    {
        soundManager.PlaySound(musicSounds);
        musicDuration = soundManager.GetSoundAudioSource(musicSounds).clip.length;
        musicTimer = 0f;
    }
}
