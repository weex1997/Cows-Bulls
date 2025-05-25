
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] LocalizedString StagesScore;
    [SerializeField] LocalizedString ChallngeScore;
    [SerializeField] LocalizedString StagesPlace;
    [SerializeField] LocalizedString ChallngePlace;
    [SerializeField] TMP_InputField nameInput1;
    [SerializeField] TMP_InputField nameInput2;

    public GameObject PlayerDataObject;

    // Singleton instance.
    public static PlayerDataManager Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance , set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        dataUbdated();
    }
    void OnEnable()
    {
        StagesScore.Arguments = new[] { PlayerPrefs.GetString("PlayerStrikeScore") ?? "0" };
        StagesScore.StringChanged += PlayerStrikeScore;

        StagesPlace.Arguments = new[] { PlayerPrefs.GetString("PlayerStrikePlace") ?? "0" };
        StagesPlace.StringChanged += PlayerStrikePlace;



        ChallngeScore.Arguments = new[] { PlayerPrefs.GetString("PlayerChallengScore") ?? "0" };
        ChallngeScore.StringChanged += PlayerChallengScore;

        ChallngePlace.Arguments = new[] { PlayerPrefs.GetString("PlayerChallengPlace") ?? "0" };
        ChallngePlace.StringChanged += PlayerChallengPlace;

    }

    void OnDisable()
    {
        StagesScore.StringChanged -= PlayerStrikeScore;
        StagesPlace.StringChanged -= PlayerStrikePlace;
        ChallngeScore.StringChanged -= PlayerChallengScore;
        ChallngePlace.StringChanged -= PlayerChallengPlace;
    }

    void PlayerStrikeScore(string s)
    {
        PlayerDataObject.transform.GetChild(2).GetComponent<TMP_Text>().text = s;
    }
    void PlayerStrikePlace(string s)
    {
        PlayerDataObject.transform.GetChild(3).GetComponent<TMP_Text>().text = s;
    }

    void PlayerChallengScore(string s)
    {
        PlayerDataObject.transform.GetChild(4).GetComponent<TMP_Text>().text = s;
    }

    void PlayerChallengPlace(string s)
    {
        PlayerDataObject.transform.GetChild(5).GetComponent<TMP_Text>().text = s;
    }


    // void OnGUI()
    // {
    //     // This calls UpdateString immediately (if the table is loaded) or when the table is available.
    //     StagesScore.RefreshString();
    //     GUILayout.Label(PlayerDataObject.transform.GetChild(2).GetComponent<TMP_Text>().text);
    // }
    public void dataUbdated()
    {
        PlayerDataObject.transform.GetChild(1).GetComponent<TMP_Text>().text = PlayerPrefs.GetString("PlayerName") ?? "name";
    }

    public void SubmetName()
    {
        PlayerPrefs.SetString("PlayerName", nameInput1.text);
        PlayerPrefs.Save();
        PlayFabManager.Instance.SubmitNameButton();
    }

    public void UpdateName()
    {
        if (nameInput2.text != "")
        {
            PlayerPrefs.SetString("PlayerName", nameInput2.text);
            PlayerPrefs.Save();
            PlayFabManager.Instance.SubmitNameButton();
        }
        else
        {
            Debug.Log("name is empty");
        }

    }

    public void RandomName()
    {

        string[] nameComponent1 = new string[] { "White", "Yellow", "Gold", "Pink", "Red", "Plum", "Green", "Blue", "Navy", "Black" };
        string[] nameComponent2 = new string[] { "Cow", "Bull" };

        string nameCompfirst = nameComponent1[Random.Range(0, nameComponent1.Length)];
        string nameCompSecond = nameComponent2[Random.Range(0, nameComponent2.Length)];

        string result = nameCompfirst + nameCompSecond;

        nameInput1.text = result;


    }
}
