using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [Header("Inventory Panel")]
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] RectTransform inventoryOpenPos;
    [SerializeField] RectTransform inventoryClosePos;
    
    [Header("Info Panel")]
    [SerializeField] GameObject infoPanel;
    [SerializeField] RectTransform infoOpenPos;
    [SerializeField] RectTransform infoClosePos;
    InfoPanelUI infoPanelUI;
    bool infoIsOpen;

    [Header("Objectives Panel")]
    [SerializeField] CanvasGroup objectivesPanel;

    [SerializeField] UITweenParameters tp;

    void Awake() {
        infoPanelUI = infoPanel.GetComponent<InfoPanelUI>();
    }

    public void ToggleInventoryPanel(bool enable) {
        DOTween.Kill(inventoryPanel.transform);
        inventoryPanel.transform.DOMove(enable ? inventoryOpenPos.position : inventoryClosePos.position, tp.PanelSlideDur);
    }

    public void ToggleInfoPanel() {
        DOTween.Kill(infoPanel.transform);
        infoPanel.transform.DOMove(infoIsOpen ? infoClosePos.position : infoOpenPos.position, tp.PanelSlideDur);
        infoIsOpen = !infoIsOpen;
    }

    public void ToggleObjectivesPanel() {
        DOTween.Kill(objectivesPanel);
        if (objectivesPanel.alpha == 0)
        {
            objectivesPanel.DOFade(1.0f, tp.PanelFadeDur);
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ObjectiveMenu_Open");
        }
        else
        {
            objectivesPanel.DOFade(0.0f, tp.PanelFadeDur);
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ObjectiveMenu_Close");
        }
    }
}

[Serializable]
public struct UITweenParameters {
    public float PanelSlideDur;
    public float PanelFadeDur;
}