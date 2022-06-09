using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using IOperation = R5T.T0098.IOperation;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    using N8;


    public static partial class IOperationExtensions
    {
        /// <summary>
        /// <inheritdoc cref="N8.IOperationExtensions.PostParse_ForTypes_20220420{T}(IOperation, T)" path="/summary"/>
        /// Chooses <see cref="N8.IOperationExtensions.PostParse_ForTypes_20220420{T}(IOperation, T)"/>.
        /// </summary>
        public static T PostParse_ForTypes<T>(this IOperation _,
            T typeDeclaration)
            where T : TypeDeclarationSyntax
        {
            var output = _.PostParse_ForTypes_20220420(typeDeclaration);
            return output;
        }
    }
}


namespace R5T.S0030.F002.N8
{
    public static partial class IOperationExtensions
    {
        /// <summary>
        /// Post-parse actions that should be run on all created type declarations, as of 20220420.
        /// </summary>
        public static T PostParse_ForTypes_20220420<T>(this IOperation _,
            T typeDeclaration)
            where T : TypeDeclarationSyntax
        {
            typeDeclaration = typeDeclaration
                .PostParse_ForSyntaxNode()
                .EnsureHasBraces()
                ;

            return typeDeclaration;
        }
    }
}