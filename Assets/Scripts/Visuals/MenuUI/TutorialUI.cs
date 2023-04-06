using UnityEngine;
using System;
using TMPro;

public class TutorialUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI upButtonText;
    [SerializeField] private TextMeshProUGUI downButtonText;
    [SerializeField] private TextMeshProUGUI leftButtonText;
    [SerializeField] private TextMeshProUGUI rightButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI interactAlternativeButtonText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;

	private void Awake() {
		
	}

	private void Start() {
        Show();
        UpdateVisual();
		GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
		KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
	}


	private void GameInput_OnBindingRebind(object sender, EventArgs e) {
        UpdateVisual();
    }
	private void KitchenGameManager_OnStateChanged(object sender, EventArgs e) {
		if(KitchenGameManager.Instance.IsCountdownToStart()) {
            Hide();
		}
	}

	private void UpdateVisual() {
        upButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        downButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        leftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        rightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternativeButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
