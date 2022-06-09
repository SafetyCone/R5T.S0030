using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace R5T.S0030.T003
{
    public interface IHasBaseTypeAnnotation : IHasMemberAnnotation
    {
        ISyntaxNodeAnnotation<BaseTypeDeclarationSyntax> BaseTypeAnnotation { get; }
    }


    public interface IHasBaseTypeAnnotation<T> : IHasBaseTypeAnnotation, IHasMemberAnnotation<T>
        where T : BaseTypeDeclarationSyntax
    {
        new ISyntaxNodeAnnotation<T> BaseTypeAnnotation { get; }
    }
}
