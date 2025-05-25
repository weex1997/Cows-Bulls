using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageWindow : MonoBehaviour
{
    public void addAttemptet()
    {
        AdmobAdsManager.Instance.ShowRewardedAd(false, false, true, false);
    }
    public void close()
    {
        SceneManager.UnloadSceneAsync("Masseges");
        GameManager.Instance.itsLosing = true;
        AdmobAdsManager.Instance.ShowInterstitialAd();
    }
}
