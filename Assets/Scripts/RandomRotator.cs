using UnityEngine;

namespace Assets.Scripts
{
	public class RandomRotator : MonoBehaviour
	{
		public float Tumble;

		private Rigidbody _rigidbody;

		private void Awake()
		{
			
		}

		private void Start()
		{
			_rigidbody.angularVelocity = Random.insideUnitSphere * Tumble;
		}
	}
}
