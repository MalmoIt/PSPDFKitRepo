using System;
using Microsoft.Maui.Handlers;

namespace PSPDFKitRepro.Handlers
{
    public partial class PdfPageHandler : PageHandler
    {
        public PdfPageHandler() : base(Mapper, CommandMapper)
        {
        }
    }
}

