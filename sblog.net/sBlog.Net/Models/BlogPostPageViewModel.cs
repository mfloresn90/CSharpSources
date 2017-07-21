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
using System.Collections.Generic;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Models
{
    public class BlogPostPageViewModel : PagedModel
    {
        public List<PostModel> Posts { get; set; }
        public CategoryEntity Category { get; set; }
        public TagEntity Tag { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string AuthorName { get; set; }
        public string AuthorDisplayName { get; set; }
        
        public string BlogName { get; set; }
        public string BlogCaption { get; set; }

        // For disqus
        public bool DisqusEnabled { get; set; }
        public string ShortName { get; set; }
        public bool DisqusDevMode { get; set; }

        public bool Any
        {
            get
            {
                return Posts != null && Posts.Count > 0;
            }
        }
    }
}
