using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject SettingCanvas;
    public bool timeLinePlayed;
    public bool timeCounting;
    public bool isTimeAttack;
    GameObject LoadingPanel;
    [SerializeField] float loadDuration = 0.4f;
    [SerializeField] float scoreTime;
    SoundController soundC;
    TimeController timeC;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(SettingCanvas);
        soundC = GetComponent<SoundController>();
        timeC = GetComponent<TimeController>();
        LoadingPanel = SettingCanvas.transform.GetChild(0).gameObject;
        timeCounting = false;
        isTimeAttack = false;
    }

    public void PlayGame()
    {
        isTimeAttack = true;
        Loading();
        StartCoroutine(MoveStage("Stage1"));
    }

    public void ChangeToStage(int stageNo)
    {
        isTimeAttack = true;
        Loading();
        StartCoroutine(MoveStage("Stage" + stageNo.ToString()));
        timeC.SetTime(scoreTime);
    }

    public void ToAdventure(int stageNo)
    {
        Loading();
        StartCoroutine(MoveStage("Adventure" + stageNo.ToString()));
    }


    public void Loading()
    {
        CanvasGroup canvasG = LoadingPanel.GetComponent<CanvasGroup>();
        StartCoroutine(FadeLoadingPanel(canvasG, loadDuration));
    }

    public void SetScoreTime(float time)
    {
        scoreTime = time;
    }

    public float GetScoreTime()
    {
        return scoreTime;
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ToMainScene()
    {
        SceneManager.LoadScene("Main");
        Destroy(SettingCanvas);
        Destroy(gameObject);
    }

    public void HideSettingButton()
    {
        SettingCanvas.transform.GetChild(1).gameObject.SetActive(false);
    }

    private IEnumerator MoveStage(string stageName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(stageName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    private IEnumerator FadeLoadingPanel(CanvasGroup canvasG, float duration)
    {
        float counter = 0.0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasG.alpha = Mathf.Lerp(1.0f, 0.0f, counter / duration);
            yield return null;
        }
    }
}
