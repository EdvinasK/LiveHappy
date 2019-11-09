using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHappy.Infrastructure.Utilities
{
    public static class SelectListItemAdapter
    {
        /// <summary>
        /// Converts a list to SelectListItem list which can be used in Razor Select list.
        /// </summary>
        /// <typeparam name="T">Source list type</typeparam>
        /// <param name="source">This is the source list from the model that will be converted to an IEnumerable<SelectListItem></param>
        /// <param name="text">Select a property to use for the Text property of the SelectListItem list items.</param>
        /// <param name="value">Select a property to use for the Value property of the SelectListItem list items.</param>
        /// <param name="createEmpty">If true, which is the default value, an empty SelectListItem will be added to the list that contains the text Please Select.</param>
        /// <returns>Returns IEnumerable<SelectListItem></returns>
        public static IEnumerable<SelectListItem> ConvertToSelectListItemCollection<T>
            (IEnumerable<T> source, Func<T, string> text, Func<T, string> value, bool createEmpty = true) where T: class
        {
            var selectListItems = new List<SelectListItem>();

            if (createEmpty)
            {
                selectListItems.Add(new SelectListItem { Text = "Please Select", Value = "", Selected = true });
            }

            foreach (var item in source)
            {
                selectListItems.Add(new SelectListItem { Text = text(item), Value = value(item) });
            }

            return selectListItems;
        }

        /// <summary>
        /// Converts a list to SelectListItem list which can be used in Razor Select list.
        /// </summary>
        /// <typeparam name="T">Source list type</typeparam>
        /// <param name="source">This is the source list from the model that will be converted to an IEnumerable<SelectListItem></param>
        /// <param name="textAndValue">Select a property to use for the Text and Value property of the SelectListItem list items.</param>
        /// <param name="createEmpty">If true, which is the default value, an empty SelectListItem will be added to the list that contains the text Please Select.</param>
        /// <returns>Returns IEnumerable<SelectListItem></returns>
        public static IEnumerable<SelectListItem> ConvertToSelectListItemCollection<T>
            (IEnumerable<T> source, Func<T, string> textAndValue, bool createEmpty = true) where T : class
        {
            return ConvertToSelectListItemCollection(source, textAndValue, textAndValue, createEmpty);
        }
    }
}
