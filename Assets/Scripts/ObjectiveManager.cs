using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour {
    [field: SerializeField] public Objective Objective;

    // Fish Requirements (Average)
    [field: SerializeField] public IdealRange SpaceRange { get; private set; }
    [field: SerializeField] public IdealRange WaterQualityRange { get; private set; }

    [SerializeField] Tank tank;

    //#region Objectives

    //#endregion

    #region Requirements

    public bool AllRequirementsComplete() {
        return CurrentFishCount() >= Objective.FishCount && CurrentDecorationCount() >= Objective.DecorationCount;
        // SpaceRange.IsGood(CalculateSpaceLevel()) &&
        //       WaterQualityRange.IsGood(CalculateWaterQualityLevel())
    }


    void Start()
    {
        Debug.Log(string.Format("Goal Fish Count: {0} Goal Decoration Count: {1}", Objective.FishCount, Objective.DecorationCount));
    }

    /// <summary>
    /// Calculate average requirements ranges of all fish in tank
    /// </summary>
    void UpdateRequirements()
    {
        SpaceRange.MinOk = tank.Fishes.Average(x => x.SpaceRange.MinOk);
        SpaceRange.MinGood = tank.Fishes.Average(x => x.SpaceRange.MinGood);
        SpaceRange.MaxGood = tank.Fishes.Average(x => x.SpaceRange.MaxGood);
        SpaceRange.MaxOk = tank.Fishes.Average(x => x.SpaceRange.MaxOk);

        WaterQualityRange.MinOk = tank.Fishes.Average(x => x.CleanlinessRange.MinOk);
        WaterQualityRange.MinGood = tank.Fishes.Average(x => x.CleanlinessRange.MinGood);
        WaterQualityRange.MaxGood = tank.Fishes.Average(x => x.CleanlinessRange.MaxGood);
        WaterQualityRange.MaxOk = tank.Fishes.Average(x => x.CleanlinessRange.MaxOk);
    }

    public float CalculateSpaceLevel() { return tank.Fishes.Sum(x => x.Space) + tank.Decorations.Sum(x => x.Space); }
    public float CalculateWaterQualityLevel() { return tank.Fishes.Sum(x => x.Cleanliness) + tank.Decorations.Sum(x => x.Cleanliness); }

    public int CurrentFishCount()
    {
        return tank.Fishes.Count;
    }

    public int CurrentDecorationCount()
    {
        return tank.Decorations.Count;
    }

    #endregion
}

[System.Serializable]
public struct Objective
{
    public int FishCount;
    public string FishObjectiveText;
    public int DecorationCount;
    public string DecorationObjectiveText;
}
