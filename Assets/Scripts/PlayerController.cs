using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float speed = default;
		[SerializeField] private float degrees = default;
		[SerializeField] private SpriteRenderer flame = default;

		private void Update()
		{
			if (flame)
			{
				flame.gameObject.SetActive(Input.GetKey(KeyCode.W));
			}

			Movement();
			Rotation();
			Wrapping();
		}

		private void Movement()
		{
			if (Input.GetKey(KeyCode.W))
			{
				this.transform.position += speed * Time.deltaTime * transform.up;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				this.transform.position += speed * Time.deltaTime * (-transform.up);
			}
		}

		private void Rotation()
		{
			if (Input.GetKey(KeyCode.A))
			{
				this.transform.eulerAngles += degrees * Time.deltaTime * Vector3.forward;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				this.transform.eulerAngles += degrees * Time.deltaTime * Vector3.back;
			}
		}

		private void Wrapping()
		{
			var viewportPosition = Camera.main.WorldToViewportPoint(this.transform.position);
			var newPosition = transform.position;

			if(viewportPosition.x < 0 || viewportPosition.x > 1)
			{
				newPosition.x *= -1;
			}
			if (viewportPosition.y < 0 || viewportPosition.y > 1)
			{
				newPosition.y *= -1;
			}

			transform.position = newPosition;
		}
	}
}
