using System;
using System.ComponentModel;

namespace FluentHtml.Html.Input
{
    public enum InputType
    {
        [Description("text")]
        Text,
        [Description("password")]
        Password,
        [Description("checkbox")]
        Checkbox,
        [Description("radio")]
        Radio,
        [Description("button")]
        Button,
        [Description("submit")]
        Submit,
        [Description("reset")]
        Reset,
        [Description("file")]
        File,
        [Description("hidden")]
        Hidden,
        [Description("image")]
        Image,

        /* new HTML 5 */
        [Description("datetime")]
        DateTime,
        [Description("datetime-local")]
        DateTimeLocal,
        [Description("date")]
        Date,
        [Description("month")]
        Month,
        [Description("time")]
        Time,
        [Description("week")]
        Week,
        [Description("number")]
        Number,
        [Description("range")]
        Range,
        [Description("email")]
        Email,
        [Description("url")]
        Url,
        [Description("search")]
        Search,
        [Description("tel")]
        Tel,
        [Description("color")]
        Color,
    }
}