using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
    public string sceneName; // 
    public void NavigateToScene()
    {
        SceneManager.LoadScene(sceneName);
         Debug.Log("Navigating to scene: " + sceneName);
    }
}
