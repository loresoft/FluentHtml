using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace FluentHtml.Html.Input
{
    public class SliderCheckBox : CheckBox
    {
        public SliderCheckBox(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            SliderAttributes = new RouteValueDictionary();
            IncludeHidden = false;
            TrueText = "On";
            FalseText = "Off";
        }

        public IDictionary<string, object> SliderAttributes { get; private set; }

        public string TrueText { get; set; }
        public string FalseText { get; set; }

        public override string ToHtmlString()
        {
            string checkBoxHtml = GetCheckboxHtml();

            var onBuilder = new TagBuilder("span");
            onBuilder.AddCssClass("sliderTrue");
            onBuilder.SetInnerText(TrueText ?? "On");

            var offBuilder = new TagBuilder("span");
            offBuilder.AddCssClass("sliderFalse");
            offBuilder.SetInnerText(FalseText ?? "Off");

            var blockBuilder = new TagBuilder("span");
            blockBuilder.AddCssClass("sliderBlock");

            var sliderBuffer = new StringBuilder();
            sliderBuffer.AppendLine();
            sliderBuffer.AppendLine(onBuilder.ToString(TagRenderMode.Normal));
            sliderBuffer.AppendLine(offBuilder.ToString(TagRenderMode.Normal));
            sliderBuffer.AppendLine(blockBuilder.ToString(TagRenderMode.Normal));

            var sliderBuilder = new TagBuilder("span");
            sliderBuilder.AddCssClass("slider");
            sliderBuilder.InnerHtml = sliderBuffer.ToString();

            var labelBuffer = new StringBuilder();
            labelBuffer.AppendLine();
            labelBuffer.AppendLine(checkBoxHtml);
            labelBuffer.AppendLine(sliderBuilder.ToString(TagRenderMode.Normal));

            var labelBuilder = new TagBuilder("label");
            labelBuilder.MergeAttributes(SliderAttributes);
            labelBuilder.AddCssClass("sliderLabel");
            labelBuilder.InnerHtml = labelBuffer.ToString();

            string labelHtml = labelBuilder.ToString(TagRenderMode.Normal);
            if (!IncludeHidden)
                return labelHtml;

            // Render an additional <input type="hidden".../> for checkboxes.
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine(labelHtml);

            var hiddenHtml = GetHiddenHtml();
            inputBuilder.Append(hiddenHtml);

            return inputBuilder.ToString();
        }
    }
}