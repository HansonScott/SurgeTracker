using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.IO;

namespace SurgeTracker
{
    public partial class MainViewController : UIViewController
    {
		public static string FILENAME_HISTORY = "SurgeHistory.txt";

		public List<SurgeItem> Surges;
		public SurgeItem CurrentSurge
		{
			get
			{
				if (Surges == null) { return null;}
				else if (Surges.Count == 0) { return null; }
				else if (Surges[Surges.Count - 1].end != null) //don't return a completed surge!
				{
					return null;
				}
				else
				{
					return Surges[Surges.Count - 1];
				}

			}
		}
		public SurgeItem LastSurge
		{
			get
			{
				if (Surges == null) { return null; }
				else if (Surges.Count == 0) { return null; }
				else
				{
					return Surges[Surges.Count - 1];
				}

			}
		}
		bool Surging = false;
		bool delaying = false;
		Timer SurgingTimer = null;
		Timer DelayingTimer = null;

        public MainViewController (IntPtr handle) : base (handle)
        {
        }

		/// <summary>
		/// this gets called everytime the app comes to the front, including resume from minimized, so don't clear!
		/// </summary>
		public override void ViewDidLoad()
		{
			if (Surges == null)
			{
				this.Surges = new List<SurgeItem>();
				LoadSurgeFile();
			}
		}

		internal void LoadSurgeFile()
		{
			// if file exist
			string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string path = Path.Combine(docs, FILENAME_HISTORY);

			if (!System.IO.File.Exists(path)) { return; }

			// read all text from filee
			string[] surgeHistoryText = File.ReadAllLines(path);
			if (surgeHistoryText.Length == 0) { return; }

			// foreach line
			foreach (string s in surgeHistoryText)
			{
				// call Surge constructor and add to list
				this.Surges.Add(new SurgeItem(s));
			}

			// and load current UI if we have a surge currently
			if (CurrentSurge != null)
			{
				StartDisplayingCurrentSurge();
			}
			else
			{
				StartDisplayingDelay();
			}
		}

		internal void SaveSurgeFile()
		{
			StringBuilder sb = new StringBuilder();

			// save the history first...

			// foreach surge
			if (this.Surges != null)
			{
				for (int i = 0; i < this.Surges.Count; i++)
				{
					SurgeItem s = Surges[i];

					if (i > 0)
					{
						// create a newline in the file for each surge
						sb.Append(Environment.NewLine);
					}

					// get string representation of this item
					s.Save(sb);
				}
			}

 			// write to filee
			string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string path = Path.Combine(docs, FILENAME_HISTORY);
			System.IO.File.WriteAllText(path, sb.ToString());
		}

		partial void btnStart_Click(UIButton sender)
		{
			// setup the new current surge object model
			SurgeItem si = new SurgeItem();
			// and set the start time
			si.start = DateTime.Now;

			// add to our collection
			Surges.Add(si);

			// and show current surge on screen
			StartDisplayingCurrentSurge();
		}

		partial void btnStop_Click(UIButton sender)
		{
			// if we do have a current surge, add the stop value to it.
			if (CurrentSurge != null)
			{
				CurrentSurge.end = DateTime.Now;
			}

			StartDisplayingDelay();
		}

		partial void btnClear_Click(UIButton sender)
		{
			// reset the state flags
			Surging = false;
			delaying = false;

			// reset the labels to default
			lblCurrentSurgeValue.Text = "0:00";
			lblDelayValue.Text = "0:00";

			// reset the buttons to default
			this.btnStart.Enabled = true;
			this.btnStop.Enabled = false;

			// clear history too!
			Surges = new List<SurgeItem>();
		}

		void StartDisplayingCurrentSurge()
		{
			// set all the state flags
			Surging = true;
			delaying = false;

			// and button states
			this.btnStart.Enabled = false;
			this.btnStop.Enabled = true;
   
			// clear the previous surge value, immediately
			lblCurrentSurgeValue.Text = "0:00";

			// Create a timer that waits one second, then invokes every second, calling the handle metho
			SurgingTimer = new Timer(new TimerCallback(HandleSurgeTimerCallback), Surging, 1000, 1000);
		}

		void StartDisplayingDelay()
		{
			this.btnStart.Enabled = true;
			this.btnStop.Enabled = false;

			// start the timer to show the delay labe
			Surging = false;
			delaying = true;

			// Create a timer that waits one second, then invokes every second, calling the handle method
			DelayingTimer = new Timer(new TimerCallback(HandleDelayTimerCallback), delaying, 1000, 1000);
		}

		void HandleSurgeTimerCallback(object state)
		{
			// only update if we're still surging.
			if (!Surging)
			{
				if (this.SurgingTimer != null)
				{
					// then we've stopped, so destroy this timer.
					this.SurgingTimer.Dispose();
					this.SurgingTimer = null;
				}
				return;
			}

			// update both, since we're tracking start to start
			DisplayCurrent();
			DisplayDelay();
		}

		void DisplayCurrent()
		{
			// calculate the new time to display
			TimeSpan ts = (DateTime.Now - CurrentSurge.start).GetValueOrDefault();
			string s = ts.TotalSeconds.ToString("N1") + " sec";

			// call the main thread to update the UI
			InvokeOnMainThread(delegate
			{
				this.lblCurrentSurgeValue.Text = s;
			});
		}

		void HandleDelayTimerCallback(object state)
		{
			// only update if we're still delaying
			if (!delaying)
			{
				if (this.DelayingTimer != null)
				{
					// then we've stopped, so destroy this timer
					this.DelayingTimer.Dispose();
					this.DelayingTimer = null;
				}
				return;
			}

			DisplayDelay();
		}

		void DisplayDelay()
		{
			// calculate the delay since the last completed surge
			if (LastSurge != null)
			{
				TimeSpan ts = (DateTime.Now - LastSurge.start).GetValueOrDefault();
				string s = ts.Hours + ":" + ts.Minutes.ToString("D2") + ":" + ts.Seconds.ToString("D2");

				// update the UI on the main thread
				InvokeOnMainThread(delegate
				{
					this.lblDelayValue.Text = s;
				});
			}
		}

		// this method is used by the controller to pass the model to the history controller
		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			// pass the history collection to the new view controlle
			if (segue.DestinationViewController != null &&
			    segue.DestinationViewController is SurgeHistoryController)
			{
				(segue.DestinationViewController as SurgeHistoryController).Surges = Surges;
			}
		}
	}
}