using System;

namespace SprocRigger
{
    public interface IDataInstruction
    {
        IDataInstruction Use(ISprocInstanceBase sprocInstance);
        IDataInstruction Use(Func<ISprocInstanceBase> sprocInstance);
        void Execute();
    }
}