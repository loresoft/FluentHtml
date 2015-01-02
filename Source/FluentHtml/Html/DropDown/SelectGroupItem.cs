using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FluentHtml.Html.DropDown
{
    public class SelectGroupItem
    {
        public SelectGroupItem()
        {
            Items = new List<SelectListItem>();
        }

        public string Label { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}