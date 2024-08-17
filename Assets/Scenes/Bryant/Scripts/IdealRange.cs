using System;

[Serializable]
public struct IdealRange {
    public float MinOk;
    public float MinGood;
    public float MaxGood;
    public float MaxOk;

    public bool IsGood(float value) { return MinGood <= value && value <= MaxGood; }
    public bool IsOk(float value) { return MinOk <= value && value < MinGood || MaxGood < value && value <= MaxOk; }
    public bool IsBad(float value) { return MaxOk < value || value < MinOk; }
}