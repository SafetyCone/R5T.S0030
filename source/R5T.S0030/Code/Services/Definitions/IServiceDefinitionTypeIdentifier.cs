﻿using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0030
{
    [ServiceDefinitionMarker]
    public interface IServiceDefinitionTypeIdentifier : IServiceDefinition
    {
        Task<ITypeNamedCodeFilePathed[]> GetServiceDefinitionTypes(
            string serviceDefinitionCodeFilePath);
    }
}
