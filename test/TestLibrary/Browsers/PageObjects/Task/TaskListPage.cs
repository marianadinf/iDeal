using UIT.iDeal.Common.Web;
using UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework;
using UIT.iDeal.ViewModel.Tasks;
using By = UIT.iDeal.TestLibrary.Browsers.Locators.By;
namespace UIT.iDeal.TestLibrary.Browsers.PageObjects.Task
{
    public class TaskListPage : Page<TaskItemViewModel>
    {
        public GridComponent<TaskItemViewModel> Grid
        {
            get { return ForGrid<TaskItemViewModel>(LocalSiteMap.Locators.Grids.Tasks); }
        }

        public override string PageId
        {
            get { return LocalSiteMap.Navigation.PageIds.Tasks.Index; }
        }

        public override string PageTitle
        {
            get { return LocalSiteMap.PageText.PageTitles.Tasks.Index; }
        }

        public AddTaskPage ClickOnAddTask()
        {
            return NavigateTo<AddTaskPage>(By.Id(LocalSiteMap.Locators.ActionElementIds.Tasks.AddTaskButton));
        }
    }
}
