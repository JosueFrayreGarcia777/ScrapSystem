using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScrapSystemm
{
    internal static class QuantityPrompt
    {
        public static decimal? Show(IWin32Window owner, string title, string message, decimal defaultValue = 1m, int decimalPlaces = 3)
        {
            using var form = new Form
            {
                Text = title,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(320, 140),
                MinimizeBox = false,
                MaximizeBox = false,
                ShowInTaskbar = false
            };

            var lbl = new Label { AutoSize = true, Text = message, Location = new Point(12, 12) };
            var nud = new NumericUpDown
            {
                Location = new Point(15, 40),
                Width = 280,
                DecimalPlaces = decimalPlaces,
                Increment = 0.001m,
                Minimum = 0m,
                Maximum = 1000000m,
                Value = defaultValue
            };

            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(140, 90), Width = 70 };
            var btnCancel = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Location = new Point(225, 90), Width = 70 };

            form.Controls.Add(lbl);
            form.Controls.Add(nud);
            form.Controls.Add(btnOk);
            form.Controls.Add(btnCancel);
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            var dr = form.ShowDialog(owner);
            if (dr == DialogResult.OK)
                return nud.Value;
            return null;
        }
    }
}
