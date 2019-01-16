// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SurgeTracker
{
    [Register ("SurgeTableView")]
    partial class SurgeTableView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        SurgeTracker.SurgeHistoryController dataSource { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (dataSource != null) {
                dataSource.Dispose ();
                dataSource = null;
            }
        }
    }
}