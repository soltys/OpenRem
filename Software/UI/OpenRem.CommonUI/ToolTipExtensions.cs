using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace OpenRem.CommonUI
{
    public class ToolTipExtensions : DependencyObject
    {
        public const string NewLine = "<br/>";
        public const string StyleIdPrefix = "StyleId.";

        #region Private types

        private class Superscript : Span
        {
            public Superscript(Inline childInline) : base(childInline)
            {
                /* Note: FontVariants only works if the font really does have the requested variant, which is generally not the case!
                Typography.Variants = FontVariants.Superscript;

                We are using BaselineAlignment.TextTop instead of BaselineAlignment.Superscript to maintain line height to be same in whole application
                */

                BaselineAlignment = BaselineAlignment.TextTop;
                FontSize = 3 * FontSize / 4;
                /**/
            }
        }

        private class Subscript : Span
        {
            public Subscript(Inline childInline) : base(childInline)
            {
                /* Note: FontVariants only works if the font really does have the requested variant, which is generally not the case!
                Typography.Variants = FontVariants.Subscript;
                /*/
                BaselineAlignment = BaselineAlignment.Subscript;
                FontSize = 3 * FontSize / 4;
                /**/
            }
        }

        #endregion Private types

        #region Attached properties

        #region FormattedToolTip
        public static string GetFormattedToolTip(DependencyObject obj)
        {
            return (string)obj.GetValue(ToolTipExtensions.FormattedToolTipProperty);
        }
        public static void SetFormattedToolTip(DependencyObject obj, string value)
        {
            obj.SetValue(ToolTipExtensions.FormattedToolTipProperty, value);
        }
        /// <summary>
        /// Allows text that contains formatting information like &lt;Bold&gt; or &lt;Italic&gt; to be used in a <see cref="TextBlock"/>.
        /// </summary>
        public static readonly DependencyProperty FormattedToolTipProperty =
            DependencyProperty.RegisterAttached("FormattedToolTip",
                                                typeof(string),
                                                typeof(ToolTipExtensions),
                                                new PropertyMetadata(null, OnFormattedToolTipChanged)
                                               );

        private static void OnFormattedToolTipChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateInlines(d);
        }

        #endregion FormattedToolTip

        #region InlineStyles
        /// <summary>
        /// Gets the collection of styles for inline elements of this textblock.
        /// The method never returns null. If collection has not been set the method does it here (sets the empty collection).
        /// </summary>
        public static InlineStyles GetInlineStyles(DependencyObject obj)
        {
            return (InlineStyles)obj.GetValue(ToolTipExtensions.InlineStylesProperty);
        }
        /// <summary>
        /// For the given textblock sets the collection of styles for inline elements.
        /// </summary>
        public static void SetInlineStyles(DependencyObject obj, InlineStyles value)
        {
            obj.SetValue(ToolTipExtensions.InlineStylesProperty, value);
        }
        /// <summary>
        /// Attached property with special name. This is done intentionally to allow adding elements of the colleciton in XAML directly
        /// without specyfying the collection object itself.
        /// The property is not interited by childred object (for performance) - it is enough that only the given object is clipped.
        /// Its children will be clipped automatically by it.
        /// </summary>
        public static readonly DependencyProperty InlineStylesProperty =
            DependencyProperty.RegisterAttached("InlineStyles", typeof(InlineStyles), typeof(ToolTipExtensions),
            new PropertyMetadata(null, OnInlineStylesChanged));

        private static void OnInlineStylesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateInlines(d);
        }
        #endregion InlineStyles

        private static void UpdateInlines(DependencyObject d)
        {
            var textBlock = new TextBlock();
            var inlines = textBlock.Inlines;

            var textWithFormattingInfo = GetFormattedToolTip(d);

            if (textWithFormattingInfo != null)
            {
                inlines.Clear();
                inlines.Add(Traverse(textBlock, textWithFormattingInfo));
            }

            ((FrameworkElement)d).ToolTip = textBlock;
        }
        #endregion Attached properties

        /// <summary>
        /// Traverses the provided text, which contains embedded formatting markup, and returns an Inline
        /// that contains the objects necessary for the TextBlock to render the formatted text.
        /// </summary>
        /// <remarks>
        /// This code (and the methods that support it) is a slightly modified version of the code
        /// that "Vincent" provided in response to a question on stackoverflow.com,
        /// here: http://stackoverflow.com/questions/5565885/how-to-bind-a-textblock-to-a-resource-containing-formatted-text
        /// </remarks>
        /// <param name="targetObject">The text object (TextBlock or Inline) to with the extension is attached</param>
        /// <param name="value">The text string, which may contain embedded formatting markup characters</param>
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Documents.Run.#ctor(System.String)")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "FxCop 2015 does not like multiple case statements in a switch statement. But we are ok with these.")]
        public static Inline Traverse(DependencyObject targetObject, string value)
        {
            // Get the sections/inlines
            string[] sections = SplitIntoSections(value);

            // Check for grouping
            if (sections.Length.Equals(1))
            {
                string section = sections[0];
                string token; // E.g <Bold>
                int tokenStart, tokenEnd; // Where the token/section starts and ends.

                // Check for token
                if (GetTokenInfo(section, out token, out tokenStart, out tokenEnd))
                {
                    // Get the content for further examination
                    string content = token.Length.Equals(tokenEnd - tokenStart) ?
                        null :
                        section.Substring(token.Length, section.Length - 1 - token.Length * 2);

                    switch (token)
                    {
                        case "<Bold>":
                        case "[Bold]":
                        case "<b>":
                        case "[b]":
                        case "<strong>":
                        case "[strong]":
                            return new Bold(Traverse(targetObject, content));

                        case "<Italic>":
                        case "[Italic]":
                        case "<i>":
                        case "[i]":
                        case "<em>":
                        case "[em]":
                            return new Italic(Traverse(targetObject, content));

                        case "<Underline>":
                        case "[Underline]":
                        case "<u>":
                        case "[u]":
                            return new Underline(Traverse(targetObject, content));

                        case "<Superscript>":
                        case "[Superscript]":
                        case "<sup>":
                        case "[sup]":
                            return new Superscript(Traverse(targetObject, content));

                        case "<Subscript>":
                        case "[Subscript]":
                        case "<sub>":
                        case "[sub]":
                            return new Subscript(Traverse(targetObject, content));

                        case "<LineBreak/>":
                        case "[LineBreak/]":
                        case "<br/>":
                        case "[br/]":
                            return new LineBreak();

                        case "<Space/>":
                        case "[Space/]":
                        case "<nbsp/>":
                        case "[nbsp/]":
                            return new Run("\u00A0");

                        default:
                            if (token.Contains(ToolTipExtensions.StyleIdPrefix))
                            {
                                return SetStyle(targetObject, Traverse(targetObject, content), token);
                            }
                            return new Run(section);
                    }
                }
                else
                {
                    return new Run(section);
                }
            }
            else // Group together
            {
                var span = new Span();

                foreach (string section in sections)
                {
                    span.Inlines.Add(Traverse(targetObject, section));
                }

                return span;
            }
        }

        /// <summary>
        /// Examines the passed string and find the first token, where it begins, and where it ends.
        /// </summary>
        /// <param name="value">The string to examine.</param>
        /// <param name="token">The found token.</param>
        /// <param name="startIndex">Where the token begins.</param>
        /// <param name="endIndex">Where the end-token ends.</param>
        /// <returns>True if a token was found.</returns>
        private static bool GetTokenInfo(string value, out string token, out int startIndex, out int endIndex)
        {
            token = null;
            endIndex = -1;

            startIndex = value.IndexOf("<", StringComparison.Ordinal);
            int startTokenEndIndex = value.IndexOf(">", StringComparison.Ordinal);
            var closeToken = "/>";

            if (startIndex < 0)
            {
                startIndex = value.IndexOf("[", StringComparison.Ordinal);
                startTokenEndIndex = value.IndexOf("]", StringComparison.Ordinal);
                closeToken = "/]";
            }

            // No token here
            if (startIndex < 0)
            {
                return false;
            }

            // No token here
            if (startTokenEndIndex < 0)
            {
                return false;
            }

            token = value.Substring(startIndex, startTokenEndIndex - startIndex + 1);

            // Check for closed token. E.g. <LineBreak/> or [LineBreak/]
            if (token.EndsWith(closeToken, StringComparison.Ordinal))
            {
                endIndex = startIndex + token.Length;
                return true;
            }

            string endToken = token.Insert(1, "/");

            // Detect nesting;
            int nesting = 0;
            int pos = 0;
            do
            {
                int startTokenIndex = value.IndexOf(token, pos, StringComparison.Ordinal);
                int endTokenIndex = value.IndexOf(endToken, pos, StringComparison.Ordinal);

                if (startTokenIndex >= 0 && startTokenIndex < endTokenIndex)
                {
                    nesting++;
                    pos = startTokenIndex + token.Length;
                }
                else if (endTokenIndex >= 0 && nesting > 0)
                {
                    nesting--;
                    pos = endTokenIndex + endToken.Length;
                }
                else // Invalid tokenized string
                {
                    return false;
                }

            } while (nesting > 0);

            endIndex = pos;

            return true;
        }

        /// <summary>
        /// Splits the string into sections of tokens and regular text.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <returns>An array with the sections.</returns>
        private static string[] SplitIntoSections(string value)
        {
            var sections = new List<string>();

            while (!string.IsNullOrEmpty(value))
            {
                // Check if this is a token section
                if (GetTokenInfo(value, out _, out var tokenStartIndex, out var tokenEndIndex))
                {
                    // Add pretext if the token isn't from the start
                    if (tokenStartIndex > 0)
                        sections.Add(value.Substring(0, tokenStartIndex));

                    sections.Add(value.Substring(tokenStartIndex, tokenEndIndex - tokenStartIndex));
                    value = value.Substring(tokenEndIndex); // Trim away
                }
                else
                {
                    // No tokens, just add the text
                    sections.Add(value);
                    value = null;
                }
            }

            return sections.ToArray();
        }

        private static Inline SetStyle(DependencyObject targetObject, Inline content, string token)
        {
            var startIndex = token.IndexOf(ToolTipExtensions.StyleIdPrefix, StringComparison.Ordinal) + ToolTipExtensions.StyleIdPrefix.Length;
            var indices = new[]
            {
                token.IndexOf("/", StringComparison.Ordinal),
                token.IndexOf("]", StringComparison.Ordinal),
                token.IndexOf(">", StringComparison.Ordinal)
            };
            var endIndex = indices.Where(index => index > 0).Min();
            var styleId = token.Substring(startIndex, endIndex - startIndex);
            var style = GetInlineStyles(targetObject)?.Styles.FirstOrDefault(inlineStyle => inlineStyle.Id == styleId)?.Style;
            return style != null ? new Span(content) { Style = style } : content;
        }
    }

    [ContentProperty("Styles")]
    public class InlineStyles : DependencyObject
    {
        public Collection<InlineStyle> Styles { get; } = new Collection<InlineStyle>();
    }

    /// <summary>
    /// The style associated with the specific inline element
    /// </summary>
    [ContentProperty("Style")]
    public class InlineStyle : DependencyObject
    {
        /// <summary>
        /// Inline's id.
        /// </summary>
        public string Id
        {
            get { return (string)GetValue(InlineStyle.IdProperty); }
            set { SetValue(InlineStyle.IdProperty, value); }
        }
        /// <summary>
        /// Dependency property storing the inline's id.
        /// </summary>
        public static readonly DependencyProperty IdProperty =
             DependencyProperty.Register("Id", typeof(string), typeof(InlineStyle));

        /// <summary>
        /// Style for the inline.
        /// </summary>
        public Style Style
        {
            get { return (Style)GetValue(InlineStyle.StyleProperty); }
            set { SetValue(InlineStyle.StyleProperty, value); }
        }
        /// <summary>
        /// Dependency property storing the style for the inline.
        /// </summary>
        public static readonly DependencyProperty StyleProperty =
             DependencyProperty.Register("Style", typeof(Style), typeof(InlineStyle));
    }
}
