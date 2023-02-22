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

			if (Input.GetKey(KeyCode.W))
			{
				this.transform.position += speed * Time.deltaTime * transform.up;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				this.transform.position += speed * Time.deltaTime * (-transform.up);
			}

			if (Input.GetKey(KeyCode.A))
			{
				this.transform.eulerAngles += degrees * Time.deltaTime * Vector3.forward;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				this.transform.eulerAngles += degrees * Time.deltaTime * Vector3.back;
			}
		}
	}
}
