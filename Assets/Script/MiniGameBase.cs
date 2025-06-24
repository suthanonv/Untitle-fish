using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameBase : MonoBehaviour
{
    protected bool gameEnded = false;

    protected void EndMiniGame(bool win)
    {
        if (gameEnded) return;
        gameEnded = true;

        Debug.Log(win ? "Mini-game: WIN" : "Mini-game: LOSE");

        if (win && GameManager.instance != null)
        {
            // GameManager.instance.AddScore(1);
        }

        StartCoroutine(UnloadAfterDelay(1f));
    }

    protected virtual IEnumerator UnloadAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
