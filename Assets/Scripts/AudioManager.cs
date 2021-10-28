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
    private bool moving = false;

    public void PlayMoveSound()
    {
        if (!moving)
        {
            moving = true;
            eatingPellet = false;
            soundEffectPlayer.clip = soundEffects[0];
            soundEffectPlayer.volume = 0.25f;
            soundEffectPlayer.loop = true;
            soundEffectPlayer.Play();
        }
    }

    public void EatPellet()
    {
        if (!eatingPellet)
        {
            eatingPellet = true;
            moving = false;
            soundEffectPlayer.clip = soundEffects[1];
            soundEffectPlayer.volume = 0.35f;
            soundEffectPlayer.loop = true;
            soundEffectPlayer.Play();
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

    public bool PlayingSoundEffect()
    {
        return soundEffectPlayer.isPlaying;
    }

    public void StopSoundEffects()
    {
        moving = false;
        eatingPellet = false;
        soundEffectPlayer.loop = false;
        soundEffectPlayer.Stop();
    }
}
