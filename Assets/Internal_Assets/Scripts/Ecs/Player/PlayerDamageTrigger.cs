using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Secret
{
    public class PlayerDamageTrigger : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private bool _isCantAttack;
        [SerializeField] private List<TentacleProvider> _tentacles;

        private TentacleProvider GetReadyTentacles()
        {
            var readyTentacle = _tentacles.FirstOrDefault(x => x.IsReady);
            return readyTentacle;
        }

        private void Update()
        {
            if (PlayerLiveStats.Instance.IsCargoFull)
            {
                if (!_isCantAttack)
                {
                    _isCantAttack = true;
                    _collider.enabled = false;

                    foreach (var tentacle in _tentacles)
                    {
                        if(tentacle.Enemy != null)
                            tentacle.SetHome();
                    }
                }
            }
            else
            {
                if (_isCantAttack)
                {
                    _isCantAttack = false;
                    _collider.enabled = true;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isCantAttack) return;
            if (other.gameObject.tag == "Enemy")
            {
                if (other.gameObject.TryGetComponent(out ActiveDamage activeDamage))
                {
                    return;
                }
                else
                {
                    var tentacle = GetReadyTentacles();
                    if (tentacle != null)
                    {
                        tentacle.SetAttack(other.transform);
                        other.gameObject.AddComponent<ActiveDamage>();
                        
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
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(_isCantAttack) return;
            if (other.gameObject.TryGetComponent(out ActiveDamage activeDamage))
            {
                return;
            }
            else
            {
                var tentacle = GetReadyTentacles();
                if (tentacle != null)
                {
                    tentacle.SetAttack(other.transform);
                    other.gameObject.AddComponent<ActiveDamage>();
                    
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
                    
                    var tentacle = _tentacles.FirstOrDefault(x => x.Enemy == other.transform);
                    if (tentacle)
                    {
                        tentacle.SetHome();
                    }
                }
            }
        }
    }
}
