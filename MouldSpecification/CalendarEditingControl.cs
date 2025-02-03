using System;
using System.Windows.Forms;

/// <summary>
/// Represents a custom for a DataGridView that uses a CalendraCell for displaying and editing date values.
/// </summary>
public class CalendarColumn : DataGridViewColumn
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarColumn"/> class with a default <see cref="CalendarCell"/>.
    /// </summary>
    public CalendarColumn() : base(new CalendarCell())
    {
    }

    /// <summary>
    /// Gets or sets the template for the cell used in this column.
    /// Ensures that the cell used for the template is off type <see cref="CalendarCell"/>.
    /// </summary>
    /// <exception cref="InvalidCastException">
    /// Thrown when the value assigned is not of type <see cref="CalendarCell"/>.
    /// </exception>
    public override DataGridViewCell CellTemplate
    {
        get
        {
            // Return the base CellTemplate.
            return base.CellTemplate;
        }
        set
        {
            // Ensure that the cell used for the template is a CalendarCell.
            if (value != null &&
                !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
            {
                // If the value is not of type CalendarCell, throw an InvalidCastException.
                throw new InvalidCastException("Must be a CalendarCell");
            }

            // Set the CellTemplate to the provided value.
            base.CellTemplate = value;
        }
    }
}

/// <summary>
/// Represents a custom cell in a DataGridView that displays and allows editing of date values using a calendar control.
/// Inherits from <see cref="DataGridViewTextBoxCell"/>.
/// </summary>
public class CalendarCell : DataGridViewTextBoxCell
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarCell"/> class.
    /// Sets the default date format to short date (dd-MM-yyyy).
    /// </summary>
    public CalendarCell()
        : base()
    {
        // Set the short date format for the calendar cell display.
        this.Style.Format = "dd-MM-yyyy";
    }

    /// <summary>
    /// Intializes the editing control for the calendar cell.
    /// Sets the value of the editing control to the current value or to the default new row value if the value is null.
    /// </summary>
    /// <param name="rowIndex"> The index of the row containing the cell. </param>
    /// <param name="initialFormattedValue"> The initial formatted value to display in the editing control. </param>
    /// <param name="dataGridViewCellStyle"> The style applied to the cell during editing. </param>
    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        // Initialize the base editing control first.
        base.InitializeEditingControl(rowIndex, initialFormattedValue,dataGridViewCellStyle);

        // Get the calendar editing control from the DataGridView's editing control.
        CalendarEditingControl ctl =DataGridView.EditingControl as CalendarEditingControl;
        
        // If the current cell's value is null or an empty string, use the default new row value (current date).
        if ((string.IsNullOrEmpty(this.Value as string)))
        {
            ctl.Value = (DateTime)this.DefaultNewRowValue;
        }
        else
        {
            // Otherwise, set the editing control's value to the current cell's value.
            ctl.Value = (DateTime)this.Value;
        }
    }

    /// <summary>
    /// Gets the type of the editing control used by the <see cref="CalendarCell"/>
    /// </summary>
    public override Type EditType
    {
        get
        {
            // Return the type of the editing control that CalendarCell uses (CalendarEditingControl).
            return typeof(CalendarEditingControl);
        }
    }

    /// <summary>
    /// Gets the type of the value that the <see cref="CalendarCell"/> contains.
    /// </summary>
    public override Type ValueType
    {
        get
        {
            // Return the type of the value contained in the CalendarCell (DateTime).
            return typeof(DateTime);
        }
    }

    /// <summary>
    /// Gets the default value to use when a new row is added and no value is specified.
    /// </summary>
    public override object DefaultNewRowValue
    {
        get
        {
            // Use the current date and time as the default value.
            return DateTime.Now;
        }
    }
}

/// <summary>
/// Represents a custom calendar editing control used in a DataGridView.
/// It is based on the <see cref="DateTimePicker"/> control and implements.
/// the <see cref="IDataGridViewEditingControl"/> interface for editing date values in a DataGridView.
/// </summary>
class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
{
    DataGridView dataGridView;
    private bool valueChanged = false;
    int rowIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarEditingControl"/> class.
    /// Sets the format of the DateTimePicker control to short date.
    /// </summary>
    public CalendarEditingControl()
    {
        // Set the format of the DateTimePicker to short date.
        this.Format = DateTimePickerFormat.Short;
    }

    /// <summary>
    /// Gets or sets the formatted value of the editing control.
    /// The formatted value is represented as a short date string.
    /// </summary>
    public object EditingControlFormattedValue
    {
        get
        {
            // Return the value of the DateTimePicker as a short date string.
            return this.Value.ToShortDateString();
        }
        set
        {
            if (value is String)
            {
                try
                {
                    // Try parsing the string value into a DateTime.
                    this.Value = DateTime.Parse((String)value);
                }
                catch
                {
                    // If an exception occurs (e.g., invalid date format), use the default date.
                    this.Value = DateTime.Now;
                }
            }
        }
    }

    /// <summary>
    /// Gets the formatted value of the editing control for a specified error context.
    /// </summary>
    /// <param name="context"> The context in which the error occured (not used in this case). </param>
    /// <returns></returns>
    public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {
        return EditingControlFormattedValue;
    }

    /// <summary>
    /// Applies the cell style to the editing control, including font and color settings.
    /// </summary>
    /// <param name="dataGridViewCellStyle"> The style to apply to the editing control. </param>
    public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
        this.Font = dataGridViewCellStyle.Font;
        this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
        this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
    }

    /// <summary>
    /// Gets or sets the row index the editing control.
    /// </summary>
    public int EditingControlRowIndex
    {
        get
        {
            return rowIndex;
        }
        set
        {
            rowIndex = value;
        }
    }

    /// <summary>
    /// Datermines whether the editing control wants to process a particular input key.
    /// </summary>
    /// <param name="key"> The key to process. </param>
    /// <param name="dataGridViewWantsInputKey"> Indicates whether the DataGridView wants to process the key. </param>
    /// <returns> True if the editing control wants to process the key; otherwise, false. </returns>
    public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
    {
        // Handle common keys (e.g., arrow keys, home, end, delete) within the DateTimePicker.
        switch (key & Keys.KeyCode)
        {
            case Keys.Left:
            case Keys.Up:
            case Keys.Down:
            case Keys.Right:
            case Keys.Home:
            case Keys.End:
            case Keys.PageDown:
            case Keys.PageUp:
            case Keys.Delete:
                return true;
            default:
                return !dataGridViewWantsInputKey;
        }
    }

    /// <summary>
    /// Prepares the editing control for editing. In this case, no preparation is needed.
    /// </summary>
    /// <param name="selectAll"> Indicates whether all text should be selected. </param>
    public void PrepareEditingControlForEdit(bool selectAll)
    {
        // No preparation needs to be done.
    }

    /// <summary>
    /// Gets a value indicating whether the editing control should reposition its cursor when the value changes.
    /// </summary>
    public bool RepositionEditingControlOnValueChange
    {
        get
        {
            // No need to reposition the control when the value changes.
            return false;
        }
    }

    /// <summary>
    /// Gets or sets the DateGridView associated with the editing control.
    /// </summary>
    public DataGridView EditingControlDataGridView
    {
        get
        {
            return dataGridView;
        }
        set
        {
            dataGridView = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating wheher the value of the editing control has changed.
    /// </summary>
    public bool EditingControlValueChanged
    {
        get
        {
            return valueChanged;
        }
        set
        {
            valueChanged = value;
        }
    }

    /// <summary>
    /// Gets the cursor used by the editing panel of the control.
    /// </summary>
    public Cursor EditingPanelCursor
    {
        get
        {
            return base.Cursor;
        }
    }

    /// <summary>
    /// Handles the <see cref="ValueChanged"/> event of the DateTimePicker control.
    /// Notifies the DataGridView that the value of the cell has changed.
    /// </summary>
    /// <param name="eventargs"> The event arguments for the value change event. </param>
    protected override void OnValueChanged(EventArgs eventargs)
    {
        // Mark the editing control as having a value change and notify the DataGridView.
        valueChanged = true;
        this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
        base.OnValueChanged(eventargs);
    }
}
