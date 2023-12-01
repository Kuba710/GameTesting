using Sirenix.OdinInspector;
using UnityEngine;

public class Singleton<T> : SerializedMonoBehaviour
    where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            return instance;
        }
    }

    public static bool IsReady => instance != null;

    private void OnDestroy()
    {
        if (instance != null)
        {
            instance = null;
        }
    }
}
