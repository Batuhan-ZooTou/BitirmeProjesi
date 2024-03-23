using UnityEngine;
namespace _Game.Scripts.Extensions
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected bool dontDestroy = true;
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                if (dontDestroy)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
