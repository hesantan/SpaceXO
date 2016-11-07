using System;

using UnityEngine;

namespace Assets.Scripts
{
	[Serializable]
	public class Boundary
	{
		public float XMin, XMax, ZMin, ZMax;
	}

	public class PlayerController : MonoBehaviour
	{
		public float Speed;
		public float Tilt;
		public float FireRate;

		public Boundary Boundary;
		public GameObject Shot;
		public Transform ShotSpawn;

		private float _nextFire;

		private Rigidbody _rigidbody;
		private AudioSource _audioSource;
		private Quaternion _calibrationQuaternion;

		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_audioSource = GetComponent<AudioSource>();

			CalibrateAccellerometer();
		}

		private void Update()
		{
			if (!Input.GetButton("Fire1") && !Input.GetKeyDown(KeyCode.Space) || !(Time.time > _nextFire))
			{
				return;
			}

			_nextFire = Time.time + FireRate;
			Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);
			_audioSource.Play();
		}

		private void FixedUpdate()
		{
			var moveHorizontal = Input.GetAxis("Horizontal");
			var moveVertical = Input.GetAxis("Vertical");

			var movement = new Vector3(moveHorizontal, 0f, moveVertical);
			_rigidbody.velocity = movement * Speed;

			_rigidbody.position = new Vector3(
				Mathf.Clamp(_rigidbody.position.x, Boundary.XMin, Boundary.XMax),
				0f,
				Mathf.Clamp(_rigidbody.position.z, Boundary.ZMin, Boundary.ZMax)
			);

			_rigidbody.rotation = Quaternion.Euler(0f, 0f, _rigidbody.velocity.x * Tilt);
		}

		private void CalibrateAccellerometer()
		{
			var accelerationSnapshot = Input.acceleration;
			var rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), accelerationSnapshot);
			_calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
		}

		private Vector3 FixAcceleration(Vector3 acceleration)
		{
			var fixedAccelation = _calibrationQuaternion * acceleration;
			return fixedAccelation;
		}
	}
}
