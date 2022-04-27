using System;

using Microsoft.CodeAnalysis;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    using N8;


    public static class SyntaxNodeExtensions
    {
        /// <summary>
        /// The latest post-creation actions that should be run on all created syntax nodes.
        /// Chooses <see cref="N8.SyntaxNodeExtensions.PostParse_ForSyntaxNode_20220420{TNode}(TNode)"/>.
        /// </summary>
        public static TNode PostParse_ForSyntaxNode<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            var output = node.PostParse_ForSyntaxNode_20220420();
            return output;
        }
    }
}


namespace R5T.S0030.F002.N8
{
    public static class SyntaxNodeExtensions
    {
        /// <summary>
        /// Post-creation actions that should be run on all created syntax nodes, as of 20220420.
        /// </summary>
        public static TNode PostParse_ForSyntaxNode_20220420<TNode>(this TNode node)
            where TNode : SyntaxNode
        {
            node = node
                .NormalizeWhitespace()
                // Standardize to leading trivia.
                .MoveDescendantTrailingTriviaToLeadingTrivia()
                ;

            return node;
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
