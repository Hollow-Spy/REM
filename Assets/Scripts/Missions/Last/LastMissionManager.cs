using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LastMissionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] float TimeLeft;
    void Start()
    {
        timeText.gameObject.SetActive(true);
    }
    //https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime(TimeLeft);
        TimeLeft -= Time.deltaTime;
    }
}
