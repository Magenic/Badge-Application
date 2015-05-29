using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Web;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// A function that takes an item of type TIn, converts it to type TOut, and stores the result in the supplied
    /// reference variable "result". A ListItemConverter should return false if the conversion was unsccessful, or if
    /// the converted item should not be added to a new converted list.
    /// </summary>
    /// <typeparam name="TIn">The type of items in the list being converted.</typeparam>
    /// <typeparam name="TOut">The type of items in the newly converted list.</typeparam>
    /// <param name="item">An item from the original list to convert.</param>
    /// <param name="result">A variable that the converted result should be stored in.</param>
    /// <returns>True if conversion was successful and the result should be added to the converted list.</returns>
    [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "This conversion uses the \"TryParse\" or \"TryConvert\" convention, which is valid.")]
    public delegate bool ListItemConverter<TIn, TOut>(TIn item, out TOut result);

    /// <summary>
    /// 
    /// </summary>
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Concatenates all items in the enumerable object into a string, using the specified item conversion function, seperator, and strings to
        /// prepend and postpend to the list.
        /// </summary>
        /// <typeparam name="T">The item type of the enumerable object.</typeparam>
        /// <param name="list">The enumerable object to build a seperated list from.</param>
        /// <param name="itemSerializer">A callback to use to turn each item into a string. Pass null to use each item's ToString() function.</param>
        /// <param name="separator">A string to place between each item. Defaults to ",", or to String.Empty if null is passed.</param>
        /// <param name="prepend">A string to place before the first item. Defaults to String.Empty.</param>
        /// <param name="postpend">A string to place after the last item. Defaults to String.Empty.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Yes they should.")]
        public static string ListElements<T>(this IEnumerable<T> list, Func<T, string> itemSerializer, string separator = ",", string prepend = "", string postpend = "") {
            StringBuilder output = new StringBuilder();
            list = list ?? new T[] { };
            itemSerializer = itemSerializer ?? (item => item.ToString());
            separator = separator ?? string.Empty;
            prepend = prepend ?? string.Empty;
            postpend = postpend ?? string.Empty;
            output.Append(prepend);
            foreach (T item in list) {
                output.Append(itemSerializer(item)).Append(separator);
            }
            if (separator.Length > 0 && list.GetEnumerator().MoveNext()) {
                output.Remove(output.Length - separator.Length, separator.Length);
            }
            output.Append(postpend);
            return output.ToString();
        }

        /// <summary>
        /// Converts an enumerable object with items of type TIn to a List with items of type TOut using a given item conversion function.
        /// </summary>
        /// <typeparam name="TIn">The type of items in the original enumerable object.</typeparam>
        /// <typeparam name="TOut">The type of items in the converted list.</typeparam>
        /// <param name="inList">The enumerable object to convert items from.</param>
        /// <param name="itemConverter">The conversion function to use.</param>
        /// <returns>A list of converted items from the original enumerable object.</returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "This conversion uses the \"TryParse\" or \"TryConvert\" convention, which is valid.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "This is an appropriate use of lists.")]
        public static List<TOut> Convert<TIn, TOut>(this IEnumerable<TIn> inList, ListItemConverter<TIn, TOut> itemConverter) {
            if (inList == null) return null;
            if (itemConverter == null) throw new ArgumentNullException("itemConverter");
            List<TOut> outList = new List<TOut>();

            foreach (TIn inItem in inList) {
                TOut outItem;
                if (itemConverter(inItem, out outItem))
                    outList.Add(outItem);
            }

            return outList;
        }
    }
}