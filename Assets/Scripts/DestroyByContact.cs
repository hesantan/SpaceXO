using UnityEngine;

namespace Assets.Scripts
{
	public class DestroyByContact : MonoBehaviour
	{
		public GameObject Explosion;
		public GameObject PlayerExplosion;

		public int ScoreValue;

		private GameController _gameController;

		private void Start()
		{
			var gameControllerObject = GameObject.FindWithTag("GameController");

			if (gameControllerObject != null)
			{
				_gameController = gameControllerObject.GetComponent<GameController>();
			}

			if (_gameController == null)
			{
				Debug.Log("Cannot find 'GameController' script");
			}
		}

		private void OnTriggerEnter(Component other)
		{
			switch (other.tag)
			{
				case "Boundary":
				case "Enemy":
					return;
				case "Player":

					Instantiate(PlayerExplosion, other.transform.position, other.transform.rotation);

					_gameController.GameOver();

					break;
			}

			if (Explosion != null)
			{
				Instantiate(Explosion, transform.position, transform.rotation);
			}

			_gameController.AddScore(ScoreValue);

			Destroy(gameObject);
			Destroy(other.gameObject);
		}
	}
}
