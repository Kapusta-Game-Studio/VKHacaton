using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class PauseAndSceneController : MonoBehaviour
    {
        public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        public void ExitToMenu() => SceneManager.LoadScene("MainMenu");
        public void PutGameOnPause(bool on) => Time.timeScale = on ? 0 : 1;
    }
}