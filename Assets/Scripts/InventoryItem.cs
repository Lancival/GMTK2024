using UnityEngine;

public class InventoryItem : MonoBehaviour {
    public StatsDatabase.StatItem statItem;

    HoverEvent hover;

    void Awake() {
        hover = GetComponent<HoverEvent>();
        hover.OnHoverEnter += OnHoverEnter;
        hover.OnHoverExit += OnHoverExit;
    }

    void OnHoverEnter() {
        UIManager.Instance.ToggleInfoPanel(true);
        UIManager.Instance.UpdateInfoPanel(statItem);
    }
    void OnHoverExit() {
        UIManager.Instance.ToggleInfoPanel(false);
    }

    public void SpawnItem() {
        if (statItem.assetType == "Fish") {
            Factory.Instance.CreateFish(statItem, transform.position);
        } else if (statItem.assetType == "Decoration") {
            Factory.Instance.CreateDeco(statItem, transform.position);
        }
    }
}