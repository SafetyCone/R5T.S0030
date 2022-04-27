using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Lombardy;

using R5T.D0065;
using R5T.D0101;
using R5T.T0063;


namespace R5T.S0030
{
	public static partial class IServiceCollectionExtensions
	{

		/// <summary>
		/// Adds the <see cref="ExecutableDirectoryFilePathProvider"/> implementation of <see cref="IExecutableDirectoryFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
		/// </summary>
		public static IServiceCollection AddExecutableDirectoryFilePathProvider(this IServiceCollection services,
			IServiceAction<IExecutableDirectoryPathProvider> executableDirectoryPathProviderAction,
			IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
		{
			services
				.Run(executableDirectoryPathProviderAction)
				.Run(stringlyTypedPathOperatorAction)
				.AddSingleton<IExecutableDirectoryFilePathProvider, ExecutableDirectoryFilePathProvider>();

			return services;
		}

		/// <summary>
		/// Adds the <see cref="HardCodedMainFileContextFilePathsProvider"/> implementation of <see cref="IMainFileContextFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
		/// </summary>
		public static IServiceCollection AddHardCodedMainFileContextFilePathsProvider(this IServiceCollection services)
		{
			services.AddSingleton<IMainFileContextFilePathsProvider, HardCodedMainFileContextFilePathsProvider>();

			return services;
		}

		/// <summary>
		/// Adds the <see cref="ProjectFilePathsProvider"/> implementation of <see cref="IProjectFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
		/// </summary>
		public static IServiceCollection AddProjectFilePathsProvider(this IServiceCollection services,
			IServiceAction<IProjectRepository> projectRepositoryAction)
		{
			services
				.Run(projectRepositoryAction)
				.AddSingleton<IProjectFilePathsProvider, ProjectFilePathsProvider>();

			return services;
		}

		/// <summary>
		/// Adds the <see cref="ServiceDefinitionCodeFilePathsProvider"/> implementation of <see cref="IServiceDefinitionCodeFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
		/// </summary>
		public static IServiceCollection AddServiceDefinitionCodeFilePathsProvider(this IServiceCollection services)
		{
			services.AddSingleton<IServiceDefinitionCodeFilePathsProvider, ServiceDefinitionCodeFilePathsProvider>();

			return services;
		}

		/// <summary>
		/// Adds the <see cref="ServiceDefinitionTypeIdentifier"/> implementation of <see cref="IServiceDefinitionTypeIdentifier"/> as a <see cref="ServiceLifetime.Singleton"/>.
		/// </summary>
		public static IServiceCollection AddServiceDefinitionTypeIdentifier(this IServiceCollection services)
		{
			services.AddSingleton<IServiceDefinitionTypeIdentifier, ServiceDefinitionTypeIdentifier>();

			return services;
		}

		/// <summary>
		/// Adds the <see cref="ServiceImplementationCandidateIdentifier"/> implementation of <see cref="IServiceImplementationCandidateIdentifier"/> as a <see cref="ServiceLifetime.Singleton"/>.
		/// </summary>
		public static IServiceCollection AddServiceImplementationCandidateIdentifier(this IServiceCollection services)
		{
			services.AddSingleton<IServiceImplementationCandidateIdentifier, ServiceImplementationCandidateIdentifier>();

			return services;
		}

		/// <summary>
		/// Adds the <see cref="ServiceImplementationCodeFilePathsProvider"/> implementation of <see cref="IServiceImplementationCodeFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
		/// </summary>
		public static IServiceCollection AddServiceImplementationCodeFilePathsProvider(this IServiceCollection services)
		{
			services.AddSingleton<IServiceImplementationCodeFilePathsProvider, ServiceImplementationCodeFilePathsProvider>();

			return services;
		}

		/// <summary>
		/// Adds the <see cref="ServiceImplementationTypeIdentifier"/> implementation of <see cref="IServiceImplementationTypeIdentifier"/> as a <see cref="ServiceLifetime.Singleton"/>.
		/// </summary>
		public static IServiceCollection AddServiceImplementationTypeIdentifier(this IServiceCollection services)
		{
			services.AddSingleton<IServiceImplementationTypeIdentifier, ServiceImplementationTypeIdentifier>();

			return services;
		}
	}
}