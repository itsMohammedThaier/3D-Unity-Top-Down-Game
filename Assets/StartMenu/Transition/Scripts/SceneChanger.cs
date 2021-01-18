using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    public void ChangeTheScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
