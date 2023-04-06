using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour {

	public static KitchenGameManager Instance { get; private set;}

	public event EventHandler OnStateChanged;


	public enum GameState {
		WaitingToStart,
		CountdownToStart,
		GamePlaying,
		GameOver
	}

	private GameState gameState;
	private float countdownToStartTimer = 3f;
	[SerializeField] private float gamePlayingTimerMax = 10f;
	private float gamePlayingTimer = 10f;

	// Pause
	private bool isPaused = false;
	public event EventHandler OnPause;
	public event EventHandler OnUnpause;

	private void Awake() {
		Instance = this;
		gameState = GameState.WaitingToStart;
		Debug.Log("Game start " + Time.realtimeSinceStartup);
	}

	private void Start() {
		GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
		GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
		gamePlayingTimer = gamePlayingTimerMax;
	}

	private void GameInput_OnInteractAction(object sender, EventArgs e) {
		if(gameState == GameState.WaitingToStart) {
			gameState = GameState.CountdownToStart;
			OnStateChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	private void Update() {
		switch(gameState) {
			case GameState.WaitingToStart:
				break;
			case GameState.CountdownToStart:
				countdownToStartTimer -= Time.deltaTime;
				if (countdownToStartTimer < 0f) {
					gameState = GameState.GamePlaying;
					OnStateChanged?.Invoke(this, EventArgs.Empty);
					gamePlayingTimer = gamePlayingTimerMax;
				}
				break;
			case GameState.GamePlaying:
				gamePlayingTimer -= Time.deltaTime;
				if (gamePlayingTimer < 0f) {
					gameState = GameState.GameOver;
					OnStateChanged?.Invoke(this, EventArgs.Empty);
				}
				break;
			case GameState.GameOver:
				break;

		}
	}


	private void GameInput_OnPauseAction(object sender, EventArgs e) {
		TogglePause();
	}

	public void TogglePause() {
		if (isPaused) {
			Time.timeScale = 1;
			isPaused = false;
			OnUnpause?.Invoke(this, EventArgs.Empty);
		} else {
			Time.timeScale = 0;
			isPaused = true;
			OnPause?.Invoke(this, EventArgs.Empty);
		}
	}

	public bool IsGamePlaying() {
		return gameState == GameState.GamePlaying;
	}
	public bool IsCountdownToStart() {
		return gameState == GameState.CountdownToStart;
	}
	public bool IsGameOver() {
		return gameState == GameState.GameOver;
	}
	public bool IsWaitingToStart() {
		return gameState == GameState.WaitingToStart;
	}
	public float GetCountDownToStartTimer() {
		return countdownToStartTimer;
	}
	public float GetGamePlayingTimerNormalized() {
		return gamePlayingTimer / gamePlayingTimerMax;
	}
}
