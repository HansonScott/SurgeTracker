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
    [Register ("SurgeHistoryController")]
    partial class SurgeHistoryController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        SurgeTracker.SurgeTableView SurgeTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (SurgeTable != null) {
                SurgeTable.Dispose ();
                SurgeTable = null;
            }
        }
    }
}