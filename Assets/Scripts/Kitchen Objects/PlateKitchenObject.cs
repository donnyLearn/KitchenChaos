using UnityEngine;
using System.Collections.Generic;
using System;

public class PlateKitchenObject : KitchenObject {
	[SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

	private List<KitchenObjectSO> kitchenObjectSOList;

	public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
	public class OnIngredientAddedEventArgs {
		public KitchenObjectSO kitchenObjectSO;
	}

	private void Awake() {
		kitchenObjectSOList = new List<KitchenObjectSO>();
	}

	public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
		if(!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
			return false;
		}

		// make burger stack, no double cheese etc
		if(kitchenObjectSOList.Contains(kitchenObjectSO)) {
			return false;
		} else {
			kitchenObjectSOList.Add(kitchenObjectSO);
			OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
				kitchenObjectSO = kitchenObjectSO
			});
			return true;
		}
	}

	public List<KitchenObjectSO> GetKitchenObjectSOList() {
		return kitchenObjectSOList;
	}
}
