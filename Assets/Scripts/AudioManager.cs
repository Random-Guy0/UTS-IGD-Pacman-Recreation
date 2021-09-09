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
    private bool dead = false;

    private void Start()
    {
        musicPlayer.clip = music[0];
        musicPlayer.Play();
        Invoke("SetupAudioSources", music[0].length);
    }

    public void SetupAudioSources()
    {
        GhostNormalState();

        PlayMoveSound();

        dead = false;
    }

    private void PlayMoveSound()
    {
        if (!dead)
        {
            soundEffectPlayer.clip = soundEffects[0];
            soundEffectPlayer.volume = 0.25f;
            soundEffectPlayer.loop = true;
            soundEffectPlayer.Play();
        }
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
            PlayMoveSound();
        }
    }

    public IEnumerator CollideWithWall()
    {
        soundEffectPlayer.clip = soundEffects[2];
        soundEffectPlayer.loop = false;
        soundEffectPlayer.Play();
        yield return new WaitForSeconds(soundEffects[2].length);
        PlayMoveSound();
    }

    public void PacmanDeath() //update this method when the game has actually been made
    {
        musicPlayer.Stop();
        soundEffectPlayer.clip = soundEffects[3];
        soundEffectPlayer.loop = false;
        soundEffectPlayer.Play();
    }

    public void GhostNormalState()
    {
        musicPlayer.clip = music[1];
        musicPlayer.loop = true;
        musicPlayer.Play();
    }

    public void GhostScaredState()
    {
        musicPlayer.clip = music[2];
        musicPlayer.loop = true;
        musicPlayer.Play();
    }

    public void GhostDeadState()
    {
        musicPlayer.clip = music[3];
        musicPlayer.loop = true;
        musicPlayer.Play();
    }
}
