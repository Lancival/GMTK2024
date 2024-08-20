using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Factory : Singleton<Factory> {
    [SerializeField] GameObject fishPrefab;
    [SerializeField] GameObject decorationPrefab;

    public Fish CreateFish(StatsDatabase.StatItem statItem, Vector2 pos) {
        if (statItem == null || statItem.assetType != "Fish") {
            return null;
        }
        
        Fish fish = Instantiate(fishPrefab, pos, Quaternion.identity).GetComponent<Fish>();
        fish.Init(statItem);
        Collider2D col = fish.AddComponent<CircleCollider2D>();
        col.isTrigger = true;

        return fish;
    }
    
    public Decoration CreateDeco(StatsDatabase.StatItem statItem, Vector2 pos) {
        if (statItem == null || statItem.assetType != "Decoration") {
            return null;
        }
        
        Decoration deco = Instantiate(decorationPrefab, pos, Quaternion.identity).GetComponent<Decoration>();
        deco.Init(statItem);
        Collider2D col = deco.AddComponent<CircleCollider2D>();
        col.isTrigger = true;

        return deco;
    }
}
