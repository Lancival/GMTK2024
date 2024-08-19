using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtons : MonoBehaviour
{
    public GameObject m_ObjectiveUI;

    private void Start()
    {
        m_ObjectiveUI.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void ShowObjectiveUI()
    {
        m_ObjectiveUI.GetComponent<CanvasGroup>().alpha = 1f;
    }
}
