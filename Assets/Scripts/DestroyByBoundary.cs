using UnityEngine;

namespace Assets.Scripts
{
	public class DestroyByBoundary : MonoBehaviour {

		private void OnTriggerExit(Component other)
		{
			Destroy(other.gameObject);
		}
	}
}
