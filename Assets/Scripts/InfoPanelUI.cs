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
    
    public void UpdateInfo() {
        // TODO: query item data -> change panel
    }
}
