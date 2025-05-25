using System.Collections;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class ShareButton : MonoBehaviour
{
    Camera _camera;
    GameObject Cameraa;
    public Image finishImage;
    public Sprite win;
    public Sprite lose;
    [SerializeField] TMP_Text Name;
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text Place;
    [SerializeField] TMP_Text gameMode;

    private void Start()
    {
        if (GameManager.Instance.gameMode == GameMode.Puzzel)
        {
            gameMode.text = GameManager.Instance.gameMode.ToString();
            gameMode.color = new Color32(99, 136, 85, 255);
            Name.text = PlayerPrefs.GetString("PlayerName");
            Score.text = "";
            Place.text = "";
        }
        if (GameManager.Instance.gameMode == GameMode.Challeng)
        {
            gameMode.text = GameManager.Instance.gameMode.ToString();
            gameMode.color = new Color32(75, 111, 192, 255);
            Name.text = PlayerPrefs.GetString("PlayerName");
            Score.text = "Best Score: " + (PlayerPrefs.GetString("PlayerChallengScore") ?? "0");
            Place.text = "Place: " + (PlayerPrefs.GetString("PlayerChallengPlace") ?? "0");
        }
        if (GameManager.Instance.gameMode == GameMode.Strike)
        {
            gameMode.text = GameManager.Instance.gameMode.ToString();
            gameMode.color = new Color32(187, 68, 53, 255);
            Name.text = PlayerPrefs.GetString("PlayerName");
            Score.text = "Best Score: " + (PlayerPrefs.GetString("PlayerStrikeScore") ?? "0");
            Place.text = "Place: " + (PlayerPrefs.GetString("PlayerStrikePlace") ?? "0");
        }

        if (GameManager.Instance.Winning)
            finishImage.sprite = win;
        else if (GameManager.Instance.Losing)
            finishImage.sprite = lose;

    }

    public void Share()
    {
        Cameraa = GameObject.Find("ShareCamera");
        _camera = Cameraa.GetComponent<Camera>();
        StartCoroutine(TakeScreenshotAndShare());


    }

    public string RandomText()
    {

        string[] nameComponent = new string[] { "Calling all puzzle enthusiasts! I've been mastering the art of Cows & Bulls. Can you beat my score?",

"Calling all puzzle enthusiasts! I've been mastering the art of Cows & Bulls and now I dare you to beat my score. Can you crack the code faster?",

"Attention, codebreakers! I've set a high score in Cows & Bulls that's begging to be beaten.",

"Hey, puzzle pros! I've been sharpening my Cows & Bulls skills and I'm ready to take on all challengers.",

"Attention, brainteaser enthusiasts! I've set a high score in Cows & Bulls that's just waiting to be surpassed." };

        string result = nameComponent[Random.Range(0, nameComponent.Length)];

        return result;


    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = _camera.targetTexture;

        // Render the camera's view.
        _camera.Render();

        Texture2D ss = new Texture2D(_camera.targetTexture.width, _camera.targetTexture.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        //yield return new WaitForSeconds(1);

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Cows & Bulls").SetText(RandomText() + " #CowsAndBulls").SetUrl("https://discord.gg/zejRR9S6gs")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
