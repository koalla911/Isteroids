using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class ShipDataProvider : MonoBehaviour
	{
		[SerializeField] private ShipConfigData shipConfig = default;
		[SerializeField] private GameObject thrusterForward = default;
		[SerializeField] private GameObject thrusterBack = default;
		private Vector2 position = default;
		[SerializeField] private Vector2 velocity = default;
		private float bearing = default;
		private float angularVelocity = default;

		private void Update()
		{
			if (thrusterForward)
			{
				thrusterForward.SetActive(Input.GetKey(KeyCode.W));
			}
			
			if (thrusterBack)
			{
				thrusterBack.SetActive(velocity.x >= 0.5f || velocity.y >= 0.5f);
			}

			ProcessInput(new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
			Move();

			transform.position = new Vector3(position.x, position.y, 0);
			transform.rotation = Quaternion.Euler(0, 0, bearing);
		}

		public void ProcessInput(Vector2 input)
		{
			if (Mathf.Abs(input.x) > 0)
			{
				angularVelocity += shipConfig.AngularThrust * Time.deltaTime * input.x;
			}
			else
			{
				angularVelocity -= shipConfig.AngularThrust * 2 * Time.deltaTime * Mathf.Sign(angularVelocity);
			}

			if (Mathf.Abs(input.y) > 0)
			{
				float bearingRad = bearing * Mathf.PI / 180;
				velocity.x += shipConfig.ForwardThrust * Mathf.Cos(bearingRad) * Time.deltaTime;
				velocity.y += shipConfig.ForwardThrust * Mathf.Sin(bearingRad) * Time.deltaTime;
			}
			else
			{
				if (velocity.x != 0f)
				{
					velocity.x -= velocity.x * Time.deltaTime;
				}

				if (velocity.y != 0f)
				{
					velocity.y -= velocity.y * Time.deltaTime;
				}
			}
		}

		public void Move()
		{
			position += velocity * Time.deltaTime;
			bearing += angularVelocity * Time.deltaTime;

			position = WrapCoordinates(position);
		}

		private Vector2 WrapCoordinates(Vector2 position)
		{
			Vector2 output = new();
			var screen = new Vector2(Screen.width, Screen.height);
			var screenPos = Camera.main.WorldToScreenPoint(position);

			output.x = screenPos.x;
			output.y = screenPos.y;

			if (screenPos.x < 0)
			{
				output.x = screenPos.x + Screen.width;
			}
			else if (screenPos.x >= Screen.width)
			{
				output.x = screenPos.x - Screen.width;
			}
			else if (screenPos.y < 0)
			{
				output.y = screenPos.y + Screen.height;
			}
			else if (screenPos.y >= Screen.height)
			{
				output.y = screenPos.y - Screen.height;
			}

			return Camera.main.ScreenToWorldPoint(output);
		}
	}
}
