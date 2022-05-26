using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> partList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> partRock = new List<GameObject>();

    private int myPart;
    private bool partSet = true;
    // Start is called before the first frame update

    private void OnEnable()
    {
        myPart = GameManager.Instance.data.part;
        for (int a = 0; myPart > a; a++)
        {
            partList[a].gameObject.SetActive(partSet);
            partRock[a].gameObject.SetActive(!partSet);
        }
    }
}