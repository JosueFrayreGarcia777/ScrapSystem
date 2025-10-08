using Microsoft.Data.SqlClient;
using System.Data;

namespace ScrapSystemm
{
    public partial class SubBOMForm : Form
    {
        private readonly string _dbConnection;
        private readonly string _materialBase;
        private readonly DataTable _boleta;
        private DataGridView _gridBOM;

        public SubBOMForm(string dbConnection, string materialBase, DataTable boleta)
        {
            InitializeComponent();
            _dbConnection = dbConnection;
            _materialBase = materialBase;
            _boleta = boleta;
            this.Load += SubBOMForm_Load;
            btnAgregar.Click += BtnAgregar_Click;
        }

        private async void SubBOMForm_Load(object? sender, EventArgs e)
        {
            await CargarBOMAsync();
        }

        private async Task CargarBOMAsync()
        {
            try
            {
                await using var conn = new SqlConnection(_dbConnection);
                await conn.OpenAsync();
                var sql = @"SELECT [Component], [MaterialDescription], [ComponentQuantity], [ComponentUnity] FROM [dbo].[BOM] WHERE [Material] = @material";
                await using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@material", SqlDbType.VarChar, 100) { Value = _materialBase });
                var table = new DataTable();
                table.Columns.Add("Component", typeof(string));
                table.Columns.Add("MaterialDescription", typeof(string));
                table.Columns.Add("ComponentQuantity", typeof(decimal));
                table.Columns.Add("ComponentUnity", typeof(string));
                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var compCode = reader["Component"]?.ToString() ?? string.Empty;
                    var desc = reader["MaterialDescription"]?.ToString() ?? string.Empty;
                    var qtyObj = reader["ComponentQuantity"];
                    decimal qty = 0;
                    if (qtyObj != DBNull.Value) qty = Convert.ToDecimal(qtyObj);
                    var unity = (reader["ComponentUnity"]?.ToString() ?? string.Empty).Trim();
                    if (string.IsNullOrWhiteSpace(unity))
                        continue;
                    table.Rows.Add(compCode, desc, qty, unity);
                }
                RenderBOM(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar BOM: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderBOM(DataTable datos)
        {
            panelMain.SuspendLayout();
            try
            {
                panelMain.Controls.Clear();
                var grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = false,
                    AutoGenerateColumns = false,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.CellSelect,
                    EditMode = DataGridViewEditMode.EditOnEnter
                };
                var colCode = new DataGridViewTextBoxColumn
                {
                    Name = "Component",
                    DataPropertyName = "Component",
                    HeaderText = "Codigo",
                    Visible = false
                };
                var colDesc = new DataGridViewTextBoxColumn
                {
                    Name = "MaterialDescription",
                    DataPropertyName = "MaterialDescription",
                    HeaderText = "Material Description",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    ReadOnly = true,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                var colQty = new DataGridViewNumericUpDownColumn
                {
                    Name = "ComponentQuantity",
                    DataPropertyName = "ComponentQuantity",
                    HeaderText = "Qty",
                    Width = 100,
                    ReadOnly = false,
                    DecimalPlaces = 3,
                    Increment = 0.001m,
                    Minimum = 0m,
                    Maximum = 1000000m,
                    ThousandsSeparator = false
                };
                var colUnit = new DataGridViewTextBoxColumn
                {
                    Name = "ComponentUnity",
                    DataPropertyName = "ComponentUnity",
                    HeaderText = "Unit",
                    Width = 80,
                    ReadOnly = true,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                grid.Columns.Add(colCode);
                grid.Columns.Add(colDesc);
                grid.Columns.Add(colQty);
                grid.Columns.Add(colUnit);
                grid.CellEnter += (s, e) =>
                {
                    if (e.ColumnIndex == colQty.Index && e.RowIndex >= 0)
                    {
                        grid.BeginEdit(true);
                    }
                };
                grid.DataError += (s, e) => { e.Cancel = true; };
                grid.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        foreach (DataGridViewCell cell in grid.SelectedCells)
                        {
                            if (cell.OwningColumn.DataPropertyName == "ComponentQuantity" && !cell.ReadOnly)
                            {
                                cell.Value = 0m;
                            }
                        }
                        e.Handled = true;
                    }
                };
                // Doble clic recursivo
                grid.CellDoubleClick += async (s, e) =>
                {
                    if (e.RowIndex >= 0 && e.ColumnIndex == colDesc.Index)
                    {
                        var compCode = grid.Rows[e.RowIndex].Cells[colCode.Index].Value?.ToString();
                        if (!string.IsNullOrWhiteSpace(compCode))
                        {
                            // Verificar si el componente tiene BOM antes de abrir la ventana
                            bool tieneBOM = false;
                            try
                            {
                                using var conn = new SqlConnection(_dbConnection);
                                conn.Open();
                                var sql = "SELECT COUNT(*) FROM [dbo].[BOM] WHERE [Material] = @material";
                                using var cmd = new SqlCommand(sql, conn);
                                cmd.Parameters.Add(new SqlParameter("@material", SqlDbType.VarChar, 100) { Value = compCode });
                                var count = (int)cmd.ExecuteScalar();
                                tieneBOM = count > 0;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al consultar BOM: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (!tieneBOM)
                            {
                                MessageBox.Show("Este componente no cuenta con BOM.", "Sin BOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            var subForm = new SubBOMForm(_dbConnection, compCode, _boleta);
                            subForm.ShowDialog(this);
                        }
                    }
                };
                grid.DataSource = datos;
                panelMain.Controls.Add(grid);
                _gridBOM = grid;
            }
            finally
            {
                panelMain.ResumeLayout();
            }
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            if (_gridBOM == null) return;
            var registroId = Guid.NewGuid().ToString();
            foreach (DataGridViewRow row in _gridBOM.Rows)
            {
                if (row.IsNewRow) continue;
                var unidad = Convert.ToString(row.Cells["ComponentUnity"].Value)?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(unidad)) continue;
                decimal qty = 0;
                var qtyObj = row.Cells["ComponentQuantity"].Value;
                if (qtyObj != null && qtyObj != DBNull.Value)
                {
                    decimal.TryParse(Convert.ToString(qtyObj), out qty);
                }
                var componenteDesc = Convert.ToString(row.Cells["MaterialDescription"].Value) ?? string.Empty;
                if (string.IsNullOrWhiteSpace(componenteDesc)) continue;
                var componenteCod = Convert.ToString(row.Cells["Component"].Value) ?? string.Empty;
                var newRow = _boleta.NewRow();
                newRow["Turno"] = string.Empty;
                newRow["Linea"] = string.Empty;
                newRow["NumeroParte"] = _materialBase;
                newRow["ComponenteCodigo"] = componenteCod;
                newRow["Componente"] = componenteDesc;
                newRow["DescripcionDefecto"] = string.Empty;
                newRow["Cantidad"] = qty;
                newRow["Unidad"] = unidad;
                newRow["RegistroId"] = registroId;
                newRow["Omitido"] = qty <= 0;
                newRow["Origen"] = "SUBBOM";
                _boleta.Rows.Add(newRow);
            }
            MessageBox.Show("Componentes agregados a la bitácora.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
