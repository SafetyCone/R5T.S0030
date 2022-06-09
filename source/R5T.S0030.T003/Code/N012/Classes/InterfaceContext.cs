using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace R5T.S0030.T003.N012
{
    public class InterfaceContext : IInterfaceContext
    {
        public ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> Annotation { get; set; }

        ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> IHasInterfaceAnnotation.InterfaceAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> IHasTypeAnnotation<InterfaceDeclarationSyntax>.TypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> IHasBaseTypeAnnotation<InterfaceDeclarationSyntax>.BaseTypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<InterfaceDeclarationSyntax> IHasMemberAnnotation<InterfaceDeclarationSyntax>.MemberAnnotation => this.Annotation;

        ISyntaxNodeAnnotation<TypeDeclarationSyntax> IHasTypeAnnotation.TypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<BaseTypeDeclarationSyntax> IHasBaseTypeAnnotation.BaseTypeAnnotation => this.Annotation;
        ISyntaxNodeAnnotation<MemberDeclarationSyntax> IHasMemberAnnotation.MemberAnnotation => this.Annotation;

        public IInterfaceContext InterfaceContext_N012 => this;
    }
}
