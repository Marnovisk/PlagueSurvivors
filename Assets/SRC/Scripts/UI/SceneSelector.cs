using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSelector : MonoBehaviour
{
   //public string sceneName;

    public void SceneSelections(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
