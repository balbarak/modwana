using Modwana.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core.Entities
{
    [Owned]
    public class LocaleString
    {
        public string Arabic { get; set; }

        public string English { get; set; }

        public LocaleString()
        {

        }

        public LocaleString(string arabic, string english)
        {
            Arabic = arabic;
            English = english;
        }

        public override string ToString()
        {

            if (Language.IsEnglish)
                return English;
            else
                return Arabic;

        }
    }
}
