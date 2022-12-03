using Orleans;

namespace OT.GrainInterface;
public interface ISequenceGrain : IGrainWithStringKey
{
    ValueTask<long> GetSequenceAsync();
}