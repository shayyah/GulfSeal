using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GulfSeal.Areas.Admin.Models.ViewModels
{
    public class StaticTranslationsViewModel
    {
        public List<StaticTranslationsWithLanguagesViewModel> StaticTranslationsWithLanguages { get; set; }
    }

    public class StaticTranslationsWithLanguagesViewModel
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        public StaticContentTranslation StaticContentTranslation { get; set; }
    }
}