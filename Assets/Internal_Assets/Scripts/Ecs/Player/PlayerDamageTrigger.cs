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

                if (other.gameObject.TryGetComponent(out ActiveHeal activeHeal))
                {
                    Object.Destroy(activeHeal);
                }

                if (other.gameObject.TryGetComponent(out HealWait healwait))
                {
                    Object.Destroy(healwait);
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
                    var hw = other.gameObject.AddComponent<HealWait>();
                    hw.Setup(2f);
                }
            }
        }
    }
}
