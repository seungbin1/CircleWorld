using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu: MonoBehaviour
{
    public enum Kind
    {
        SETTING,
        SETTINGEXIT,
        SCENEMOVE,
        STOPUI,
        RESUME,
        SETUI,
        REWARD,
        BACKMENU,
        NULL
    }

    public Score score;

    public GameObject gameObjectTrue;

    public GameObject gameObjectFalse;

    public Kind kind;

    public GameObject settingMenu;

    private int admobC = 0;

    public bool eacape;
    public string eacpaeSceneName;

    public string sceneName=null;

    private Button button;


    private void Start()
    {
        button = GetComponent<Button>();
        admobC = GameManager.Instance.data.admobC;

        switch (kind)
        {
            case Kind.SETTING:

                button.onClick.AddListener(Setting);
                break;

            case Kind.SETTINGEXIT:

                button.onClick.AddListener(SettingExit);
                break;

            case Kind.SCENEMOVE:

                button.onClick.AddListener(SceneMove);
                break;

            case Kind.STOPUI:
                button.onClick.AddListener(StopUI);
                break;

            case Kind.RESUME:
                button.onClick.AddListener(Resume);
                break;

            case Kind.SETUI:
                button.onClick.AddListener(SetUI);
                break;

            case Kind.BACKMENU:

                break;
            case Kind.REWARD:
                button.onClick.AddListener(Reward);
                break;

            case Kind.NULL:
                break;

            default:
                break;
        }
    }

    private void Update()
    {
        if (eacape)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(eacpaeSceneName);
            }
        }

        if (kind == Kind.BACKMENU)
        {
            if (gameObjectTrue.gameObject.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameObjectTrue.gameObject.SetActive(true);
                    gameObjectFalse.gameObject.SetActive(false);
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameObjectTrue.gameObject.SetActive(true);
                    gameObjectFalse.gameObject.SetActive(false);
                }
            }
        }

        else { return; }
    }

    public void SceneMove()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);

        Admob();

        TimeInit();
    }

    public void Setting()
    {
        settingMenu.SetActive(true);
        Admob();
    }

    public void SettingExit()
    {
        settingMenu.SetActive(false);
        GameManager.Instance.Save(GameManager.Instance.data);
        Admob();
    }    

    public void Admob()
    {
        admobC++;

        GameManager.Instance.data.SaveAdmob(admobC);
        GameManager.Instance.SaveData();

        if (admobC > 10)
        {
            AdMobManager._adMobManager.ShowInterstitialAd();

            admobC = 0;
            GameManager.Instance.data.SaveAdmob(admobC);
            GameManager.Instance.SaveData();
        }
    }

    public void StopUI()
    {
        Time.timeScale = 0;
        gameObjectTrue.gameObject.SetActive(true);
        gameObjectFalse.gameObject.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObjectTrue.gameObject.SetActive(true);
        Admob();
        gameObjectFalse.gameObject.SetActive(false);
    }
    public void SetUI()
    {
        gameObject.gameObject.SetActive(true);
        Admob();
    }

    public void Reward()
    {
        gameObjectFalse.gameObject.SetActive(false);
        gameObjectTrue.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        GameManager.Instance.limitTime = 100;
        Time.timeScale = 1;
        score.StartCoroutine(score.LimitTime());
    }

    private void TimeInit()
    {
        GameManager.Instance.limitTime = 100;
    }


}
