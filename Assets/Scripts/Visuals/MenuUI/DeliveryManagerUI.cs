 using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

	private void Awake() {
		recipeTemplate.gameObject.SetActive(false);
	}
	private void Start() {
		DeliveryManager.Instance.OnWaitingRecipeUpdate += DeliveryManager_OnDeliveryUpdate;
	}

	private void DeliveryManager_OnDeliveryUpdate(object sender, DeliveryManager.OnDeliveryUpdateEventArgs e) {
        foreach(Transform child in container) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
		}
		foreach(RecipeSO recipeSO in e.waitingRecipeSOList) {
			Transform recipeTransform = Instantiate(recipeTemplate, container);
			recipeTransform.gameObject.SetActive(true);
			recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);

		}
	}
}
