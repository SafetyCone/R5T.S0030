﻿using System;

using R5T.D0082;
using R5T.T0064;


namespace R5T.S0030.T003.N005
{
    [ServiceDefinitionMarker]
    public interface IRemoteRepositoryContextProvider : IServiceDefinition
    {
        IGitHubOperator GitHubOperator { get; }
    }
}
