using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace UIT.iDeal.Common.Web
{
    public static class LocalSiteMap
    {
        public static MvcHtmlString SetUpPageIdWith(this HtmlHelper htmlHelper, string pageId)
        {
            return htmlHelper.Hidden("pageId", pageId);
        }
        
        public static class Navigation
        {
            public static class PageIds
            {
               

                public static class Tasks
                {
                    public static readonly string Index = "taskList";

                    public static readonly string Create = "addTask";

                    public static readonly string Delete = "deleteTask";
                }
                public static class Users
                {
                    public static readonly string Index = "usersList";
                    public static readonly string Create = "addUser";
                }

                public static class Home
                {
                    public static readonly string Index = "home";
                }
            }
           
        }

        public static class Locators
        {
            public static class PanelIds
            {
                public static class AddUser
                {
                    public static readonly string Fieldset = "CreateUserFieldset";
                }
            }

            public static class ActionElementIds
            {
                public static class Tasks
                {
                    public static readonly string AddTaskButton = "AddTaskButton";
                }

                public static class AddTask
                {
                    public static readonly string SaveButton = "CreateTaskButton";
                }

                public static class AddUser
                {
                    public static readonly string AddUserButton = "AddUserButton";
                    public static readonly string BackToUserList = "BackToUserList";
                }

            }

            public static class TextElementIds
            {
               
            }

            public static class CssElementClass
            {

               
            }

            public static class Grids
            {
                public static readonly string Tasks = "TaskList";
                public static readonly string Users = "UserList";
                public static readonly string ApplicationPermissions = "ApplicationPermissionList";
            }

            
        }

        public static class PageText
        {
            public static class PageTitles
            {
                public static class Tasks
                {
                    public static readonly string Index = "Tasks List";
                    public static readonly string Create = "Add a new Task";
                }

                public static class Users
                {
                    public static readonly string Index = "Users List";
                    public static readonly string Create = "Add a new User";
                }

               
            }

            public static class DOMElements
            {
               
            }

            
        }

    }
}