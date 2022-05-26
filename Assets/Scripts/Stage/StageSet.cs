using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stageList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> stageRock = new List<GameObject>();

    private bool stageSet=true;

    private int myStage;
    private int myPart;

    public int part;

    // Start is called before the first frame update

    private void OnEnable()
    {
        myStage = GameManager.Instance.data.stage;
        myPart = GameManager.Instance.data.part;
        if(part == myPart)
        {
            for(int i =0; myStage > i; i++)
            {
                stageList[i].gameObject.SetActive(stageSet);
                stageRock[i].gameObject.SetActive(!stageSet);
            }
        }

        else if(part < myPart)
        {
            for (int i = 0; i < stageList.Count; i++)
            {
                stageList[i].gameObject.SetActive(stageSet);
                stageRock[i].gameObject.SetActive(!stageSet);
            }
        }
    }
}
