using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
   public static PausePanel Instance;
   public bool _isPaused;
   [SerializeField] private Button _pauseGameButton;
   [SerializeField] private Button _continueGameButton;
   [SerializeField] private Button _restartGameButton;
   [SerializeField] private Button _exitGameButton;
   [SerializeField] private GameObject _panel;
   private GameModel _gameModel;

   private void Awake()
   {
      Instance = this;
      _continueGameButton.onClick.AddListener(ContinueGame);
      _restartGameButton.onClick.AddListener(RestartGame);
      _exitGameButton.onClick.AddListener(ExitGame);
      _isPaused = false;
   }

   public void SetGameModel(GameModel gameModel)
   {
      _gameModel = gameModel;
   }

   public void PausedGame()
   {
      _isPaused = true;
      _panel.SetActive(true);
      Time.timeScale = 0;
   }
   private void ContinueGame()
   {
      _isPaused = false;
      Time.timeScale = 1;
      _gameModel.StartSimulation();
      _pauseGameButton.gameObject.SetActive(true);
      _panel.SetActive(false);
   }

   private void RestartGame()
   {
      _panel.SetActive(false);
      Time.timeScale = 1;
      _isPaused = false;
      _pauseGameButton.gameObject.SetActive(true);
      SceneManager.LoadScene("SampleScene");
   }

   private void ExitGame()
   {
      Application.Quit();
   }
}
