using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResultController : MonoBehaviour
{
    GameController gameC;
    SoundController soundC;
    void Start()
    {
        gameC = GameObject.Find("GameControllerObject").GetComponent<GameController>();
        soundC = GameObject.Find("GameControllerObject").GetComponent<SoundController>();
        soundC.Stop();
        soundC.PlayMainBGM();
        gameC.HideSettingButton();
    }
    public void ToMainScene()
    {
        gameC.ToMainScene();
    }

    public void Ranking()
    {
        float scoreTime = gameC.GetScoreTime();
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking (scoreTime);
    }
    public void TweetAdventure()
    {
        string tweetText = "GroundwaterのAdventureモードをクリアしました!!";
        naichilab.UnityRoomTweet.Tweet ("groundwater", tweetText, "Groundwater", "unity1week");
    }
    public void Tweet()
    {
        string tweetText = "Groundwaterを " + gameC.GetScoreTime().ToString("f2") + " 秒でクリアしました!!";
        naichilab.UnityRoomTweet.Tweet ("groundwater", tweetText, "Groundwater", "unity1week");
    }
}
