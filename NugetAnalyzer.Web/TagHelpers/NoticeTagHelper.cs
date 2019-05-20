﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NugetAnalyzer.Web.TagHelpers
{
    [HtmlTargetElement("notice")]
    public class NoticeTagHelper : TagHelper
    {
        public string Type { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }

        public NoticeTagHelper()
        {
            Type = "info";
            Width = "20px";
            Height = "20px";
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "svg";
            output.TagMode = TagMode.StartTagAndEndTag;

            var styles = $"width:{Width}; " +
                         $"height:{Height}; ";

            switch (Type.ToLower())
            {
                case "error":
                    styles += "fill: red; stroke-width: 2px;";
                    output.Attributes.SetAttribute("viewBox", "0 0 1000 1000");
                    output.Attributes.SetAttribute("enable-background", "new 0 0 1000 1000");
                    output.Content.SetHtmlContent(
                        @"<path d=""M500,990C229.8,990,10,770.2,10,500C10,229.8,229.8,10,500,10c270.2,0,490,219.8,490,490C990,770.2,770.2,990,500,990zM500,60.4C257.6,60.4,60.4,257.6,60.4,500c0,242.4,197.2,439.6,439.6,439.6c242.4,0,439.6-197.2,439.6-439.6C939.6,257.6,742.4,60.4,500,60.4zM429.3,746.1c0-19.1,6.7-35,20-47.6c18.1-17.1,36.4-25.6,54.8-25.6c20.2,0,37.5,7.2,51.8,21.5c14.4,14.3,21.5,31.8,21.5,52.3c0,20.8-6.8,38.2-20.3,52.2c-13.5,14.1-30.6,21.1-51.5,21.1c-21.9,0-40.1-7.4-54.5-22.1C436.6,783.2,429.3,765.9,429.3,746.1zM526.2,614c-8.9,0.6-25.2-0.3-48.6,0l-36.4-164.2c-12.7-64.1-21.3-110.8-25.5-139.9c-2.2-18.3-3.2-31.8-3.2-40.6c0-2.1,0.8-7.4,2.5-15.9c1.8-17.7,8.2-30.7,19.5-39.3c11.3-8.5,27.2-13.5,47.6-15c11.5-1.8,18.9-2.6,22.4-2.6c23.4,0,43.1,6,59.1,17.9c16,11.9,24,28.8,24,50.5c0,20.3-3.4,54.5-10.2,102.4l-27.5,142.6C543,551.3,535.1,586.9,526.2,614z"" />");
                    break;

                case "warning":
                    styles += "fill: orange";
                    output.Attributes.SetAttribute("viewBox", "0 0 512 512");
                    output.Attributes.SetAttribute("enable-background", "new 0 0 512 512");
                    output.Content.SetHtmlContent(
                        @"<path d = ""m 507.641 431.876 l -224 -384.002 c -5.734 -9.828 -16.258 -15.875 -27.641 -15.875 c -11.383 0 -21.906 6.047 -27.641 15.875 l -224 384.002 c -5.773 9.898 -5.813 22.125 -0.109 32.063 c 5.711 9.938 16.289 16.063 27.75 16.063 h 448.001 c 11.461 0 22.039 -6.125 27.75 -16.063 c 5.703 -9.938 5.664 -22.165 -0.11 -32.063 Z m -251.641 -15.878 c -17.656 0 -32 -14.328 -32 -32 c 0 -17.672 14.344 -32 32 -32 c 17.688 0 32 14.328 32 32 c 0 17.671 -14.312 32 -32 32 Z m 32 -127.998 c 0 17.672 -14.328 32 -32 32 s -32 -14.328 -32 -32 v -96 c 0 -17.672 14.328 -32 32 -32 s 32 14.328 32 32 v 96 Z""/>");
                    break;

                case "info":
                    styles += "fill: blue";
                    output.Attributes.SetAttribute("viewBox", "0 0 48 48");
                    output.Content.SetHtmlContent(
                        @"<circle cy=""24"" cx=""24"" r=""23"" stroke=""#00f"" fill=""none"" />
                                                    <circle cx=""24"" cy=""11.6"" r=""4.7"" />
                                                    <path d=""m17.4 18.8v2.15h1.13c2.26 0 2.26 1.38 2.26 1.38v15.1s0 1.38-2.26 1.38h-1.13v2.08h14.2v-2.08h-1.13c-2.26 0-2.26-1.38-2.26-1.38v-18.6""/>");
                    break;

                case "obsolete":
                    styles += "fill: grey";
                    output.Attributes.SetAttribute("viewBox", "0 0 1024 1024");
                    output.Content.SetHtmlContent(
                        @"<path d=""M828.751642 886.921995h-28.728298c-5.059226-104.608499-28.66383-183.425589-62.9671-242.550175l-0.459465-1.171686c-36.032658-61.828161-83.673701-102.201683-133.834124-126.792754 50.161447-25.039279 97.803513-64.956408 133.834124-126.714983l0.459465-1.241271c35.472909-61.564148 59.83169-144.751778 63.525825-256.161171h28.167527c19.238157 0 34.372855-15.102976 34.372855-34.079166 0-18.969027-15.134699-34.060747-34.372855-34.060747H198.688713c-19.010983-0.002047-34.339086 15.09172-34.339086 34.0587 0 18.978237 15.328104 34.078143 34.341133 34.078143h28.204365c4.107551 111.411439 27.982308 194.59907 63.498196 256.163218l0.710175 1.24127c36.008099 61.758576 83.420945 101.675704 133.768633 126.714983-50.353828 24.590048-97.764628 64.964594-133.768633 126.792755l-0.710175 1.171686c-34.015721 59.124586-57.694003 137.943722-62.742996 242.550175H198.688713c-19.013029 0-34.341133 15.09479-34.341133 33.592072 0 19.431561 15.328104 34.562167 34.341133 34.562167h630.060883c19.238157 0 34.372855-15.131629 34.372855-34.562167 0.001023-18.497283-15.131629-33.591049-34.370809-33.591049zM325.89693 665.039535l0.524956-1.459234c41.34771-71.02666 86.510306-108.948341 146.412604-127.476323l2.344395-1.433652h0.494257c1.236154-0.483001 2.412956-0.972141 3.453658-1.956562l0.422625-0.451278 1.473561-1.017166 0.225128-0.490164 1.468444-1.427512 0.260943-0.489141c0.973164-1.014096 1.435699-2.189875 2.446725-3.420912v-0.459465c1.011027-1.496074 1.473561-3.200902 1.473561-4.891403l0.220011-0.458441v-1.496074l0.232291-1.213641v-0.947582l-0.232291-1.23206v-0.981351l-0.220011-0.975211c0-1.692548-0.457418-3.394307-1.473561-4.860704v-0.489141c-1.011027-1.427512-1.473561-2.439562-2.446725-3.385097l-0.260943-0.753153-1.468444-1.213641h-0.225128l-1.473561-1.425466-0.422625-0.497326c-1.040702-0.74599-2.216481-1.459235-3.453658-2.204202l-0.494257-0.26299-2.344395-0.947581c-59.899228-18.003026-105.063871-56.443523-146.412604-127.96137l-0.524956-0.490164c-32.152281-56.222489-53.994752-132.825145-57.883315-235.980549h491.471454c-3.453658 103.155405-25.369807 179.759083-57.912991 235.975432l-0.490164 0.489141c-41.156352 71.51887-86.513376 109.964484-145.895834 127.960347l-2.929726 0.948604-0.488117 0.261967c-1.210571 0.747014-2.219551 1.459235-3.461845 2.206248l-0.451278 0.496304-1.474584 1.427512-1.660826 1.213641-0.227174 0.753153c-0.782829 0.944512-1.464351 1.949398-1.985214 3.38305l-0.48914 0.490164a245.315148 245.315148 0 0 0-1.467421 4.860705v7.304359c0.490164 1.691525 1.007957 3.394307 1.467421 4.891404l0.48914 0.458441a15.771195 15.771195 0 0 0 1.985214 3.421936l0.227174 0.48914 1.660826 1.427512v0.489141l1.474584 1.01819 0.451278 0.451278c1.007957 0.984421 2.25332 1.473561 3.461845 1.955538h0.488117l2.929726 1.435698c59.382459 18.526959 104.739483 56.446593 145.895834 127.4753l0.490164 1.460258c17.250896 29.225625 31.070646 64.739467 41.343617 106.08206 2.182712 11.773138 9.504468 37.72009 11.517311 71.252811-32.692586-28.942169-124.285654-50.180889-233.800907-51.819203v-322.280054a26.051329 26.051329 0 0 0 3.27151-1.227967l0.265037-0.489141 5.840008-2.181688 0.52291-0.264013h0.488117c39.660278-12.392238 71.739904-32.092929 98.002034-57.919131 28.29851-27.715225 49.663096-62.251809 64.202231-100.238982 3.942799-10.697643-0.877997-22.375613-11.609409-26.255991-10.69662-4.370541-22.629393 0.98749-26.803459 11.706623-12.81691 32.354895-30.619368 62.028729-54.750975 85.634356-21.431102 20.935822-47.876404 37.439704-80.974219 47.681976l-0.980328 0.459464-2.409886 0.489141-1.991354 0.970094-1.956561-0.970094-1.465374-0.489141-1.925863-0.459464c-32.54523-10.241248-59.58405-26.748201-80.772628-47.681976-24.328082-23.603581-42.067095-53.278437-54.954613-85.634356-3.875261-10.719132-16.07307-16.077164-26.248827-11.706623-10.730389 3.880377-15.622816 15.559371-11.738345 26.255991 14.644535 37.986149 35.574217 72.523757 64.243163 100.238982 25.791409 25.826202 58.404178 45.524846 97.794303 57.919131h0.229221l0.74292 0.264013 5.350868 2.181688 1.467421 0.489141c0.756223 0.366344 1.562589 0.744967 2.462075 1.131776v322.275961c-102.604866 0-191.33063 17.181311-233.751788 42.153053 1.461281-16.479323 4.870938-35.158754 11.337209-61.482283 10.241248-41.829687 24.327058-76.855412 41.379433-106.082061z""/>");
                    break;

                default:
                    output = null;
                    return;
            }

            output.Attributes.SetAttribute("xmlns", @"http://www.w3.org/2000/svg");
            output.Attributes.SetAttribute("version", "1.1");
            output.Attributes.SetAttribute("style", styles);
        }
    }
}