using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
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

    void OnEnable()
    {
        UpdateProgress(m_spaceProgressMask, m_spaceProgressRect);
        UpdateProgress(m_waterQualityProgressMask, m_waterQualityProgressRect);

        UpdateObjectiveItem(m_objectiveItemTopCheckboxEmpty, m_objectiveItemTopCheckboxChecked, false);
        UpdateObjectiveItem(m_objectiveItemBottomCheckboxEmpty, m_objectiveItemBottomCheckboxChecked, true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    void UpdateProgress(RectMask2D mask, RectTransform transform)
    {
        mask.padding = new Vector4(0, 0, transform.sizeDelta.x * 0.2f, 0);
    }

    void UpdateObjectiveItem(GameObject checkBoxEmpty, GameObject checkBoxChecked, bool isChecked) 
    {
        checkBoxEmpty.gameObject.SetActive(!isChecked);
        checkBoxChecked.gameObject.SetActive(isChecked);
    }
}
