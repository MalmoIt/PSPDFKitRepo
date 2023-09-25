using System;
using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace PSPDFKitRepro.Handlers
{
    public class PsPdfTabbedViewControllerRenderer : PSPDFTabbedViewController, IPSPDFTabbedViewControllerDelegate
    {
       // readonly IOpenDocumentsManager _openDocumentsManager;

        public UIBarButtonItem ClearTabsButtonItem { get; set; }

        //public PsPdfTabbedViewControllerRenderer(IOpenDocumentsManager openDocumentsManager)
        public PsPdfTabbedViewControllerRenderer()
        {
            //_openDocumentsManager = openDocumentsManager;
            // Documents = _openDocumentsManager.GetAll()
            //     .Select(pdfDocument => new PSPDFDocument(NSUrl.FromFilename(pdfDocument.FilePath))
            //     {
            //         Title = pdfDocument.Name,
            //         Uid = pdfDocument.FileGuid
            //     }).ToArray();
            // VisibleDocument = _openDocumentsManager.CurrentDocument is PdfDocument ? Documents.FirstOrDefault(d => d.Uid == _openDocumentsManager.CurrentDocument.FileGuid) : Documents.LastOrDefault();
        }
       
       public override void CommonInit(PSPDFViewController pdfController)
       {
           base.CommonInit(pdfController);
       
           pdfController = PdfController;
           Delegate = this;
       
           pdfController.UpdateConfiguration(builder =>
           {
               builder.RenderStatusViewPosition = PSPDFRenderStatusViewPosition.Top;
       
               builder.PageTransition = PSPDFPageTransition.ScrollContinuous;
               builder.SpreadFitting = PSPDFConfigurationSpreadFitting.Fit;
               builder.PageMode = PSPDFPageMode.Single;
               builder.ScrollDirection = PSPDFScrollDirection.Vertical;
               builder.SettingsOptions = PSPDFSettingsOptions.PageTransition | PSPDFSettingsOptions.ScrollDirection | PSPDFSettingsOptions.Brightness;
               builder.ShouldHideNavigationBarWithUserInterface = true;
               builder.ShouldHideUserInterfaceOnPageChange = false;
               builder.ShouldShowUserInterfaceOnViewWillAppear = true;
               builder.UseParentNavigationBar = true;
               builder.ShouldHideStatusBar = true;
               builder.UserInterfaceViewMode = PSPDFUserInterfaceViewMode.Always;
           });
       
           EnableAutomaticStatePersistence = true;
       }
       
       public override void ViewDidAppear(bool animated)
       {
           base.ViewDidAppear(animated);
           NavigationController.TopViewController.NavigationItem.Title = "Dokumenter";
           InitPdfControls();
           //PdfController.PageIndex = (nuint)(_openDocumentsManager.GetAll().FirstOrDefault(d => d.FileGuid == VisibleDocument.Uid).PageIndex ?? 0);
       }
       
       public override void ViewWillDisappear(bool animated)
       {
           //_openDocumentsManager.ReArrange(Documents.Select(d => d.Uid).ToList());
           //_openDocumentsManager.CurrentDocument = _openDocumentsManager.GetAll().FirstOrDefault(x => x.Name == VisibleDocument.Title && x.FileGuid == VisibleDocument.Uid);
           base.ViewWillDisappear(animated);
       }
       
       
       /// <summary>
       /// Can only be called after NavigationController has been set.
       /// </summary>
       void InitPdfControls()
       {
           ClearTabsButtonItem = new UIBarButtonItem(PSPDFKitGlobal.GetImage("trash"), UIBarButtonItemStyle.Plain, OnClearTabs);
       
           var items = new List<UIBarButtonItem>
           {
               PdfController.SettingsButtonItem,
               ClearTabsButtonItem,
               PdfController.ThumbnailsButtonItem,
               PdfController.OutlineButtonItem,
               PdfController.BookmarkButtonItem,
               PdfController.SearchButtonItem
           };
       
           NavigationController.TopViewController.NavigationItem.SetRightBarButtonItems(items.ToArray(), false);
       }
       
       void OnClearTabs(object sender, EventArgs e)
       {
           var alertController = UIAlertController.Create(
               title: "Lukk faner",
               message: "Ønsker du å lukke alle faner?",
               preferredStyle: UIAlertControllerStyle.Alert);
       
           alertController.AddAction(UIAlertAction.Create(
               title: "Ja",
               style: UIAlertActionStyle.Destructive,
               handler: (obj) =>
               {
                   Documents = Array.Empty<PSPDFDocument>();
                  // _openDocumentsManager.Clear();
               }));
       
           alertController.AddAction(UIAlertAction.Create(
               title: "Nei",
               style: UIAlertActionStyle.Cancel,
               handler: null));
       
           PresentViewController(
               viewControllerToPresent: alertController,
               animated: true,
               completionHandler: null);
       }
       
       [Export("multiPDFController:didChangeDocuments:")]
       public void DidChangeDocuments(PSPDFMultiDocumentViewController multiPdfController, PSPDFDocument[] oldDocuments)
       {
       }
       
       [Export("multiPDFController:didChangeVisibleDocument:")]
       public void DidChangeVisibleDocument(PSPDFMultiDocumentViewController multiPdfController, PSPDFDocument oldDocument)
       {
          // if (VisibleDocument != null && _openDocumentsManager.GetAll().FirstOrDefault(x => x.Name == VisibleDocument.Title && x.FileGuid == VisibleDocument.Uid) is var currentDocument)
          //     _openDocumentsManager.VisibleDocumentChanged?.Invoke(currentDocument);
          // else
          //     _openDocumentsManager.VisibleDocumentChanged?.Invoke(null);
       }
       
       /// <summary>
       /// Called when user closes a single PDF
       /// </summary>
       /// <param name="viewController"></param>
       /// <param name="closedDocument"></param>
       [Export("tabbedPDFController:didCloseDocument:")]
       public void DidCloseDocument(PSPDFTabbedViewController viewController, PSPDFDocument closedDocument)
       {
         //  _openDocumentsManager.Clear(closedDocument.Uid);
       }
    }
}

