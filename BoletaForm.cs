using System.Data;
using System.Drawing.Printing;

namespace ScrapSystemm
{
    public partial class BoletaForm : Form
    {
        private readonly DataView _view;
        private PrintDocument? _printDoc;
        private int _printRowIndex;
        private DataTable? _currentGrouped; // asegura la misma fuente para el grid y la impresión

        public BoletaForm(DataTable boleta, string turnoActual, string lineaActual, string numeroParteActual)
        {
            InitializeComponent();

            gridBoleta.AutoGenerateColumns = false;
            gridBoleta.Columns.Clear();
            gridBoleta.RowHeadersVisible = false;
            gridBoleta.AllowUserToAddRows = false;
            gridBoleta.AllowUserToDeleteRows = false;
            gridBoleta.ReadOnly = true;
            gridBoleta.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            gridBoleta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Turno", HeaderText = "Turno", Width = 50 });
            gridBoleta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Linea", HeaderText = "Linea", Width = 90 });
            gridBoleta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NumeroParte", HeaderText = "Numero de Parte", Width = 160 });
            gridBoleta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DescripcionDefecto", HeaderText = "Descripcion del Defecto", Width = 200 });
            gridBoleta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Componentes", HeaderText = "Componentes (faltantes)", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True, Alignment = DataGridViewContentAlignment.TopLeft } });
            gridBoleta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "N0" } });

            _view = new DataView(boleta);
            ApplyFilter(turnoActual, lineaActual, numeroParteActual);

            btnImprimir.Click += BtnImprimir_Click;
        }

        private void BtnImprimir_Click(object? sender, EventArgs e)
        {
            try
            {
                _currentGrouped ??= BuildGroupedTable(_view);
                if (_currentGrouped.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para imprimir.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                gridBoleta.DataSource = _currentGrouped;
                gridBoleta.Refresh();

                var ps = new PrinterSettings();
                if (!ps.IsValid)
                {
                    MessageBox.Show("No hay impresora predeterminada disponible.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _printDoc = new PrintDocument
                {
                    DocumentName = "Boleta de Rechazos",
                    PrinterSettings = ps
                };
                _printDoc.DefaultPageSettings.Landscape = true;
                _printDoc.DefaultPageSettings.Margins = new Margins(30, 30, 30, 30);
                _printDoc.OriginAtMargins = true;

                // IMPORTANTE: reiniciar el índice de fila SIEMPRE que comienza una tarea de impresión
                _printDoc.BeginPrint += (s2, e2) => { _printRowIndex = 0; };
                _printDoc.PrintPage += PrintDoc_PrintPage;

                using var dlg = new PrintPreviewDialog
                {
                    Document = _printDoc,
                    UseAntiAlias = true,
                    Width = 1200,
                    Height = 900
                };
                dlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo mostrar la vista previa: {ex.Message}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object? sender, PrintPageEventArgs e)
        {
            var dt = _currentGrouped;
            if (dt is null || dt.Rows.Count == 0)
            {
                e.HasMorePages = false;
                return;
            }

            var g = e.Graphics;
            var margin = e.MarginBounds;
            int x = margin.Left;
            int y = margin.Top;

            using var titleFont = new Font("Segoe UI", 11, FontStyle.Bold);
            using var headerFont = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            using var contentFont = new Font("Segoe UI", 8.25f, FontStyle.Regular);

            int lineHeight = (int)Math.Ceiling(contentFont.GetHeight(g)) + 4;

            g.DrawString("Boleta de Rechazos", titleFont, Brushes.Black, x, y);
            y += titleFont.Height + 6;

            var headers = new[] { "Turno", "Linea", "Numero de Parte", "Descripcion del Defecto", "Componentes (faltantes)", "Cantidad" };
            int wTurno = 45, wLinea = 90, wNum = 160, wDef = 200, wCant = 60;
            int wComp = margin.Width - (wTurno + wLinea + wNum + wDef + wCant) - 8;
            var colWidths = new[] { wTurno, wLinea, wNum, wDef, wComp, wCant };

            int cx = x;
            for (int i = 0; i < headers.Length; i++)
            {
                g.DrawString(headers[i], headerFont, Brushes.Black, new RectangleF(cx, y, colWidths[i], lineHeight));
                cx += colWidths[i];
            }
            y += lineHeight + 3;

            var sf = new StringFormat(StringFormatFlags.LineLimit) { Trimming = StringTrimming.Word };

            while (_printRowIndex < dt.Rows.Count)
            {
                var row = dt.Rows[_printRowIndex];
                int rowHeight = lineHeight;
                string comps = Convert.ToString(row["Componentes"]) ?? string.Empty;
                var compsSize = g.MeasureString(comps, contentFont, colWidths[4], sf);
                rowHeight = Math.Max(rowHeight, (int)Math.Ceiling(compsSize.Height) + 4);

                if (y + rowHeight > margin.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                cx = x;
                g.DrawString(Convert.ToString(row["Turno"]) ?? "", contentFont, Brushes.Black, new RectangleF(cx, y, colWidths[0], rowHeight)); cx += colWidths[0];
                g.DrawString(Convert.ToString(row["Linea"]) ?? "", contentFont, Brushes.Black, new RectangleF(cx, y, colWidths[1], rowHeight)); cx += colWidths[1];
                g.DrawString(Convert.ToString(row["NumeroParte"]) ?? "", contentFont, Brushes.Black, new RectangleF(cx, y, colWidths[2], rowHeight)); cx += colWidths[2];
                g.DrawString(Convert.ToString(row["DescripcionDefecto"]) ?? "", contentFont, Brushes.Black, new RectangleF(cx, y, colWidths[3], rowHeight)); cx += colWidths[3];
                g.DrawString(comps, contentFont, Brushes.Black, new RectangleF(cx, y, colWidths[4], rowHeight), sf); cx += colWidths[4];
                g.DrawString(string.Format("{0:N0}", row["Cantidad"]), contentFont, Brushes.Black, new RectangleF(cx, y, colWidths[5], rowHeight));

                y += rowHeight + 4;
                _printRowIndex++;
            }

            e.HasMorePages = false;
        }

        public void ApplyFilter(string turnoActual, string lineaActual, string numeroParteActual)
        {
            var filters = new List<string>();
            if (!string.IsNullOrWhiteSpace(turnoActual)) filters.Add($"Turno = '{turnoActual.Replace("'", "''")}'");
            if (!string.IsNullOrWhiteSpace(lineaActual)) filters.Add($"Linea = '{lineaActual.Replace("'", "''")}'");
            if (!string.IsNullOrWhiteSpace(numeroParteActual)) filters.Add($"NumeroParte = '{numeroParteActual.Replace("'", "''")}'");

            if (filters.Count == 0)
            {
                _view.RowFilter = null; // limpiar cualquier filtro previo
            }
            else
            {
                _view.RowFilter = string.Join(" AND ", filters);
            }

            _currentGrouped = BuildGroupedTable(_view);
            gridBoleta.DataSource = _currentGrouped;
            gridBoleta.Refresh();
        }

        private static string Norm(string? s)
            => (s ?? string.Empty).Trim().ToUpperInvariant();

        private static DataTable BuildGroupedTable(DataView view)
        {
            var table = new DataTable();
            table.Columns.Add("Turno", typeof(string));
            table.Columns.Add("Linea", typeof(string));
            table.Columns.Add("NumeroParte", typeof(string));
            table.Columns.Add("DescripcionDefecto", typeof(string));
            table.Columns.Add("Componentes", typeof(string));
            table.Columns.Add("Cantidad", typeof(int));

            var rows = view.Cast<DataRowView>().ToList();

            // 1) Manejar registros provenientes de TRW: sumar cantidades, campos Turno/Linea vacíos y Componentes vacío
            var trwRows = rows.Where(r => view.Table.Columns.Contains("Origen") &&
                                           r["Origen"] != DBNull.Value &&
                                           string.Equals(Convert.ToString(r["Origen"]), "TRW", StringComparison.OrdinalIgnoreCase))
                               .ToList();

            var trwGroups = trwRows
                .GroupBy(r => new
                {
                    Numero = Convert.ToString(r["NumeroParte"]) ?? string.Empty,
                    Desc = Convert.ToString(r["DescripcionDefecto"]) ?? string.Empty
                });

            foreach (var g in trwGroups)
            {
                decimal total = 0m;
                foreach (var r in g)
                {
                    if (r["Cantidad"] != DBNull.Value) total += Convert.ToDecimal(r["Cantidad"]);
                }
                var row = table.NewRow();
                row["Turno"] = string.Empty;
                row["Linea"] = string.Empty;
                row["NumeroParte"] = g.Key.Numero;
                row["DescripcionDefecto"] = g.Key.Desc;
                row["Componentes"] = string.Empty; // no mostrar faltantes
                row["Cantidad"] = (int)Math.Round(total, MidpointRounding.AwayFromZero);
                table.Rows.Add(row);
            }

            // 2) Resto de registros (RECHAZO, etc.) con lógica de faltantes y conteo por tiro
            var normalRows = rows.Except(trwRows).ToList();
            if (normalRows.Count > 0)
            {
                var registros = normalRows
                    .GroupBy(r => Convert.ToString(r["RegistroId"]))
                    .Where(g => !string.IsNullOrEmpty(g.Key));

                var agg = new Dictionary<string, (HashSet<string> Turnos, string Linea, string Numero, string Defecto, string Componentes, int Count)>();

                foreach (var reg in registros)
                {
                    var activos = reg.Where(r =>
                    {
                        var qty = r["Cantidad"] != DBNull.Value ? Convert.ToDecimal(r["Cantidad"]) : 0m;
                        var omit = view.Table.Columns.Contains("Omitido") && r["Omitido"] != DBNull.Value && Convert.ToBoolean(r["Omitido"]);
                        return qty > 0m && !omit;
                    }).ToList();
                    var omitidos = reg.Where(r =>
                    {
                        var qty = r["Cantidad"] != DBNull.Value ? Convert.ToDecimal(r["Cantidad"]) : 0m;
                        var omit = view.Table.Columns.Contains("Omitido") && r["Omitido"] != DBNull.Value && Convert.ToBoolean(r["Omitido"]);
                        return qty <= 0m || omit;
                    }).ToList();

                    var baseRows = activos.Count > 0 ? activos : omitidos;
                    if (baseRows.Count == 0) continue;

                    var first = baseRows.First();
                    string turno = Convert.ToString(first["Turno"]) ?? string.Empty;
                    string linea = Convert.ToString(first["Linea"]) ?? string.Empty;
                    string numero = Convert.ToString(first["NumeroParte"]) ?? string.Empty;
                    string defecto = Convert.ToString(first["DescripcionDefecto"]) ?? string.Empty;

                    string componentesTexto;
                    string faltantesKey;
                    if (omitidos.Count == 0)
                    {
                        componentesTexto = "Pieza terminada";
                        faltantesKey = "__TERMINADA__";
                    }
                    else
                    {
                        var faltantes = omitidos
                            .Select(r => new
                            {
                                Code = (r["ComponenteCodigo"] ?? string.Empty).ToString(),
                                Desc = (r["Componente"] ?? string.Empty).ToString(),
                                Unit = (r["Unidad"] ?? string.Empty).ToString()
                            })
                            .Select(x => new { Code = Norm(x.Code), Desc = Norm(x.Desc), Unit = Norm(x.Unit), CodeRaw = x.Code, DescRaw = x.Desc, UnitRaw = x.Unit })
                            .OrderBy(x => x.Code)
                            .ThenBy(x => x.Desc)
                            .ThenBy(x => x.Unit)
                            .ToList();

                        faltantesKey = string.Join("|", faltantes.Select(x => $"{x.Code}~{x.Desc}~{x.Unit}"));

                        var lines = faltantes.Select(x => $"- {x.CodeRaw} | {x.DescRaw} (x0 {x.UnitRaw})");
                        componentesTexto = string.Join(Environment.NewLine, lines);
                    }

                    var key = string.Join("||", new[] { Norm(linea), Norm(numero), Norm(defecto), faltantesKey });
                    if (agg.TryGetValue(key, out var entry))
                    {
                        entry.Turnos.Add(Norm(turno));
                        agg[key] = (entry.Turnos, entry.Linea, entry.Numero, entry.Defecto, entry.Componentes, entry.Count + 1);
                    }
                    else
                    {
                        var turns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        if (!string.IsNullOrWhiteSpace(turno)) turns.Add(Norm(turno));
                        agg[key] = (turns, linea.Trim(), numero.Trim(), defecto.Trim(), componentesTexto, 1);
                    }
                }

                foreach (var kvp in agg
                             .OrderBy(v => Norm(v.Value.Linea))
                             .ThenBy(v => Norm(v.Value.Numero))
                             .ThenBy(v => Norm(v.Value.Defecto)))
                {
                    var row = table.NewRow();
                    var turnos = kvp.Value.Turnos.OrderBy(t => t).ToArray();
                    row["Turno"] = string.Join(",", turnos);
                    row["Linea"] = kvp.Value.Linea;
                    row["NumeroParte"] = kvp.Value.Numero;
                    row["DescripcionDefecto"] = kvp.Value.Defecto;
                    row["Componentes"] = kvp.Value.Componentes;
                    row["Cantidad"] = kvp.Value.Count;
                    table.Rows.Add(row);
                }
            }

            return table;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
