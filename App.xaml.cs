namespace PSPDFKitRepro;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

		Routing.RegisterRoute(nameof(PdfPage), typeof(PdfPage));
    }
}

