using UIT.iDeal.Common.Errors;

namespace UIT.iDeal.Common.Interfaces.Forms
{
    public interface IFormProcessor
    {
        ExecutionResult Execute<TForm>(TForm form) 
            where TForm : class;
    }
}
