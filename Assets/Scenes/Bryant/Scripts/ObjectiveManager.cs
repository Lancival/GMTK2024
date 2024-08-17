using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bryant {
public class ObjectiveManager : MonoBehaviour {
    [field: SerializeField] public List<Objective> Objectives { get; private set; }

    public bool AllObjectivesComplete() { return Objectives.All(o => o.IsComplete()); }

    void AddCountObjective(Action OnValueIncrement, Action OnValueDecrement, int target, int current) {
        CountObjective o = new CountObjective(target, current);
        OnValueIncrement += o.Increment;
        OnValueDecrement += o.Decrement;
        Objectives.Add(o);
    }

    void AddAllFishObjective() {
        AllFishObjective o = new AllFishObjective();
        Objectives.Add(o);
    }
}

public abstract class Objective {
    public abstract bool IsComplete();
}
public class CountObjective : Objective {
    public int Target;
    public int Current;

    public CountObjective(int target, int current) {
        Target = target;
        Current = current;
    }

    public override bool IsComplete() { return Current >= Target; }
    public void Increment() { Current++; }
    public void Decrement() {
        Current--;
        if (Current < 0) { Current = 0; }
    }
}
public class AllFishObjective : Objective {
    public override bool IsComplete() {
        // TODO
        return false;
    }
}
}