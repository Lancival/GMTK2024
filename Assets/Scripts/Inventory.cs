using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_ItemPrefab;
    [SerializeField] private List<Button> m_Tabs;
    [SerializeField] private StatsDatabase m_Stats;

    private List<string> fishes = new();
    private List<string> decos = new();

    void Start()
    {
        var stats = m_Stats.stats;
        var stage1Data = stats.Where(x => x.stage == "1 - Child");
        var stage1DataFishes = stage1Data.Where(x => x.assetType == "Fish");
        foreach (var fish in stage1DataFishes)
        {
            fishes.Add(fish.name);
        }

        var stage1DataDecos = stage1Data.Where(x => x.assetType == "Decoration");
        foreach (var deco in stage1DataDecos)
        {
            decos.Add(deco.name);
        }

        m_Tabs[0].interactable = false;

        UpdateScrollContent(fishes);
    }

    public void PopulateFishes()
    {
        m_Tabs[0].interactable = false;
        m_Tabs[1].interactable = true;
        ClearScrollContentItems();
        UpdateScrollContent(fishes);
    }

    public void PopulateDecos()
    {
        m_Tabs[0].interactable = true;
        m_Tabs[1].interactable = false;
        ClearScrollContentItems();
        UpdateScrollContent(decos);
    }

    private void ClearScrollContentItems()
    {
        foreach (Transform child in m_ContentContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void UpdateScrollContent(List<string> contentItems)
    {
        for (int i = 0; i < contentItems.Count; i++)
        {
            var item = Instantiate(m_ItemPrefab);
            // TODO: fetch content as images
            // item.GetComponentInChildren<TMP_Text>().text = contentItems[i];
            item.transform.SetParent(m_ContentContainer);
            item.transform.localScale = Vector2.one;
        }
    }
}
