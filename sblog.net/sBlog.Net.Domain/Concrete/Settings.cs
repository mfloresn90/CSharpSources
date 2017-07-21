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
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;
using System.Data.Linq;

namespace sBlog.Net.Domain.Concrete
{
    public class Settings : ISettings
    {
        private Table<SettingsEntity> BlogSettings { get; set; }
        private readonly DataContext _context;
        private readonly string _connectionString;

        public Settings()
        {
            _connectionString = ApplicationDomainConfiguration.ConnectionString;
            _context = new DataContext(_connectionString);
            LoadSettings();
        }

        public string BlogName
        {
            get
            {
                var blogName = GetValue("BlogName");
                return blogName ?? "sBlog.Net";
            }
            set
            {
                var blogName = GetValueInternal(value) ?? "sBlog.Net";
                UpdateSettings("BlogName", blogName);
            }
        }

        public string BlogCaption
        {
            get
            {
                var blogCaption = GetValue("BlogCaption");
                return blogCaption ?? "Just another .net site";
            }
            set
            {
                var blogCaption = GetValueInternal(value) ?? "Just another .net site";
                UpdateSettings("BlogCaption", blogCaption);
            }
        }

        public int BlogPostsPerPage
        {
            get
            {
                int postsPerPage;
                if (!Int32.TryParse(GetValue("BlogPostsPerPage"), out postsPerPage))
                    postsPerPage = 5;
                return postsPerPage;
            }
            set
            {
                UpdateSettings("BlogPostsPerPage", value.ToString());
            }
        }

        public string BlogTheme
        {
            get
            {
                var blogTheme = GetValue("BlogTheme");
                return blogTheme ?? "PerfectBlemish";
            }
            set
            {
                var blogTheme = GetValueInternal(value) ?? "PerfectBlemish";
                UpdateSettings("BlogTheme", blogTheme);
            }
        }

        public bool BlogSocialSharing
        {
            get
            {
                var blogSharing = GetValue("BlogSocialSharing");
                bool result;
                if (!bool.TryParse(blogSharing, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogSocialSharing", value.ToString());
            }
        }

        public bool BlogSyntaxHighlighting
        {
            get
            {
                var blogSharing = GetValue("BlogSyntaxHighlighting");
                bool result;
                if (!bool.TryParse(blogSharing, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogSyntaxHighlighting", value.ToString());
            }
        }

        public string BlogSyntaxTheme
        {
            get
            {
                var blogTheme = GetValue("BlogSyntaxTheme");
                return blogTheme ?? "Default";
            }
            set
            {
                var blogTheme = GetValueInternal(value) ?? "Default";
                UpdateSettings("BlogSyntaxTheme", blogTheme);
            }
        }
         
        public string BlogSyntaxScripts
        {
            get
            {
                var blogSyntaxScripts = GetValue("BlogSyntaxScripts");
                return blogSyntaxScripts ?? "CSharp";
            }
            set
            {
                var blogSyntaxScripts = GetValueInternal(value) ?? "CSharp";
                UpdateSettings("BlogSyntaxScripts", blogSyntaxScripts);
            }
        }

        public bool BlogAkismetEnabled
        {
            get
            {
                var blogAkismetEnabled = GetValue("BlogAkismetEnabled");
                bool result;
                if (!bool.TryParse(blogAkismetEnabled, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogAkismetEnabled", value.ToString());
            }
        }

        public string BlogAkismetKey
        {
            get
            {
                var blogAkismetKey = GetValue("BlogAkismetKey");
                return blogAkismetKey ?? string.Empty;
            }
            set
            {
                var blogAkismetKey = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogAkismetKey", blogAkismetKey);
            }
        }

        public string BlogAkismetUrl
        {
            get
            {
                var blogAkismetUrl = GetValue("BlogAkismetUrl");
                return blogAkismetUrl ?? string.Empty;
            }
            set
            {
                var blogAkismetUrl = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogAkismetUrl", blogAkismetUrl);
            }
        }

        public bool BlogAkismetDeleteSpam
        {
            get
            {
                var blogAkismetDeleteSpam = GetValue("BlogAkismetDeleteSpam");
                bool result;
                if (!bool.TryParse(blogAkismetDeleteSpam, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogAkismetDeleteSpam", value.ToString());
            }
        }

        public int BlogSocialSharingChoice
        {
            get
            {
                int blogSocialSharingChoice;
                if (!Int32.TryParse(GetValue("BlogSocialSharingChoice"), out blogSocialSharingChoice))
                    blogSocialSharingChoice = 2;
                return blogSocialSharingChoice;
            }
            set
            {
                UpdateSettings("BlogSocialSharingChoice", value.ToString());
            }
        }

        public bool BlogSiteErrorEmailAction
        {
            get
            {
                var blogSiteErrorAction = GetValue("BlogSiteErrorEmailAction");
                bool result;
                if (!bool.TryParse(blogSiteErrorAction, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("BlogSiteErrorEmailAction", value.ToString());
            }
        }

        public string BlogAdminEmailAddress
        {
            get
            {
                var blogEmail = GetValue("BlogAdminEmailAddress");
                return blogEmail ?? string.Empty;
            }
            set
            {
                var blogEmail = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogAdminEmailAddress", blogEmail);
            }
        }

        public string BlogSmtpAddress
        {
            get
            {
                var smtpAddress = GetValue("BlogSmtpAddress");
                return smtpAddress ?? string.Empty;
            }
            set
            {
                var smtpAddress = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogSmtpAddress", smtpAddress);
            }
        }

        public string BlogSmtpPassword
        {
            get
            {
                var smtpAddress = GetValue("BlogSmtpPassword");
                return smtpAddress ?? string.Empty;
            }
            set
            {
                var smtpAddress = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogSmtpPassword", smtpAddress);
            }
        }

        public bool InstallationComplete
        {
            get
            {
                var installationComplete = GetValue("InstallationComplete");
                bool result;
                if (!bool.TryParse(installationComplete, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("InstallationComplete", value.ToString());
            }
        }

        public int ManageItemsPerPage
        {
            get
            {
                int manageItemsPerPage;
                if (!Int32.TryParse(GetValue("ManageItemsPerPage"), out manageItemsPerPage))
                    manageItemsPerPage = 5;
                return manageItemsPerPage;
            }
            set
            {
                UpdateSettings("ManageItemsPerPage", value.ToString());
            }
        }

        public bool DisqusEnabled
        {
            get
            {
                var disqusEnabled = GetValue("DisqusEnabled");
                bool result;
                if (!bool.TryParse(disqusEnabled, out result))
                    result = false;
                return result;
            }
            set
            {
                UpdateSettings("DisqusEnabled", value.ToString());
            }
        }

        public string BlogDisqusShortName
        {
            get
            {
                var disqusShortName = GetValue("BlogDisqusShortName");
                return disqusShortName ?? string.Empty;
            }
            set
            {
                var disqusShortName = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("BlogDisqusShortName", disqusShortName);
            }
        }

        public string EditorType
        {
            get
            {
                var editorType = GetValue("EditorType");
                return editorType ?? string.Empty;
            }
            set
            {
                var editorType = GetValueInternal(value) ?? string.Empty;
                UpdateSettings("EditorType", editorType);
            }
        }

        public string GetValue(string key)
        {
            string value = null;
            var setting = BlogSettings.SingleOrDefault(s => s.KeyName == key);
            if (setting != null)
                value = setting.KeyValue;
            return value;
        }

        public bool UpdateSettings(string key, string value)
        {
            var setting = BlogSettings.Single(s => s.KeyName == key);
            if (setting != null)
            {
                setting.KeyValue = value;
                _context.SubmitChanges();
                return true;
            }
            return false;
        }

        private void LoadSettings()
        {
            BlogSettings = _context.GetTable<SettingsEntity>();
        }

        private static string GetValueInternal(string value)
        {
            var valueInternal = value;
            if (value == null || value.Trim() == string.Empty)
                valueInternal = null;
            return valueInternal;
        }
    }
}
