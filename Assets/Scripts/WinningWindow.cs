using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WinningWindow : MonoBehaviour
{
    [SerializeField] GameObject WinningEffect;
    [SerializeField] GameObject winningWindow;
    [SerializeField] Transform LeaderboardScrollContact;
    [SerializeField] GameObject Leaderboard;
    [SerializeField] GameObject ScoreObject;
    [SerializeField] GameObject WinningText;

    [SerializeField] TMP_Text guessText;
    [SerializeField] TMP_Text Score;

    [SerializeField] AudioClip[] ClipWinn;
    [SerializeField] AudioClip[] ClipHomeButton;
    [SerializeField] AudioClip[] ClipStartGameButton;
    [SerializeField] ShareWindow shareWindow;

    string ScorePrefsString;
    string leaderboardName;
    int playerTotalScore;

    private void Start()
    {
        shareWindow = GameObject.FindObjectOfType<ShareWindow>();
        if (GameManager.Instance.gameMode == GameMode.Puzzel)
        {
            Leaderboard.SetActive(false);
            ScoreObject.SetActive(false);
            int rand = Random.Range(0, 3);
            if (rand == 1)
            {
                WinningText.GetComponent<TMP_Text>().text = "You Gain +1 <sprite index=0>";
                int hint = PlayerPrefs.GetInt("Hint") + 1;
                PlayerPrefs.SetInt("Hint", hint);
                PlayerPrefs.Save();
            }
            else
            {
                WinningText.GetComponent<TMP_Text>().text = "You Gain +1 <sprite index=1>";
                int hintRemove = PlayerPrefs.GetInt("HintRemove") + 1;
                PlayerPrefs.SetInt("HintRemove", hintRemove);
                PlayerPrefs.Save();
            }
        }
        if (GameManager.Instance.gameMode == GameMode.Strike)
        {
            ScorePrefsString = "PlayerScoreStrike";
            leaderboardName = "Cows&BullsStrikeMode";
            playerTotalScore = PlayerPrefs.GetInt("PlayerScoreStrikeTotal");
        }

        if (GameManager.Instance.gameMode == GameMode.Challeng)
        {
            ScorePrefsString = "PlayerScoreChallenge";
            leaderboardName = "Cows&BullsLeaderboard";
            playerTotalScore = GameManager.Instance._PlayerScore;
        }

        if (GameManager.Instance.gameMode == GameMode.Challeng || GameManager.Instance.gameMode == GameMode.Strike)
        {
            WinningText.SetActive(false);

            int prevusScore = 0;
            if (PlayerPrefs.HasKey(ScorePrefsString))
            {
                prevusScore = PlayerPrefs.GetInt(ScorePrefsString);
            }
            else
            {
                PlayerPrefs.SetInt(ScorePrefsString, 0);
            }

            Debug.Log("prevuss" + prevusScore);

            if (prevusScore < playerTotalScore)
            {
                PlayerPrefs.SetInt(ScorePrefsString, playerTotalScore);
            }
            if (PlayerPrefs.GetInt(ScorePrefsString) > 0)
            {
                PlayFabManager.Instance.LeaderboardScrollContact = LeaderboardScrollContact;

                PlayFabManager.Instance.SendLeaderboard(PlayerPrefs.GetInt(ScorePrefsString), leaderboardName);

            }
            else
            {
                PlayFabManager.Instance.LeaderboardScrollContact = LeaderboardScrollContact;
                StartCoroutine(WatingLeaderboard());
            }

            if (GameManager.Instance.gameMode == GameMode.Strike)
            {
                Score.text = "Your Score: " + playerTotalScore + " \n Best Score: " + PlayerPrefs.GetInt(ScorePrefsString).ToString();
                Debug.Log("current Score" + playerTotalScore);

                PlayerPrefs.SetInt(ScorePrefsString, 0);
            }
            if (GameManager.Instance.gameMode == GameMode.Challeng)
            {
                Score.text = "Your Score: " + playerTotalScore + " \n Best Score: " + PlayerPrefs.GetInt(ScorePrefsString).ToString();
                Debug.Log("current Score" + playerTotalScore);
            }
        }

        foreach (int num in GameManager.Instance.hiddenNumberList)
        {
            guessText.text += num.ToString();

        }
        SoundManager.Instance.RandomSoundEffect(ClipWinn);
        StartCoroutine(WatingWinning());

    }

    IEnumerator WatingWinning()
    {
        WinningEffect.SetActive(true);
        yield return new WaitForSeconds(3);
        winningWindow.SetActive(true);
    }
    IEnumerator WatingLeaderboard()
    {

        yield return new WaitForSeconds(1);
        PlayFabManager.Instance.GetLeaderboard(leaderboardName);

    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        SoundManager.Instance.RandomSoundEffect(ClipHomeButton);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        SoundManager.Instance.RandomSoundEffect(ClipStartGameButton);
    }
    public void Share()
    {
        shareWindow.ShareButton();
        gameObject.SetActive(false);
        GameButtonsManager.Instance.winningWindow = gameObject;
        GameManager.Instance.Corsier.SetActive(false);
        if (!shareWindow.shareWindowObject.activeSelf)
        {

            shareWindow.shareWindowObject.SetActive(true);

        }
        else
        {
            shareWindow.shareWindowObject.SetActive(false);

        }

    }
}
