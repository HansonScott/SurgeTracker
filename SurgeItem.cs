using System;
using System.Text;

namespace SurgeTracker
{
	public class SurgeItem
	{
		public static char Delimiter = '|';

		public DateTime? start;
		public DateTime? end;
		public int Intensity;

		public SurgeItem() { }

		public SurgeItem(string s)
		{
			LoadData(s);
		}

		public string Description 
		{
			get
			{
				string result = string.Empty;
				if (start != null)
				{
					result = this.start.GetValueOrDefault().ToString("MM/dd/yy HH:mm");
				}

				result += '\t';
				TimeSpan ts;
				if (end != null)
				{
					ts = (end - start).GetValueOrDefault();
				}
				else // it is current!
				{
					ts = (DateTime.Now - this.start).GetValueOrDefault();
				}

				result += ts.TotalSeconds.ToString("N1") + " sec";
				return result;
			}
		}

		void LoadData(string s)
		{
			// parse start
			string[] parts = s.Split(Delimiter);
			if (parts.Length > 0 &&
			    !String.IsNullOrEmpty(parts[0]))
			{
				DateTime dtStart;
				if (DateTime.TryParse(parts[0], out dtStart))
				{
					this.start = dtStart;
				}
			}

			// parse end
			if (parts.Length > 1 &&
			    !String.IsNullOrEmpty(parts[1]))
			{
				DateTime dtEnd;
				if (DateTime.TryParse(parts[1], out dtEnd))
				{
					this.end = dtEnd;
				}
			}

			// parse intensity
			if (parts.Length > 2 &&
			   !String.IsNullOrEmpty(parts[2]))
			{
				Int32.TryParse(parts[2], out this.Intensity);
			}
		}

		internal void Save(StringBuilder sb)
		{
			if (this.start != null)
			{
				sb.Append(start.Value.ToString());
			}

			sb.Append(Delimiter);
			if (this.end != null)
			{
				sb.Append(this.end.ToString());
			}

			sb.Append(Delimiter);
			sb.Append(this.Intensity);
		}
	}
}
