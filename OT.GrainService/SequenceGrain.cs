using Microsoft.Extensions.Logging;
using Orleans;
using OT.GrainInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.GrainService;

public sealed class SequenceGrain : Grain<long>, ISequenceGrain
{
    private readonly ILogger<SequenceGrain> logger;
    public SequenceGrain(ILogger<SequenceGrain> logger)
    {
        this.logger = logger;
    }

    public async ValueTask<long> GetSequenceAsync()
    {
        logger.LogInformation("Current value is {seq}", this.State);
        this.State++;
        await this.WriteStateAsync();
        return this.State;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (this.State == 0)
        {
            logger.LogInformation("Sequence value is {seq}, set to default {def}", this.State, 1);
            this.State = 1;
            await this.WriteStateAsync();
        }
        else
        {
            logger.LogInformation("Initialize value {val} from storage", this.State);
        }
    }
}
