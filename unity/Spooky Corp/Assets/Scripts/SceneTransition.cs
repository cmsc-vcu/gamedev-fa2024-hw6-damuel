using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private string mainGameSceneName = "MainGame"; // Set this in the Unity Inspector

    public void LoadMainGame()
    {
        SceneManager.LoadScene(mainGameSceneName);
    }
}