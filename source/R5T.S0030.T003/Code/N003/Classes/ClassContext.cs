using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace R5T.S0030.T003.N003
{
    public class ClassContext : IClassContext
    {
        public ISyntaxNodeAnnotation<ClassDeclarationSyntax> Annotation { get; set; }

        ISyntaxNodeAnnotation<ClassDeclarationSyntax> IHasClassAnnotation.ClassAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<ClassDeclarationSyntax> IHasTypeAnnotation<ClassDeclarationSyntax>.TypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<ClassDeclarationSyntax> IHasBaseTypeAnnotation<ClassDeclarationSyntax>.BaseTypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<ClassDeclarationSyntax> IHasMemberAnnotation<ClassDeclarationSyntax>.MemberAnnotation => this.Annotation;

        ISyntaxNodeAnnotation<TypeDeclarationSyntax> IHasTypeAnnotation.TypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<BaseTypeDeclarationSyntax> IHasBaseTypeAnnotation.BaseTypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<MemberDeclarationSyntax> IHasMemberAnnotation.MemberAnnotation => this.Annotation;

        IClassContext IHasClassContext.ClassContext_N003 => this;
    }
}
