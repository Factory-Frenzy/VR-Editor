using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//sing UnityEngine.XR.Interaction.Toolkit;

namespace DefaultNamespace
{
    public class SoundManager : MonoBehaviour
    {
        public XRBaseController leftXrController;
        public XRBaseController rightXrController;
        
        public AudioSource spawnSound;
        public AudioSource despawnSound;
        public AudioSource saveSound;

        public void VibrateController(ControllerHand hand, float intensity, float duratio)
        {
            if (hand == ControllerHand.Left)
            {
                Vibrate(leftXrController, intensity, duratio);
            }

            if (hand == ControllerHand.Right)
            {
                Vibrate(rightXrController, intensity, duratio);
            }
        }

        private void Vibrate(XRBaseController xrController, float intensity, float duration)
        {
            // Assurez-vous que le XRController est valide et connecté
            if (xrController == null || !xrController)
            {
                return;
            }
            
            // Définir la fréquence de vibration (entre 0 et 1)
            float frequency = 1.0f;

            // Définir le canal de vibration (0 pour tous les canaux)
            uint channel = 0;
            
            // Déclencher la vibration
            xrController.SendHapticImpulse( intensity, duration);
        }

        public void PlaySpawnSound()
        {
            spawnSound.Play();
        }

        public void PlayDespawnSound()
        {
            despawnSound.Play();
        }

        public void PlaySaveSound()
        {
            saveSound.Play();
        }
    }

    public enum ControllerHand
    {
        Left = 0,
        Right = 1,
    }
}