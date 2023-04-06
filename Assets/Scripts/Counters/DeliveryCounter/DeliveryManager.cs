using UnityEngine;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour {
	public static DeliveryManager Instance { get; private set; }

	[SerializeField] private RecipeListSO recipeListSO;
	private List<RecipeSO> waitingRecipeSOList;

	private float spawnRecipeTimer;
	private float spawnRecipeTimerMax = 4f;
	private int waitingRecipeCount;
	private int waitingRecipeMax = 4;
	private int successfulOrderAmount = 0;

	public event EventHandler<OnDeliveryUpdateEventArgs> OnWaitingRecipeUpdate;
	public class OnDeliveryUpdateEventArgs {
		public List<RecipeSO> waitingRecipeSOList;
	}
	public event EventHandler OnDeliverySuccess;
	public event EventHandler OnDeliveryFail;


	private void Awake() {
		Instance = this;
		waitingRecipeSOList = new List<RecipeSO>();
	}
	private void Start() {
		spawnRecipeTimer = spawnRecipeTimerMax;
		waitingRecipeCount = 0;
		OnWaitingRecipeUpdate?.Invoke(this, new OnDeliveryUpdateEventArgs {
			waitingRecipeSOList = waitingRecipeSOList
		});
	}

	private void Update() {
		spawnRecipeTimer -= Time.deltaTime;
		if(spawnRecipeTimer <= 0f) {
			spawnRecipeTimer = spawnRecipeTimerMax;
			if(KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeCount < waitingRecipeMax) {
				waitingRecipeCount++;
				RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
				waitingRecipeSOList.Add(waitingRecipeSO);
				OnWaitingRecipeUpdate?.Invoke(this, new OnDeliveryUpdateEventArgs {
					waitingRecipeSOList = waitingRecipeSOList
				});
			}
		}
	}

	public bool DeliverRecipe(PlateKitchenObject plateKitchenObject) {
		for(int i = 0; i < waitingRecipeSOList.Count; i++) {
			RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
			if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
				// Basically only check if the recipe has the same amount of ingredient as the plate
				bool recipeMatch = true;
				foreach(KitchenObjectSO kitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
					bool ingredientMatch = false;
					foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
						// O(N^2) check
						if(kitchenObjectSO == plateKitchenObjectSO) {
							ingredientMatch = true;
							break;
						}
					}
					if(!ingredientMatch) {
						// there is an ingredient not in the plate
						recipeMatch = false;
						break;
					}
				}
				if(recipeMatch) {
					// all ingredients on the recipe exist in the plate
					//     because we checked the amount of ingredients, it also true from the other side

					// deliver the order
					Debug.Log("Delivered " + waitingRecipeSOList[i].recipeName);
					waitingRecipeSOList.RemoveAt(i);
					waitingRecipeCount--;
					OnWaitingRecipeUpdate?.Invoke(this, new OnDeliveryUpdateEventArgs {
						waitingRecipeSOList = waitingRecipeSOList
					});
					OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
					successfulOrderAmount++;
					return true;
				}
			}
		}
		// No matching recipe
		Debug.Log("No order fulfilled");
		OnDeliveryFail?.Invoke(this, EventArgs.Empty);
		return false;
	}

	public int GetSuccessfulOrderAmount() {
		return successfulOrderAmount;
	}
}
