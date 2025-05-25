using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    //Countdown
    [SerializeField] AudioClip[] ClipCountdown = new AudioClip[0];
    [SerializeField] GameObject CountdownParent;
    [SerializeField] TimerManager timerManager;

    public int countdownTime;
    public Text countdownDisplay;

    private void Start()

    {
        //StartCoroutine(CountdownToStart());

    }


    IEnumerator CountdownToStart()

    {
        yield return new WaitForSeconds(0.5f);
        //SoundManager.Instance.RandomSoundEffect(ClipCountdown);
        while (countdownTime > 0)
        {
            LeanTween.scale(countdownDisplay.gameObject, Vector3.zero, 0.7f).setEase(LeanTweenType.punch);
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            //LeanTween.scale(countdownDisplay.gameObject, Vector3.zero, 0f).setEase(LeanTweenType.easeOutElastic);

            countdownTime--;
            //yield return new WaitForSeconds(1f);
        }

        //countdownDisplay.text = "Start";

        //name of the start game window
        //GameObject.instance.OpenScene();

        yield return new WaitForSeconds(0.1f);

        //mooooooooooooreee;

        CountdownParent.SetActive(false);

        timerManager.StartStopwatch();

    }
}
