using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
	}

    public void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent) {
        if (kitchenObjectParent != null) {
            kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = newKitchenObjectParent;
        transform.parent = newKitchenObjectParent.GetKOPlacementPoint();
        transform.localPosition = Vector3.zero;
        if(newKitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("Counter already has a KitchenObject");
		}
        newKitchenObjectParent.SetKitchenObject(this);
	}

    public IKitchenObjectParent GetClearCounter() {
        return this.kitchenObjectParent;
	}

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
	}

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kichenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kichenObjectParent);
        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if(this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
		} else {
            plateKitchenObject = null;
            return false;
		}
	}
}
