using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

using R5T.Magyar;


namespace R5T.S0030.F004
{
    public static class ModifiersHelper
    {
        private static Func<ModiferGrid, WasFound<SyntaxToken>>[] GetModifierActions { get; } = ModifiersHelper.GetGetModifierActions();


        /// <summary>
        /// Get all functions needed to return modifiers based on values in a modifier grid.
        /// </summary>
        private static Func<ModiferGrid, WasFound<SyntaxToken>>[] GetGetModifierActions()
        {
            var output = new List<Func<ModiferGrid, WasFound<SyntaxToken>>>
            {
                ModifiersHelper.GetGetModifierAction(x => x.Abstract, Instances.SyntaxFactory.Abstract),
                ModifiersHelper.GetGetModifierAction(x => x.Async, Instances.SyntaxFactory.Async),
                ModifiersHelper.GetGetModifierAction(x => x.Const, Instances.SyntaxFactory.Const),
                ModifiersHelper.GetGetModifierAction(x => x.Extern, Instances.SyntaxFactory.Extern),
                ModifiersHelper.GetGetModifierAction(x => x.Internal, Instances.SyntaxFactory.Internal),
                ModifiersHelper.GetGetModifierAction(x => x.Override, Instances.SyntaxFactory.Override),
                ModifiersHelper.GetGetModifierAction(x => x.Private, Instances.SyntaxFactory.Private),
                ModifiersHelper.GetGetModifierAction(x => x.Protected, Instances.SyntaxFactory.Protected),
                ModifiersHelper.GetGetModifierAction(x => x.Public, Instances.SyntaxFactory.Public),
                ModifiersHelper.GetGetModifierAction(x => x.ReadOnly, Instances.SyntaxFactory.ReadOnly),
                ModifiersHelper.GetGetModifierAction(x => x.Sealed, Instances.SyntaxFactory.Sealed),
                ModifiersHelper.GetGetModifierAction(x => x.Static, Instances.SyntaxFactory.Static),
                ModifiersHelper.GetGetModifierAction(x => x.Unsafe, Instances.SyntaxFactory.Unsafe),
                ModifiersHelper.GetGetModifierAction(x => x.Virtual, Instances.SyntaxFactory.Virtual),
                ModifiersHelper.GetGetModifierAction(x => x.Volatile, Instances.SyntaxFactory.Volatile),
            }
            .ToArray();

            return output;
        }

        /// <summary>
        /// Gets a function that will return a modifier based on the value in a modifer grid.
        /// </summary>
        private static Func<ModiferGrid, WasFound<SyntaxToken>> GetGetModifierAction(
            Func<ModiferGrid, bool> addModifierPredicate,
            Func<SyntaxToken> modifierConstructor)
        {
            return modifierGrid =>
            {
                var addModifier = addModifierPredicate(modifierGrid);
                if(addModifier)
                {
                    var modifier = modifierConstructor();

                    return WasFound.Found(modifier);
                }
                else
                {
                    return WasFound.NotFound<SyntaxToken>();
                }
            };
        }

        public static SyntaxTokenList GetModifers(ModiferGrid modiferGrid)
        {
            var output = ModifiersHelper.GetGetModifierActions()
                .Select(xGetModiferAction => xGetModiferAction(modiferGrid))
                .Where(xModifierWasFound => xModifierWasFound.Exists)
                .Select(xModiferWasFound => xModiferWasFound.Result)
                .ToSyntaxTokenList();

            return output;
        }
    }
}
