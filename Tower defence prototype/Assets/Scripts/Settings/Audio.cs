using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
  public AudioMixer audioMixer;

  public void SetMasterVolume(float volume)
  {
    audioMixer.SetFloat("Master", volume);
  }
  public void SetMusicVolume(float volume)
  {
    audioMixer.SetFloat("Music", volume);
  }
  public void SetSFXVolume(float volume)
  {
    audioMixer.SetFloat("SFX", volume);
  }
  
}
