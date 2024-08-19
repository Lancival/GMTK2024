using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItem : MonoBehaviour {
    public string SpawnName;

    public void SpawnItem() {
        StatsDatabase.StatItem statItem = StatsDatabase.Items.Find(x => x.name == SpawnName);
        // TODO: create fish or deco game object from statItem
    }
}
