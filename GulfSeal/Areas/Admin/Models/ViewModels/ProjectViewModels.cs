using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GulfSeal.Areas.Admin.Models.ViewModels
{
    public class ProjectTranslationsViewModel
    {
        public List<ProjectTranslationsWithLanguagesViewModel> ProjectTranslationsWithLanguages { get; set; }
    }

    public class ProjectTranslationsWithLanguagesViewModel
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        public ProjectTranslation ProjectTranslation { get; set; }
    }
}