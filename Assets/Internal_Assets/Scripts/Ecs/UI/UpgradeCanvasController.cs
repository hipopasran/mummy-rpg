using System;
using System.Collections.Generic;
using UnityEngine;

namespace Secret
{
    public class UpgradeCanvasController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _resBlocks;

        private void OnEnable()
        {
            foreach (var block in _resBlocks)
            {
                block.SetActive(true);
            }
        }
    }
}
