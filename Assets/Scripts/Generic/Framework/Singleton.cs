using UnityEngine;

namespace DylanJay.Framework
{
    /// <summary>
    /// Be aware this will not prevent a non singleton constructor
    ///   such as `T myT = new T();`
    /// To prevent that, add `protected T () {}` to your singleton class.
    /// 
    /// As a note, this is made as MonoBehaviour because we need Coroutines.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviourWrapper where T : MonoBehaviourWrapper
    {
        private static T _instance = null;
        private static GameObject _gameObject = null;
        private static Transform _transform = null;
        private static object _lock = new object();
        private static bool appIsQuitting = false;

        public static T Instance
        {
            get
            {
                if (appIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed on application quit." +
                        " Won't create again - returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopening the scene might fix it.");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof(T).ToString();

                            DontDestroyOnLoad(singleton);
                            _gameObject = singleton;
                            _transform = singleton.transform;

                            Debug.Log("[Singleton] An instance of " + typeof(T) +
                                " is needed in the scene, so '" + singleton +
                                "' was created with DontDestroyOnLoad.");
                        }
                    }
                    return _instance;
                }
            }

            protected set
            {
                if (_instance == null)
                {
                    _instance = value;
                    _gameObject = _instance.gameObject;
                    _transform = _instance.transform;
                    DontDestroyOnLoad(_gameObject);
                }
                else if (_instance != value)
                {
                    Debug.LogErrorFormat("[Singleton] Tried to make multiple instances of {0}", _instance);
                }
            }
        }

        void OnDestroy()
        {
            appIsQuitting = true;
        }

        public new static GameObject gameObject
        {
            get { return _gameObject; }
        }

        public new static Transform transform
        {
            get { return _transform; }
        }
    }
}
