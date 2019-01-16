using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace SurgeTracker
{
    public partial class SurgeHistoryController : UITableViewController
    {
		public List<SurgeItem> Surges;

		public static NSString CellColumnName = new NSString("Duration");

		public SurgeHistoryController (IntPtr handle) : base (handle)
        {
			TableView.RegisterClassForCellReuse(typeof(UITableViewCell), CellColumnName);
			TableView.Source = new SurgeTableSource(this);
			Surges = new List<SurgeItem>();
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//load table with data
		}
    }
}