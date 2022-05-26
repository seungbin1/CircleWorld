using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip mainClip;
    public AudioClip gameClip;
    public AudioMixerGroup gameMixer;
    public AudioMixerGroup mainMixer;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        AudioSetting();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame

    private void Update()
    {
        if (Time.timeScale == 1 && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }

        else if(Time.deltaTime == 1 && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    private void AudioSetting()
    {
        audioSource.playOnAwake = true;
        audioSource.loop = true;
    }

    private void Main()
    {
        audioSource.clip = mainClip;
        audioSource.outputAudioMixerGroup = mainMixer;
        audioSource.Play();
    }

    private void Game()
    {
        audioSource.clip = gameClip;
        audioSource.outputAudioMixerGroup = gameMixer;
        audioSource.Play();
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Stage"))
        {
            Game();
        }
        
        else if(audioSource.clip == gameClip)
        {
            Main();
        }
    }
}