using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingWindow : MonoBehaviour
{
    [SerializeField] GameObject loseWindow;

    [SerializeField] TMP_Text guessText;
    [SerializeField] TMP_Text Score;

    [SerializeField] AudioClip[] Cliplose;
    [SerializeField] AudioClip[] ClipHomeButton;
    [SerializeField] AudioClip[] ClipStartGameButton;

    [SerializeField] ShareWindow shareWindow;

    void Awake()
    {
        loseWindow.SetActive(true);

    }
    private void Start()
    {
        shareWindow = GameObject.FindObjectOfType<ShareWindow>();

        foreach (int num in GameManager.Instance.hiddenNumberList)
        {
            guessText.text += num.ToString();

        }
        if (GameManager.Instance.gameMode != GameMode.Puzzel)
        {
            Score.text = "Your Score: " + PlayerPrefs.GetInt("PlayerScoreStrikeTotal") + " \n Best Score: " + PlayerPrefs.GetInt("PlayerScoreStrike").ToString();
            PlayerPrefs.SetInt("PlayerScoreStrikeTotal", 0);
        }


        SoundManager.Instance.RandomSoundEffect(Cliplose);

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
        GameButtonsManager.Instance.LosingWindow = gameObject;
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
