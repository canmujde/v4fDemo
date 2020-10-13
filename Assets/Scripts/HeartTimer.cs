using System;
using UnityEngine;
using UnityEngine.UI;

public class HeartTimer : MonoBehaviour
{

    [SerializeField] private float waitTimeInSeconds;
    [SerializeField] private Text timerText;
    [SerializeField] private Button button;
    [SerializeField] private ulong lastHeartTake;

    private void Start()
    {
        Init();
    }



    private void Update()
    {
        if (!button.IsInteractable())
        {
            if (IsHeartsReady())
            {
                button.interactable = true;
                return;
            }
            timerText.text = TimeLeftToText();
        }
    }

    private void Init()
    {
        DateTime today = DateTime.Now;
        DateTime tomorrow = today.AddDays(1);

        string s = tomorrow.Ticks.ToString();

        lastHeartTake = ulong.Parse(PlayerPrefs.GetString("lastHeartTake",s));





        if (!IsHeartsReady())
            button.interactable = false;
    }

    public void Take()
    {
        PlayerStats.instance.SetDefaultHearts();
        UIManager.instance.UpdateAll();
        UIManager.instance.CloseAdPanel();
        lastHeartTake = ((ulong)DateTime.Now.Ticks);
        PlayerPrefs.SetString("lastHeartTake", lastHeartTake.ToString());
        button.interactable = false;
    }

    private string TimeLeftToText()
    {
        float time = Timer();
        string timeLeft = "";

        timeLeft += ((int)time / 3600) + "h ";
        time -= ((int)time / 3600) * 3600;

        timeLeft += ((int)time / 60).ToString("00") + "m ";

        timeLeft += (time % 60).ToString("00") + "s ";

        return timeLeft;
    }

    private float Timer()
    {
        ulong diff = (ulong)DateTime.Now.Ticks - lastHeartTake;
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)((waitTimeInSeconds * 1000) - m) / 1000.0f;

        return secondsLeft;
    }

    private bool IsHeartsReady()
    {
        if (Timer() < 0)
        {
            timerText.text = "Ready!";
            return true;
        }


        return false;
    }
}
