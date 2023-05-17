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


    public void SetActiveNode(bool active)
    {
        print(active);
        nodeTransform?.gameObject.SetActive(active);
    }

    public void SetActiveMine()
    {
        mineTransform?.gameObject.SetActive(true);
        numberTransform?.gameObject.SetActive(false);
    }
    public void SetActiveFlag()
    {
        flagTransform?.gameObject.SetActive(true);
    }

    public void SetNumber(int number)
    {
        numberMeshProUGUI.text = number.ToString();
        Debug.Log(numberMeshProUGUI.text);
    }
}
