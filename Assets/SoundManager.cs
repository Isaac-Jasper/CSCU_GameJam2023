using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SoundManager
{
    public enum Sound { //a reference variable of each sound which, when used with GameSoundController.SoundAudioClip, will relate to an audio clip
        //the catagories are spaced out for convenience as if they are not places that reference that variable will not have the same value is it is incrementing
        //eg. debug and developer sounds range from 0-99 in this catagory, and then UI sounds in 100-199, player in 200-299, ect.
        Debug_TestDelaySound = 0, 

        Player_Death = 100, 
        Player_Turn,

        Level_NextRound = 200,
    }
    private static float volume;

    private static Dictionary<Sound, float> soundTimerDictionary; //used to relate a Sound to a delay time, example in Initialize()
    private static GameObject tempAudioObject;
    private static AudioSource tempAudioSource;
    public static void Initialize() {
        tempAudioObject = new GameObject("Temp Sound");
        tempAudioSource = tempAudioObject.AddComponent<AudioSource>();

        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.Debug_TestDelaySound] = 0f; //example of initializing a delayed sound
    }
    public static void PlaySound(Sound sound) { //used for 2D sound
        if (CanPlaySound(sound)) {
            //use audioSource.[settings to set] 
            //in these spaces to change the values 
            //of played audio
            tempAudioSource.volume = volume;
            tempAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }
    private static bool CanPlaySound(Sound sound) { //used for any sound that needs a delay, if sound doesnt need a delay returns true
        switch (sound) {
            default:
                return true;
            case Sound.Debug_TestDelaySound: //example of a implementing a sound with a delay looks like
                if (soundTimerDictionary.ContainsKey(sound)) {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float testSoundTimerMax = .05f;
                    
                    if (lastTimePlayed + testSoundTimerMax < Time.time) {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    return true;
                }
                //break;
        }
    }
    private static AudioClip GetAudioClip(Sound sound) { //finds the correct audioClip based on the given Sound value
        foreach (SoundAudioClip soundAudioClip in GameSoundController.i.soundAudioClipArray) {
            if (soundAudioClip.audioClip != null && soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    //Setter Methods
    public static void SetVolume(float set) {
        volume = set;
    }
}
