using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections;

public class sceneManager : MonoBehaviour
{
    [SerializeField] private List<MiniGame> MiniGameScene = new List<MiniGame>();
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private MiniGame currentMiniGame;
    
    //right click on the component and click "Load All ScriptableObjects" to load all scriptable objects
    #if UNITY_EDITOR
        [ContextMenu("Load All ScriptableObjects")]
        void LoadAllInEditor()
        {
            MiniGameScene.Clear();
            string[] guids = AssetDatabase.FindAssets("t:MiniGame");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                MiniGame obj = AssetDatabase.LoadAssetAtPath<MiniGame>(path);
                if (obj != null)
                    MiniGameScene.Add(obj);
            }
        }
    #endif
    
    //random minigame from the minigame pool and increase down time
    public MiniGame randomMiniGame()
    {
        int rand = 0;
        do
        {
            rand = Random.Range(0, MiniGameScene.Count);
        } while (MiniGameScene[rand].currentDownTime < MiniGameScene[rand].coolDown);

        foreach (var m in MiniGameScene)
        {
            m.currentDownTime++;
        }

        MiniGameScene[rand].currentDownTime = 0;

        currentMiniGame = MiniGameScene[rand];
        return MiniGameScene[rand];
    }
    
    //change scene
    public void ChangeScene(string SceneName)
    {
        StartCoroutine(LoadLevel(SceneName));
    }
    //play animation before and after load scene
    IEnumerator LoadLevel(string SceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        transitionAnim.SetTrigger("Start");
    }

    //temp code to test scene changing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene(randomMiniGame().SceneName);
        }
    }

    //call to unload minigame scene and return to intermission scene
    public void EndMiniGame(bool areYouWinningSon)
    {
        ScoreSystem scoreCS = this.transform.parent.GetChild(3).gameObject.GetComponent<ScoreSystem>();
        
        SceneManager.UnloadSceneAsync(currentMiniGame.SceneName);

        if (areYouWinningSon)
        {
            scoreCS.Win();
        }
        else
        {
            scoreCS.Lose();
        }

        StartCoroutine(TriggerMiniGame());
    }

    IEnumerator TriggerMiniGame()
    {
        yield return new WaitForSeconds(3);
        ChangeScene(randomMiniGame().SceneName);
    }

}
