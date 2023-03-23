using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSoundController : MonoBehaviour
{
    public static GameSoundController i { get; private set; }

    [SerializeField]
    public SoundAudioClip[] soundAudioClipArray;
    [SerializeField]
    private AudioSource[] MusicSource;

    public float MusicVolume
                , SoundEffectVolume
                , MasterVolume
                , BackEndVolumeController;

    private void Awake() {
        if (i != null && i != this) {
            Destroy(this);
        } else {
            i = this;
        }
        SoundManager.Initialize();
    }
    private void Update() {

        float SoundEffectVolumeTEMPT = MasterVolume * SoundEffectVolume * BackEndVolumeController;
        float MusicVolumeTEMPT = MasterVolume * MusicVolume * BackEndVolumeController;

        for (int i = 0; i < MusicSource.Length; i++) {
            MusicSource[i].volume = MusicVolumeTEMPT;
        }

        SoundManager.SetVolume(SoundEffectVolumeTEMPT);
    }
}

[System.Serializable]
public class SoundAudioClip {
    public SoundManager.Sound sound;
    public AudioClip audioClip;
}
