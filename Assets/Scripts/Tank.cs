using System;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    [field: SerializeField] public List<Fish> Fishes { get; private set; }
    [field: SerializeField] public List<Decoration> Decorations { get; private set; }

    public event Action OnFishAdded;
    public event Action OnFishRemoved;
    public event Action OnDecoAdded;
    public event Action OnDecoRemoved;

    public void AddFish(Fish fish) {
        Fishes.Add(fish);
        OnFishAdded?.Invoke();
    }
    public void RemoveFish(Fish fish) {
        if (Fishes.Remove(fish)) {
            OnFishRemoved?.Invoke();
        }
    }

    public void AddDeco(Decoration deco) {
        Decorations.Add(deco);
        OnDecoAdded?.Invoke();
    }
    public void RemoveDeco(Decoration deco) {
        if (Decorations.Remove(deco)) {
            OnDecoRemoved?.Invoke();
        }
    }
}