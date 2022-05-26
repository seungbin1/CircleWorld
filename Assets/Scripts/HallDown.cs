using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallDown : MonoBehaviour
{
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.tag == "Player")
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D Player)
    {
        if (Player.tag == "Player")
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
