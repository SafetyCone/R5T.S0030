using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace R5T.S0030.T003
{
    public interface IHasTypeAnnotation : IHasBaseTypeAnnotation
    {
        ISyntaxNodeAnnotation<TypeDeclarationSyntax> TypeAnnotation { get; }
    }


    public interface IHasTypeAnnotation<T> : IHasTypeAnnotation, IHasBaseTypeAnnotation<T>
        where T : TypeDeclarationSyntax
    {
        new ISyntaxNodeAnnotation<T> TypeAnnotation { get; }
    }
}
