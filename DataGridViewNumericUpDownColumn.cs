using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ScrapSystemm
{
    // Editing control that hosts a NumericUpDown for DataGridView cells
    [DesignTimeVisible(false)]
    internal class DataGridViewNumericUpDownEditingControl : NumericUpDown, IDataGridViewEditingControl
    {
        private bool _valueChanged;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridView EditingControlDataGridView { get; set; } = null!;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object EditingControlFormattedValue
        {
            get => Value.ToString(CultureInfo.CurrentCulture);
            set
            {
                if (value is string s && decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out var dec))
                {
                    Value = CoerceInRange(dec);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EditingControlRowIndex { get; set; }

        public bool RepositionEditingControlOnValueChange => false;
        public Cursor EditingPanelCursor => Cursors.Default;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EditingControlValueChanged
        {
            get => _valueChanged;
            set => _valueChanged = value;
        }
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
            TextAlign = HorizontalAlignment.Right;
        }
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            // Let NumericUpDown handle navigation keys that make sense
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Home:
                case Keys.End:
                case Keys.PageUp:
                case Keys.PageDown:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) => EditingControlFormattedValue;
        public void PrepareEditingControlForEdit(bool selectAll) { /* no-op */ }

        protected override void OnValueChanged(EventArgs e)
        {
            _valueChanged = true;
            EditingControlDataGridView?.NotifyCurrentCellDirty(true);
            base.OnValueChanged(e);
        }

        private decimal CoerceInRange(decimal value)
        {
            if (value < Minimum) return Minimum;
            if (value > Maximum) return Maximum;
            return value;
        }
    }

    internal class DataGridViewNumericUpDownCell : DataGridViewTextBoxCell
    {
        public DataGridViewNumericUpDownCell()
        {
            this.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public override Type EditType => typeof(DataGridViewNumericUpDownEditingControl);
        public override Type ValueType => typeof(decimal);
        public override object DefaultNewRowValue => 0m;

        public override void InitializeEditingControl(int rowIndex, object? initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            if (DataGridView?.EditingControl is DataGridViewNumericUpDownEditingControl ctl)
            {
                // Copy column properties to the editing control
                if (OwningColumn is DataGridViewNumericUpDownColumn col)
                {
                    ctl.DecimalPlaces = col.DecimalPlaces;
                    ctl.Increment = col.Increment;
                    ctl.Minimum = col.Minimum;
                    ctl.Maximum = col.Maximum;
                    ctl.ThousandsSeparator = col.ThousandsSeparator;
                    ctl.TextAlign = col.TextAlign;
                }

                decimal val = 0m;
                if (this.Value is decimal d)
                    val = d;
                else if (this.Value != null && decimal.TryParse(Convert.ToString(this.Value, CultureInfo.CurrentCulture), out var parsed))
                    val = parsed;

                // Ensure in range
                if (val < ctl.Minimum) val = ctl.Minimum;
                if (val > ctl.Maximum) val = ctl.Maximum;
                ctl.Value = val;
            }
        }

        public override object? ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            if (formattedValue is string s && decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out var dec))
                return dec;
            return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
        }
    }

    [DesignTimeVisible(false)]
    public class DataGridViewNumericUpDownColumn : DataGridViewColumn
    {
        public DataGridViewNumericUpDownColumn() : base(new DataGridViewNumericUpDownCell())
        {
            this.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        [DefaultValue(3)]
        public int DecimalPlaces { get; set; } = 3;
        [DefaultValue(typeof(decimal), "0.001")]
        public decimal Increment { get; set; } = 0.001m;
        [DefaultValue(typeof(decimal), "0")]
        public decimal Minimum { get; set; } = 0m;
        [DefaultValue(typeof(decimal), "1000000")]
        public decimal Maximum { get; set; } = 1000000m;
        [DefaultValue(false)]
        public bool ThousandsSeparator { get; set; } = false;
        [DefaultValue(HorizontalAlignment.Right)]
        public HorizontalAlignment TextAlign { get; set; } = HorizontalAlignment.Right;

        public override object Clone()
        {
            var col = (DataGridViewNumericUpDownColumn)base.Clone();
            col.DecimalPlaces = this.DecimalPlaces;
            col.Increment = this.Increment;
            col.Minimum = this.Minimum;
            col.Maximum = this.Maximum;
            col.ThousandsSeparator = this.ThousandsSeparator;
            col.TextAlign = this.TextAlign;
            return col;
        }
    }
}
