using UnityEngine;

public interface IKitchenObjectParent {
	public Transform GetKOPlacementPoint();

	public void SetKitchenObject(KitchenObject newKitchenObject);

	public KitchenObject GetKitchenObject();

	public void ClearKitchenObject();

	public bool HasKitchenObject();
}
