using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GulfSeal.Areas.Admin.Models.ViewModels
{
    public class InformationTranslationsViewModel
    {
        public List<InformationTranslationsWithLanguagesViewModel> InformationTranslationsWithLanguages { get; set; }
    }

    public class InformationTranslationsWithLanguagesViewModel
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        public InformationTranslation InformationTranslation { get; set; }
    }

}