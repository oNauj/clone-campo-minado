using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrefabVisual : MonoBehaviour
{
    [SerializeField] private Transform nodeTransform;
    [SerializeField] private Transform mineTransform;
    [SerializeField] private Transform numberTransform;
    [SerializeField] private Transform flagTransform;
    [SerializeField] private TextMeshProUGUI numberMeshProUGUI;

    bool isNodeActive = true;


    public void SetActiveNode(bool active)
    {
        nodeTransform?.gameObject.SetActive(active);
        isNodeActive = active;

        SetActiveFlag(false);
    }
    public void SetNumberColor(Color color)
    {
        numberMeshProUGUI.color = color;
    }
    public bool GetNodeisActive()
    {
        return isNodeActive;
    }

    public void SetActiveMine()
    {
        mineTransform?.gameObject.SetActive(true);
        numberTransform?.gameObject.SetActive(false);
    }
    public void SetActiveFlag(bool active)
    {
        flagTransform?.gameObject.SetActive(active);
    }

    public void SetActiveNumber(bool active)
    {
        numberTransform?.gameObject.SetActive(active);
    }

    public void SetNumber(int number)
    {
        SetActiveNumber(true);
        numberMeshProUGUI.text = number.ToString();
    }
}
