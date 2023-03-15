using UnityEngine;

namespace Game
{
	[CreateAssetMenu(fileName = nameof(ShipConfigData), menuName = "Data/" + nameof(ShipConfigData))]

	public class ShipConfigData : ScriptableObject
	{
		[SerializeField] private float forwardThrust = default;
		public float ForwardThrust => forwardThrust;

		[SerializeField] private float angularThrust = default;
		public float AngularThrust => angularThrust;

	}
}
