using System;
using Prime;
using PrimeTutorial.Core.FunctionalBlocks;

namespace PrimeTutorial.Core.Extensions
{
    public static class TutorialExtensions
    {
        public static IChain<TIn, TOut> IfThenElse<TIn, TOut>(this IPrimeFactory factory, Func<TIn, bool> condition, IChain<TIn, TOut> thenChain, TOut elseValue)
        {
            var ifBlock = new IfBlock<TIn, TOut>
            {
                Condition = condition,
                ElseValue = elseValue,
                ThenBlock = thenChain.ToFunctionalBlock().Process
            };

            return factory.CreateChain(ifBlock);
        }
    }
}
