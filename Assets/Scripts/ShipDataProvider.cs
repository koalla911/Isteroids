using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class ShipDataProvider : MonoBehaviour
	{
		[SerializeField] private ShipConfigData shipConfig = default;
		[SerializeField] private SpriteRenderer flame = default;
		private Vector2 position = default;
		private Vector2 velocity = default;
		private float bearing = default;
		private float angularVelocity = default;

		private void Update()
		{
			if (flame)
			{
				flame.gameObject.SetActive(Input.GetKey(KeyCode.W));
			}

			if (Input.GetKey(KeyCode.A))
			{
				angularVelocity += shipConfig.AngularThrust * Time.deltaTime;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				angularVelocity -= shipConfig.AngularThrust * Time.deltaTime;

			}
			else
			{
				angularVelocity *= 0.99f;
			}

			if (Input.GetKey(KeyCode.W))
			{
				float bearingRad = bearing * Mathf.PI / 180;
				velocity.x += shipConfig.ForwardThrust * Mathf.Sin(bearingRad) * Time.deltaTime;
				velocity.y += shipConfig.ForwardThrust * Mathf.Cos(bearingRad) * Time.deltaTime;
			}
			else
			{
			}

			position += velocity * Time.deltaTime;
			transform.position = new Vector3(position.x, position.y, 0);

			transform.rotation = Quaternion.Euler(0, 0, bearing);
			bearing += angularVelocity * Time.deltaTime;

			Wrapping();
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
