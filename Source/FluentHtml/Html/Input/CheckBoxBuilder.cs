using System;

namespace FluentHtml.Html.Input
{
    public class CheckBoxBuilder : InputBuilderBase<CheckBox, CheckBoxBuilder>
    {
        public CheckBoxBuilder(CheckBox component)
            : base(component)
        {
        }

        public CheckBoxBuilder Checked(bool value = true)
        {
            Component.Checked = value;

            return this;
        }

        public CheckBoxBuilder IncludeHidden(bool value = true)
        {
            Component.IncludeHidden = value;

            return this;
        }

    }
}