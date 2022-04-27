using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;

using Instances = R5T.S0030.F001.Instances;


namespace R5T.S0030.F001
{
    public static class IExpressionGeneratorExtensions
    {
        public static ExpressionSyntax RunServiceActions(this IExpressionGenerator _,
            ExpressionSyntax memberedExpression,
            IEnumerable<string> serviceActionIdentifierNames)
        {
            var currentExpression = memberedExpression;

            foreach (var serviceActionIdentifierName in serviceActionIdentifierNames)
            {
                var memberAccessExpression = _.RunServiceActionMemberAccess(currentExpression);

                currentExpression = _.RunServiceActionInvocation(
                    memberAccessExpression,
                    serviceActionIdentifierName);
            }

            return currentExpression;
        }

        public static InvocationExpressionSyntax RunServiceActionInvocation(this IExpressionGenerator _,
            MemberAccessExpressionSyntax memberAccessExpression,
            string serviceActionIdentifierName)
        {
            var output = Instances.SyntaxFactory.Invocation(
                memberAccessExpression,
                Instances.SyntaxFactory.Argument(serviceActionIdentifierName));

            return output;
        }

        public static MemberAccessExpressionSyntax RunServiceActionMemberAccess(this IExpressionGenerator _,
            ExpressionSyntax memberedExpression)
        {
            var output = Instances.SyntaxFactory.MemberAccess(
                memberedExpression,
                Instances.SyntaxFactory.Name(
                    Instances.MethodName.Run()));

            return output;
        }

        public static MemberAccessExpressionSyntax AddSingleton_MemberAccess(this IExpressionGenerator _,
            ExpressionSyntax memberedExpression,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName)
        {
            var output = Instances.SyntaxFactory.MemberAccess(
                memberedExpression,
                Instances.NameGenerator.AddSingleton(serviceDefinitionTypeName, serviceImplementationTypeName));

            return output;
        }

        public static InvocationExpressionSyntax AddSingleton_Invocation(this IExpressionGenerator _,
            ExpressionSyntax memberedExpression,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName)
        {
            var memberAccess = _.AddSingleton_MemberAccess(
                memberedExpression,
                serviceDefinitionTypeName,
                serviceImplementationTypeName);

            var output = Instances.SyntaxFactory.Invocation(memberAccess);
            return output;
        }

        public static InvocationExpressionSyntax AddSingleton(this IExpressionGenerator _,
            ExpressionSyntax memberedExpression,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName)
        {
            var output = _.AddSingleton_Invocation(
                memberedExpression,
                serviceDefinitionTypeName,
                serviceImplementationTypeName);

            return output;
        }

        public static InvocationExpressionSyntax AddXInvocation(this IExpressionGenerator _,
            string serviceCollectionIdentifierName,
            string serviceImplementationTypeName,
            IEnumerable<string> serviceDependencyParameterNames)
        {
            var methodName = Instances.MethodNameOperator.GetAddXMethodName(serviceImplementationTypeName);

            var output = Instances.ExpressionGenerator.Invocation(
                Instances.ExpressionGenerator.MemberAccess(
                    serviceCollectionIdentifierName,
                    methodName),
                serviceDependencyParameterNames);

            return output;
        }

        public static SimpleLambdaExpressionSyntax AddXLambdaInvocation(this IExpressionGenerator _,
            string serviceCollectionIdentifierName,
            InvocationExpressionSyntax addXInvocation)
        {
            var output = _.Lambda(
                serviceCollectionIdentifierName,
                addXInvocation);

            return output;
        }

        public static SimpleLambdaExpressionSyntax AddXLambdaInvocation(this IExpressionGenerator _,
            string serviceCollectionIdentifierName,
            string serviceImplementationTypeName,
            IEnumerable<string> serviceDependencyParameterNames)
        {
            var addXInvocation = Instances.ExpressionGenerator.AddXInvocation(
                serviceCollectionIdentifierName,
                serviceImplementationTypeName,
                serviceDependencyParameterNames);

            var output = _.AddXLambdaInvocation(
                serviceCollectionIdentifierName,
                addXInvocation);

            return output;
        }

        public static InvocationExpressionSyntax ServiceActionNewInvocation(this IExpressionGenerator _,
            string serviceActionBaseIdentifierName,
            string serviceDefinitionTypeName,
            SimpleLambdaExpressionSyntax addXLambdaInvocation)
        {
            var newMethodName = Instances.MethodNameOperator.GetServiceActionNewOfTypeNameMethodName(
                serviceDefinitionTypeName);

            var memberAccess = Instances.ExpressionGenerator.MemberAccess(
                serviceActionBaseIdentifierName,
                newMethodName);

            var argument = Instances.SyntaxFactory.Argument(
                addXLambdaInvocation);

            var invocation = Instances.ExpressionGenerator.Invocation(
                memberAccess,
                argument);

            return invocation;
        }

        public static InvocationExpressionSyntax ServiceActionNewInvocation(this IExpressionGenerator _,
            string serviceActionBaseIdentifierName,
            string serviceCollectionIdentifierName,
            string serviceDefinitionTypeName,
            string serviceImplementationTypeName,
            IEnumerable<string> serviceDependencyParameterNames)
        {
            var addXLambdaInvocation = Instances.ExpressionGenerator.AddXLambdaInvocation(
                serviceCollectionIdentifierName,
                serviceImplementationTypeName,
                serviceDependencyParameterNames);

            var serviceActionNewInvocation = Instances.ExpressionGenerator.ServiceActionNewInvocation(
                serviceActionBaseIdentifierName,
                serviceDefinitionTypeName,
                addXLambdaInvocation);

            return serviceActionNewInvocation;
        }
    }
}
