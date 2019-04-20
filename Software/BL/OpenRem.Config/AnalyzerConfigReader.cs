using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OpenRem.Config
{
    class AnalyzerConfigReader : IAnalyzerConfigReader
    {
        public AnalyzerConfigReader()
        {
        }

        public AnalyzerConfig GetConfig(string name)
        {
            try
            {

                var manifestEmbeddedProvider = new ManifestEmbeddedFileProvider(typeof(AnalyzerConfigReader).Assembly);
                var config = new ConfigurationBuilder()
                            .AddJsonFile(manifestEmbeddedProvider, "Config/Analyzer.json", false, false)
                            .Build();
                var dtos = ExtractConfig<AnalyzerDto>(config, "AnalyzerCollection");
                Console.Write(dtos.Count);
            }
            catch (Exception e)
            {

            }
            return new AnalyzerConfig();
            //var arduinoList = AnalyzerList.DeserializeFrom(configFile);
            //var arduinoConfig = arduinoList.Analyzer.Single(x => x.Name == name);
            //return new AnalyzerConfig
            //{
            //    Name = arduinoConfig.Name,
            //    SubChunkSize = int.Parse(arduinoConfig.SubChunkSize),
            //    ChannelsNumber = int.Parse(arduinoConfig.ChannelsNumber),
            //    SampleRate = int.Parse(arduinoConfig.SampleRate),
            //    Probes = arduinoConfig.Probe.Select(probe => new ProbeConfig()
            //    {
            //        Side = probe.Side.ToSide(),
            //        InputChannel = int.Parse(probe.Input.Channel),
            //        OutputChannel = int.Parse(probe.Output.Channel)
            //    }).ToArray()
            //};
        }

        private static List<T> ExtractConfig<T>(IConfigurationRoot config, string sectionName) where T : class, new()
        {
            var entries = config.GetSection(sectionName)
                                 .GetChildren();

            var dtos = new List<T>(entries.Count());
            foreach (var entry in entries)
            {
                var dto = new T();
                entry.Bind(dto);
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}