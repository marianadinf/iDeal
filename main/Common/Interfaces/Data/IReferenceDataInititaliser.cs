namespace UIT.iDeal.Common.Interfaces.Data
{
    public interface IReferenceDataInititaliser<in TContext>
        where TContext:class
    {
        void Populate(TContext context);
    }
}