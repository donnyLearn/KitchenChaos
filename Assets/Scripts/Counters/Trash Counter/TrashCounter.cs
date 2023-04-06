using UnityEngine;
using System;

public class TrashCounter : BaseCounter {

	public static event EventHandler OnTrashAny;
	
	new public static void ResetStaticData() {
		OnTrashAny = null;
	}


	public override void Interact(Player player) {
		if (player.HasKitchenObject()) {
			player.GetKitchenObject().DestroySelf();
			OnTrashAny?.Invoke(this, EventArgs.Empty);
		}
	}
}
