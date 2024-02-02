using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneTransitionData
{
    public string sceneName;
}

public class SceneExample : MonoBehaviour
{
    [SerializeField] private SceneTransitionData sceneTransitionData;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneTransitionData.sceneName);
    }
}
