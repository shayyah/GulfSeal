using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GulfSeal.Areas.Admin.Models.ViewModels
{
    public class ArticleContentsViewModels
    {  
        public List<ArticleContentsWithLanguagesViewModel> ArticleContentsWithLanguages { get; set; }
    }

    public class ArticleContentsWithLanguagesViewModel
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        public ArticleContent ArticleContents { get; set; }
    }
}