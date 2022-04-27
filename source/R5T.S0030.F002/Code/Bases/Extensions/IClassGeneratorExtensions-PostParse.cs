using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.B0006;


namespace R5T.S0030.F002
{
    using N8;


    public static partial class IClassGeneratorExtensions
    {
        /// <summary>
        /// Chooses <see cref="N8.IClassGeneratorExtensions.PostParse_20220420(IClassGenerator, ClassDeclarationSyntax)"/>.
        /// </summary>
        public static ClassDeclarationSyntax PostParse(this IClassGenerator _,
            ClassDeclarationSyntax classDeclaration)
        {
            var output = _.PostParse_20220420(classDeclaration);
            return output;
        }
    }
}


namespace R5T.S0030.F002.N8
{
    public static partial class IClassGeneratorExtensions
    {
        /// <summary>
        /// Post-creation actions that should be run on all created class declarations, as of 20220420.
        /// </summary>
        public static ClassDeclarationSyntax PostParse_20220420(this IClassGenerator _,
            ClassDeclarationSyntax classDeclaration)
        {
            classDeclaration = classDeclaration
                .PostParse_ForSyntaxNode()
                .EnsureHasBraces()
                ;

            return classDeclaration;
        }
    }
}
