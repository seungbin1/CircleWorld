using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCount : MonoBehaviour
{
    public int part;
    public int stage;

    private int myPart;
    private int myStage;

    private void OnEnable()
    {
        myStage = GameManager.Instance.data.stage;
        myPart = GameManager.Instance.data.part;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (part == myPart)
            {
                if (stage == myStage)
                {
                    myStage++;
                    if (myStage == 16)
                    {
                       myPart++;
                       myStage = 1;
                    }
                }

                else if (stage < myStage)
                {
                    return;
                }
            }

            else if (part < myPart)
            {
                return;
            }
        }

        GameManager.Instance.data.SaveStage(myStage, myPart);
        GameManager.Instance.Save(GameManager.Instance.data);
    }
}