using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{

    public void LoadNewScene(string p_sceneName)
    {
        SceneManager.LoadScene(p_sceneName);
    }
}
