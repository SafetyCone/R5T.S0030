using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.S0030.T003.N004;

using Instances = R5T.S0030.T003.Instances;


namespace System
{
    public static partial class IHasClassContextExtensions
    {
        public static CompilationUnitSyntax AddUsings(this IHasClassContext hasClassContext,
            CompilationUnitSyntax compilationUnit,
            params string[] namespaceNames)
        {
            // Any namespace names reachable from the class context's own namespace do not need to be added as a using namespace directive.
            var classContextNamespaceName = hasClassContext.ClassContext_N004.NamespaceContext_N002.Annotation.Get(
                compilationUnit,
                @namespace => @namespace.GetFullName());

            var namespaceNamesToAdd = namespaceNames
                .Where(x => !Instances.NamespaceNameOperator.IsReachableFrom(
                    x,
                    classContextNamespaceName))
                .Now();

            var output = hasClassContext.ClassContext_N004.CompilationUnitContext_N001.AddUsings(
                compilationUnit,
                namespaceNamesToAdd);

            return output;
        }

        public static CompilationUnitSyntax AddUsings(this IHasClassContext _,
            CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var output = _.AddUsings(
                compilationUnit,
                namespaceNames.ToArray());
            
            return output;
        }

        //public static CompilationUnitSyntax AddUsings(this IHasClassContext hasClassContext,
        //    CompilationUnitSyntax compilationUnit,
        //    params (string DestinationName, string SourceNameExpression)[] nameAliasValues)
        //{
        //    var output = hasClassContext.ClassContext_N004.CompilationUnitContext_N001.AddUsings(
        //        compilationUnit,
        //        nameAliasValues);

        //    return output;
        //}

        ///// <summary>
        ///// Gets the line indentation of class signature line (line indentation includes any leading new line).
        ///// See also: <seealso cref="GetLineIndentation"/>.
        ///// </summary>
        //public static SyntaxTriviaList GetLineIndentation<TNode>(this IHasClassContext hasClassContext,
        //    TNode classNodeOrParent)
        //    where TNode : SyntaxNode
        //{
        //    var output = hasClassContext.ClassContext_N004.ClassContext_N003.GetLineIndentation(classNodeOrParent);
        //    return output;
        //}

        ///// <summary>
        ///// Gets the indentation of class signature line (line indentation minus any leading new line).
        ///// See also: <seealso cref="GetLineIndentation"/>.
        ///// </summary>
        //public static SyntaxTriviaList GetIndentation<TNode>(this IHasClassContext hasClassContext,
        //    TNode classNodeOrParent)
        //    where TNode : SyntaxNode
        //{
        //    var output = hasClassContext.ClassContext_N004.ClassContext_N003.GetIndentation(classNodeOrParent);
        //    return output;
        //}
    }
}
