using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text timerText;

    private void OnEnable()
    {
        timerText = GetComponent<Text>();
        StartCoroutine(LimitTime());
    }

    private void Start()
    {
        timerText.text = " " + GameManager.Instance.limitTime;
    }
    private void Update()
    {
        if (GameManager.Instance.limitTime == 100)
        {
            timerText.text = " " + GameManager.Instance.limitTime;
        }
    }
    public IEnumerator LimitTime()
    {
        while (Time.timeScale ==1)
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.Instance.limitTime > 0)
            {
                GameManager.Instance.limitTime += -1 ;
                timerText.text = ""+ GameManager.Instance.limitTime;
            }

        }
    }
}
