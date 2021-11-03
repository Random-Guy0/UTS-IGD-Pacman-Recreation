using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundEffects;
    [SerializeField] private AudioClip[] music;

    [SerializeField] private AudioSource soundEffectPlayer;
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource wallHitPlayer;
    [SerializeField] private AudioSource cherrySoundPlayer;
    [SerializeField] private AudioSource pacmanDeathPlayer;

    private bool eatingPellet = false;
    private bool moving = false;
    private bool atWall = false;

    public void PlayMoveSound()
    {
        if (!moving)
        {
            moving = true;
            eatingPellet = false;
            atWall = false;
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
            atWall = false;
            soundEffectPlayer.clip = soundEffects[1];
            soundEffectPlayer.volume = 0.35f;
            soundEffectPlayer.loop = true;
            soundEffectPlayer.Play();
        }
    }

    public void CollideWithWall()
    {
        if (!atWall)
        {
            atWall = true;
            moving = false;
            eatingPellet = false;
            wallHitPlayer.Play();
        }
    }

    public void PacmanDeath()
    {
        atWall = false;
        moving = false;
        eatingPellet = false;
        soundEffectPlayer.Stop();
        pacmanDeathPlayer.Play();
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

    public void CollectCherry()
    {
        cherrySoundPlayer.Play();
    }

    public void StopMusic()
    {
        musicPlayer.Stop();
    }
}
