using System;
using System.N0;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;

using R5T.S0030.F001;

using ServiceImplementationDescriptor = R5T.S0030.T001.Level02.ImplementationDescriptor;

using Instances = R5T.S0030.F001.Instances;


namespace System
{
    public static partial class IMethodGeneratorExtensions
    {
        public static MethodDeclarationSyntax GetAddXAction(this IMethodGenerator _,
            ServiceImplementationDescriptor implementationDescriptor)
        {
            var typeNamesBaseDescriptor = implementationDescriptor.GetTypeNameBasedIMplementationDescriptor();

            // Method - Signature.
            // Get method name.
            var methodName = Instances.MethodNameOperator.GetAddXActionMethodName(typeNamesBaseDescriptor.ImplementationTypeName);

            // Extension parameter (this IServiceAction _).
            var extensionBaseParameterName = Instances.ParameterName.Underscore();

            var extensionParameter = Instances.ParameterGenerator_Old.ExtensionParameter(
                Instances.TypeName.IServiceAction_Base(),
                extensionBaseParameterName);

            // Depedency parameters.
            var serviceDependencyParameters = Instances.ParameterGenerator.GetServiceActionParameters(
                typeNamesBaseDescriptor.DependencyTypeNames);

            var serviceDependencyParameterNames = serviceDependencyParameters
                .Select(x => x.GetName())
                .Now_OLD();

            // Get the return type.
            var returnTypeName = Instances.TypeNameOperator.GetIServiceActionOfTypeNameTypeName(
                typeNamesBaseDescriptor.DefinitionTypeName);

            // The serviceAction variable name.
            var serviceActionVariableName = Instances.VariableName.ServiceAction();

            // Method - Statements.
            // Single new service action statement.
            var serviceActionDeclaration = Instances.StatementGenerator.GetServiceAction(
                extensionBaseParameterName,
                typeNamesBaseDescriptor.DefinitionTypeName,
                typeNamesBaseDescriptor.ImplementationTypeName,
                serviceDependencyParameterNames);

            serviceActionDeclaration = serviceActionDeclaration.NormalizeWhitespace();

            // Indent each argument within the service action lambda.
            var arguments = serviceActionDeclaration
                .DescendantNodes()
                .Where(xNode => xNode is SimpleLambdaExpressionSyntax)
                .First()
                .DescendantNodes()
                .Where(xNode => xNode is ArgumentSyntax)
                .Now_OLD();

            serviceActionDeclaration = serviceActionDeclaration.AnnotateNodes(
                arguments,
                out var annotationsByArgument);

            foreach (var annotation in annotationsByArgument.Values)
            {
                serviceActionDeclaration = annotation.ModifyNode<LocalDeclarationStatementSyntax, ArgumentSyntax>(
                    serviceActionDeclaration,
                    argument =>
                    {
                        argument = argument.IndentStartLine(
                            // Indent as if method had zero indentation.
                            Instances.LineIndentation.ByTabCount_SyntaxTriviaList(2));

                        return argument;
                    });
            }

            // Return statement.
            var returnServicesStatement = Instances.StatementGenerator.ReturnIdentifier(serviceActionVariableName)
                .PrependBlankLine();

            // Method - Documentation.
            var summaryDocumentationComment = Instances.DocumentationGenerator.AddXMethodXmlSummary(
                typeNamesBaseDescriptor.DefinitionTypeName,
                typeNamesBaseDescriptor.ImplementationTypeName)
                .SetNewLines();

            // Method.
            var method = Instances.MethodGenerator.PublicStatic(methodName, returnTypeName)
                .AddParameterListParameters(extensionParameter)
                .AddParameterListParameters(serviceDependencyParameters
                    // Indent the depdencney parameters on their own lines.
                    .IndentStartLine(
                        Instances.LineIndentation.ByTabCount_SyntaxTriviaList(1))
                    .Now_OLD())
                .AddDocumentation_Latest(summaryDocumentationComment)
                .AddBodyStatements_WithIndentation(
                    serviceActionDeclaration,
                    returnServicesStatement)
                ;

            return method;
        }

        public static MethodDeclarationSyntax GetAddX(this IMethodGenerator _,
            ServiceImplementationDescriptor implementationDescriptor)
        {
            // Get type names.
            var typeNamesBaseDescriptor = implementationDescriptor.GetTypeNameBasedIMplementationDescriptor();

            // Create the method.
            var servicesParameterName = Instances.ParameterName.Services();

            var serviceCollectionTypeName = Instances.TypeName.IServiceCollection();

            // Method - Parameters.
            var extensionParameter = Instances.ParameterGenerator_Old.ExtensionParameter(serviceCollectionTypeName, servicesParameterName);

            var serviceDependencyParameters = Instances.ParameterGenerator.GetServiceActionParameters(
                typeNamesBaseDescriptor.DependencyTypeNames);

            // Method - Statements.
            // Fluent run statement.
            var serviceDependencyParameterNames = serviceDependencyParameters
                .Select(x => x.GetName())
                .Now_OLD();

            var addSingletonAndRunDependencyServiceActionsStatement = Instances.StatementGenerator.AddServiceImplementationAsSingletonAndRunDependencyServiceActions(
                typeNamesBaseDescriptor.ImplementationTypeName,
                typeNamesBaseDescriptor.DefinitionTypeName,
                serviceDependencyParameterNames,
                servicesParameterName);

            // Tabinate as if the AddX() method is the root.
            var runMethodIndentationTabCount = 2;
            var runMethodIndentation = Instances.LineIndentation.ByTabCount_SyntaxTriviaList(runMethodIndentationTabCount);

            // If there are any dependencies, indent dot operatros. Else, do not, so that the AddSingleton<TDefinition, TImplementation>() method call is on one line.
            if(typeNamesBaseDescriptor.DependencyTypeNames.Any())
            {
                addSingletonAndRunDependencyServiceActionsStatement = addSingletonAndRunDependencyServiceActionsStatement
                    .IndentDotOperators(runMethodIndentation);
            }

            // Return statement.
            var returnServicesStatement = Instances.StatementGenerator.ReturnIdentifier(servicesParameterName)
                .PrependBlankLine();

            // Method - Documentation.
            var summaryDocumentationComment = Instances.DocumentationGenerator.AddXMethodXmlSummary(
                typeNamesBaseDescriptor.DefinitionTypeName,
                typeNamesBaseDescriptor.ImplementationTypeName)
                .SetNewLines();

            // Method.
            var methodName = Instances.MethodNameOperator.GetAddXMethodName(typeNamesBaseDescriptor.ImplementationTypeName);
            var returnTypeName = serviceCollectionTypeName;

            var method = Instances.MethodGenerator.PublicStatic(methodName, returnTypeName)
                .AddParameterListParameters(extensionParameter)
                .AddParameterListParameters(serviceDependencyParameters
                    // Indent the depdencney parameters on their own lines.
                    .IndentStartLine(
                        Instances.LineIndentation.ByTabCount_SyntaxTriviaList(1))
                    .Now_OLD())
                .AddDocumentation_Latest(summaryDocumentationComment)
                .AddBodyStatements_WithIndentation(
                    addSingletonAndRunDependencyServiceActionsStatement,
                    returnServicesStatement)
                ;

            return method;
        }
    }
}
