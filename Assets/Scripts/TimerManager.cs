using System;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    //timer
    bool stopwatchActive = false;
    public float currentTime = 0;
    [SerializeField] TMP_Text currentTimeText;




    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive == true)
        {

            currentTime = currentTime - Time.deltaTime;

            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.ToString(@"mm\.ss");
        }

    }


    public void StartStopwatch()
    {
        stopwatchActive = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }


}
