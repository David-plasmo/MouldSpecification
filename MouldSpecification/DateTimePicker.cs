//----------------------------------------------------------------------------------
// - Author			   - Pham Minh Tri
// - Last Updated      - 19/Nov/2003
//----------------------------------------------------------------------------------
// - Component:        - Nullable DateTimePicker
// - Version:          - 1.0
// - Description:      - A datetimepicker that allow null value.
//----------------------------------------------------------------------------------

using System;
using System.Windows.Forms;  

namespace UIComponent
{
	/// <summary>
	/// Summary description for DateTimePicker.
	/// </summary>
	public class DateTimePicker : System.Windows.Forms.DateTimePicker   
	{
		private DateTimePickerFormat oldFormat = DateTimePickerFormat.Long;
		private string oldCustomFormat = null;
		private bool bIsNull = false;

		public DateTimePicker() : base()
		{
		}

		public new DateTime Value 
		{
			get 
			{
				if (bIsNull)
					return DateTime.MinValue;
				else
					return base.Value;
			}
			set 
			{
				if (value == DateTime.MinValue)
				{
					if (bIsNull == false) 
					{
						oldFormat = this.Format;
						oldCustomFormat = this.CustomFormat;
						bIsNull = true;
					}

					this.Format = DateTimePickerFormat.Custom;
					this.CustomFormat = " ";
				}
				else 
				{
					if (bIsNull) 
					{
						this.Format = oldFormat;
						this.CustomFormat = oldCustomFormat;
						bIsNull = false;
					}
					base.Value = value;
				}
			}
		}

		protected override void OnCloseUp(EventArgs eventargs)
		{
			if (Control.MouseButtons == MouseButtons.None) 
			{
				if (bIsNull) 
				{
					this.Format = oldFormat;
					this.CustomFormat = oldCustomFormat;
					bIsNull = false;
				}
			}
			base.OnCloseUp (eventargs);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown (e);

			if (e.KeyCode == Keys.Delete)
				MessageBox.Show("setting minvalue");
				this.Value = Convert.ToDateTime("1900-01-01"); 

        }
	}

    public class DataGridView : System.Windows.Forms.DataGridView
    {
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {

            //MessageBox.Show("Key Press Detected=" + keyData.ToString());

            if ((keyData == ( Keys.Delete)))
            {
                MessageBox.Show("Key Press Detected=" + keyData.ToString());
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
