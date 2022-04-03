using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HouseholdmanagementTool.UI.WPFElements;

[DefaultEvent("OnSelectedIndexChanged")]
public sealed class CustomDropDown : UserControl
{
    private readonly Color backgroundColor;
    private readonly Color iconColor;
    private readonly Color listBackgroundColor;
    private readonly Color listTextColor;
    private readonly Color borderColor;

    private const int BorderSize = 1;
    private readonly ComboBox cmbList = new();
    private readonly Label lblText = new();
    private readonly Button btnIcon = new();

    public event EventHandler? OnSelectedIndexChanged;

    public CustomDropDown(Color backgroundColor, Color iconColor, Color listBackgroundColor, Color listTextColor, Color borderColor)
    {
        this.backgroundColor = backgroundColor;
        this.iconColor = iconColor;
        this.listBackgroundColor = listBackgroundColor;
        this.listTextColor = listTextColor;
        this.borderColor = borderColor;
        SuspendLayout();
            
        cmbList.BackColor = this.listBackgroundColor;
        cmbList.Font = new Font(Font.Name, 10F);
        cmbList.ForeColor = listTextColor;
        cmbList.SelectedIndexChanged += CmbList_SelectedIndexChanged;
        cmbList.TextChanged += ComboBox_TextChanged;
            
        btnIcon.Dock = DockStyle.Right;
        btnIcon.FlatStyle = FlatStyle.Flat;
        btnIcon.FlatAppearance.BorderSize = 0;
        btnIcon.BackColor = this.backgroundColor;
        btnIcon.Size = new Size(30, 30);
        btnIcon.Cursor = Cursors.Hand;
        btnIcon.Click += Icon_Click;
        btnIcon.Paint += Icon_Paint;
            
        lblText.Dock = DockStyle.Fill;
        lblText.AutoSize = false;
        lblText.BackColor = this.backgroundColor;
        lblText.TextAlign = ContentAlignment.MiddleLeft;
        lblText.Padding = new Padding(8, 0, 0, 0);
        lblText.Font = new Font(Font.Name, 10F);
            
        lblText.Click += Surface_Click;
        lblText.MouseEnter += Surface_MouseEnter;
        lblText.MouseLeave += Surface_MouseLeave;
            
        Controls.Add(lblText);
        Controls.Add(btnIcon);
        Controls.Add(cmbList);
        MinimumSize = new Size(200, 30);
        Size = new Size(200, 30);
        ForeColor = Color.DimGray;
        Padding = new Padding(BorderSize);
        Font = new Font(Font.Name, 10F);
        base.BackColor = borderColor; 
        ResumeLayout();
        AdjustComboBoxDimensions();
    }

    private void AdjustComboBoxDimensions()
    {
        cmbList.Width = lblText.Width;
        cmbList.Location = new Point
        {
            X = Width - Padding.Right - cmbList.Width,
            Y = lblText.Bottom - cmbList.Height
        };
    }
    private void CmbList_SelectedIndexChanged(object? sender, EventArgs e)
    {
        OnSelectedIndexChanged?.Invoke(sender, e);
        lblText.Text = cmbList.Text;
    }

    private void Icon_Paint(object? sender, PaintEventArgs e)
    {
        //Fields
        const int IconWidht = 14;
        const int IconHeight = 6;
        const int PenWidth = 2;
        var rectIcon = new Rectangle((btnIcon.Width - IconWidht) / 2, (btnIcon.Height - IconHeight) / 2, IconWidht, IconHeight);
        var graph = e.Graphics;
        //Draw arrow down icon
        using GraphicsPath path = new GraphicsPath();
        using var pen = new Pen(iconColor, PenWidth);
        graph.SmoothingMode = SmoothingMode.AntiAlias;
        path.AddLine(rectIcon.X, rectIcon.Y, rectIcon.X + (IconWidht / PenWidth), rectIcon.Bottom);
        path.AddLine(rectIcon.X + (IconWidht / PenWidth), rectIcon.Bottom, rectIcon.Right, rectIcon.Y);
        graph.DrawPath(pen, path);
    }
    private void Surface_MouseLeave(object? sender, EventArgs e)
    {

    }

    private void Surface_MouseEnter(object? sender, EventArgs e)
    {
            
    }

    private void Surface_Click(object? sender, EventArgs e)
    {
        this.OnClick(e);
        cmbList.Select();
        if (cmbList.DropDownStyle == ComboBoxStyle.DropDownList)
            cmbList.DroppedDown = true;
    }

    private void Icon_Click(object? sender, EventArgs e)
    {
        cmbList.Select();
        cmbList.DroppedDown = true;
    }

    private void ComboBox_TextChanged(object? sender, EventArgs args)
    {
        lblText.Text = cmbList.Text;
    }
}