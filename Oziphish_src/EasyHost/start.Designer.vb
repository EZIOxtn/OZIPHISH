<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class start
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(start))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MetroSetProgressBar1 = New MetroSet_UI.Controls.MetroSetProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Guna2PictureBox1 = New Guna.UI2.WinForms.Guna2PictureBox()
        CType(Me.Guna2PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(73, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(455, 142)
        Me.Label1.TabIndex = 5
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'MetroSetProgressBar1
        '
        Me.MetroSetProgressBar1.BackgroundColor = System.Drawing.Color.Black
        Me.MetroSetProgressBar1.BorderColor = System.Drawing.Color.Black
        Me.MetroSetProgressBar1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.MetroSetProgressBar1.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.MetroSetProgressBar1.DisabledProgressColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MetroSetProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MetroSetProgressBar1.IsDerivedStyle = True
        Me.MetroSetProgressBar1.Location = New System.Drawing.Point(0, 312)
        Me.MetroSetProgressBar1.Maximum = 100
        Me.MetroSetProgressBar1.Minimum = 0
        Me.MetroSetProgressBar1.Name = "MetroSetProgressBar1"
        Me.MetroSetProgressBar1.Orientation = MetroSet_UI.Enums.ProgressOrientation.Vertical
        Me.MetroSetProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MetroSetProgressBar1.Size = New System.Drawing.Size(608, 49)
        Me.MetroSetProgressBar1.Style = MetroSet_UI.Enums.Style.Custom
        Me.MetroSetProgressBar1.StyleManager = Nothing
        Me.MetroSetProgressBar1.TabIndex = 4
        Me.MetroSetProgressBar1.Text = "MetroSetProgressBar1"
        Me.MetroSetProgressBar1.ThemeAuthor = "Narwin"
        Me.MetroSetProgressBar1.ThemeName = "MetroLite"
        Me.MetroSetProgressBar1.Value = 0
        '
        'Timer1
        '
        Me.Timer1.Interval = 50
        '
        'Guna2PictureBox1
        '
        Me.Guna2PictureBox1.ErrorImage = CType(resources.GetObject("Guna2PictureBox1.ErrorImage"), System.Drawing.Image)
        Me.Guna2PictureBox1.Image = CType(resources.GetObject("Guna2PictureBox1.Image"), System.Drawing.Image)
        Me.Guna2PictureBox1.Location = New System.Drawing.Point(253, 179)
        Me.Guna2PictureBox1.Name = "Guna2PictureBox1"
        Me.Guna2PictureBox1.ShadowDecoration.Parent = Me.Guna2PictureBox1
        Me.Guna2PictureBox1.Size = New System.Drawing.Size(101, 101)
        Me.Guna2PictureBox1.TabIndex = 6
        Me.Guna2PictureBox1.TabStop = False
        Me.Guna2PictureBox1.Visible = False
        '
        'start
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(608, 361)
        Me.Controls.Add(Me.Guna2PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MetroSetProgressBar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "start"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        CType(Me.Guna2PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents MetroSetProgressBar1 As MetroSet_UI.Controls.MetroSetProgressBar
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Guna2PictureBox1 As Guna.UI2.WinForms.Guna2PictureBox
End Class
