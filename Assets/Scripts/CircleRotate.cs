using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotate : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.telePort)
        {
            GameManager.Instance.telePort = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<RoundPlatformer_Component>() == null)
                {
                    for (int j = 0; j < transform.GetChild(i).childCount; j++)
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<RoundPlatformer_Component>().Teleport();
                    }
                }

                else
                {
                    transform.GetChild(i).GetComponent<RoundPlatformer_Component>().Teleport();
                }
            }
        }
    }
}
