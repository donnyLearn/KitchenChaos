using UnityEngine;
using UnityEditor;

public class InspectorLock : MonoBehaviour {
	[MenuItem("Edit/Toggle Inspector Lock %Q")]
	public static void LockInspector() {
		ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
		ActiveEditorTracker.sharedTracker.ForceRebuild();
	}
	[MenuItem("Edit/Toggle Inspector Lock %Q", true)]
	public static bool Valid() {
		return ActiveEditorTracker.sharedTracker.activeEditors.Length != 0;
	}
}
