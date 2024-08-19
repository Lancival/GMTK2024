using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_ItemPrefab;
    [SerializeField] private List<Button> m_Tabs;

    int level = 1; // TODO: pull from GameManager

    void Start() {
        m_Tabs[0].interactable = false;

        UpdateScrollContent(level);
    }

    public void PopulateFishes() {
        m_Tabs[0].interactable = false;
        m_Tabs[1].interactable = true;
        ClearScrollContentItems();
        UpdateScrollContent(level);
    }

    public void PopulateDecos() {
        m_Tabs[0].interactable = true;
        m_Tabs[1].interactable = false;
        ClearScrollContentItems();
        UpdateScrollContent(level);
    }

    private void ClearScrollContentItems() {
        foreach (Transform child in m_ContentContainer.transform) {
            Destroy(child.gameObject);
        }
    }

    private void UpdateScrollContent(int stage) {
        List<StatsDatabase.StatItem> contentItems = StatsDatabase.Items.Where(x => x.stage == stage).ToList();

        for (int i = 0; i < contentItems.Count; i++) {
            var item = Instantiate(m_ItemPrefab, m_ContentContainer, true);
            item.transform.localScale = Vector2.one;
            item.GetComponent<Button>().image.sprite = contentItems[i].sprite;

            InventoryItem invItem = item.GetComponent<InventoryItem>();
            invItem.SpawnName = contentItems[i].name;
        }
    }
}