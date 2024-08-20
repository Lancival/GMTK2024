using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class StatsDatabase : MonoBehaviour {
    [SerializeField] private TextAsset m_StatsDatabaseText;

    const string spritesPath = "Sprites/";

    [System.Serializable]
    public class StatItem {
        public int stage;
        public string assetType;
        public string name;
        public Sprite sprite;
        public string space;
        public string waterQuality;
        public string diversity;
        public string size;
        public string flavorText;
    }
    
    private static List<StatItem> m_items;

    public static List<StatItem> Items { get { return m_items; } }

    List<Sprite> sprites;

    void Awake() {
        m_items = new();
        sprites = LoadSprites();
        ReadCSV();
    }

    void ReadCSV() {
        string[] dataLines = m_StatsDatabaseText.text.Split('\n');

        for (int i = 1; i < dataLines.Length; i++) {
            var data = Regex.Split(dataLines[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            Assert.AreEqual(data.Length, 8);
            var statItem = new StatItem {
                assetType = data[1],
                name = data[2],
                space = data[3],
                waterQuality = data[4],
                diversity = data[5],
                size = data[6],
                flavorText = data[7]
            };

            switch (data[0]) {
                case "1 - Child":
                    statItem.stage = 1;
                    break;
                case "2 - Teenager":
                    statItem.stage = 2;
                    break;
                case "3 - Adult":
                    statItem.stage = 3;
                    break;
                case "4 - Senior":
                    statItem.stage = 4;
                    break;
            }

            foreach (Sprite sprite in sprites) {
                if (sprite.name == statItem.name) {
                    statItem.sprite = sprite;
                    break;
                }
            }

            Items.Add(statItem);
        }
    }

    List<Sprite> LoadSprites() {
        Sprite[] sArr = Resources.LoadAll<Sprite>(spritesPath);

        if (sArr == null || sArr.Length == 0) {
            Debug.LogError($"No sprites found at path '{spritesPath}'.");
            return null;
        }

        return new List<Sprite>(sArr);
    }
}