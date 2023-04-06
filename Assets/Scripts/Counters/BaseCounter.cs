using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

	[SerializeField] private Transform counterTopPoint;
	public static event EventHandler OnItemPlacedAny;
	public static void ResetStaticData() {
		OnItemPlacedAny = null;
	}

	private KitchenObject kitchenObject;

	public virtual void Interact(Player player) {
		// will be overriden, so shouldn't be called
		Debug.LogError("BaseCounter.Interact()");
	}

	public virtual void InteractAlternative(Player player) {
	}


	public Transform GetKOPlacementPoint() {
		return counterTopPoint;
	}

	public void SetKitchenObject(KitchenObject newKitchenObject) {
		this.kitchenObject = newKitchenObject;
		if(newKitchenObject != null) {
			Debug.Log(OnItemPlacedAny.GetInvocationList().Length);
			OnItemPlacedAny?.Invoke(this, EventArgs.Empty);
		}
	}

	public KitchenObject GetKitchenObject() {
		return kitchenObject;
	}

	public void ClearKitchenObject() {
		kitchenObject = null;
	}

	public bool HasKitchenObject() {
		return kitchenObject != null;
	}
}
