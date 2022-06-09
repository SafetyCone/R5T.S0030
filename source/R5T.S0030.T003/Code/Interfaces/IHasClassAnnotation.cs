﻿using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace R5T.S0030.T003
{
    public interface IHasClassAnnotation : IHasTypeAnnotation<ClassDeclarationSyntax>
    {
        ISyntaxNodeAnnotation<ClassDeclarationSyntax> ClassAnnotation { get; }
    }
}
