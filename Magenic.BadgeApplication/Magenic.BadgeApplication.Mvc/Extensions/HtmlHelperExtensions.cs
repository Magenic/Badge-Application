using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Generates the conditional navigation.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="actionResult">The action result.</param>
        /// <returns></returns>
        public static MvcHtmlString GenerateConditionalNavigation(this HtmlHelper html, string linkText, ActionResult actionResult)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<li>");
            stringBuilder.Append(html.ActionLink(linkText, actionResult).ToHtmlString());
            stringBuilder.Append("</li>");
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
        
        /// <summary>
        /// Creates a tag-style text box with auto-complete and paste funtionality for the given property, with each item in the autoCompleteList as a possible tag to add.
        /// </summary>
        /// <typeparam name="TModel">The Type of the model the field is from.</typeparam>
        /// <typeparam name="TProperty">The Type of the property the field is for. (Should be a collection or enumerable type, since this control is always multi-select.)</typeparam>
        /// <param name="html">The HtmlHelper object being used to generate markup for the web page.</param>
        /// <param name="expression">An expression that specified the property in the model that the field is for.</param>
        /// <param name="autoCompleteList">An enumerable object of items available for selection.</param>
        /// <param name="invalidTagBogLabel">A message to display when the user tries to paste in unavailable tags.</param>
        /// <returns>An MvcHtmlString containing the code for the form element.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Forces/Encourages proper use of this helper function in page views.")]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "This is an extension method. Argument 'html' will not be null.")]
        public static MvcHtmlString TagSelectBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> autoCompleteList, string invalidTagBogLabel) {
            return TagSelectBoxFor(html, expression, autoCompleteList, invalidTagBogLabel, null);
        }
        
        /// <summary>
        /// Creates a tag-style text box with auto-complete and paste funtionality for the given property, with each item in the autoCompleteList as a possible tag to add.
        /// </summary>
        /// <typeparam name="TModel">The Type of the model the field is from.</typeparam>
        /// <typeparam name="TProperty">The Type of the property the field is for. (Should be a collection or enumerable type, since this control is always multi-select.)</typeparam>
        /// <param name="html">The HtmlHelper object being used to generate markup for the web page.</param>
        /// <param name="expression">An expression that specified the property in the model that the field is for.</param>
        /// <param name="autoCompleteList">An enumerable object of items available for selection.</param>
        /// <param name="invalidTagBogLabel">A message to display when the user tries to paste in unavailable tags.</param>
        /// <param name="onPasteItemSeparators">An string of characters by which the select box should delimit tags when text of 0 or more tags is pasted into it.
        /// Regex character classes are recognized (e.g. @"\s" will recognize any white space character as a delimiter).
        /// Forward slashes '/' must be escaped (e.g. use @"\/" instead of "/").
        /// Defaults to @"\n,;".</param>
        /// <returns>An MvcHtmlString containing the code for the form element.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Forces/Encourages proper use of this helper function in page views.")]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "This is an extension method. Argument 'html' will not be null.")]
        public static MvcHtmlString TagSelectBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> autoCompleteList, string invalidTagBogLabel, string onPasteItemSeparators) {
            StringBuilder output = new StringBuilder();

            //Initialize and calculate values used in code generation.
            string mdlName = ExpressionHelper.GetExpressionText(expression);
            string fieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(mdlName);//Name of the hidden form field that will contain the values that get sent on form submit.
            string fieldId = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(mdlName);//ID for the hidden form field that will contain the values that get sent on form submit.
            string tagBoxId = fieldId + "tags";//ID of the actual visible tag box the user interacts with.
            string tagValBoxId = fieldId + "valBox";//ID of a field that the widget requires, which would normally contain the values to submit with the form, but which we will be
                                                    //overriding, since the widget has no way of using something other than the tag text (e.g. a database ID) as the submit value.
            string invTagBoxId = fieldId + "invTags";//ID of the visible list of invalid tags to display when the user tries to paste in tags
            string invTagFieldId = fieldId + "invTagFld";//Id for value field required by the widget when we create the invalid tag display box.
            string invTagBlkId = fieldId + "invTagCont";//Id of the html element (probably a div) containing all invalid paste tag elements such as the invTagBox, other hidden elements that make it work, and related error msgs.
            string invTagClrBtnId = fieldId + "invTagClrBtn";//Id of the button to clear the list of invalid pasted tags.

            string pasteRegex = onPasteItemSeparators == null ? @"/[\n,;]/" : "/[" + onPasteItemSeparators + "]/";

            //----------------------------------------------
            //------Generate Script to generate the widget from our html elements at page run time and control the widget's behavior------
            //----------------------------------------------
            //Begin the script block...
            output.Append("<script type=\"text/javascript\">(function(){");

            //Declare local script variables...
            output.Append("var tagElmnt,idsElmnt,invTagElmnt,invTagBlk,invTagCount=0,pasting=false,tagList=");
            //Initialize list of tag names...
            output.Append(autoCompleteList.ListElements(item => string.Format(CultureInfo.InvariantCulture, "\"{0}\"", item.Text.JSONEsc(StringEnclosureType.DoubleQuotes)), ",", "[", "]"));
            //Initialize object to serve as name->value dictionary for tag options...
            output.Append(",valList=").Append(autoCompleteList.ListElements(
                option => string.Format(CultureInfo.CurrentCulture, "\"{0}\":\"{1}\"", option.Text.JSONEsc(StringEnclosureType.DoubleQuotes), option.Value.JSONEsc(StringEnclosureType.DoubleQuotes) ?? "null"),//Formatter for object members
                ",", "{", "}")//object member seperater and opening/closing characters.
            );
            //End of local variable declarations
            output.Append(';');
            //Declare local script functions (as variables that contain them)...
            //Function to check if the tag box will accept an entered tag.
            output.Append("var validateTag=function(tagLbl){return tagList.indexOf(tagLbl) > -1;};");
            //Function to refresh the hidden field of selected/entered tag values when a tag is added or removed.
            output.Append("var refreshValsField=function(){var tags=tagElmnt.tagit(\"assignedTags\");if(tags.length==0){idsElmnt.val(\"\");return;}var newVal=valList[tags[0]];for(var i=1;i<tags.length;i++)newVal+=\",\"+valList[tags[i]];idsElmnt.val(newVal);};");
            
            //------Generate the jQuery widget initialization call------
            //Begin the jQuery on-document-load call that does all the widget generation and initialization magic.
            output.Append("$(document).ready(function(){");

            //Init any of the variables declared earlier that we needed to wait for document-ready to init.
            output.AppendFormat("tagElmnt=$(\"#{0}\");", tagBoxId)
                .AppendFormat("idsElmnt=$(\"#{0}\");", fieldId)
                .AppendFormat("invTagElmnt=$(\"#{0}\");", invTagBoxId)
                .AppendFormat("invTagBlk=$(\"#{0}\");", invTagBlkId);
            //Call the tag-it widget on the jQuery element, passing in appropriate args.
            output.Append("tagElmnt.tagit({")//Start of call and args object
                .Append("'fieldName':'','availableTags':tagList,'allowSpaces':true,'showAutocompleteOnFocus':true,'autocomplete':{'delay':0},'singleField':true")
                .AppendFormat(",'singleFieldNode':$('#{0}')", tagValBoxId)
                .Append(",'beforeTagAdded':function(e,ui){if(validateTag(ui.tagLabel))return true;else{if(pasting)invTagElmnt.tagit(\"createTag\",ui.tagLabel);return false;}}")
                .Append(",'afterTagAdded':function(e,ui){if(!pasting)refreshValsField();}")
                .Append(",'afterTagRemoved':function(e,ui){refreshValsField();}")
            .Append("});");//end of args obj and call
            //Find the actual text box in the widget that tags are typed/entered/pasted into
            output.Append("var inpPt=tagElmnt.find('.tagit-new .ui-widget-content');");
            //Bind to its paste event with a function to parse pasted text as tags and add them appropriately
            output.Append("inpPt.bind('paste',function(e){pasting=true;e.preventDefault();var pastedData=e.originalEvent.clipboardData.getData('text')")
                .AppendFormat(",tags=pastedData.split({0});", pasteRegex)
                .Append("for(var i=0;i<tags.length;i++)tagElmnt.tagit(\"createTag\",tags[i]);refreshValsField();pasting=false;});");
            //Now ALSO call the tag-it widget on the invalid tag box to create a tag widget for displaying badly pasted tags.
            output.Append("invTagElmnt.tagit({")//Start of call and args object
                .Append("'fieldName':'','singleField':true,'allowSpaces':true")
                .AppendFormat(",'singleFieldNode':$('#{0}')", invTagFieldId)
                .Append(",'beforeTagAdded':function(e,ui){if(invTagCount==0)tagElmnt.after(invTagBlk);return true;}")
                .Append(",'afterTagAdded':function(e,ui){invTagCount++;}")
                .Append(",'afterTagRemoved':function(e,ui){invTagCount--;if(invTagCount==0)invTagBlk.detach();}")
            .Append("});");//end of args obj and call
            //Add classes to the invalid tag box so we can make it and its tags look different.
            output.Append("invTagElmnt.addClass('invTagBox');");
            //Disable the text box in the widget (no manually adding tags to the invalid tag display)
            output.Append("invTagElmnt.find(\".tagit-new\").attr('disabled','true').css('display','none');");
            //Set the invalid tag clear button's on click event.
            output.AppendFormat(CultureInfo.InvariantCulture, "$(\"#{0}\")", invTagClrBtnId)
                .Append(".bind('click',function(e){invTagElmnt.tagit(\"removeAll\");});");
            //Remove the invalid tag stuff container for later use when it is needed.
            output.Append("invTagBlk.detach();");
            //Add selected items to the tag box.
            foreach (SelectListItem sli in autoCompleteList) {
                if (sli.Selected)
                    output.AppendFormat(CultureInfo.InvariantCulture, "tagElmnt.tagit(\"createTag\",\"{0}\");", sli.Text);
            }

            //End the jQuery on-document-load call that does all the widget generation and initialization magic.
            output.Append("});");

            //End the script block...
            output.Append("})();</script>");

            //----------------------------------------------
            //------Generate the HTML elements that the widget will be generated from at page run time------
            //----------------------------------------------
            //First the submitted value field
            TagBuilder bldr = new TagBuilder("input");
            bldr.MergeAttribute("id", fieldId, true);
            bldr.MergeAttribute("name", fieldName, true);
            bldr.MergeAttribute("type", "hidden", true);
            output.Append(bldr.ToString(TagRenderMode.SelfClosing));
            //Then the required dummy value field to be overridden
            bldr = new TagBuilder("input");
            bldr.MergeAttribute("id", tagValBoxId, true);
            bldr.MergeAttribute("name", String.Empty, true);
            bldr.MergeAttribute("type", "hidden", true);
            output.Append(bldr.ToString(TagRenderMode.SelfClosing));
            //Now the visible element for the widget, with any selected tags already added.
            bldr = new TagBuilder("ul");
            bldr.MergeAttribute("id", tagBoxId, true);
            output.Append(bldr.ToString(TagRenderMode.Normal));
            //output.Append(bldr.ToString(TagRenderMode.StartTag));
            //foreach (SelectListItem tag in autoCompleteList) {
            //    if (tag.Selected) {
            //        TagBuilder liBldr = new TagBuilder("li");
            //        liBldr.SetInnerText(tag.Text);
            //        output.Append(liBldr.ToString(TagRenderMode.Normal));
            //    }
            //}
            //output.Append(bldr.ToString(TagRenderMode.EndTag));
            //--Now the invalid tag block with the invalid tag widget and its hidden value field.--
            //Start the div containing it all
            bldr = new TagBuilder("div");
            bldr.MergeAttribute("id", invTagBlkId, true);
            output.Append(bldr.ToString(TagRenderMode.StartTag));
            //  The required hidden value field
            TagBuilder fldBldr = new TagBuilder("input");
            fldBldr.MergeAttribute("id", invTagFieldId, true);
            fldBldr.MergeAttribute("name", String.Empty, true);
            fldBldr.MergeAttribute("type", "hidden", true);
            output.Append(fldBldr.ToString(TagRenderMode.SelfClosing));
            //  Label containing the error message for the invalid tag box.
            fldBldr = new TagBuilder("label");
            fldBldr.AddCssClass("invTagBoxLabel");
            fldBldr.SetInnerText(invalidTagBogLabel);
            fldBldr.MergeAttribute("for", invTagBoxId, true);
            output.Append(fldBldr.ToString(TagRenderMode.Normal));
            //  The visible invalid tag widget
            fldBldr = new TagBuilder("ul");
            fldBldr.MergeAttribute("id", invTagBoxId, true);
            output.Append(fldBldr.ToString(TagRenderMode.Normal));
            //  Button for clearing the invalid tag box
            fldBldr = new TagBuilder("button");
            fldBldr.SetInnerText("Clear");
            fldBldr.AddCssClass("btn");
            fldBldr.AddCssClass("btn-default");
            fldBldr.AddCssClass("invTagClearBtn");
            fldBldr.MergeAttribute("id", invTagClrBtnId);
            output.Append(fldBldr.ToString(TagRenderMode.Normal));
            //End the containing div
            output.Append(bldr.ToString(TagRenderMode.EndTag));

            return new MvcHtmlString(output.ToString());
        }

    }
}