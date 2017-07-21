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
using System.Web.Mvc;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.DependencyManagement
{
    public class InstanceFactory
    {
        public static IUser CreateUserInstance()
        {
            return DependencyResolver.Current.GetService<IUser>();
        }

        public static ISettings CreateSettingsInstance()
        {
            return DependencyResolver.Current.GetService<ISettings>();
        }

        public static IError CreateErrorInstance()
        {
            return DependencyResolver.Current.GetService<IError>();
        }

        public static IRole CreateRoleInstance()
        {
            return DependencyResolver.Current.GetService<IRole>();
        }

        public static ISchema CreateSchemaInstance()
        {
            return DependencyResolver.Current.GetService<ISchema>();
        }

        public static IPathMapper CreatePathMapperInstance()
        {
            return DependencyResolver.Current.GetService<IPathMapper>();
        }
    }
}
