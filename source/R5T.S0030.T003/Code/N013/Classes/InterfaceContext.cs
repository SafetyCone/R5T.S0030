using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;

using R5T.S0030.T003.N001;
using R5T.S0030.T003.N002;


namespace R5T.S0030.T003.N013
{
    public class InterfaceContext : IInterfaceContext
    {
        public ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> Annotation => this.InterfaceContext_N012.Annotation;

        ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> IHasInterfaceAnnotation.InterfaceAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> IHasTypeAnnotation<InterfaceDeclarationSyntax>.TypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> IHasBaseTypeAnnotation<InterfaceDeclarationSyntax>.BaseTypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> IHasMemberAnnotation<InterfaceDeclarationSyntax>.MemberAnnotation => this.Annotation;

        ISyntaxNodeAnnotation<TypeDeclarationSyntax> IHasTypeAnnotation.TypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<BaseTypeDeclarationSyntax> IHasBaseTypeAnnotation.BaseTypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<MemberDeclarationSyntax> IHasMemberAnnotation.MemberAnnotation => this.Annotation;

        public ICompilationUnitContext CompilationUnitContext_N001 { get; set; }
        public N012.IInterfaceContext InterfaceContext_N012 { get; set; }
        public INamespaceContext NamespaceContext_N002 { get; set; }

        public IInterfaceContext InterfaceContext_N013 => this;
    }
}
