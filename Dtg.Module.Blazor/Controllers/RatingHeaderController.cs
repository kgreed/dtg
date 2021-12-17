using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using Dtg.Module.BusinessObjects;
namespace Dtg.Module.Blazor.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RatingHeaderController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public RatingHeaderController()
        {
            InitializeComponent();
            TargetObjectType = typeof(RatingHeader);
            var newRatingHeader = new SimpleAction(this, "New Ratings", PredefinedCategory.View)
            {
                Caption = "New Ratings"
                //ConfirmationMessage = "Are you sure you want to clear the Tasks list?",
                //ImageName = "Action_Clear"
            };
            newRatingHeader.Execute += NewRatingHeader_Execute;
        }

        private void NewRatingHeader_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var os = Application.CreateObjectSpace(typeof(RatingHeader));
            var header = os.CreateObject<RatingHeader>();
            var metrics = os.GetObjects<Metric>().ToList();
            foreach (var metric in metrics)
            {
                var entry = os.CreateObject<RatingEntry>();
                entry.Metric = metric;
                entry.RatingHeader = header;
                header.Entries.Add(entry);
            }

            View.Refresh();
            var detailView = Application.CreateDetailView(os, header, true);
            e.ShowViewParameters.CreatedView = detailView;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}