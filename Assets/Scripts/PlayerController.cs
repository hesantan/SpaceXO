using System;

using UnityEngine;

namespace Assets.Scripts
{
	[Serializable]
	public class Boundary
	{
		public float xMin, xMax, zMin, zMax;
	}

	public class PlayerController : MonoBehaviour
	{
		public float Speed;
		public float Tilt;
		public float FireRate;
		public Boundary Boundary;

		public GameObject Shot;
		public Transform ShotSpawn;

		private Rigidbody _rigidbody;
		private float _nextFire;
		

		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			if (!Input.GetButton("Fire1") || !(Time.time > _nextFire)) return;

			_nextFire = Time.time + FireRate;
			Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);
		}

		private void FixedUpdate()
		{
			var moveHorizontal = Input.GetAxis("Horizontal");
			var moveVertical = Input.GetAxis("Vertical");

			var movement = new Vector3(moveHorizontal, 0f, moveVertical);
			_rigidbody.velocity = movement * Speed;

			_rigidbody.position = new Vector3(
				Mathf.Clamp(_rigidbody.position.x, Boundary.xMin, Boundary.xMax),
				0f,
				Mathf.Clamp(_rigidbody.position.z, Boundary.zMin, Boundary.zMax)
			);

			_rigidbody.rotation = Quaternion.Euler(0f, 0f, _rigidbody.velocity.x * Tilt);
		}

	}
}
