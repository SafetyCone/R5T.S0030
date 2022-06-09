using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;

using R5T.S0030.T003.N001;
using R5T.S0030.T003.N002;


namespace R5T.S0030.T003.N004
{
    public class ClassContext : IClassContext
    {
        public ISyntaxNodeAnnotation<ClassDeclarationSyntax> Annotation => this.ClassContext_N003.Annotation;

        ISyntaxNodeAnnotation<ClassDeclarationSyntax> IHasClassAnnotation.ClassAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<ClassDeclarationSyntax> IHasTypeAnnotation<ClassDeclarationSyntax>.TypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<ClassDeclarationSyntax> IHasBaseTypeAnnotation<ClassDeclarationSyntax>.BaseTypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<ClassDeclarationSyntax> IHasMemberAnnotation<ClassDeclarationSyntax>.MemberAnnotation => this.Annotation;

        ISyntaxNodeAnnotation<TypeDeclarationSyntax> IHasTypeAnnotation.TypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<BaseTypeDeclarationSyntax> IHasBaseTypeAnnotation.BaseTypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<MemberDeclarationSyntax> IHasMemberAnnotation.MemberAnnotation => this.Annotation;

        public ICompilationUnitContext CompilationUnitContext_N001 { get; set; }
        public INamespaceContext NamespaceContext_N002 { get; set; }
        public N003.IClassContext ClassContext_N003 { get; set; }

        IClassContext IHasClassContext.ClassContext_N004 => this;

    }
}
