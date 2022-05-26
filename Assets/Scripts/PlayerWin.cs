using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    private int playerRotation = 1;

    [SerializeField]
    private string winAnimation;

    [SerializeField]
    private string loseAnimation;

    private GameObject gameMenu;
    private GameObject winUI;

    private GameObject obstcle;

    private GameObject stopUI;

    private GameObject loseUI;

    private Rigidbody2D rigidbody2D;

    private Vector3 teleportPosition;

    private Animator animator;


    private void OnEnable()
    {
        GameManager.Instance.right = true;
        GameManager.Instance.left = true;
    }
    private void Start()
    {
        gameMenu = GameObject.Find("MenuUI");
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        obstcle = GameObject.Find("Obstcle");
        winUI = gameMenu.transform.Find("WinUI").gameObject;
        loseUI = gameMenu.transform.Find("LoseUI").gameObject;
        stopUI = gameMenu.transform.Find("StopUIButton").gameObject;

    }
    private void OnTriggerExit2D(Collider2D hole)
    {
        if (hole.tag == "Tree")
        {
            GameManager.Instance.right = true;
            GameManager.Instance.left = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D hole)
    {
        if (hole.tag == "Tree")
        {
            if (hole.transform.position.x > 0)
            {
                GameManager.Instance.right = false;
            }
            if (hole.transform.position.x < 0)
            {
                GameManager.Instance.left = false;
            }
        }

        if (hole.tag == "TeleportUp1")
        {
            animator.Play("Player1Win");
            obstcle.transform.Rotate(0, 0, 180);
            GameManager.Instance.telePort = true;
            StartCoroutine(Teleport1());
        }

        if (hole.tag == "TeleportUp")
        {
            animator.Play("Player1Win");
            teleportPosition = new Vector3(0, -4.4f, 0);
            StartCoroutine(Teleport());
        }

        if (hole.tag == "TeleportDown")
        {
            animator.Play("Player1Win");
            teleportPosition = new Vector3(0, 4.4f, 0);
            StartCoroutine(Teleport());
        }

        if (hole.tag == "Win")
        {
            rigidbody2D.gravityScale = 0;
            StartCoroutine(PlayerW());
        }

    }
    IEnumerator PlayerW()
    {
        animator.Play(winAnimation);
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        winUI.gameObject.SetActive(true);
        stopUI.gameObject.SetActive(false);
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1);
        transform.position += teleportPosition;
        animator.Play("Player1Teleport");
        yield return new WaitForSeconds(1);
        animator.speed = 1;
        Time.timeScale = 1;
    }

    IEnumerator Teleport1()
    {
        yield return new WaitForSeconds(1);
        animator.Play("Player1Teleport");
        yield return new WaitForSeconds(1);
        animator.speed = 1;
        Time.timeScale = 1;
    }

    private void Update()
    {
        Camera.main.transform.position = new Vector3(0, this.transform.position.y - 2.25f, -10);
        GameManager.Instance.playerY = this.transform.position.y;
        if (GameManager.Instance.limitTime == 0)
        {
            stopUI.gameObject.SetActive(false);
        }
        if (Time.timeScale == 1)
        {
            if (GameManager.Instance.limitTime == 0)
            {
                print(1);
                animator.Play(loseAnimation);
                Invoke("Lose", 0.5f);
            }
            if (GameManager.Instance.playerY > 6)
            {
                /*if (true)
                {
                    obstcle.transform.Rotate(0, 0, (45 - playerY) * Time.deltaTime);
                    playerRotation = -180;
                    PlayerRotation();
                }*/
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.position.x > Screen.width / 2&& GameManager.Instance.right)
                    {
                        PlayerRotation(1);
                    }
                    if (touch.position.x < Screen.width / 2&& GameManager.Instance.left)
                    {
                        PlayerRotation(-1);
                    }
                }
            }
        }
    }
  
    private void PlayerRotation(int distance)
    {
        obstcle.transform.Rotate(0, 0, (distance*45 - GameManager.Instance.playerY) * Time.deltaTime);
        playerRotation = -180;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, playerRotation, 0));
        animator.speed = 1f;
        animator.Play("Player1 walk");
    }

    private void Lose()
    {
        Time.timeScale = 0;
        loseUI.gameObject.SetActive(true);
        stopUI.gameObject.SetActive(false);
    }
}
