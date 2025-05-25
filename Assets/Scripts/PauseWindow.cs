
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] AudioClip[] ClipSettingsButtonClose;
    [SerializeField] AudioClip[] ClipHomeButton;
    [SerializeField] AudioClip[] ClipStartGameButton;

    [SerializeField] GameObject restartButton;
    void Start()
    {
        if (GameManager.Instance.gameMode == GameMode.Strike)
            restartButton.SetActive(false);
        else
            restartButton.SetActive(true);
    }
    public void Home()
    {
        if (GameManager.Instance.gameMode == GameMode.Strike)
            DataPristinceManager.Instance.SaveGame();

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        SoundManager.Instance.RandomSoundEffect(ClipHomeButton);
    }
    public void Resume()
    {
        SceneManager.UnloadSceneAsync("PauseWindow");

        Time.timeScale = 1;
        SoundManager.Instance.RandomSoundEffect(ClipSettingsButtonClose);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        SoundManager.Instance.RandomSoundEffect(ClipStartGameButton);
    }
}
