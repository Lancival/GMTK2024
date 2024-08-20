using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour {
    [field: SerializeField] public List<Objective> Objectives { get; private set; }
    
    // Fish Requirements (Average)
    [field: SerializeField] public IdealRange SpaceRange { get; private set; }
    [field: SerializeField] public IdealRange CleanlinessRange { get; private set; }

    [SerializeField] Tank tank;

    #region Objectives

    public bool AllObjectivesComplete() { return Objectives.All(o => o.IsComplete()); }

    public void AddCountObjective(Action OnValueIncrement, Action OnValueDecrement, int target, int current) {
        CountObjective o = new CountObjective(target, current);
        OnValueIncrement += o.Increment;
        OnValueDecrement += o.Decrement;
        Objectives.Add(o);
    }

    public void AddAllFishObjective() {
        AllFishObjective o = new AllFishObjective();
        Objectives.Add(o);
    }

    #endregion

    #region Requirements

    public bool AllRequirementsComplete() {
        return SpaceRange.IsGood(CalculateSpaceLevel()) &&
               CleanlinessRange.IsGood(CalculateCleanlinessLevel());
    }

    /// <summary>
    /// Calculate average requirements ranges of all fish in tank
    /// </summary>
    void UpdateRequirements() {
        SpaceRange.MinOk = tank.Fishes.Average(x => x.SpaceRange.MinOk);
        SpaceRange.MinGood = tank.Fishes.Average(x => x.SpaceRange.MinGood);
        SpaceRange.MaxGood = tank.Fishes.Average(x => x.SpaceRange.MaxGood);
        SpaceRange.MaxOk = tank.Fishes.Average(x => x.SpaceRange.MaxOk);
        
        CleanlinessRange.MinOk = tank.Fishes.Average(x => x.CleanlinessRange.MinOk);
        CleanlinessRange.MinGood = tank.Fishes.Average(x => x.CleanlinessRange.MinGood);
        CleanlinessRange.MaxGood = tank.Fishes.Average(x => x.CleanlinessRange.MaxGood);
        CleanlinessRange.MaxOk = tank.Fishes.Average(x => x.CleanlinessRange.MaxOk);
    }

    float CalculateSpaceLevel() { return tank.Fishes.Sum(x => x.Space) + tank.Decorations.Sum(x => x.Space); }
    float CalculateCleanlinessLevel() { return tank.Fishes.Sum(x => x.Cleanliness) + tank.Decorations.Sum(x => x.Cleanliness); }
    int CalculateVarietyLevel() { return tank.Decorations.Select(x => x.Name).Distinct().Count(); }

    #endregion
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
        // TODO: waiting to see how we store fish collection data
        return false;
    }
}
