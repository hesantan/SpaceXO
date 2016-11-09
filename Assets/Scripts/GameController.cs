using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class GameController : MonoBehaviour
	{
		public GameObject[] Hazards;
		public Vector3 SpawnValues;
		//public Text RestartText;
		public Text ScoreText;
		public Text GameOverText;
		public GameObject RestartButton;

		public int HazardCount;

		public float SpawnWait;
		public float StartWait;
		public float WaveWait;

		private int _score;

		private bool _gameOver;
		//private bool _restart;

		// Use this for initialization
		private void Start ()
		{
			_gameOver = false;
			//_restart = false;
			_score = 0;

			//RestartText.text = "";
			GameOverText.text = "";
			RestartButton.SetActive(false);

			UpdateScore();
			StartCoroutine(SpawnWaves());
		}

		//private void Update()
		//{
		//	if (!_restart)
		//	{
		//		return;
		//	}

		//	if (Input.GetKeyDown(KeyCode.R))
		//	{
		//		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		//	}
		//}

		private IEnumerator SpawnWaves()
		{
			yield return new WaitForSeconds(StartWait);

			while (true)
			{
				for (var i = 0; i < HazardCount; i++)
				{
					var hazard = Hazards[Random.Range(0, Hazards.Length)];

					var spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
					var spawnRotation = Quaternion.identity;

					Instantiate(hazard, spawnPosition, spawnRotation);

					yield return new WaitForSeconds(SpawnWait);
				}

				yield return new WaitForSeconds(WaveWait);

				if (!_gameOver)
				{
					continue;
				}

				//RestartText.text = "Press 'R' for Restart";
				RestartButton.SetActive(true);
				//_restart = true;
				break;
			}
		}

		public void AddScore(int newScoreValue)
		{
			_score += newScoreValue;
			UpdateScore();
		}

		private void UpdateScore()
		{
			ScoreText.text = "Score: " + _score;
		}

		public void GameOver()
		{
			GameOverText.text = "Game Over!";
			_gameOver = true;
		}

		public void RestartGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}