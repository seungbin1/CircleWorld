using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //싱글톤
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public double mainSound;
    public double gameSound;

    public int stage;
    public int part;

    public int admobC;

    public bool telePort=false;
   
    public class Data
    {
        public double mainSound;
        public double gameSound;

        public int stage;
        public int part;

        public int admobC;
        public Data(int admobC,int stage, int part,double mainSound, double gameSound)
        {
            this.admobC = admobC;
            this.stage = stage;
            this.part = part;
            this.mainSound = mainSound;
            this.gameSound = gameSound;
        }
        public void SaveAdmob(int admobC)
        {
            this.admobC = admobC;
        }

        public void SaveStage(int stage, int part)
        {
            this.stage = stage;
            this.part = part;
        }

        public void SaveSound(double mainSound, double gameSound)
        {
            this.mainSound = mainSound;
            this.gameSound = gameSound;
        }
    }

    public int limitTime = 100;
    private int first;

    public bool right;
    public bool left;

    public float playerY;

    public Data data;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //저장 소리세팅, 최고 점수, 불러오기

        first = PlayerPrefs.GetInt("First");
        if (first == 0)
        {
            data = new Data(0, 1, 1, 0.5f, 0.5f);
        }
        data = new Data(0, 15, 1, 0.5f, 0.5f);
        SaveData();
        first++;
        PlayerPrefs.SetInt("First", first);

        JsonData json = Load();

        admobC=int.Parse(json["admobC"].ToString());

        part = int.Parse(json["part"].ToString());
        stage = int.Parse(json["stage"].ToString());

        mainSound = float.Parse(json["mainSound"].ToString());
        gameSound = float.Parse(json["gameSound"].ToString());

        data = new Data( admobC,stage,part,mainSound, gameSound);

        SceneManager.sceneLoaded += Init;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void Save(Data data)
    {
        JsonData jsondata = JsonMapper.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + @"\data.json", jsondata.ToString());
    }

    public void SaveData()
    {
        Save(data);
    }

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }

    private void Init(Scene scene, LoadSceneMode mode)
    {
        limitTime = 100;
    }
}
