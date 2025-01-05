using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay.Conveyor
{
    [Serializable]
    public sealed class ConveyorWorkZone
    {
        [SerializeField] private bool _isBusy;
        private readonly ConveyorAttributes _attributes;
        private readonly ConveyorRecipe _currentRecipe;
        
        public ConveyorWorkZone(ConveyorAttributes attributes, ConveyorRecipe recipe)
        {
            _attributes = attributes;
            _currentRecipe = recipe;
        }

        public async UniTask<ConveyorResource> ConvertResourceAsync(ConveyorResource resource, CancellationTokenSource cts)
        {
            if (_isBusy)
            {
                await UniTask.WaitUntil(() => _isBusy == true, cancellationToken: cts.Token);
            }

            if (!CanConvert(resource))
            {
                return default;
            }
            _isBusy = true;
            
            var convertTimeInSeconds = _attributes.BaseWorkTime * resource.ConvertTimeModifier;
            await UniTask.Delay(TimeSpan.FromSeconds(convertTimeInSeconds), cancellationToken: cts.Token);
            
            _isBusy = false;
            
            return _currentRecipe.ResultingResource.Clone();
        }

        private bool CanConvert(ConveyorResource resource)
        {
            if (resource == null) return false;
            
            return _currentRecipe.RequiredResource.Prototype.Name == resource.Name;
        }
    }
}