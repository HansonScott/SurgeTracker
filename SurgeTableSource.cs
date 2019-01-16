using System;
using Foundation;
using UIKit;

namespace SurgeTracker
{
	// this class is the controller specifically for the table object
	public class SurgeTableSource: UITableViewSource
	{
		// this is the parent view controller for the screen on which the table is located
		SurgeHistoryController Parent;

		// constructor
		public SurgeTableSource(SurgeHistoryController controller)
		{
			// capture the parent
			Parent = controller;
		}

		// necessary override for a custom table controller class, I think getting a 'blank' cell, for a new cell
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(SurgeHistoryController.CellColumnName);

			int row = indexPath.Row;
			cell.TextLabel.Text = (Parent.Surges[row] as SurgeItem).Description;
			return cell;
		}

		// necessary override for a custom table controller class, returns the count of items for a given section
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return Parent.Surges.Count;
		}

		// optional override for a custom table controller class, to enable the edit mode, and row delete function
		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			// delete from the source model first, or else it has index problems!
			Parent.Surges.RemoveAt(indexPath.Row);
			tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
		}
	}
}
