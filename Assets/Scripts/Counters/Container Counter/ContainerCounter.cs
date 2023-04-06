using UnityEngine;
using System;

public class ContainerCounter : BaseCounter {

	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	public event EventHandler OnPlayerGrab;

	public override void Interact(Player player) {
		if (!player.HasKitchenObject()) {
			KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
			OnPlayerGrab?.Invoke(this, EventArgs.Empty);
		}
	}
}
