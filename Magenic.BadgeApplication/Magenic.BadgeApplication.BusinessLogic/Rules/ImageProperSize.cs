using Csla.Core;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.IO;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public class ImageProperSize : BusinessRule
    {
        public ImageProperSize(IPropertyInfo imageProperty) : base(imageProperty)
        {

            if (imageProperty == null)
            {
                throw new ArgumentException("imageProperty cannot be null.");
            }
            if (imageProperty.Type != typeof(byte[]))
            {
                throw new ArgumentException("imageProperty must be a byte[].");
            }

            InputProperties = new List<IPropertyInfo> { imageProperty };
        }

        protected override void Execute(RuleContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null");
            }

            var imageArray = (byte[])context.InputPropertyValues[this.PrimaryProperty];
            if (imageArray != null && imageArray.Length > 0)
            {
                var ms = new MemoryStream(imageArray);
                try
                {
                    var image = System.Drawing.Image.FromStream(ms);
                    if (image.Height != 100 || image.Width != 85)
                    {
                        context.AddErrorResult("The supplied image must have a height of 100 and a width of 85.");
                    }
                }
                catch (ArgumentException)
                {
                    context.AddErrorResult("Image property must be set with a valid image type.");
                }
            }
        }
    }
}
