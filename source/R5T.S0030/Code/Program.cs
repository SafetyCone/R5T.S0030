using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        }

        private async Task RunOperation()
        {
            await this.ServiceProvider.Run<O006_IdentityServiceImplementationsLackingMarkerInterface>();
            //await this.ServiceProvider.Run<O005_IdentityServiceDefinitionsLackingMarkerInterface>();
            //await this.ServiceProvider.Run<O004_IdentifyPossibleServiceImplementations>();
            //await this.ServiceProvider.Run<O003_IdentifyServiceImplementations>();
            //await this.ServiceProvider.Run<O002_IdentifyPossibleServiceDefinitions>();
            //await this.ServiceProvider.Run<O001_IdentifyServiceDefinitions>();

            //await this.ServiceProvider.Run<O999_Scratch>();
        }

        //private async Task RunMethod()
        //{
        
        //}
    }
}