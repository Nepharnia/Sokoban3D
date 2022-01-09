using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool readyForInput;
    private Player player;
    public LevelBuilder levelBuilder;
    public GameObject nextButton;

    void Start()
    {
        levelBuilder.Build();
        player = FindObjectOfType<Player>();
        nextButton.SetActive(false);
    }

    void Update() {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        input.Normalize();

        if (input.sqrMagnitude > 0.5) {
            if (readyForInput) {
                readyForInput = false;
                player.Move(input);
                nextButton.SetActive(IsLevelComplete());

            }
        }
        else {
            readyForInput = true;
        }
    }

    public void NextLevel() {
        nextButton.SetActive(false);
        levelBuilder.NextLevel();
        StartCoroutine(ResetSceneAsync());
    }

    public void ResetScene() {
        StartCoroutine(ResetSceneAsync());
    }

    
    bool IsLevelComplete() {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (var box in boxes) {
            if (!box.onGoal) 
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator ResetSceneAsync() {
        if (SceneManager.sceneCount > 1) {
            AsyncOperation async = SceneManager.UnloadSceneAsync("LevelScene");
            while (!async.isDone) {
                yield return null;
                //Debug.Log("Unloading");
            }
            //Debug.Log("Unloading done.");
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
            //Debug.Log("Loading");
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        levelBuilder.Build();
        player = FindObjectOfType<Player>();
        //Debug.Log("Level loaded.");
    }  
}
