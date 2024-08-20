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

    private bool topChecked = false;
    private bool bottomChecked = false;

    public void UpdateUI()
    {
        if (m_objectiveManager == null) {
            Debug.Log("Reassigning objective manager");
            m_objectiveManager = (ObjectiveManager)FindObjectOfType<ObjectiveManager>();
        }

        var spaceProgress = m_objectiveManager.CalculateSpaceLevel();
        var waterQualityProgress = m_objectiveManager.CalculateWaterQualityLevel();

        UpdateProgress(m_spaceProgressMask, m_spaceProgressRect, 1f - spaceProgress);
        UpdateProgress(m_waterQualityProgressMask, m_waterQualityProgressRect, 1f - waterQualityProgress);

        var currentFishes = m_objectiveManager.CurrentFishCount();
        var currentDecoration = m_objectiveManager.CurrentDecorationCount();
        
        bool shouldCheckTop = currentFishes >= m_objectiveManager.Objective.FishCount;
        bool shouldCheckBottom = currentDecoration >= m_objectiveManager.Objective.DecorationCount;
        Debug.Log("Should Check Top" +shouldCheckTop);
        Debug.Log("Should Check bottom" + shouldCheckBottom);

        if (shouldCheckTop)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ObjectiveMenu_Complete");
            topChecked = true;
        }
        else if (topChecked)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ObjectiveMenu_Incomplete");
            topChecked = false;
        }

        if (shouldCheckBottom)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ObjectiveMenu_Complete");
            bottomChecked = true;
        }
        else if (bottomChecked)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/ObjectiveMenu_Incomplete");
            bottomChecked = false;
        }

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
