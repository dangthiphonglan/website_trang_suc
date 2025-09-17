using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace TreasureL.Helpers
{
    public static class HTMLHelpers
    {
        public static string FormatPrice(this IHtmlHelper htmlHelper, double? price)
        {
            return String.Format("{0:#,###}", price);
        }
    }
}
