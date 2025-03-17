using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] string levelToLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LevelLoad()
    {
         SceneManager.LoadScene(levelToLoad);
    }
}
