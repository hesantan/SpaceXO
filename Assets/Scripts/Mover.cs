using UnityEngine;

namespace Assets.Scripts
{
	public class Mover : MonoBehaviour
	{
		public float Speed;

		private Rigidbody _rigidbody;

		// Use this for initialization
		private void Start ()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_rigidbody.velocity = transform.forward * Speed;
		}

	}
}
