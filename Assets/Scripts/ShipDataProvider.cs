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
				angularVelocity -= shipConfig.AngularThrust * Time.deltaTime;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				angularVelocity += shipConfig.AngularThrust * Time.deltaTime;

			}
			else
			{
				angularVelocity *= 0.99f;
			}

			if (Input.GetKey(KeyCode.W))
			{
				velocity.x += shipConfig.ForwardThrust * Mathf.Sin(angularVelocity) * Time.deltaTime;
				velocity.y += shipConfig.ForwardThrust * Mathf.Cos(angularVelocity) * Time.deltaTime;
			}
			else
			{
				velocity.x *= 0.99f;
				velocity.y *= 0.99f;
			}

			position = velocity * Time.deltaTime;
			bearing = angularVelocity * Time.deltaTime;

			transform.position += new Vector3(position.x, position.y, transform.up.z);
			transform.rotation *= Quaternion.AngleAxis(bearing, Vector3.forward);

			Wrapping();
		}

		private void Movement()
		{
			if (Input.GetKey(KeyCode.W))
			{
				//this.transform.position += speed * Time.deltaTime * transform.up;
			}
		}

		private void Rotation()
		{
			if (Input.GetKey(KeyCode.A))
			{
				//this.transform.eulerAngles += degrees * Time.deltaTime * Vector3.forward;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				//this.transform.eulerAngles += degrees * Time.deltaTime * Vector3.back;
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
