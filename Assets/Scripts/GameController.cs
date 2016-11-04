using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class GameController : MonoBehaviour
	{
		public GameObject[] Hazards;
		public Vector3 SpawnValues;
		public GUIText ScoreText;
		public GUIText RestartText;
		public GUIText GameOverText;

		public int HazardCount;

		public float SpawnWait;
		public float StartWait;
		public float WaveWait;

		private int _score;

		private bool _gameOver;
		private bool _restart;

		// Use this for initialization
		private void Start ()
		{
			_gameOver = false;
			_restart = false;
			_score = 0;

			RestartText.text = "";
			GameOverText.text = "";

			UpdateScore();
			StartCoroutine(SpawnWaves());
		}

		private void Update()
		{
			if (!_restart)
			{
				return;
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}

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

				RestartText.text = "Press 'R' for Restart";
				_restart = true;
				break;
			}
		}

		public void AddScore(int newScoreValue)
		{
			_score += newScoreValue;
			UpdateScore();
		}

		public void GameOver()
		{
			GameOverText.text = "Game Over!";
			_gameOver = true;
		}

		private void UpdateScore()
		{
			ScoreText.text = "Score: " + _score;
		}
	}
}