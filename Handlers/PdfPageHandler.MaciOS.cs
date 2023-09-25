using ContentView = Microsoft.Maui.Platform.ContentView;

namespace PSPDFKitRepro.Handlers;

public partial class PdfPageHandler
{
    PsPdfTabbedViewControllerRenderer _pdfTabbedViewControllerRenderer;

    protected override void ConnectHandler(ContentView nativeView)
    {
        var pdfPage = (PdfPage)VirtualView;

        _pdfTabbedViewControllerRenderer = new PsPdfTabbedViewControllerRenderer();
        ViewController.AddChildViewController(_pdfTabbedViewControllerRenderer);
        base.ConnectHandler(nativeView);
    }
}

