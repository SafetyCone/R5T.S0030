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
    public static class IMethodOperatorExtensions
    {
        public static CompilationUnitSyntax AddMethod(this IMethodOperator _,
            CompilationUnitSyntax compilationUnit,
            IClassContext classContext,
            IEnumerable<string> requiredNamespaceNames,
            MethodDeclarationSyntax method)
        {
            var classIndentation = classContext.GetIndentation(
                        compilationUnit);

            compilationUnit = classContext.AddUsings(
                compilationUnit,
                requiredNamespaceNames);

            compilationUnit = classContext.Annotation.ModifySynchronous(
                compilationUnit,
                (@class) =>
                {
                    @class = @class.AddMethod(
                        method.IndentBlock(
                            Instances.Indentation.IncreaseByTab_SyntaxTriviaList(
                                classIndentation)));

                    return @class;
                });

            return compilationUnit;
        }

        public static CompilationUnitSyntax AddMethod(this IMethodOperator _,
           CompilationUnitSyntax compilationUnit,
           IClassContext classContext,
           MethodDeclarationSyntax method)
        {
            var output = _.AddMethod(
                compilationUnit,
                classContext,
                EnumerableHelper.Empty<string>(),
                method);

            return output;
        }
    }
}
