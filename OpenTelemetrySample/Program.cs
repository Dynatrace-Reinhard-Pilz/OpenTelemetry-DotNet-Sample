using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace OpenTelemetrySample
{
    class Program
    {

        private const string activitySource = "Dynatrace.DotNetApp.Sample";
        public static readonly ActivitySource MyActivitySource = new ActivitySource(activitySource);
        

        static void Main(string[] args)
        {
            var token = "Api-Token #####################################################"; 

            var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetSampler(new AlwaysOnSampler())
                .AddSource(activitySource)
                .SetResourceBuilder(ResourceBuilder.CreateEmpty().AddService("dotnet-quickstart"))
                .AddOtlpExporter(otlpOptions =>
                {
                    // https://{your-environment-id}.live.dynatrace.com/api/v2/otlp/v1/traces
                    // https://{your-domain}/e/{your-environment-id}/api/v2/otlp/v1/traces
                    otlpOptions.Endpoint = new Uri("https://#######.live.dynatrace.com/api/v2/otlp/v1/traces");
                    otlpOptions.Protocol = OtlpExportProtocol.HttpProtobuf;
                    otlpOptions.Headers = $"Authorization={token}";
                })
                .Build();


            executeBusinessCode();
        }

        static void executeBusinessCode()
        {
            for (; ; )
            {
                executeFunction();
            }
        }

        static void executeFunction()
        {
            using (var activity = MyActivitySource.StartActivity("my-span"))
            {
                activity?.SetTag("my-key-1", "my-value-1"); //TODO Add attributes

                // 
                Thread.Sleep(5000);
            }
        }

    }
}
