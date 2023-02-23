using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
   [SerializeField] private Button _pauseGameButton;
   [SerializeField] private Button _continueGameButton;
   [SerializeField] private Button _restartGameButton;
   [SerializeField] private Button _exitGameButton;

   private void Awake()
   {
      _continueGameButton.onClick.AddListener(ContinueGame);
      _restartGameButton.onClick.AddListener(RestartGame);
      _exitGameButton.onClick.AddListener(ExitGame);
   }

   private void ContinueGame()
   {
      _pauseGameButton.gameObject.SetActive(true);
      Time.timeScale = 1;
      gameObject.SetActive(false);
   }

   private void RestartGame()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene("SampleScene");
   }

   private void ExitGame()
   {
      Application.Quit();
   }
}
