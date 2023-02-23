using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
   [SerializeField] private Button _pauseGameButton;
   [SerializeField] private Button _continueGameButton;
   [SerializeField] private Button _restartGameButton;
   [SerializeField] private Button _exitGameButton;
   private GameModel _gameModel;

   private void Awake()
   {
      _continueGameButton.onClick.AddListener(ContinueGame);
      _restartGameButton.onClick.AddListener(RestartGame);
      _exitGameButton.onClick.AddListener(ExitGame);
   }

   public void SetGameModel(GameModel gameModel)
   {
      _gameModel = gameModel;
   }

   private void ContinueGame()
   {
      Time.timeScale = 1;
      _gameModel.StartSimulation();
      _pauseGameButton.gameObject.SetActive(true);
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
