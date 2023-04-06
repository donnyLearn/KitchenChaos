using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {
    private Animator animator;
	[SerializeField] private CuttingCounter cuttingCounter;

	private const string CUT = "Cut";
	private void Awake() {
		animator = GetComponent<Animator>();
	}

	private void Start() {
		cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
	}

	private void CuttingCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
		animator.SetTrigger(CUT);
	}
}
