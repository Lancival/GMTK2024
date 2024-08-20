using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public ObjectiveManager m_objectiveManager;

    public RectMask2D m_spaceProgressMask;
    public RectTransform m_spaceProgressRect;

    public RectMask2D m_waterQualityProgressMask;
    public RectTransform m_waterQualityProgressRect;

    public GameObject m_objectiveItemTopCheckboxEmpty;
    public GameObject m_objectiveItemTopCheckboxChecked;
    public TMP_Text m_objectiveItemTopText;

    public GameObject m_objectiveItemBottomCheckboxEmpty;
    public GameObject m_objectiveItemBottomCheckboxChecked;
    public TMP_Text m_objectiveItemBottomText;

    public void UpdateUI()
    {
        var spaceProgress = m_objectiveManager.CalculateSpaceLevel();
        var waterQualityProgress = m_objectiveManager.CalculateWaterQualityLevel();

        UpdateProgress(m_spaceProgressMask, m_spaceProgressRect, 1f - spaceProgress);
        UpdateProgress(m_waterQualityProgressMask, m_waterQualityProgressRect, 1f - waterQualityProgress);

        var currentFishes = m_objectiveManager.CurrentFishCount();
        var currentDecoration = m_objectiveManager.CurrentDecorationCount();

        bool shouldCheckTop = currentFishes == m_objectiveManager.Objective.FishCount;
        bool shouldCheckBottom = currentDecoration == m_objectiveManager.Objective.DecorationCount;

        UpdateObjectiveItem(m_objectiveItemTopCheckboxEmpty, m_objectiveItemTopCheckboxChecked, shouldCheckTop, m_objectiveItemTopText, m_objectiveManager.Objective.FishObjectiveText);
        UpdateObjectiveItem(m_objectiveItemBottomCheckboxEmpty, m_objectiveItemBottomCheckboxChecked, shouldCheckBottom, m_objectiveItemBottomText, m_objectiveManager.Objective.DecorationObjectiveText);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    void UpdateProgress(RectMask2D mask, RectTransform transform, float percent)
    {
        mask.padding = new Vector4(0, 0, transform.sizeDelta.x * percent, 0);
    }

    void UpdateObjectiveItem(GameObject checkBoxEmpty, GameObject checkBoxChecked, bool isChecked, TMP_Text tmpText, string objectiveText) 
    {
        checkBoxEmpty.gameObject.SetActive(!isChecked);
        checkBoxChecked.gameObject.SetActive(isChecked);
        tmpText.text = objectiveText;
    }
}
