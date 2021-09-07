using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundEffects;
    [SerializeField] private AudioClip[] music;

    [SerializeField] private AudioSource soundEffectPlayer;
    [SerializeField] private AudioSource musicPlayer;

    private bool eatingPellet = false;

    private void Start()
    {
        musicPlayer.clip = music[0];
        musicPlayer.Play();
        Invoke("SetupAudioSources", music[0].length);
    }

    private void SetupAudioSources()
    {
        musicPlayer.clip = music[1];
        musicPlayer.loop = true;
        musicPlayer.Play();

        soundEffectPlayer.clip = soundEffects[0];
        soundEffectPlayer.loop = true;
        soundEffectPlayer.Play();
    }

    public IEnumerator EatPellet()
    {
        if (!eatingPellet)
        {
            soundEffectPlayer.clip = soundEffects[1];
            soundEffectPlayer.volume = 1.0f;
            soundEffectPlayer.loop = false;
            soundEffectPlayer.Play();
            eatingPellet = true;
            yield return new WaitForSeconds(soundEffects[1].length);
            eatingPellet = false;
            soundEffectPlayer.clip = soundEffects[0];
            soundEffectPlayer.volume = 0.25f;
            soundEffectPlayer.loop = true;
            soundEffectPlayer.Play();
        }
    }
}
