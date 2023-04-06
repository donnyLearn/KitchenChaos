using UnityEngine.SceneManagement;
using UnityEngine;

public static class Loader {

	public enum Scene {
		MainMenuScene,
		GameScene,
		LoadingScene
	}
	private static Scene targetScene;

	public static void Load(Scene targetScene) {
		Debug.Log("Start loading loading screen " + Time.realtimeSinceStartup);
		Loader.targetScene = targetScene;
		SceneManager.LoadScene(Scene.LoadingScene.ToString());
	}

	public static void LoaderCallback() {
		Debug.Log("start loading game scene " + Time.realtimeSinceStartup);
		SceneManager.LoadScene(targetScene.ToString());
	}

}
