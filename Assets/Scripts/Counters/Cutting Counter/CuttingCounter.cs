using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IHasProgress {

	[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
	private int cuttingProgress;

	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
	public static event EventHandler OnCutAny;

	new public static void ResetStaticData() {
		OnCutAny = null;
	}

	public override void Interact(Player player) {
		if (HasKitchenObject()) {
			// something on the counter
			if (!player.HasKitchenObject()) {
				// empty handed, take
				GetKitchenObject().SetKitchenObjectParent(player);
			} else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
				// plate on hand, take
				if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
					GetKitchenObject().DestroySelf();
				}
			}
		} else {
			if (player.HasKitchenObject()) {
				// holding something, counter clear
				CuttingRecipeSO cuttingRecipeSO = GetRecipeSOFromKitchenSO(player.GetKitchenObject().GetKitchenObjectSO());
				if (cuttingRecipeSO != null) {
					// object can be cut
					player.GetKitchenObject().SetKitchenObjectParent(this);
					cuttingProgress = 0;
				}
			}
		}
	}

	public override void InteractAlternative(Player player) {
		if(HasKitchenObject()) {
			CuttingRecipeSO cuttingRecipeSO = GetRecipeSOFromKitchenSO(GetKitchenObject().GetKitchenObjectSO());
			if(cuttingRecipeSO != null) {
				cuttingProgress++;
				OnCutAny?.Invoke(this, EventArgs.Empty);
				
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
					progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
				});

				if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
					//KitchenObjectSO slicedKitchenObjectSO = GetSlicedSOFromKitchenSO(kitchenObjectSO);
					KitchenObjectSO slicedKitchenObjectSO = cuttingRecipeSO.output;
					GetKitchenObject().DestroySelf();
					KitchenObject.SpawnKitchenObject(slicedKitchenObjectSO, this);
				}
			}
		}
	}

	//private bool HasRecipe(KitchenObjectSO kitchenObjectSO) {
	//	CuttingRecipeSO cuttingRecipeSO = GetRecipeSOFromKitchenSO(kitchenObjectSO);
	//	return cuttingRecipeSO != null;
	//}

	//private KitchenObjectSO GetSlicedSOFromKitchenSO(KitchenObjectSO kitchenObjectSO) {
	//	CuttingRecipeSO cuttingRecipeSO = GetRecipeSOFromKitchenSO(kitchenObjectSO);
	//	if(cuttingRecipeSO != null) {
	//		return cuttingRecipeSO.output;
	//	} else {
	//		return null;
	//	}
	//}

	private CuttingRecipeSO GetRecipeSOFromKitchenSO(KitchenObjectSO kitchenObjectSO) {
		foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
			if (cuttingRecipeSO.input == kitchenObjectSO) {
				return cuttingRecipeSO;
			}
		}
		return null;
	}

}
