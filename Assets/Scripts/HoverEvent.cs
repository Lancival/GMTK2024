using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public event Action OnHoverEnter;
    public event Action OnHoverExit;

    private InventoryItem item;

    bool isHovering;

    void Start()
    {
        item = GetComponent<InventoryItem>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!isHovering) {
            isHovering = true;
            OnHoverEnter?.Invoke();
        }

        if (item.AssetType == "Fish")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Inventory_Fish_Hover");
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Inventory_Dec_Hover");
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (isHovering) {
            isHovering = false;
            OnHoverExit?.Invoke();
        }
    }
}