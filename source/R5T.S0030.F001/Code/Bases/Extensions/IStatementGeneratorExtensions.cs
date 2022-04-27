using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;

using Instances = R5T.S0030.F001.Instances;


namespace R5T.S0030.F001
{
    public static class IStatementGeneratorExtensions
    {
        public static LocalDeclarationStatementSyntax GetServiceAction(this IStatementGenerator _,
            string serviceActionBaseIdentifierName,
            string serviceCollectionIdentifierName,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName,
            IEnumerable<string> serviceDependencyParameterNames)
        {
            var serviceActionNewInvocation = Instances.ExpressionGenerator.ServiceActionNewInvocation(
                serviceActionBaseIdentifierName,
                serviceCollectionIdentifierName,
                serviceDefinitionTypeName,
                serviceImplementationTypeName,
                serviceDependencyParameterNames);

            var serviceActionDeclaration = Instances.SyntaxFactory.LocalDeclaration(
                Instances.SyntaxFactory.VarVariableDeclaration(
                    Instances.SyntaxFactory.VariableDeclarator(
                        Instances.VariableName.ServiceAction())
                    .WithInitializer(
                        Instances.SyntaxFactory.EqualsValueClause(
                            serviceActionNewInvocation))));

            return serviceActionDeclaration;
        }

        public static LocalDeclarationStatementSyntax GetServiceAction(this IStatementGenerator _,
            string serviceActionBaseIdentifierName,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName,
            IEnumerable<string> serviceDependencyParameterNames)
        {
            var serviceCollectionIdentifierName = Instances.ParameterName.Services();

            var output = _.GetServiceAction(
                serviceActionBaseIdentifierName,
                serviceCollectionIdentifierName,
                serviceDefinitionTypeName,
                serviceImplementationTypeName,
                serviceDependencyParameterNames);

            return output;
        }

        public static ExpressionStatementSyntax AddServiceImplementationAsSingletonAndRunDependencyServiceActions(this IStatementGenerator _,
            string implementationTypeName,
            string definitionTypeName,
            IEnumerable<string> serviceActionVariableNames,
            string serviceCollectionIdentifierName)
        {
            var servicesIdentifier = Instances.SyntaxFactory.IdentifierName(serviceCollectionIdentifierName);

            var runServiceActions = Instances.ExpressionGenerator.RunServiceActions(
                servicesIdentifier,
                serviceActionVariableNames);

            var addSingleton = Instances.ExpressionGenerator.AddSingleton(
                runServiceActions,
                definitionTypeName,
                implementationTypeName);

            var output = Instances.SyntaxFactory.ExpressionStatement(addSingleton);
            return output;
        }
    }
}
