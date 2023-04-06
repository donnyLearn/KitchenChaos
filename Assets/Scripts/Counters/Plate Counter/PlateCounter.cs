using UnityEngine;
using System;

public class PlateCounter : BaseCounter {

	[SerializeField] private KitchenObjectSO plateKitchenObjectSO;

	private float spawnPlateTimer;
	private float spawnPlateTimerMax = 4f;
	private int plateAmount;
	private int plateAmountMax = 4;

	public event EventHandler OnPlateSpawned;
	public event EventHandler OnPlateRemoved;


	private void Start() {
		plateAmount = 0;
		spawnPlateTimer = 0f;
	}

	private void Update() {
		spawnPlateTimer += Time.deltaTime;
		if(spawnPlateTimer > spawnPlateTimerMax) {
			spawnPlateTimer = 0f;

			if(KitchenGameManager.Instance.IsGamePlaying() && plateAmount < plateAmountMax) {
				plateAmount++;
				OnPlateSpawned?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public override void Interact(Player player) {
		if(!player.HasKitchenObject() && plateAmount > 0) {
			plateAmount--;
			KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
			OnPlateRemoved?.Invoke(this, EventArgs.Empty);
		}
	}
}
