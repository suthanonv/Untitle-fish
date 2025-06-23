using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections;

public class sceneManager : MonoBehaviour
{
    [SerializeField] private List<MiniGame> MiniGameScene = new List<MiniGame>();
    [SerializeField] private Animator transitionAnim;
    
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
        
        return MiniGameScene[rand];
    }
    
    public void ChangeScene(string SceneName)
    {
        StartCoroutine(LoadLevel(SceneName));
    }
    IEnumerator LoadLevel(string SceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        transitionAnim.SetTrigger("Start");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene(randomMiniGame().SceneName);
        }
    }

}
