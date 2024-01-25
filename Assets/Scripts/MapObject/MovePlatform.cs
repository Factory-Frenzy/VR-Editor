using UnityEngine;

namespace MapObject
{
    public class MovePlatform : MonoBehaviour
    {
        public Transform EndPointA;
        public Transform EndPointB;
        public float Speed = 1.0f;
        
        private Transform target;
        public bool active { get; set; }
        
        void Start()
        {
            active = true;
            // Démarre en se dirigeant vers le point A
            target = EndPointA;
        }
        
        void Update()
        {
            MoveTowardsTarget();
        }
        
        void MoveTowardsTarget()
        {
            if (!active) return;

            if (target != null)
            {
                // Calcule la nouvelle position
                Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
                // D�place le GameObject vers la nouvelle position
                transform.position = newPosition;
            }

            // Vérifie si le GameObject a atteint la cible
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                // Alterne la cible
                target = target == EndPointA ? EndPointB : EndPointA;
            }
        }
    }
}