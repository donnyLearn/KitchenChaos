using UnityEngine;

public class BurnBarFlashingUI : MonoBehaviour {
	[SerializeField] private StoveCounter stoveCounter;
	private Animator animator;
	private const string IS_BURNING = "isBurning";

	private void Awake() {
		animator = GetComponent<Animator>();
	}
	private void Start() {
		stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
		animator.SetBool(IS_BURNING, false);

	}

	private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
		float burnThreshold = .5f;
		bool showUI = stoveCounter.IsCooked() && e.progressNormalized > burnThreshold;
		animator.SetBool(IS_BURNING, showUI);
	}
}
