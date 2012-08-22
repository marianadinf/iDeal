using System;
using UIT.iDeal.Common.Web;
using UIT.iDeal.TestLibrary.Browsers.Locators;
using UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework;
using UIT.iDeal.ViewModel.Tasks;

namespace UIT.iDeal.TestLibrary.Browsers.PageObjects.Task
{
    public class AddTaskPage : Page<AddTaskForm>
    {
        public override string PageId
        {
            get { return LocalSiteMap.Navigation.PageIds.Tasks.Create; }
        }

        public override string PageTitle
        {
            get { return LocalSiteMap.PageText.PageTitles.Tasks.Create; }
        }

        public void EnterDescription(string taskDescription)
        {
            Form.EnterTextBoxValue(x => x.Description, taskDescription);
        }

        public TaskListPage ClickOnSave()
        {
            return NavigateTo<TaskListPage>(By.Id(LocalSiteMap.Locators.ActionElementIds.AddTask.SaveButton));
        }

        public void EnterDateToBeCompletedBy(DateTime dateToBeCompletedBy)
        {
            Form.EnterTextBoxValue(x => x.ToBeCompletedByDate, dateToBeCompletedBy);
        }
    }
}
