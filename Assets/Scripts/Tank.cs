using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tank : MonoBehaviour {
    [field: SerializeField] public List<Fish> Fishes { get; private set; }
    [field: SerializeField] public List<Decoration> Decorations { get; private set; }

    [field: SerializeField] public int Capacity { get; private set; }
    public float SpaceUsed => Fishes.Sum(x => x.Space) + Decorations.Sum(x => x.Space);

    public event Action OnFishAdded;
    public event Action OnFishRemoved;
    public event Action OnDecoAdded;
    public event Action OnDecoRemoved;

    public bool Add(GameObject o) {
        if (o.TryGetComponent(out Fish fish)) {
            if (SpaceUsed + fish.Space > Capacity) {
                return false;
            }
        
            Fishes.Add(fish);
            OnFishAdded?.Invoke();
            return true;
        } else if (o.TryGetComponent(out Decoration deco)) {
            if (SpaceUsed + deco.Space > Capacity) {
                return false;
            }
        
            Decorations.Add(deco);
            OnDecoAdded?.Invoke();
            return true;
        } else {
            Debug.LogError("Can only add Fish or Decoration to Tank.");
            return false;
        }
    }
    
    public void Remove(GameObject o) {
        if (o.TryGetComponent(out Fish fish)) {
            if (Fishes.Remove(fish)) {
                OnFishRemoved?.Invoke();
            }
        } else if (o.TryGetComponent(out Decoration deco)) {
            if (Decorations.Remove(deco)) {
                OnDecoRemoved?.Invoke();
            }
        } else {
            Debug.LogError("Can only remove Fish or Decoration from Tank.");
        }
    }
}