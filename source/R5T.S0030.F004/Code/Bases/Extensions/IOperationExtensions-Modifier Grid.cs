using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0098;


namespace R5T.S0030.F004
{
    public static partial class IOperationExtensions
    {
        public static ModiferGrid GetModiferGrid(this MemberDeclarationSyntax member)
        {
            var output = new ModiferGrid
            {
                Abstract = member.IsAbstract(),
                Async = member.IsAsync(),
                Const = member.IsConst(),
                Extern = member.IsExtern(),
                Internal = member.IsInternal(),
                Override = member.IsOverride(),
                Private = member.IsPrivate(),
                Protected = member.IsProtected(),
                Public = member.IsPublic(),
                ReadOnly = member.IsReadOnly(),
                Sealed = member.IsSealed(),
                Static = member.IsStatic(),
                Unsafe = member.IsUnsafe(),
                Virtual = member.IsVirtual(),
                Volatile = member.IsVolatile(),
            };

            return output;
        }
    }
}