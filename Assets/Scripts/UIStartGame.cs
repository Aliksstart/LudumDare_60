using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartGame : MonoBehaviour
{
    public void StartScene() {
        SceneManager.LoadScene("MainScene");
    }
}
