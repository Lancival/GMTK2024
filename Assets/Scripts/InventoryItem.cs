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
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Select", "Size", 0);
            Factory.Instance.CreateFish(SpawnName, transform.position);
        } else if (AssetType == "Decoration") {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Select", "Material", 0);
            Factory.Instance.CreateDeco(SpawnName, transform.position);
        }
    }
}
