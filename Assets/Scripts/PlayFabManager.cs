using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] GameObject rowPrefab;
    [SerializeField] GameObject rowPrefabHome;

    public string playername;
    public string playerID;
    [SerializeField] GameObject PlaynameWindow;
    public Transform LeaderboardScrollContact;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] Transform ScrollContentHomeLeadeboard;

    //[SerializeField] TMP_Text VersionText;

    private bool loadingEndFlag = false;
    string _LeaderboardName;

    // Singleton instance.
    public static PlayFabManager Instance = null;

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

        //Set DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

        Login();
        //VersionText.text = "Version " + Application.version;
        //SettingWindow.SetActive(true);
        LoadingScreen.SetActive(true);
        StartCoroutine(Loading());
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful Login/acount creat!");

        if (result.InfoResultPayload.PlayerProfile != null)
        {
            playername = result.InfoResultPayload.PlayerProfile.DisplayName;
            playerID = result.InfoResultPayload.PlayerProfile.PlayerId;
            Debug.Log("Hi " + playername + " " + playerID);
            PlayerPrefs.SetString("PlayerID", playerID);
            PlayerPrefs.Save();
        }
        else
        {
            GetAccountInfo();
        }

        if (result.NewlyCreated == true)
        {
            PlaynameWindow.SetActive(true);
            Debug.Log("case 1");
        }
        else if ((playername == null || playername == "") && (PlayerPrefs.GetString("PlayerName") == null || PlayerPrefs.GetString("PlayerName") == ""))
        {
            PlaynameWindow.SetActive(true);
            Debug.Log("case 2");
        }
        else if ((playername == null || playername == "") && (playername != PlayerPrefs.GetString("PlayerName")))
        {
            PlaynameWindow.SetActive(false);
            SubmitNameButton();
            Debug.Log("case 3");
        }
        else
        {
            Debug.Log("case 4");
            PlaynameWindow.SetActive(false);

            if (playername == null || playername == "")
            {
                PlayerDataManager.Instance.RandomName();
                PlayerDataManager.Instance.SubmetName();
            }
            if (playername != PlayerPrefs.GetString("PlayerName"))
            {
                PlayerPrefs.SetString("PlayerName", playername);
                PlayerPrefs.Save();
            }
        }
        GetAppearance();
        loadingEndFlag = true;

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while loggimg in/creating account!");
        Debug.Log(error.GenerateErrorReport());
        PlaynameWindow.SetActive(false);
        //SettingWindow.SetActive(false);
    }

    #region ID
    //ID Founder
    void GetAccountInfo()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, Successs, fail);
    }


    void Successs(GetAccountInfoResult result)
    {

        playerID = result.AccountInfo.PlayFabId;
        PlayerPrefs.SetString("PlayerID", playerID);

    }


    void fail(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());
    }
    #endregion

    #region leaderboard
    public void SendLeaderboard(int score, string LeaderboardName)
    {
        _LeaderboardName = LeaderboardName;
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = LeaderboardName,
                    Value = score
                }

            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Seccssful leaderboard sent");
        StartCoroutine(WatingLeaderboard());
    }
    public void GetLeaderboard(string LeaderboardName)
    {

        _LeaderboardName = LeaderboardName;
        var request = new GetLeaderboardRequest
        {

            StatisticName = LeaderboardName,
            StartPosition = 0,
            MaxResultsCount = 100

        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        if (LeaderboardScrollContact == null)
            LeaderboardScrollContact = GameObject.Find("LeaderboardScrollContact").GetComponent<Transform>();

        foreach (Transform item in LeaderboardScrollContact)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {

            GameObject newGo = Instantiate(rowPrefab, LeaderboardScrollContact);

            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(string.Format("PLACE: {0} | ID: {1} | VALUE: {2}",
             item.Position, item.PlayFabId, item.StatValue));

            if (item.PlayFabId == playerID)
            {

                if (_LeaderboardName == "Cows&BullsLeaderboard")
                {
                    PlayerPrefs.SetString("PlayerChallengScore", texts[2].text);
                    PlayerPrefs.SetString("PlayerChallengPlace", texts[0].text);
                }
                else
                {
                    PlayerPrefs.SetString("PlayerStrikeScore", texts[2].text);
                    PlayerPrefs.SetString("PlayerStrikePlace", texts[0].text);
                }

                SaveAppearance();


            }

        }

    }
    public void GetLeaderboardHome(string LeaderboardName)
    {
        _LeaderboardName = LeaderboardName;

        var request = new GetLeaderboardRequest
        {

            StatisticName = LeaderboardName,
            StartPosition = 0,
            MaxResultsCount = 100

        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGetHome, OnError);
    }

    void OnLeaderboardGetHome(GetLeaderboardResult result)
    {

        if (ScrollContentHomeLeadeboard != null)
        {

            foreach (Transform item in ScrollContentHomeLeadeboard)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in result.Leaderboard)
            {

                GameObject newGo = Instantiate(rowPrefabHome, ScrollContentHomeLeadeboard);

                TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
                texts[0].text = (item.Position + 1).ToString();
                texts[1].text = item.DisplayName;
                texts[2].text = item.StatValue.ToString();

                Debug.Log(string.Format("PLACE: {0} | ID: {1} | VALUE: {2}",
                 item.Position, item.PlayFabId, item.StatValue));

                if (item.PlayFabId == playerID)
                {

                    if (_LeaderboardName == "Cows&BullsLeaderboard")
                    {
                        PlayerPrefs.SetString("PlayerChallengScore", texts[2].text);
                        PlayerPrefs.SetString("PlayerChallengPlace", texts[0].text);
                    }
                    else
                    {
                        PlayerPrefs.SetString("PlayerStrikeScore", texts[2].text);
                        PlayerPrefs.SetString("PlayerStrikePlace", texts[0].text);
                    }
                    SaveAppearance();


                }

            }
        }
    }

    #endregion

    #region UserName

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {

            DisplayName = PlayerPrefs.GetString("PlayerName"),

        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
        playername = result.DisplayName;
        if (PlaynameWindow != null)
        {
            PlaynameWindow.SetActive(false);
        }
        PlayerPrefs.SetString("PlayerName", playername);
        PlayerPrefs.Save();
        PlayerDataManager.Instance.dataUbdated();
    }
    #endregion

    #region PlayerAvatar&Data

    // Player data
    public void GetAppearance()
    {

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    void OnDataRecieved(GetUserDataResult result)
    {

        Debug.Log("Recieved user data!");

        if (result.Data != null && result.Data.ContainsKey("PlayerChallengScore") || result.Data.ContainsKey("PlayerChallengPlace"))
        {
            PlayerPrefs.SetString("PlayerChallengScore", result.Data["PlayerChallengScore"].Value);
            PlayerPrefs.SetString("PlayerChallengPlace", result.Data["PlayerChallengPlace"].Value);
        }

        else
        {

            Debug.Log("Player data not complete!");

        }
        //AvatarsWindow.SetActive(false);
        //SettingWindow.SetActive(false);
        loadingEndFlag = true;



    }

    public void SaveAppearance()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {

    {"PlayerChallengScore", PlayerPrefs.GetString("PlayerChallengScore") },
    {"PlayerChallengPlace", PlayerPrefs.GetString("PlayerChallengPlace")}

                }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }
    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Successful user data send!");
    }

    #endregion

    IEnumerator Loading()
    {
        //wait for all data is loaded
        yield return new WaitUntil(() => loadingEndFlag == true);
        LoadingScreen.SetActive(false);
        //enter the new scene here
    }

    IEnumerator WatingLeaderboard()
    {

        yield return new WaitForSeconds(1);
        GetLeaderboard(_LeaderboardName);
    }
}
