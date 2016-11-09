using System.Collections;

using UnityEngine;

namespace Assets.Scripts
{
	public class EvasiveManeuver : MonoBehaviour
	{
		public float Dodge;
		public float Smoothing;
		public float Tilt;

		public Vector2 StartWait;
		public Vector2 ManeuverTime;
		public Vector2 ManeuverWait;

		public Boundary Boundary;

		private float _targetManeuver;
		private float _currentSpeed;
		private const string PlayerTag = "Player";

		private Rigidbody _rigidbody;
		private Transform _playerTransform;

		// Use this for initialization
		private void Start ()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_currentSpeed = _rigidbody.velocity.z;

			var playerGameObject = GameObject.FindGameObjectWithTag(PlayerTag);

			if (_playerTransform != null)
			{
				_playerTransform = playerGameObject.transform;
			}

			StartCoroutine(Evade());
		}
	
		private void FixedUpdate ()
		{
			var newManeuver = Mathf.MoveTowards(_rigidbody.velocity.x, _targetManeuver, Time.deltaTime * Smoothing);

			_rigidbody.velocity = new Vector3(newManeuver, 0f, _currentSpeed);
			_rigidbody.position = new Vector3(
				Mathf.Clamp(_rigidbody.position.x, Boundary.XMin, Boundary.XMax),
				0f,
				Mathf.Clamp(_rigidbody.position.z, Boundary.ZMin, Boundary.ZMax)	
			);
			_rigidbody.rotation = Quaternion.Euler(0f, 0f, _rigidbody.velocity.x * -Tilt);
		}

		private IEnumerator Evade()
		{
			yield return new WaitForSeconds(Random.Range(StartWait.x, StartWait.y));

			while (true)
			{
				_targetManeuver = _playerTransform != null ? _playerTransform.position.x : Random.Range(1, Dodge) * -Mathf.Sign(transform.position.x);

				yield return new WaitForSeconds(Random.Range(ManeuverTime.x, ManeuverTime.y));

				_targetManeuver = 0;

				yield return new WaitForSeconds(Random.Range(ManeuverWait.x, ManeuverWait.y));
			}
		}
	}
}