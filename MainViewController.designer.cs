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
    [Register ("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnClear { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnHistory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnStart { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnStop { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCurrentSurgeText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCurrentSurgeValue { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDelayText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDelayValue { get; set; }

        [Action ("btnClear_Click:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void btnClear_Click (UIKit.UIButton sender);

        [Action ("btnStart_Click:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void btnStart_Click (UIKit.UIButton sender);

        [Action ("btnStop_Click:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void btnStop_Click (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnClear != null) {
                btnClear.Dispose ();
                btnClear = null;
            }

            if (btnHistory != null) {
                btnHistory.Dispose ();
                btnHistory = null;
            }

            if (btnStart != null) {
                btnStart.Dispose ();
                btnStart = null;
            }

            if (btnStop != null) {
                btnStop.Dispose ();
                btnStop = null;
            }

            if (lblCurrentSurgeText != null) {
                lblCurrentSurgeText.Dispose ();
                lblCurrentSurgeText = null;
            }

            if (lblCurrentSurgeValue != null) {
                lblCurrentSurgeValue.Dispose ();
                lblCurrentSurgeValue = null;
            }

            if (lblDelayText != null) {
                lblDelayText.Dispose ();
                lblDelayText = null;
            }

            if (lblDelayValue != null) {
                lblDelayValue.Dispose ();
                lblDelayValue = null;
            }
        }
    }
}