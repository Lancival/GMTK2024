using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public class StatsDatabase : MonoBehaviour
{
    [SerializeField] private TextAsset m_StatsDatabaseText;

    [System.Serializable]
    public class StatItem
    {
        public string stage;
        public string assetType;
        public string name;
        public string space;
        public string waterQuality;
        public string diversity;
        public string size;
        public string notes;
    }

    private List<StatItem> m_stats = new();

    public List<StatItem> stats { get { return m_stats; } }

    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] dataLines = m_StatsDatabaseText.text.Split('\n');

        for (int i = 1; i < dataLines.Length; i++)
        {
            var data = dataLines[i].Split(',');
            Assert.AreEqual(data.Length, 8);
            var statItem = new StatItem
            {
                stage = data[0],
                assetType = data[1],
                name = data[2],
                space = data[3],
                waterQuality = data[4],
                diversity = data[5],
                size = data[6],
                notes = data[7]
            };
            m_stats.Add(statItem);
        }
    }
}
