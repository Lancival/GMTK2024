using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItem : MonoBehaviour {
    public string SpawnName;
    public string AssetType;

    public void SpawnItem() {
        // TODO: create fish or deco game object from statItem
        if (AssetType == "Fish") {
            Factory.Instance.CreateFish(SpawnName, transform.position);
        } else if (AssetType == "Decoration") {
            Factory.Instance.CreateDeco(SpawnName, transform.position);
        }
    }
}
