using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtons : MonoBehaviour
{
    public GameObject m_ObjectiveUI;

    private void Start()
    {
        m_ObjectiveUI.SetActive(false);
    }

    public void ShowObjectiveUI()
    {
        m_ObjectiveUI.SetActive(true);
    }
}
