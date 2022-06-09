using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace R5T.S0030.T003
{
    public interface IHasMemberAnnotation
    {
        ISyntaxNodeAnnotation<MemberDeclarationSyntax> MemberAnnotation { get; }
    }


    public interface IHasMemberAnnotation<T> : IHasMemberAnnotation
        where T : MemberDeclarationSyntax
    {
        new ISyntaxNodeAnnotation<T> MemberAnnotation { get; }
    }
}
