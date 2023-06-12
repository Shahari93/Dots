using UnityEngine;
using UnityEditor;

namespace Dots.Utils.DeletePlayerPrefs
{
#if UNITY_EDITOR
    public class DeletePlayerPrefsScript : EditorWindow
    {
        [MenuItem("Window/Delete PlayerPrefs (All)")]
        static void DeleteAllPlayerPrefs()
        {
            if (EditorUtility.DisplayDialog("Clear PlayerPrefs?", "Are you sure you want to clear PlayerPrefs? This can't be undone ", "Yes", "No"))
            {
                PlayerPrefs.DeleteAll();
                Debug.Log("Player prefs deleted");
            }

        }
    }  
#endif
}