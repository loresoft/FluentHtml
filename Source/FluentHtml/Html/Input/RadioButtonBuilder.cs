using System;

namespace FluentHtml.Html.Input
{
    public class RadioButtonBuilder : InputBuilderBase<RadioButton, RadioButtonBuilder>
    {
        public RadioButtonBuilder(RadioButton component)
            : base(component)
        {
        }

        public RadioButtonBuilder Checked(bool value = true)
        {
            Component.Checked = value;

            return this;
        }
    }
}