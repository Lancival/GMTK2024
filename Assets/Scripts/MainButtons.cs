using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtons : MonoBehaviour
{
    public ObjectiveUI m_ObjectiveUI;

    private void Start()
    {
        m_ObjectiveUI.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void ShowObjectiveUI()
    {
        m_ObjectiveUI.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void ToggleValueChanged()
    {
        m_ObjectiveUI.UpdateUI();
    }
}
