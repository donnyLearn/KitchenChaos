using UnityEngine;

public class PlayerSound : MonoBehaviour {
    private Player player;
	private float footstepTimer;
	private float footstepTimerMax = .1f;

	private void Awake() {
		player = GetComponent<Player>();
	}

	private void Update() {
		footstepTimer -= Time.deltaTime;
		if(footstepTimer < 0f) { 
			footstepTimer = footstepTimerMax;

			if(player.IsWalking()) {
				float footstepVolume = 1f;
				Debug.Log("iswalking");
				SoundManager.Instance.PlayFootstepSound(player.transform.position, footstepVolume);
			}
		}

	}
}
