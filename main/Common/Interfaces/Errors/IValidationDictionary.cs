using System;
using System.Collections.Generic;

namespace UIT.iDeal.Common.Interfaces
{
    public interface IValidationDictionary : IEnumerable<KeyValuePair<string, IEnumerable<string>>>
    {

        #region Properties

        Boolean IsValid { get; }

        #endregion

        #region Public Methods

        void Clear();
        void AddError(String key, String errorMessage);
        void AddErrors(String key, IEnumerable<String> errorMessages);

        #endregion
    }
}