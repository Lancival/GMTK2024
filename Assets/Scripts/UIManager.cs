using System;
using DG.Tweening;
using UnityEngine;

public class UIManager : Singleton<UIManager> {
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

    [Header("Objectives Panel")]
    [SerializeField] CanvasGroup objectivesPanel;

    [SerializeField] UITweenParameters tp;

    [Header("Complete Button")]
    public RectTransform completeButton;

    void Awake() { infoPanelUI = infoPanel.GetComponent<InfoPanelUI>(); }

    public void ToggleInventoryPanel(bool enable) {
        DOTween.Kill(inventoryPanel.transform);
        inventoryPanel.transform.DOMove(enable ? inventoryOpenPos.position : inventoryClosePos.position, tp.PanelSlideDur);
        string invSFX = enable ? "event:/SFX/Inventory_Open" : "event:/SFX/Inventory_Close";
        FMODUnity.RuntimeManager.PlayOneShot(invSFX);
    }

    public void ToggleInfoPanel(bool enable) {
        DOTween.Kill(infoPanel.transform);
        infoPanel.transform.DOMove(enable ? infoOpenPos.position : infoClosePos.position, tp.PanelSlideDur);
    }

    public void UpdateInfoPanel(StatsDatabase.StatItem statItem) { infoPanelUI.UpdateInfo(statItem); }

    public void ToggleObjectivesPanel() {
        DOTween.Kill(objectivesPanel);
        if (objectivesPanel.alpha == 0) {
            objectivesPanel.DOFade(1.0f, tp.PanelFadeDur);
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ObjectiveMenu_Open");
        } else {
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