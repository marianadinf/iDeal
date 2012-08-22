using System;
using System.Web;
using System.Web.Mvc;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.TestLibrary.FakeObjects
{
    public class FakeController : BaseController
    {
        public ViewResult ActionThatThrowsAnException()
        {
            throw new Exception("FakeController action threw an exception");
        }

        public ActionResult ActionThatDoesNothing()
        {
            return new EmptyResult();
        }

        public ActionResult ActionThatFires403()
        {
            throw new HttpException(403, "Not Found");
        }

    }
}