using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        private StickMan _stickMan;
        [SerializeField]
        private GameObject _gameOverScreen;
        [SerializeField]
        private Button _restartButton;


        private void Start()
        {
            _gameOverScreen.SetActive(false);
            _stickMan.EndGameCollision += StickManOnEndGameCollision;
            _restartButton.onClick.AddListener(RestartGame);
        }


        private void StickManOnEndGameCollision()
        {
            _gameOverScreen.SetActive(true);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}