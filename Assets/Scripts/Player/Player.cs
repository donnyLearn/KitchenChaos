using UnityEngine;
using System;

public class Player : MonoBehaviour, IKitchenObjectParent {

    // Singleton Player
    public static Player Instance { get; private set; }

    // Game Input
    [SerializeField] private GameInput gameInput;

    // Movement
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;

    // Interaction
    Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }
    public event EventHandler OnPickUp;


    private KitchenObject kitchenObject;
    [SerializeField] private Transform KOPlacementPoint;

    private void Awake() {
		if(Instance != null) {
            Debug.LogError("There is more than one Player instance");
		}
        Instance = this;
	}
	private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
		gameInput.OnInteractAlternativeAction += GameInput_OnInteractAlternativeAction;
    }

	private void Update() {
        HandleMovement();
        HandleInteraction();
    }
    private void HandleInteraction() {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDist = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDist)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                if (selectedCounter != baseCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null) {
            selectedCounter?.Interact(this);
		}
    }

    private void GameInput_OnInteractAlternativeAction(object sender, EventArgs e) {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null) {
            selectedCounter?.InteractAlternative(this);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }


    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove) {
            // Allow moving along the wall when doing diagonal move near a wall
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            bool canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
            bool canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

            if (canMoveX && moveDir.x !=0) {
                moveDir = moveDirX;
                canMove = canMoveX;
            }
            if (canMoveZ && moveDir.z != 0) {
                moveDir = moveDirZ;
                canMove = canMoveZ;
            }
        }
        if (canMove) {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        isWalking = moveDir != Vector3.zero;

        if(moveDir != Vector3.zero) {
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
		}

    }

    public bool IsWalking() {
        return isWalking;
	}

    public Transform GetKOPlacementPoint() {
        return KOPlacementPoint;
    }

    public void SetKitchenObject(KitchenObject newKitchenObject) {
        this.kitchenObject = newKitchenObject;
        if(kitchenObject != null) {
            // pick up something new
            OnPickUp?.Invoke(this, EventArgs.Empty);
		}
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
        // NVM, don't to play drop sfx here
        // because we want to get different sound for trash and delivery
        //OnDrop?.Invoke(this, EventArgs.Empty);
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
