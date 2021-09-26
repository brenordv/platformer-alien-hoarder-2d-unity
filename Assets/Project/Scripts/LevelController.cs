using Cinemachine;
using Project.Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class LevelController : MonoBehaviour
    {
        public int lives = 3;
        public int score;
        public Transform spawnPoint;
        public CollectibleControl gems;
        public UiText textScore;
        public UiText textLives;

        private int _gameOverLevel;
        private int _nextLevel;
        private Player _player;
        private CinemachineVirtualCamera _virtualCamera;
        private InfoTextController _infoTextController;

        private void Awake()
        {
            _infoTextController = FindObjectOfType<InfoTextController>();
            RegisterForCollectibles();
            RegisterLinkToExitSign();
            RegisterForTrapTriggers();
            RegisterForEnemyKills();
            SetNextLevel();
            MakeCameraFollowPlayer();
        }

        private void Start()
        {
            UpdateRemainingLivesIndicator();
        }

        private void RegisterForTrapTriggers()
        {
            foreach (var trap in FindObjectsOfType<Trap>())
            {
                trap.OnTrigger += KillPlayer;
            }
        }

        private void RegisterForEnemyKills()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.OnEnemyKill += KillPlayer;
            }
        }

        private void KillPlayer()
        {
            if (lives == 0)
            {
                GameOver();
                return;
            }

            lives--;
            UpdateRemainingLivesIndicator();

            if (lives >= 1)
                _infoTextController.ShowInfo("Oh no!");
            else if (lives == 0)
                _infoTextController.ShowInfo("Careful! Last try!");

            var playerGO = _player.gameObject;
            playerGO.SetActive(false);
            playerGO.transform.position = spawnPoint.position;
            playerGO.SetActive(true);
        }

        private void MakeCameraFollowPlayer()
        {
            _player = FindObjectOfType<Player>();
            _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();

            if (_player == null || _virtualCamera == null)
            {
                Debug.LogWarning("Did not found a player or camera.");
                return;
            }

            _virtualCamera.Follow = _player.transform;
        }

        private void RegisterForCollectibles()
        {
            RegisterForGems();
        }

        private void RegisterForGems()
        {
            foreach (var gem in FindObjectsOfType<Gem>())
            {
                gems.total += 1;
                gem.OnCollect += GemCollected;
            }
        }

        private void RegisterLinkToExitSign()
        {
            var exitSign = FindObjectOfType<ExitSign>();
            if (exitSign == null)
            {
                Debug.LogWarning("NO EXIT SIGN DETECTED!! IS THIS A TEST LEVEL?");
                return;
            }

            exitSign.OnExit += NextLevel;
        }

        private void SetNextLevel()
        {
            var numScenes = SceneManager.sceneCountInBuildSettings;
            var thisSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            _gameOverLevel = numScenes - 1;

            _nextLevel =
                thisSceneBuildIndex == numScenes - 2 // Last scene, which is the "You Won scene" 
                    ? 0 // Then will return to the Menu 
                    : thisSceneBuildIndex + 1; // Otherwise, will jump to the next level.
        }

        private void GemCollected(int points)
        {
            score += points;
            gems.collected++;
            textScore.UpdateUi(score.ToPaddedString());
            if (!gems.CollectedAll()) return;
            _infoTextController.ShowInfo("Congrats! All gems collected!");
        }

        private void NextLevel()
        {
            SceneManager.LoadScene(_nextLevel);
        }

        private void UpdateRemainingLivesIndicator()
        {
            textLives.UpdateUi(lives.ToPaddedString(2));
        }

        private void GameOver()
        {
            SceneManager.LoadScene(_gameOverLevel);
        }
    }
}