using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using IOperation = R5T.T0098.IOperation;


//#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace R5T.S0030.F002
{
    public static partial class IOperationExtensions
    {
        /// <summary>
        /// Method establishing a framework for parsing text to syntax nodes.
        /// </summary>
        public static TNode Parse<TNode>(this IOperation _,
            Func<string> source,
            Func<string, string> preParse,
            Func<string, TNode> parse,
            Func<TNode, TNode> postParse)
            where TNode : SyntaxNode
        {
            var text = source();

            var preParseText = preParse(text);

            var node = parse(preParseText);

            var output = postParse(node);
            return output;
        }

        #region Asynchronous

        // All input combination of asynchronous and synchronous are provided.

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<Task<string>> source,
            Func<string, string> preParse,
            Func<string, TNode> parse,
            Func<TNode, TNode> postParse)
            where TNode : SyntaxNode
        {
            var text = await source();

            var preParseText = preParse(text);

            var node = parse(preParseText);

            var output = postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<string> source,
            Func<string, Task<string>> preParse,
            Func<string, TNode> parse,
            Func<TNode, TNode> postParse)
            where TNode : SyntaxNode
        {
            var text = source();

            var preParseText = await preParse(text);

            var node = parse(preParseText);

            var output = postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<Task<string>> source,
            Func<string, Task<string>> preParse,
            Func<string, TNode> parse,
            Func<TNode, TNode> postParse)
            where TNode : SyntaxNode
        {
            var text = await source();

            var preParseText = await preParse(text);

            var node = parse(preParseText);

            var output = postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<string> source,
            Func<string, string> preParse,
            Func<string, Task<TNode>> parse,
            Func<TNode, TNode> postParse)
            where TNode : SyntaxNode
        {
            var text = source();

            var preParseText = preParse(text);

            var node = await parse(preParseText);

            var output = postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<Task<string>> source,
            Func<string, string> preParse,
            Func<string, Task<TNode>> parse,
            Func<TNode, TNode> postParse)
            where TNode : SyntaxNode
        {
            var text = await source();

            var preParseText = preParse(text);

            var node = await parse(preParseText);

            var output = postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<string> source,
            Func<string, Task<string>> preParse,
            Func<string, Task<TNode>> parse,
            Func<TNode, TNode> postParse)
            where TNode : SyntaxNode
        {
            var text = source();

            var preParseText = await preParse(text);

            var node = await parse(preParseText);

            var output = postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<Task<string>> source,
            Func<string, Task<string>> preParse,
            Func<string, Task<TNode>> parse,
            Func<TNode, TNode> postParse)
            where TNode : SyntaxNode
        {
            var text = await source();

            var preParseText = await preParse(text);

            var node = await parse(preParseText);

            var output = postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<string> source,
            Func<string, string> preParse,
            Func<string, TNode> parse,
            Func<TNode, Task<TNode>> postParse)
            where TNode : SyntaxNode
        {
            var text = source();

            var preParseText = preParse(text);

            var node = parse(preParseText);

            var output = await postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<Task<string>> source,
            Func<string, string> preParse,
            Func<string, TNode> parse,
            Func<TNode, Task<TNode>> postParse)
            where TNode : SyntaxNode
        {
            var text = await source();

            var preParseText = preParse(text);

            var node = parse(preParseText);

            var output = await postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<string> source,
            Func<string, Task<string>> preParse,
            Func<string, TNode> parse,
            Func<TNode, Task<TNode>> postParse)
            where TNode : SyntaxNode
        {
            var text = source();

            var preParseText = await preParse(text);

            var node = parse(preParseText);

            var output = await postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<Task<string>> source,
            Func<string, Task<string>> preParse,
            Func<string, TNode> parse,
            Func<TNode, Task<TNode>> postParse)
            where TNode : SyntaxNode
        {
            var text = await source();

            var preParseText = await preParse(text);

            var node = parse(preParseText);

            var output = await postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<string> source,
            Func<string, string> preParse,
            Func<string, Task<TNode>> parse,
            Func<TNode, Task<TNode>> postParse)
            where TNode : SyntaxNode
        {
            var text = source();

            var preParseText = preParse(text);

            var node = await parse(preParseText);

            var output = await postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<Task<string>> source,
            Func<string, string> preParse,
            Func<string, Task<TNode>> parse,
            Func<TNode, Task<TNode>> postParse)
            where TNode : SyntaxNode
        {
            var text = await source();

            var preParseText = preParse(text);

            var node = await parse(preParseText);

            var output = await postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<string> source,
            Func<string, Task<string>> preParse,
            Func<string, Task<TNode>> parse,
            Func<TNode, Task<TNode>> postParse)
            where TNode : SyntaxNode
        {
            var text = source();

            var preParseText = await preParse(text);

            var node = await parse(preParseText);

            var output = await postParse(node);
            return output;
        }

        /// <inheritdoc cref="Parse{TNode}(IOperation, Func{string}, Func{string, string}, Func{string, TNode}, Func{TNode, TNode})"/>
        public static async Task<TNode> Parse<TNode>(this IOperation _,
            Func<Task<string>> source,
            Func<string, Task<string>> preParse,
            Func<string, Task<TNode>> parse,
            Func<TNode, Task<TNode>> postParse)
            where TNode : SyntaxNode
        {
            var text = await source();

            var preParseText = await preParse(text);

            var node = await parse(preParseText);

            var output = await postParse(node);
            return output;
        }

        #endregion
    }
}