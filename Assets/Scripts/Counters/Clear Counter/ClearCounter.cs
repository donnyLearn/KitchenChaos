using UnityEngine;

public class ClearCounter : BaseCounter{

	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	public override void Interact(Player player ) {
		if(HasKitchenObject()) {
			// something on the counter
			if (!player.HasKitchenObject()) {
				// empty handed, take
				GetKitchenObject().SetKitchenObjectParent(player);
			} else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
				// plate on hand, take
				if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
					GetKitchenObject().DestroySelf();
				}
			} else {
				// player holding something that is not a plate
				if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
					// plate on counter 
					if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
						player.GetKitchenObject().DestroySelf();
					}
				}
			}
		} else {
			if(player.HasKitchenObject()) {
				// holding something, counter clear, place here
				player.GetKitchenObject().SetKitchenObjectParent(this);
			}
		}
	}
}


/* TryGetKitchenObject
from
	if(player.GetKitchenObject() is PlateKitchenObject) {
		PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject);
		...
	}

to
	if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
		...
	}
 */