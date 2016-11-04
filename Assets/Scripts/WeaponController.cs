using UnityEngine;

namespace Assets.Scripts
{
	public class WeaponController : MonoBehaviour
	{
		public GameObject Shot;
		public Transform ShotSpawn;

		public float FireRate;
		public float Delay;

		private AudioSource _audioSource;

		// Use this for initialization
		private void Start ()
		{
			_audioSource = GetComponent<AudioSource>();
			InvokeRepeating("Fire", Delay, FireRate);
		}

		private void Fire()
		{
			Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);
			_audioSource.Play();
		}
	}
}
