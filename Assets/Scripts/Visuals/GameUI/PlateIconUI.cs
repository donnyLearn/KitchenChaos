using UnityEngine;

public class PlateIconUI : MonoBehaviour {
    [SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private Transform iconTemplate;

	private void Start() {
		plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
	}

	private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
		foreach (Transform child in transform) {
			if (child == iconTemplate) continue;
			Destroy(child.gameObject);
		}
		foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
			Transform iconTransform = Instantiate(iconTemplate, transform);
			iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
			iconTransform.gameObject.SetActive(true);
		}
	}
}
