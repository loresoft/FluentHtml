using System;

namespace FluentHtml.Html.Input
{
    public class SliderCheckBoxBuilder : InputBuilderBase<SliderCheckBox, SliderCheckBoxBuilder>
    {
        public SliderCheckBoxBuilder(SliderCheckBox component)
            : base(component)
        {
        }

        public SliderCheckBoxBuilder Checked(bool value = true)
        {
            Component.Checked = value;
            return this;
        }

        public SliderCheckBoxBuilder IncludeHidden(bool value = true)
        {
            Component.IncludeHidden = value;
            return this;
        }

        public SliderCheckBoxBuilder TrueText(string value)
        {
            Component.TrueText = value;
            return this;
        }

        public SliderCheckBoxBuilder FalseText(string value)
        {
            Component.FalseText = value;
            return this;
        }

        public virtual SliderCheckBoxBuilder SliderAttribute(string name, object value)
        {
            Component.SliderAttributes[name] = value;
            return this;
        }
    }
}