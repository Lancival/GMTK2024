using System;

[Serializable]
public class IdealRange {
    public float MinOk;     // low threshold Ok
    public float MinGood;   // low threshold Good
    public float MaxGood;   // high threshold Good
    public float MaxOk;     // high threshold Ok

    public bool IsGood(float value) { return MinGood <= value && value <= MaxGood; }
    public bool IsOk(float value) { return MinOk <= value && value < MinGood || MaxGood < value && value <= MaxOk; }
    public bool IsBad(float value) { return MaxOk < value || value < MinOk; }
}