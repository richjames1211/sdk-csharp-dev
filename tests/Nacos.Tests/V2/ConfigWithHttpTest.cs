﻿namespace Nacos.Tests.V2
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Nacos.V2;
    using Nacos.V2.DependencyInjection;
    using System;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "1x")]
    [Trait("Category", "2x")]
    public class ConfigWithHttpTest : ConfigBaseTest
    {
        public ConfigWithHttpTest(ITestOutputHelper output)
        {
            _output = output;

            IServiceCollection services = new ServiceCollection();

            services.AddNacosV2Config(x =>
            {
                x.ServerAddresses = new System.Collections.Generic.List<string> { "http://localhost:8848/" };
                x.EndPoint = "";
                x.Namespace = "cs-test";

                /*x.UserName = "nacos";
                 x.Password = "nacos";*/

                // swich to use http or rpc
                x.ConfigUseRpc = false;
            });

            services.AddLogging(builder => { builder.AddConsole(); });

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            _output.WriteLine($"{nameof(ConfigWithHttpTest)} BuildServiceProvider");
            _configSvc = serviceProvider.GetService<INacosConfigService>();
            _output.WriteLine($"{nameof(ConfigWithHttpTest)} Get INacosConfigService");
        }
    }
}
