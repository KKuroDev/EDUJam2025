using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        gameObject.SetActive(false);

        if (sceneName == "Main_Menu")
            AudioManager.instance.StopMusic();
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}