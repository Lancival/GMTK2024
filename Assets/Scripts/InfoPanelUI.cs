using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI typeValueText;
    [SerializeField] TextMeshProUGUI spaceValueText;
    [SerializeField] TextMeshProUGUI cleanlinessValueText;
    [SerializeField] TextMeshProUGUI flavorText;
    
    public void UpdateInfo(StatsDatabase.StatItem statItem) {
        nameText.name = statItem.name;
        nameText.text = statItem.name;
        image.sprite = statItem.sprite;
        typeValueText.text = statItem.assetType;
        spaceValueText.text = statItem.space;
        cleanlinessValueText.text = statItem.waterQuality;
        flavorText.text = statItem.flavorText;
    }
}
