using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Trigger
{
    public class LegSwapTrigger : MonoBehaviour
    {

        public PlayerLegs legs;

        private void OnTriggerEnter(Collider other)
        {
            var PC = other.GetComponent<PlayerController>();
            PC?.ApplyLegModule(legs);
        }
    }
}