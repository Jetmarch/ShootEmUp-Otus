using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.Conveyor
{
    public sealed class ConveyorController
    {
         private readonly ConveyorTransportZone _input;
         private readonly ConveyorTransportZone _output;
         private readonly ConveyorWorkZone _workZone;
        
        private readonly CancellationTokenSource _cts;

        public ConveyorController(ConveyorTransportZone input, ConveyorTransportZone output, ConveyorWorkZone workZone, CancellationTokenSource cancellationTokenSource)
        {
            _input = input;
            _output = output;
            _workZone = workZone;
            _cts = cancellationTokenSource;
        }

        public async UniTask LoadResourceAsync(ConveyorResource resource)
        {
            await _input.AddResourceAsync(resource, _cts);
        }

        public async UniTask<ConveyorResource> UnloadConvertedResourceAsync()
        {
            var resource = await _output.GetNextResourceAsync(_cts);
            return resource;
        }

        public async UniTask UpdateAsync()
        {
            var resource = await _input.GetNextResourceAsync(_cts);
            var convertedResource = await _workZone.ConvertResourceAsync(resource, _cts);
            await _output.AddResourceAsync(convertedResource, _cts);
        }
    }
}
