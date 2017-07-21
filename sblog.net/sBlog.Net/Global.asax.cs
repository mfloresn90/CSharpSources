﻿#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StackExchange.Profiling;
using sBlog.Net.Areas.Admin.Models;
using sBlog.Net.Configuration;
using sBlog.Net.CustomExceptions;
using sBlog.Net.CustomViewEngines;
using sBlog.Net.DB.Enumerations;
using sBlog.Net.DB.Helpers;
using sBlog.Net.DB.Services;
using sBlog.Net.DependencyManagement;
using sBlog.Net.Infrastructure;
using sBlog.Net.Mappers;
using sBlog.Net.Models;
using sBlog.Net.Binders;
using System.Web.Security;
using System.Security.Principal;
using sBlog.Net.Filters;

namespace sBlog.Net
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new BlogErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute("Logon", "logon",
                            new { controller = "Account", action = "LogOn" });

            routes.MapRoute("Logoff", "logoff",
                            new { controller = "Account", action = "LogOff" });

            routes.MapRoute("Credits", "credits",
                            new { controller = "ViewPage", action = "Credits" });

            routes.MapRoute("AuthorsPaged", "authors/page/{pageNumber}",
                            new { controller = "Author", action = "AuthorListing" },
                            new { pageNumber = @"\d+" });

            routes.MapRoute("Authors", "authors",
                            new { controller = "Author", action = "AuthorListing" });

            routes.MapRoute("AuthorPostsPaged", "authors/{authorName}/page/{pageNumber}",
                            new { controller = "Author", action = "PostsByAuthor" },
                            new { authorName = @"\S+", pageNumber = @"\d+" });

            routes.MapRoute("AuthorPosts", "authors/{authorName}",
                            new { controller = "Author", action = "PostsByAuthor" },
                            new { authorName = @"\S+" });

            routes.MapRoute("Pages", "pages/{pageUrl}/{status}",
                            new { controller = "ViewPage", action = "Index", status = UrlParameter.Optional },
                            new { pageUrl = @"\S+", status = @"[a-z\-]*" });

            routes.MapRoute("Page", "page/{pageNumber}",
                            new { controller = "Home", action = "Index", pageNumber = UrlParameter.Optional },
                            new { pageNumber = @"\d+" });

            routes.MapRoute("CategoryPaged", "category/{categoryName}/page/{pageNumber}",
                            new { controller = "Home", action = "PostsByCategory" },
                            new { categoryName = @"\S+", pageNumber = @"\d+" });

            routes.MapRoute("Category", "category/{categoryName}",
                            new { controller = "Home", action = "PostsByCategory" },
                            new { categoryName = @"\S+" });

            routes.MapRoute("TagPaged", "tag/{tagName}/page/{pageNumber}",
                            new { controller = "Home", action = "PostsByTag" },
                            new { tagName = @"\S+", pageNumber = @"\d+" });

            routes.MapRoute("Tag", "tag/{tagName}",
                            new { controller = "Home", action = "PostsByTag" },
                            new { tagName = @"\S+" });

            routes.MapRoute("IndividualPost", "{year}/{month}/{url}/{status}",
                            new { controller = "Home", action = "View", status = UrlParameter.Optional },
                            new { year = @"\d{4}", month = @"[0-9]{1,2}", url = @"\S+", status = @"[a-z\-]*" });

            routes.MapRoute("PostByYearMonthPaged", "{year}/{month}/page/{pageNumber}",
                            new { controller = "Home", action = "PostsByYearAndMonth" },
                            new { year = @"\d{4}", month = @"[0-9]{1,2}", pageNumber = @"\d+" });

            routes.MapRoute("PostByYearMonth", "{year}/{month}",
                            new { controller = "Home", action = "PostsByYearAndMonth" },
                            new { year = @"\d{4}", month = @"[0-9]{1,2}" });

            routes.MapRoute("Error404", "404",
                            new { controller = "Errors", action = "Index" });

            routes.MapRoute("SetupError", "under-construction",
                            new {controller = "Maintenance", action = "Index"});

            routes.MapRoute("InvalidTheme", "invalid-theme",
                            new { controller = "Maintenance", action ="InvalidTheme" });

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("NotFound", "{*catchall}",
                            new { controller = "Errors", action = "Index" });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            SetupDependencyManagement();

            RegisterRoutes(RouteTable.Routes);
            RegisterGlobalFilters(GlobalFilters.Filters);

            SetupCustomModelBinders();

            VerifyInstallation();
            
            SetupViewEngines();
        }

        protected void Application_BeginRequest()
        {
            if (Configuration.EnableMiniProfiler && Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
        
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            if (exception is UrlNotFoundException)
            {
                Log(exception);
                Response.Redirect(urlHelper.RouteUrl("Error404"), true);
            }
            else if (exception is SqlException)
            {
                Response.Redirect(urlHelper.RouteUrl("SetupError"), true);
            }
        }

        protected void Session_Start()
        {
            var databaseStatus = (SetupStatus)Application["Installation_Status"];
            if (databaseStatus == null) return;
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            switch (databaseStatus.StatusCode)
            {
                case SetupStatusCode.HasUpdates:
                    Response.Redirect(urlHelper.RouteUrl("UpdateDatabase"), true);
                    break;
                case SetupStatusCode.DatabaseNotSetup:
                    Response.Redirect(urlHelper.RouteUrl("InitializeDatabase"), true);
                    break;
                case SetupStatusCode.DatabaseError:
                    Response.Redirect(urlHelper.RouteUrl("SetupError"), true);
                    break;
            }
        }

        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void SetupDependencyManagement()
        {
            var ninjectControllerFactory = new NinjectControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(ninjectControllerFactory);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(ninjectControllerFactory.GetKernel()));
        }

        private void SetupCustomModelBinders()
        {
            ModelBinders.Binders.Add(typeof(CheckBoxListViewModel), new CheckBoxListViewModelBinder());
            ModelBinders.Binders.Add(typeof(PostViewModel), new PostViewModelBinder());
        }

        /// <summary>
        /// At this point, the engine does not use .aspx or .ascx files.
        /// So let us save some cycles. If at all you decide to use more view engines,
        /// add them here!
        /// </summary>
        private void SetupViewEngines()
        {
            ViewEngines.Engines.Clear();
            var appStatus = (SetupStatus) Application["Installation_Status"];
            if (appStatus == null || appStatus.StatusCode == SetupStatusCode.NoUpdates)
            {
                var settings = InstanceFactory.CreateSettingsInstance();
                var themeName = Configuration.Theme.FindTheme(settings, new PathMapper());
                ViewEngines.Engines.Add(new CustomRazorViewEngine(themeName));
            }
            else
            {
                ViewEngines.Engines.Add(new RazorViewEngine());
            }            
        }

        private void VerifyInstallation()
        {
            var schemaInstance = InstanceFactory.CreateSchemaInstance();
            var pathMapper = InstanceFactory.CreatePathMapperInstance();
            var dbStatusGenerator = new SetupStatusGenerator(schemaInstance, pathMapper);
            Application["Installation_Status"] = dbStatusGenerator.GetSetupStatus();
        }

        private void Log(Exception exception)
        {
            var errorLogger = new ErrorLogger(exception);
            errorLogger.Log();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    var ticket = FormsAuthentication.Decrypt(encTicket);
                    var id = new UserIdentity(ticket);
                    var userRoles = Roles.GetRolesForUser(id.Name);
                    var prin = new GenericPrincipal(id, userRoles);
                    HttpContext.Current.User = prin;
                }
            }
        }

        private static readonly SblogNetSettingsConfiguration Configuration =
            ConfigurationManager.GetSection("sblognetSettings") as SblogNetSettingsConfiguration;
    }
}
