using UnityEngine;
using System;

public class StoveCounter : BaseCounter, IHasProgress {

	public enum CookingState {
		IDLE,
		COOKING,
		COOKED,
		BURNED
	}

	[SerializeField] private CookingRecipeSO[] cookingRecipeSOArray;
	[SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

	private CookingState state;
	private float cookingTimer;
	private CookingRecipeSO cookingRecipeSO;
	private float burningTimer;
	private BurningRecipeSO burningRecipeSO;



	// UI
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

	public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
	public class OnStateChangedEventArgs : EventArgs {
		public CookingState state;
	}


	private void Start() {
		state = CookingState.IDLE;
	}

	private void Update() {
		if(HasKitchenObject()) {
			switch(state) {
				case CookingState.IDLE:
					break;
				case CookingState.COOKING:
					cookingTimer += Time.deltaTime;
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
						progressNormalized = cookingTimer / cookingRecipeSO.cookingTimeMax
					});
					if (cookingTimer > cookingRecipeSO.cookingTimeMax) {
						GetKitchenObject().DestroySelf();
						KitchenObject.SpawnKitchenObject(cookingRecipeSO.output, this);
						burningRecipeSO = GetBurningRecipeSOFromKitchenSO(GetKitchenObject().GetKitchenObjectSO());
						burningTimer = 0f;
						state = CookingState.COOKED;
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
					}
					break;
				case CookingState.COOKED:
					burningTimer += Time.deltaTime;
					if(burningTimer > burningRecipeSO.burningTimeMax) {
						GetKitchenObject().DestroySelf();
						KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
						state = CookingState.BURNED;
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
					}
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
						progressNormalized = burningTimer / burningRecipeSO.burningTimeMax
					});
					break;
				case CookingState.BURNED:
					break;
			}
		}
	}

	public override void Interact(Player player) {
		if (HasKitchenObject()) {
			// something on the counter
			if (!player.HasKitchenObject()) {
				// empty handed, take
				GetKitchenObject().SetKitchenObjectParent(player);
				state = CookingState.IDLE;
				OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
					progressNormalized = 0f
				});
			} else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
				// plate on hand, take
				if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
					GetKitchenObject().DestroySelf();
					state = CookingState.IDLE;
					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
						progressNormalized = 0f
					});
				}
			}
		} else {
			if (player.HasKitchenObject()) {
				// holding something, counter clear, place here
				cookingRecipeSO = GetCookingRecipeSOFromKitchenSO(player.GetKitchenObject().GetKitchenObjectSO());
				if (cookingRecipeSO != null) {
					// able to be cook
					player.GetKitchenObject().SetKitchenObjectParent(this);
					cookingTimer = 0f;
					state = CookingState.COOKING;
					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
				}
			}
		}
	}

	private CookingRecipeSO GetCookingRecipeSOFromKitchenSO(KitchenObjectSO kitchenObjectSO) {
		foreach (CookingRecipeSO cookingRecipeSO in cookingRecipeSOArray) {
			if (cookingRecipeSO.input == kitchenObjectSO) {
				return cookingRecipeSO;
			}
		}
		return null;
	}

	private BurningRecipeSO GetBurningRecipeSOFromKitchenSO(KitchenObjectSO kitchenObjectSO) {
		foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
			if (burningRecipeSO.input == kitchenObjectSO) {
				return burningRecipeSO;
			}
		}
		return null;
	}

	public bool IsCooked() {
		return state == CookingState.COOKED;
	}
}
