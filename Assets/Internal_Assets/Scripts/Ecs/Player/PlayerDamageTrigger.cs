using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Secret
{
    public class PlayerDamageTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (other.gameObject.TryGetComponent(out ActiveDamage activeDamage))
                {
                    return;
                }
                else
                {
                    other.gameObject.AddComponent<ActiveDamage>();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (other.gameObject.TryGetComponent(out ActiveDamage activeDamage))
                {
                    Object.Destroy(activeDamage);
                    other.gameObject.AddComponent<ActiveHeal>();
                }
            }
        }
    }
}
