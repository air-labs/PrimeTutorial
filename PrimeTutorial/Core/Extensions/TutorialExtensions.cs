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

        public static IChain<TRepeaterBigIn, TRepeaterBigOut> CreateRepeater
            <TRepeaterBigIn, TRepeaterBigOut, TChainSmallIn, TChainSmallOut>(
            this IPrimeFactory factory,
            IRepeaterBlock<TRepeaterBigIn, TRepeaterBigOut, TChainSmallIn, TChainSmallOut>
                repeaterBlock,
            IChain<TChainSmallIn, TChainSmallOut> privateChaine)
        {
            var functionalBlock = privateChaine.ToFunctionalBlock();
            return factory.CreateChain(new FunctionalBlock<TRepeaterBigIn, TRepeaterBigOut>(
                input =>
                {
                    repeaterBlock.Start(input);
                    TChainSmallIn smallIn;
                    var smallOut = default(TChainSmallOut);

                    while (repeaterBlock.MakeIteration(smallOut, out smallIn))
                    {
                        smallOut = functionalBlock.Process(smallIn);
                    }

                    return repeaterBlock.Conclude();
                }));
        }
    }
}
