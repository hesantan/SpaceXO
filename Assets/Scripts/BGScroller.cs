﻿using UnityEngine;

namespace Assets.Scripts
{
	// ReSharper disable once InconsistentNaming
	public class BGScroller : MonoBehaviour
	{
		public float ScrollSpeed;
		public float TileSizeZ;

		private Vector3 _startPosition;

		// Use this for initialization
		private void Start ()
		{
			_startPosition = transform.position;
		}
	
		// Update is called once per frame
		private void Update ()
		{
			var newPosition = Mathf.Repeat(Time.time * ScrollSpeed, TileSizeZ);
			transform.position = _startPosition + Vector3.forward * newPosition;
		}

		
	}
}
