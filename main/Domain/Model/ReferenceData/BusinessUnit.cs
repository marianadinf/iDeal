namespace UIT.iDeal.Domain.Model.ReferenceData
{
    public class BusinessUnit : UserRelatedReferenceData
    {
        internal static BusinessUnit Create(string  description)
        {
            return new BusinessUnit
            {
                Description = description
            };
        }
    }


}