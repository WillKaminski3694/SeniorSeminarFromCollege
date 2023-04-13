using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int nextSceneToLoad;
    public int previousSceneToLoad;
    public int countedScenes;
    public int currentScene;

    public string currentSceneName;

    public string[] allScenes;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        GetCorrectScene();
    }

    public void Update()
    {
        GetCorrectScene();
        DoManualRespawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            SceneManager.LoadScene(nextSceneToLoad);
        }
    }

    public void GetCorrectScene()
    {
        countedScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        allScenes = new string[countedScenes];

        for(int i = 0; i < countedScenes; i++)
        {
            allScenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            if(currentScene == i)
            {
                currentSceneName = allScenes[i];
            }
        }

        switch (currentSceneName)
        {
            case "TutorialWorld":
                nextSceneToLoad = currentScene + 1;
                break;
            case "HubWorld": //Will need an if statement that checks to see if the exploration worlds boss has been beaten.
                nextSceneToLoad = currentScene + 2;
                previousSceneToLoad = currentScene - 1;
                break;
            case "HubWorldComingFromExplorationWorld":
                nextSceneToLoad = currentScene + 1;
                previousSceneToLoad = currentScene -2;
                break;
            case "ExplorationWorld":
                nextSceneToLoad = currentScene + 4;
                previousSceneToLoad = currentScene - 1;
                break;
            case "ExplorationWorldWithoutBoss":
                nextSceneToLoad = currentScene + 3;
                previousSceneToLoad = currentScene - 2;
                break;
            case "ExplorationWorldRespawn": //Will need an if statement that checks to see if the castle worlds boss has been beaten.
                nextSceneToLoad = currentScene + 2;
                previousSceneToLoad = currentScene = 3;
                break;
            case "ExplorationWorldComingFromCastleWorld":
                nextSceneToLoad = currentScene + 1;
                previousSceneToLoad = currentScene - 4;
                break;
            case "CastleWorld":
                nextSceneToLoad = currentScene + 3;
                previousSceneToLoad = currentScene - 1;
                break;
            case "CastleWorldWithoutBoss":
                nextSceneToLoad = currentScene + 2;
                previousSceneToLoad = currentScene - 2;
                break;
            case "CastleWorldComingFromInteriorCastleWorld":
                nextSceneToLoad = currentScene + 1;
                previousSceneToLoad = currentScene - 3;
                Debug.Log("Test");
                break;
            case "InteriorCastleWorld":
                nextSceneToLoad = currentScene + 3;
                previousSceneToLoad = currentScene - 1;
                break;
            case "InteriorCastleWorldRespawn":
                nextSceneToLoad = currentScene + 2;
                previousSceneToLoad = currentScene - 2;
                break;
            case "InteriorCastleWorldComimgFromFinalWorld":
                nextSceneToLoad = currentScene + 1;
                previousSceneToLoad = currentScene - 3;
                break;
            case "FinalWorld":
                nextSceneToLoad = currentScene + 1;
                previousSceneToLoad = currentScene - 1;
                break;
        }
    }

    public void DoManualRespawn()
    {
        if (Input.GetButtonDown("Respawn"))
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
