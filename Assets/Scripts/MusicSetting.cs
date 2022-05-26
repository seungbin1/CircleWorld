using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSetting : MonoBehaviour
{
    public AudioMixer Mixer;

    public Slider main;
    public Slider game;

    private float sound;
    private float sound2;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke("Init", 0.1f);
    }
    //음악 볼륨을 조절한다.
    public void MainMusic()
    {
        sound = main.value;
        Mixer.SetFloat("Main", Mathf.Log10(sound) * 20);
        GameManager.Instance.data.SaveSound(sound, sound2);
    }
    //음악 볼륨을 조절한다.
    public void GameMusic()
    {
        sound2 = game.value;
        Mixer.SetFloat("Game", Mathf.Log10(sound2) * 20);
        GameManager.Instance.data.SaveSound(sound, sound2);
    }
    

    //저장은 세팅 나가는 버튼에 있다.

    private void Init()
    {
        sound = float.Parse(GameManager.Instance.data.mainSound.ToString());
        sound2 = float.Parse(GameManager.Instance.data.gameSound.ToString());

        Mixer.SetFloat("Main", Mathf.Log10(sound) * 20);
        Mixer.SetFloat("Game", Mathf.Log10(sound2) * 20);

        main.value = sound;
        game.value = sound2;
    }
}
