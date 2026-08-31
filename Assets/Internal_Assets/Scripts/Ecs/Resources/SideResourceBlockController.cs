using System;
using System.Collections.Generic;
using UnityEngine;

namespace Secret
{
    public class SideResourceBlockController : MonoBehaviour
    {
        [SerializeField] private List<ResourceUIPresenterUpgrade> _presenters;

        private void OnEnable()
        {
            PlayerResourceStats.Instance.OnAddResource += CheckForUpdates;
            
            CheckForUpdates(null);
        }

        private void CheckForUpdates(ResourcePack resourcePack)
        {
            foreach (var presenter in _presenters)
            {
                presenter.CheckForShowRes();
            }
        }

        private void OnDisable()
        {
            PlayerResourceStats.Instance.OnAddResource -= CheckForUpdates;
        }
    }
}
