using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;

using R5T.S0030.T001.Level02;
using R5T.S0030.T003.N004;


namespace R5T.S0030
{
    public static class IMethodGeneratorExtensions
    {
        public static Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> CreateAddXActionMethod(this IMethodGenerator _,
            ImplementationDescriptor serviceImplementation)
        {
            Task<CompilationUnitSyntax> CreateAddXMethod_Internal(CompilationUnitSyntax compilationUnit, IClassContext classContext)
            {
                var requiredNamespaceNames = serviceImplementation.GetRequiredNamespaces()
                    .Append(
                        Instances.NamespaceNameOperator.GetAddXActionMethodsRequiredNamespaces())
                    .Now();

                var method = Instances.MethodGenerator.GetAddXAction(
                    serviceImplementation);

                compilationUnit = Instances.MethodOperator.AddMethod(
                    compilationUnit,
                    classContext,
                    requiredNamespaceNames,
                    method);

                return Task.FromResult(compilationUnit);
            }

            return CreateAddXMethod_Internal;
        }

        public static Func<CompilationUnitSyntax, IClassContext, Task<CompilationUnitSyntax>> CreateAddXMethod(this IMethodGenerator _,
            ImplementationDescriptor serviceImplementation)
        {
            Task<CompilationUnitSyntax> CreateAddXMethod_Internal(CompilationUnitSyntax compilationUnit, IClassContext classContext)
            {
                var requiredNamespaceNames = serviceImplementation.GetRequiredNamespaces()
                    .Append(
                        Instances.NamespaceNameOperator.GetAddXMethodsRequiredNamespaces())
                    .Now();

                var method = Instances.MethodGenerator.GetAddX(
                    serviceImplementation);

                compilationUnit = Instances.MethodOperator.AddMethod(
                    compilationUnit,
                    classContext,
                    requiredNamespaceNames,
                    method);

                return Task.FromResult(compilationUnit);
            }

            return CreateAddXMethod_Internal;
        }
    }
}
