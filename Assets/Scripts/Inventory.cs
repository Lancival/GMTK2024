using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_ItemPrefab;
    [SerializeField] private List<Button> m_Tabs;

    private List<string> fishes = new();
    private List<string> decos = new();

    void Start()
    {
        for (int i = 0; i < 20; ++i)
        {
            fishes.Add("Fish" + i);
        }

        for (int i = 0; i < 20; ++i)
        {
            decos.Add("Deco" + i);
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
