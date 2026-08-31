using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Secret
{
    public class ExchangerController : MonoBehaviour
    {
        [SerializeField] private ResourceLibrary _resLibrary;
        [SerializeField] private ExchangerButtonController _exchBlock;
        [SerializeField] private Transform _exchangersRoot;

        [SerializeField] private List<ExchangerBlock> _excnhagers;

        [SerializeField] private List<ExchangerButtonController> _blocks;

        private void Awake()
        {
            Create();
        }

        private void OnEnable()
        {
            foreach (var block in _blocks)
            {
                block.CheckButton();
            }
        }

        private void Create()
        {
            foreach (var exch in _excnhagers)
            {

                var first = _resLibrary._resources.FirstOrDefault(x => x.ResourceType == exch.FirstType);
                var second = _resLibrary._resources.FirstOrDefault(x => x.ResourceType == exch.SecondType);
                var block = Instantiate(_exchBlock, _exchangersRoot);
                _blocks.Add(block);
                block.Setup(first.Icon, exch.FirstCount, exch.FirstType, second.Icon, exch.SecondCount, exch.SecondType);
                block.CheckButton();
            }
        }
    }

    [System.Serializable]
    public class ExchangerBlock
    {
        public ResourceType FirstType;
        public int FirstCount;
        public ResourceType SecondType;
        public int SecondCount;
    }
}
