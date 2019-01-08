using System;
using System.Collections.Generic;
using System.Text;
using FantasyLeague.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyLeague.Services.Tests
{
    public class RostersServiceTests : BaseServiceTests
    {
        private readonly IRostersService rostersService;

        public RostersServiceTests()
        {
            this.rostersService = this.Provider.GetService<IRostersService>();
        }
    }
}
