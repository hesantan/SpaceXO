using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
	public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

		private int _pointerId;
		private bool _touched;
		private bool _canFire;

		private void Awake()
		{
			_touched = false;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (_touched)
			{
				return;
			}

			_touched = _canFire = true;
			_pointerId = eventData.pointerId;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.pointerId != _pointerId)
			{
				return;
			}

			_touched = _canFire = false;
		}

		public bool CanFire()
		{
			return _canFire;
		}
	}
}
