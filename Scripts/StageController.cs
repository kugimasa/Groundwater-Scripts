using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public GameObject Player;
    public GameObject MainCamera;
    public GameObject WaterTower;
    public GameObject TimeText;
    public GameObject ClearCamera;
    GameObject GameControllerObject;
    PlayerController playerC;
    CameraController cameraC;
    WaterController waterC;
    SoundController soundC;
    TimeController timeC;
    GameController gameC;
    PlayableDirector playableD;
    [SerializeField] float waterDuration = 3.0f;
    [SerializeField] float timeLineDuration = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerC = Player.GetComponent<PlayerController>();
        cameraC = MainCamera.GetComponent<CameraController>();
        waterC = WaterTower.GetComponent<WaterController>();
        GameControllerObject = GameObject.Find("GameControllerObject");
        gameC = GameControllerObject.GetComponent<GameController>();
        soundC = GameControllerObject.GetComponent<SoundController>();
        timeC = GameControllerObject.GetComponent<TimeController>();
        playableD = GetComponent<PlayableDirector>();
        StartCoroutine(waterC.StartEvelation(waterDuration));
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameC.timeLinePlayed)
        {
            if (!gameC.isTimeAttack)
            {
                playerC.isMoving = false;
            }
            Initialize();
            soundC.Stop();
            soundC.PlayStageBGM();
        }
        if(gameC.timeCounting)
        {
            if (gameC.isTimeAttack)
            {
                UpdateTimeText();
            }
            CameraElevate();
            CameraRotate();
            Restart();
            StageClear();
        }
    }
    private void Initialize()
    {
        TimeText.GetComponent<Text>().text = gameC.GetScoreTime().ToString("f2");
        StartCoroutine(PlayTimeLine());
    }

    private IEnumerator PlayTimeLine()
    {
        yield return new WaitForSeconds(timeLineDuration);
        playableD.Play();
        gameC.timeLinePlayed = true;
    }
    private IEnumerator PlayStageClearTimeLine()
    {
        yield return new WaitForSeconds(timeLineDuration);
        ClearCamera.SetActive(true);
    }
    private void Restart()
    {
        if (playerC.drowned) 
        {
            switch (gameC.GetSceneName())
            {
                // Time Atttack
                case ("Stage1"):
                    ChangeStage(1);
                    break;
                case ("Stage2"):
                    ChangeStage(2);
                    break;
                case ("Stage3"):
                    ChangeStage(3);
                    break;
                // Adventure Mode
                case ("Adventure1"):
                    ChangeStage(1);
                    break;
                case ("Adventure2"):
                    ChangeStage(2);
                    break;
                case ("Adventure3"):
                    ChangeStage(3);
                    break;
                case ("Adventure4"):
                    ChangeStage(4);
                    break;
                case ("Adventure5"):
                    ChangeStage(5);
                    break;
            }
        }
        gameC.timeLinePlayed = true;
    }

    private void StageClear()
    {
        if (playerC.cleared)
        {
            PauseTime();
            playerC.PlayClearedSE();
            waterC.WaterStop();
            StartCoroutine(PlayStageClearTimeLine());
        }
    }

    private void CameraRotate()
    {
        if (InputController.CAMERA_R_KEY)
        {
            cameraC.ChangeState(-1);
        }
        if (InputController.CAMERA_L_KEY)
        {
            cameraC.ChangeState(1);
        }
    }
    private void CameraElevate()
    {
        float playerPosY = playerC.gameObject.transform.position.y;
        if (playerPosY >= 6.0f)
        {
            cameraC.ChangePosY(15.0f);
        }
        if (playerPosY >= 11.0f)
        {
            cameraC.ChangePosY(20.0f);
        }
        if (playerPosY >= 16.0f)
        {
            cameraC.ChangePosY(25.0f);
        }
        if (playerPosY >= 21.0f)
        {
            cameraC.ChangePosY(30.0f);
        }
    }

    private void UpdateTimeText()
    {
        if (playableD.state == PlayState.Paused)
        {
            timeC.UpdateTime();
            TimeText.GetComponent<Text>().text = timeC.GetTime().ToString("f2");
        }
    }
    // Called From TimeLine
    public void StartTime()
    {
        gameC.timeCounting = true;
        playerC.isMoving = true;
    }

    // Called From TimeLine
    public void ChangeStage(int stageNo)
    {
        if (gameC.isTimeAttack)
        {
            gameC.SetScoreTime(timeC.GetTime());
            gameC.ChangeToStage(stageNo);
        }
        else
        {
            gameC.ToAdventure(stageNo);
        }
        gameC.timeLinePlayed = false;
    }

    public void PauseTime()
    {
        gameC.timeCounting = false;
    }


}
