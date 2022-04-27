using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using R5T.D0088;
using R5T.D0090;


namespace R5T.S0030
{
    class Program : ProgramAsAServiceBase
    {
        #region Static
        
        static async Task Main()
        {
            //OverridableProcessStartTimeProvider.Override("20211214 - 163052");
            //OverridableProcessStartTimeProvider.DoNotOverride();
        
            await Instances.Host.NewBuilder()
                .UseProgramAsAService<Program, T0075.IHostBuilder>()
                .UseHostStartup<HostStartup, T0075.IHostBuilder>(Instances.ServiceAction.AddHostStartupAction())
                .Build()
                .SerializeConfigurationAudit()
                .SerializeServiceCollectionAudit()
                .RunAsync();
        }

        #endregion


        public Program(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override Task ServiceMain(CancellationToken stoppingToken)
        {
            return this.RunOperation();
            //return this.RunMethod();
        }

#pragma warning disable IDE0051 // Remove unused private members

        private async Task RunOperation()
        {
            //await this.ServiceProvider.Run<O999_Scratch>();
            //await this.ServiceProvider.Run<O900_SortExternalServiceComponentDataFiles>();

            //await this.ServiceProvider.Run<O201_AddServiceImplementationMarkerAttributeAndInterface>();
            //await this.ServiceProvider.Run<O200_AddServiceDefinitionMarkerAttributeAndInterface>();

            await this.ServiceProvider.Run<O106_OutputServiceAddMethodsForProject>();
            //await this.ServiceProvider.Run<O105_AddServiceImplementationsToRepository>();
            //await this.ServiceProvider.Run<O104_AddDependencyDefinitionsToRepository>();
            //await this.ServiceProvider.Run<O103_AddImplementedDefinitionToRepository>();
            //await this.ServiceProvider.Run<O102_AddServiceImplementationsToRepository>();
            //await this.ServiceProvider.Run<O101_AddServiceDefinitionsToRepository>();
            //await this.ServiceProvider.Run<O100_PerformAllSurveys>();

            //await this.ServiceProvider.Run<O010_DescribeAllServiceImplementations>();
            //await this.ServiceProvider.Run<O009a_DescribeAllServiceImplementationsInProject>();
            //await this.ServiceProvider.Run<O009_DescribeServiceImplementationsInFile>();
            //await this.ServiceProvider.Run<O008_DescribeSingleServiceImplementation>();
            //await this.ServiceProvider.Run<O007_IdentifyServiceImplementations>();
            //await this.ServiceProvider.Run<O006_IdentityServiceImplementationsLackingMarkerInterface>();
            //await this.ServiceProvider.Run<O005_IdentityServiceDefinitionsLackingMarkerInterface>();
            //await this.ServiceProvider.Run<O004_IdentifyPossibleServiceImplementations>();
            //await this.ServiceProvider.Run<O003_IdentifyServiceImplementations>();
            //await this.ServiceProvider.Run<O002_IdentifyPossibleServiceDefinitions>();
            //await this.ServiceProvider.Run<O001_IdentifyServiceDefinitions>();

            //await this.ServiceProvider.Run<E001_CodeElementCreation>();
        }

        private async Task RunMethod()
        {
            await this.Scratch();
        }

#pragma warning restore IDE0051 // Remove unused private members

        private async Task Scratch()
        {
            var x = this.ServiceProvider.GetRequiredService<D0116.IUsingNamespaceDirectiveBlockLabelProvider>();

            // Block label is "Default".
            var _ = await x.GetBlockLabel(
                "TestProject.Library",
                null);
        }
    }
}