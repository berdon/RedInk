using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Engine.Components
{
    public class HtmlContentsViewComponentResult : IViewComponentResult
    {
        /// <summary>
        /// Gets the encoded content.
        /// </summary>
        public IEnumerable<IHtmlContent> EncodedContent { get; }

        public HtmlContentsViewComponentResult(params IHtmlContent[] encodedContent)
        {
            if (encodedContent == null)
            {
                throw new ArgumentNullException(nameof(encodedContent));
            }

            EncodedContent = encodedContent;
        }

        /// <summary>
        /// Writes the <see cref="EncodedContent"/>.
        /// </summary>
        /// <param name="context">The <see cref="ViewComponentContext"/>.</param>
        public void Execute(ViewComponentContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (var content in EncodedContent)
            {
                context.Writer.Write(content);
            }
        }

        /// <summary>
        /// Writes the <see cref="EncodedContent"/>.
        /// </summary>
        /// <param name="context">The <see cref="ViewComponentContext"/>.</param>
        /// <returns>A completed <see cref="Task"/>.</returns>
        public Task ExecuteAsync(ViewComponentContext context)
        {
            Execute(context);

            return Task.CompletedTask;
        }
    }
}