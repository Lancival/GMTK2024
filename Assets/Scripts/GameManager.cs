using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public int currentStage { get; private set; }

    [field: SerializeField] public Stage[] stages { get; private set;}

    private ObjectiveManager objectiveManager;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        objectiveManager = (ObjectiveManager)FindFirstObjectByType(typeof(ObjectiveManager));
        UIManager.Instance.completeButton.gameObject.SetActive(false);
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        foreach (var stage in stages)
        {
            if (stage.name == scene.name)
            {
                currentStage = stage.order;
                return;
            }
        }
        
        Debug.LogError("Could not find scene name in GameManager!");
    }

    void Update()
    {
        if (objectiveManager != null)
        {
            if (objectiveManager.AllRequirementsComplete())
            {
                // I don't care if this is inefficient lol
                UIManager.Instance.completeButton.gameObject.SetActive(true);
            }
        }
    }
}
