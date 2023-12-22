using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class FbsDebugUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI fpsText;
        
        [SerializeField]
        private TextMeshProUGUI objectsCountText;

        private void LateUpdate()
        {
            if (!fpsText || !objectsCountText) return;

            fpsText.text = GetFPS().ToString(CultureInfo.InvariantCulture);
            objectsCountText.text = CountAllObjectsInScene().ToString();
        }
        
        private float GetFPS()
        {
            return 1f / Time.deltaTime;
        }
        
        int CountAllObjectsInScene()
        {
            int objectCount = 0;

            Scene currentScene = SceneManager.GetActiveScene();

            GameObject[] allRootObjects = currentScene.GetRootGameObjects();
            objectCount = allRootObjects.Length;
            foreach (GameObject rootObject in allRootObjects)
            {
                objectCount += CountObjectsRecursive(rootObject.transform);
            }

            return objectCount;
        }

        int CountObjectsRecursive(Transform parent)
        {
            int count = 0;
            count += parent.childCount;
            foreach (Transform child in parent)
            {
                count += CountObjectsRecursive(child);
            }

            return count;
        }

    }
}
