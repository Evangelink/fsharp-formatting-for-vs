﻿using System;
using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using Fantomas;
using FantomasConfig = Fantomas.FormatConfig.FormatConfig;
using System.Xml.Serialization;
using EditorConfig.Core;

namespace FantomasVs
{
    [Guid(GuidString)]
    public class FantomasOptionsPage : DialogPage
    {
        public const string GuidString = "74927147-72e8-4b47-a70d-5568807d6878";

        public static FormatConfig.FormatConfig Defaults => FormatConfig.FormatConfig.Default;

        #region Indent

        [Category("Indentation")]
        [DisplayName("Indent Try-With")]
        public bool IndentOnTryWith { get; set; } = Defaults.IndentOnTryWith;

        [Category("Indentation")]
        [DisplayName("Indent Number of Spaces")]
        [Description("This will normally be set by the editor, this is the value to fallback to.")]
        public int IndentSize { get; set; } = Defaults.IndentSize;

        #endregion

        #region Elmish

        [Category("Elmish")]
        [DisplayName("Disable for Elmish Syntax")]
        public bool DisableElmishSyntax { get; set; } = Defaults.DisableElmishSyntax;

        #endregion

        #region Boundaries

        [Category("Boundaries")]
        [DisplayName("Maximum Array or List Width")]
        public int MaxArrayOrListWidth { get; set; } = Defaults.MaxArrayOrListWidth;

        [Category("Boundaries")]
        [DisplayName("Maximum Width for Elmish Views")]
        public int MaxElmishWidth { get; set; } = Defaults.MaxElmishWidth;

        [Category("Boundaries")]
        [DisplayName("Maximum Function Binding Width")]
        public int MaxFunctionBindingWidth { get; set; } = Defaults.MaxFunctionBindingWidth;

        [Category("Boundaries")]
        [DisplayName("Maximum If-Then-Else Width")]
        public int MaxIfThenElseShortWidth { get; set; } = Defaults.MaxIfThenElseShortWidth;

        [Category("Boundaries")]
        [DisplayName("Maximum Infix Operator Expression")]
        public int MaxInfixOperatorExpression { get; set; } = Defaults.MaxInfixOperatorExpression;

        [Category("Boundaries")]
        [DisplayName("Maximum Line Length")]
        public int MaxLineLength { get; set; } = Defaults.MaxLineLength;

        [Category("Boundaries")]
        [DisplayName("Maximum Record Width")]
        public int MaxRecordWidth { get; set; } = Defaults.MaxRecordWidth;

        [Category("Boundaries")]
        [DisplayName("Maximum Record Items")]
        public int MaxRecordNumberOfItems { get; set; } = Defaults.MaxRecordNumberOfItems;

        public enum MultilineFormatterType
        {
            CharacterWidth = 0,
            NumberOfItems = 1
        }

        public static FormatConfig.MultilineFormatterType ConvertFormatterType(MultilineFormatterType type) =>
            type switch
            {
                MultilineFormatterType.CharacterWidth => FormatConfig.MultilineFormatterType.CharacterWidth,
                MultilineFormatterType.NumberOfItems => FormatConfig.MultilineFormatterType.NumberOfItems,
                _ => FormatConfig.MultilineFormatterType.CharacterWidth
            };

        [Category("Boundaries")]
        [DisplayName("Record Multi-line formatter")]
        public MultilineFormatterType RecordMultilineFormatterConfig { get; set; } = MultilineFormatterType.CharacterWidth;

        [Category("Boundaries")]
        [DisplayName("Array/List Multi-line formatter")]
        public MultilineFormatterType ArrayOrListMultilineFormatterConfig { get; set; } = MultilineFormatterType.CharacterWidth;

        internal FormatConfig.MultilineFormatterType RecordMultilineFormatter => ConvertFormatterType(RecordMultilineFormatterConfig);
        internal FormatConfig.MultilineFormatterType ArrayOrListMultilineFormatter => ConvertFormatterType(ArrayOrListMultilineFormatterConfig);

        [Category("Boundaries")]
        [DisplayName("Maximum Value Binding Width")]
        public int MaxValueBindingWidth { get; set; } = Defaults.MaxValueBindingWidth;

        [Category("Boundaries")]
        [DisplayName("Maximum Array/List Number Of Items")]
        public int MaxArrayOrListNumberOfItems { get; set; } = Defaults.MaxArrayOrListNumberOfItems;

        [Category("Boundaries")]
        [DisplayName("MultiLine Lambda Closing Newline")]
        public bool MultiLineLambdaClosingNewline { get; set; } = Defaults.MultiLineLambdaClosingNewline;

        [Category("Boundaries")]
        [DisplayName("Multiline Block Brackets On Same Column")]
        public bool MultilineBlockBracketsOnSameColumn { get; set; } = Defaults.MultilineBlockBracketsOnSameColumn;

        public enum LineEndingStyle
        {
            CR = FormatConfig.EndOfLineStyle.Tags.CR,
            LF = FormatConfig.EndOfLineStyle.Tags.LF,
            CRLF = FormatConfig.EndOfLineStyle.Tags.CRLF,
        }

        [Category("Boundaries")]
        [DisplayName("End Of Line Style")]
        public LineEndingStyle EndOfLineStyle { get; set; } = LineEndingStyle.CRLF;

        public FormatConfig.EndOfLineStyle EndOfLine
            => this.EndOfLineStyle switch
            {
                LineEndingStyle.CR => FormatConfig.EndOfLineStyle.CR,
                LineEndingStyle.CRLF => FormatConfig.EndOfLineStyle.CRLF,
                LineEndingStyle.LF => FormatConfig.EndOfLineStyle.LF,
                _ => FormatConfig.EndOfLineStyle.FromEnvironment
            };

        [Category("Boundaries")]
        [DisplayName("Maximum Dot Get Expression Width")]
        public int MaxDotGetExpressionWidth { get; set; } = Defaults.MaxDotGetExpressionWidth;

        #endregion

        #region Convention

        [Category("Convention")]
        [DisplayName("Semicolon at  End of Line")]
        public bool SemicolonAtEndOfLine { get; set; } = Defaults.SemicolonAtEndOfLine;

        [Category("Convention")]
        [DisplayName("Strict Mode")]
        [Description("Pretty printing the ASTs only (strips trivia like comments)")]
        public bool StrictMode { get; set; } = Defaults.StrictMode;

        [Category("Convention")]
        [DisplayName("Keep Newline After")]
        public bool KeepIfThenInSameLine { get; set; } = Defaults.KeepIfThenInSameLine;


        [Category("Convention")]
        [DisplayName("Newline Between Type Definition And Members")]
        public bool NewlineBetweenTypeDefinitionAndMembers { get; set; } = Defaults.NewlineBetweenTypeDefinitionAndMembers;


        [Category("Convention")]
        [DisplayName("Single-Argument Web Mode")]
        public bool SingleArgumentWebMode { get; set; } = Defaults.SingleArgumentWebMode;

        [Category("Convention")]
        [DisplayName("Align Function Signature To Indentation")]
        public bool AlignFunctionSignatureToIndentation { get; set; } = Defaults.AlignFunctionSignatureToIndentation;

        [Category("Convention")]
        [DisplayName("Alternative Long Member Definitions")]
        public bool AlternativeLongMemberDefinitions { get; set; } = Defaults.AlternativeLongMemberDefinitions;

        #endregion

        #region Spacing

        [Category("Spacing")]
        [DisplayName("Before Parameter")]
        public bool SpaceBeforeParameter { get; set; } = Defaults.SpaceBeforeParameter;

        [Category("Spacing")]
        [DisplayName("Before Colon")]
        public bool SpaceBeforeColon { get; set; } = Defaults.SpaceBeforeColon;

        [Category("Spacing")]
        [DisplayName("After Comma")]
        public bool SpaceAfterComma { get; set; } = Defaults.SpaceAfterComma;

        [Category("Spacing")]
        [DisplayName("After Semicolon")]
        public bool SpaceAfterSemicolon { get; set; } = Defaults.SpaceAfterSemicolon;

        [Category("Spacing")]
        [DisplayName("Before Semicolon")]
        public bool SpaceBeforeSemicolon { get; set; } = Defaults.SpaceBeforeSemicolon;

        [Category("Spacing")]
        [DisplayName("Around Delimiter")]
        public bool SpaceAroundDelimiter { get; set; } = Defaults.SpaceAroundDelimiter;


        [Category("Spacing")]
        [DisplayName("Before Class Constructor")]
        public bool SpaceBeforeClassConstructor { get; set; } = Defaults.SpaceBeforeClassConstructor;

        [Category("Spacing")]
        [DisplayName("Before Lowercase Invocation")]
        public bool SpaceBeforeLowercaseInvocation { get; set; } = Defaults.SpaceBeforeLowercaseInvocation;


        [Category("Spacing")]
        [DisplayName("Before Uppercase Invocation")]
        public bool SpaceBeforeUppercaseInvocation { get; set; } = Defaults.SpaceBeforeUppercaseInvocation;

        [Category("Spacing")]
        [DisplayName("Before Member")]
        public bool SpaceBeforeMember { get; set; } = Defaults.SpaceBeforeMember;

        #endregion

        #region Ordering


        #endregion

        #region Performance

        [Category("Performance")]
        [DisplayName("Apply As Diff")]
        [Description("Applies the formatting as changes, which shows which lines were changed. Turn off if computing the diff is too slow. ")]
        public bool ApplyDiff { get; set; } = true;

        [Category("Performance")]
        [DisplayName("Warmup On Start")]
        [Description("Runs through formatting code on startup to warm up the Jit. Reduces delay when first using it.")]
        public bool WarmUpOnStartup { get; set; } = false;

        [Category("Performance")]
        [DisplayName("Enable SpaceBar Heating")]
        [Description("xkcd/1172")]
        public bool EnableSpaceBarHeating { get; set; } = false;

        #endregion

        #region On Save

        [Category("On Save")]
        [DisplayName("Format On Save")]
        [Description("This triggers a formatting whenever you hit save")]
        public bool FormatOnSave { get; set; } = false;

        [Category("On Save")]
        [DisplayName("Commit Changes")]
        [Description("Set this to false if you don't want to commit formatting changes to the file unless you hit save once again")]
        public bool CommitChanges { get; set; } = true;

        #endregion

        public FantomasConfig ToOptions(int? spaces)
        {

            var config = new FantomasConfig(
                indentSize: spaces ?? this.IndentSize,
                indentOnTryWith: this.IndentOnTryWith,

                disableElmishSyntax: this.DisableElmishSyntax,
                maxArrayOrListWidth: this.MaxArrayOrListWidth,
                maxElmishWidth: this.MaxElmishWidth,
                maxFunctionBindingWidth: this.MaxFunctionBindingWidth,
                maxValueBindingWidth: this.MaxValueBindingWidth,
                maxIfThenElseShortWidth: this.MaxIfThenElseShortWidth,
                maxInfixOperatorExpression: this.MaxInfixOperatorExpression,
                maxLineLength: this.MaxLineLength,
                maxRecordWidth: this.MaxRecordWidth,
                maxRecordNumberOfItems: this.MaxRecordNumberOfItems,
                multilineBlockBracketsOnSameColumn: this.MultilineBlockBracketsOnSameColumn,
                recordMultilineFormatter: this.RecordMultilineFormatter,
                arrayOrListMultilineFormatter: this.ArrayOrListMultilineFormatter,
                maxArrayOrListNumberOfItems: this.MaxArrayOrListNumberOfItems,
                maxDotGetExpressionWidth: this.MaxDotGetExpressionWidth,
                keepIfThenInSameLine: this.KeepIfThenInSameLine,
                singleArgumentWebMode: this.SingleArgumentWebMode,
                alignFunctionSignatureToIndentation: this.AlignFunctionSignatureToIndentation,
                alternativeLongMemberDefinitions: this.AlternativeLongMemberDefinitions,
                multiLineLambdaClosingNewline: this.MultiLineLambdaClosingNewline,
                endOfLine: this.EndOfLine,

                semicolonAtEndOfLine: this.SemicolonAtEndOfLine,

                spaceBeforeClassConstructor: this.SpaceBeforeClassConstructor,
                spaceBeforeLowercaseInvocation: this.SpaceBeforeLowercaseInvocation,
                spaceBeforeUppercaseInvocation: this.SpaceBeforeUppercaseInvocation,
                spaceBeforeMember: this.SpaceBeforeMember,
                spaceBeforeParameter: this.SpaceBeforeParameter,
                spaceBeforeColon: this.SpaceBeforeColon,
                spaceAfterComma: this.SpaceAfterComma,
                spaceAfterSemicolon: this.SpaceAfterSemicolon,
                spaceBeforeSemicolon: this.SpaceBeforeSemicolon,
                spaceAroundDelimiter: this.SpaceAroundDelimiter,

                newlineBetweenTypeDefinitionAndMembers: this.NewlineBetweenTypeDefinitionAndMembers,

                strictMode: this.StrictMode
            );

            return config;
        }
    }
}
