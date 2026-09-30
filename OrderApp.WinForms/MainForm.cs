using OrderApp.Core;

namespace OrderApp.WinForms;

/// <summary>
/// 畫面只負責「讀輸入、呼叫 Core、顯示結果」，不放商業邏輯。
/// </summary>
public class MainForm : Form
{
    private readonly NumericUpDown _price = new() { Maximum = 1_000_000, DecimalPlaces = 0, Width = 150 };
    private readonly NumericUpDown _qty = new() { Minimum = 1, Maximum = 10_000, Value = 1, Width = 150 };
    private readonly CheckBox _vip = new() { Text = "VIP 會員", AutoSize = true };
    private readonly Button _calc = new() { Text = "計算", Width = 150 };
    private readonly Label _total = new() { Text = "總金額：-", AutoSize = true, Font = new Font("Microsoft JhengHei UI", 14f) };

    public MainForm()
    {
        Text = $"訂單計算器 v{Application.ProductVersion.Split('+')[0]}";
        ClientSize = new Size(320, 230);
        StartPosition = FormStartPosition.CenterScreen;

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 2 };
        layout.Controls.Add(new Label { Text = "單價", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
        layout.Controls.Add(_price, 1, 0);
        layout.Controls.Add(new Label { Text = "數量", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        layout.Controls.Add(_qty, 1, 1);
        layout.Controls.Add(_vip, 1, 2);
        layout.Controls.Add(_calc, 1, 3);
        layout.Controls.Add(_total, 0, 4);
        layout.SetColumnSpan(_total, 2);
        Controls.Add(layout);

        _calc.Click += OnCalcClick;
    }

    private void OnCalcClick(object? sender, EventArgs e)
    {
        decimal total = PriceCalculator.Calculate(_price.Value, (int)_qty.Value, _vip.Checked);
        _total.Text = $"總金額：{total:N0}";
    }
}
