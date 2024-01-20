using UnityEngine;

namespace DefaultNamespace
{
    public class LockManager : MonoBehaviour
    {
        public static bool IsObjectLocked(GameObject obj)
        {
            // Ajoutez ici la logique pour obtenir l'état de verrouillage du GameObject
            return obj.GetComponent<LockUnlockObject>().IsLocked;
        }

        public static void SetObjectLock(GameObject obj, bool lockState)
        {
            // Ajoutez ici la logique pour définir l'état de verrouillage du GameObject
            obj.GetComponent<LockUnlockObject>().IsLocked = lockState;
        }
    }
}