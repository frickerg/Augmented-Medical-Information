using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controller to prevent multiple sounds played.
 */
public class SoundController : MonoBehaviour
{   
    // holds currently playing audio
    public AudioSource currentlyPlayingAudio;

    // Plays new sound.
    // Checks if other sound is playing and stops if necessary.
    public void PlayNewSound(AudioSource audio)
    {
        if (currentlyPlayingAudio != null && currentlyPlayingAudio.isPlaying)
        {
            // other sound is playing
            currentlyPlayingAudio.Stop();
        }
        // set new sound and play
        currentlyPlayingAudio = audio;
        currentlyPlayingAudio.Play();
    }

    // Stops sound and resets currentlyPlayingAudio.
    public void StopSound()
    {
        currentlyPlayingAudio.Stop();
        currentlyPlayingAudio = null;
    }

    // Pauses sound.
    public void PauseSound()
    {
        currentlyPlayingAudio.Pause();
    }
}
