using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainController : MonoBehaviour
{
    public GameObject Player;
    public GameObject FrontWall;
    public GameObject Canvas;
    public GameObject GameControllerObject;
    PlayerController playerC;
    GameController gameC;
    [SerializeField] float moveWallSpeed = 0.01f;
    [SerializeField] float showReadyDelay = 1.0f;
    [SerializeField] float moveFurtherDelay = 2.0f;
    bool movingFrontWall;
    bool gameReady;
    bool readyShowned;
    bool gameStarting;
    void Start()
    {
        playerC = Player.GetComponent<PlayerController>();
        gameC = GameControllerObject.GetComponent<GameController>();
        movingFrontWall = false;
        gameReady = false;
        readyShowned = false;
        gameStarting = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveFrontWall();
        PlayerGameReady();
        StartGame();
    }

    private void MoveFrontWall()
    {
        // Check Player position
        if (Player.transform.position.z >= -4.0f)
        {
            movingFrontWall = true;
            Canvas.SetActive(false);
        }
        if (movingFrontWall && FrontWall.transform.position.z <= 12.0f) 
        {
            FrontWall.transform.Translate(Vector3.forward * moveWallSpeed, Space.World);
        }
        if (FrontWall.transform.position.z > 12.0f)
        {
            gameReady = true;
        }
    }

    private void PlayerGameReady()
    {
        if (gameReady)
        {
            StartCoroutine(MoveWallFurther());
        }
    }

    private IEnumerator MoveWallFurther()
    {
        string text;
        yield return new WaitForSeconds(showReadyDelay);
        if (!readyShowned)
        {
            text = "Are you ready ??";
            FrontWall.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(text);
            readyShowned = true;
        }
        yield return new WaitForSeconds(moveFurtherDelay);
        text = "SPACE to Jump";
        FrontWall.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(text);
        if (FrontWall.transform.position.z <= 20.0f) 
        {
            FrontWall.transform.Translate(Vector3.forward * moveWallSpeed, Space.World);
        }
    }

    private void StartGame()
    {
        if (Player.transform.position.y <= -50.0f && !gameStarting)
        {
            gameStarting = true;
            if (Player.transform.position.x < 0.0f) 
            {
                gameC.PlayGame();
            }
            else if (Player.transform.position.x >= 0.0f)
            {
                gameC.ToAdventure(1);
            }
        }
    }
}
