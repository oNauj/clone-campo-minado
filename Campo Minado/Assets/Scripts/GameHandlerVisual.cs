using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandlerVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flagTextMeshProUGUI;

    [SerializeField] private TextMeshProUGUI timerTextMeshProUGUI;

    [SerializeField] private Transform WinTransform;


    [SerializeField] private TextMeshProUGUI WinTimerTextMeshProUGUI;


    [SerializeField] private Transform FailTransform;


    private void Start()
    {
        GameHandler.Instance.OnAddFlag += Instance_OnAddFlag;
        GameHandler.Instance.OnAddTimer += Instance_OnAddTimer;
        GameHandler.Instance.OnVictory += Instance_OnVictory;
        GameHandler.Instance.OnFail += Instance_OnFail;
    }

    private void Instance_OnFail(object sender, System.EventArgs e)
    {
        FailTransform.gameObject.SetActive(true);
    }

    private void Instance_OnVictory(object sender, System.EventArgs e)
    {
        WinTimerTextMeshProUGUI.text = timerTextMeshProUGUI.text;
        WinTransform.gameObject.SetActive(true);
    }

    private void Instance_OnAddTimer(object sender, GameHandler.OnAddTimerEventArgs e)
    {
       timerTextMeshProUGUI.text = string.Format("{0:00} : {1:00}", e.minutes, e.seconds);
    }

    private void Instance_OnAddFlag(object sender, GameHandler.OnAddFlagEventArgs e)
    {
        flagTextMeshProUGUI.text = e.flagSize.ToString();
    }

    
}
