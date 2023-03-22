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
		private Vector2 velocity = default;
		private float bearing = default;
		private float angularVelocity = default;

		private void Update()
		{
			if (thrusterForward)
			{
				thrusterForward.SetActive(Input.GetKey(KeyCode.W));
			}

			ProcessInput(new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
			
			transform.position = new Vector3(position.x, position.y, 0);
			transform.rotation = Quaternion.Euler(0, 0, bearing);

			var r = WrapCoordinates(transform.position);
			var c = Camera.main.ScreenToWorldPoint(r);
			Debug.Log(c);
			transform.position = new Vector3(c.x, c.y, 0);
		}

		public void ProcessInput(Vector2 input)
		{
			if (Mathf.Abs(input.x) > 0)
			{
				angularVelocity += shipConfig.AngularThrust * Time.deltaTime * input.x;
			}

			if (Mathf.Abs(input.y) > 0)
			{
				float bearingRad = bearing * Mathf.PI / 180;
				velocity.x += shipConfig.ForwardThrust * Mathf.Cos(bearingRad) * Time.deltaTime;
				velocity.y += shipConfig.ForwardThrust * Mathf.Sin(bearingRad) * Time.deltaTime;
			}

			position += velocity * Time.deltaTime;
			bearing += angularVelocity * Time.deltaTime;
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

			//Debug.Log(screenPos);

			return output;
		}
	}
}
