using System;
using System.ComponentModel;

namespace FluentHtml.Html.Tag
{
    public enum ButtonType
    {
        [Description("button")]
        Button,
        [Description("submit")]
        Submit,
        [Description("reset")]
        Reset
    }
}