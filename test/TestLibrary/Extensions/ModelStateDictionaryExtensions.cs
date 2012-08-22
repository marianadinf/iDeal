using System.Web.Mvc;
using FluentAssertions;

namespace UIT.iDeal.TestLibrary.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static void ShouldHaveErrorMessage(this ModelStateDictionary modelState, string key, string errormessage)
        {
            modelState.IsValid.Should().BeFalse();
            modelState.ContainsKey(key).Should().BeTrue();
            modelState[key].Errors[0].ErrorMessage.Should().Be(errormessage);
        }
    }
}
