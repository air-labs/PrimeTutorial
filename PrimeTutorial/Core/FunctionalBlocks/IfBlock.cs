using System;
using Prime;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    class IfBlock<TIn, TOut> : IFunctionalBlock<TIn, TOut>
    {
        public Func<TIn, bool> Condition;
        public Func<TIn, TOut> ThenBlock;

        public TOut ElseValue;

        public TOut Process(TIn argument)
        {
            if (Condition(argument))
            {
                return ThenBlock(argument);
            }

            return ElseValue;
        }
    }
}
