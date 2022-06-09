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
