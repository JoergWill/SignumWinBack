Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wb_ChargenWasserTemp
    Inherits DockContent
    'Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl = New WinBack.wb_TabControl()
        Me.tp_ALG = New System.Windows.Forms.TabPage()
        Me.ALG_BerechneteWerte = New System.Windows.Forms.Label()
        Me.ALG_e_soll_neu = New System.Windows.Forms.Label()
        Me.ALG_w_soll_neu = New System.Windows.Forms.Label()
        Me.ALG_t_neu_vor_eis = New System.Windows.Forms.Label()
        Me.ALG_t_rezept = New System.Windows.Forms.Label()
        Me.ALG_t_delta = New System.Windows.Forms.Label()
        Me.ALG_delta_rmf = New System.Windows.Forms.Label()
        Me.ALG_delta_tts = New System.Windows.Forms.Label()
        Me.lbl_Header = New System.Windows.Forms.Label()
        Me.Btn_ALG_Back = New System.Windows.Forms.Button()
        Me.Btn_MSG = New System.Windows.Forms.Button()
        Me.BtnTTS = New System.Windows.Forms.Button()
        Me.BtnRMF = New System.Windows.Forms.Button()
        Me.tp_RMF = New System.Windows.Forms.TabPage()
        Me.RMF_delta_rmf = New System.Windows.Forms.Label()
        Me.RMF_basis = New System.Windows.Forms.Label()
        Me.RMF_delta_temp = New System.Windows.Forms.Label()
        Me.RMF_st_delta = New System.Windows.Forms.Label()
        Me.RMF_rt_delta = New System.Windows.Forms.Label()
        Me.RMF_rt_diff = New System.Windows.Forms.Label()
        Me.RMF_f_rt = New System.Windows.Forms.Label()
        Me.RMF_rt_0 = New System.Windows.Forms.Label()
        Me.RMF_rt = New System.Windows.Forms.Label()
        Me.RMF_mt_delta = New System.Windows.Forms.Label()
        Me.RMF_mt_diff = New System.Windows.Forms.Label()
        Me.RMF_fk_m = New System.Windows.Forms.Label()
        Me.RMF_at_m = New System.Windows.Forms.Label()
        Me.RMF_f_mt = New System.Windows.Forms.Label()
        Me.RMF_mt_0 = New System.Windows.Forms.Label()
        Me.RMF_mt = New System.Windows.Forms.Label()
        Me.Btn_RMF_Back = New System.Windows.Forms.Button()
        Me.Btn_STF = New System.Windows.Forms.Button()
        Me.tp_STF = New System.Windows.Forms.TabPage()
        Me.STF_st_delta = New System.Windows.Forms.Label()
        Me.STF_st_diff = New System.Windows.Forms.Label()
        Me.STF_fk_s = New System.Windows.Forms.Label()
        Me.STF_at_s = New System.Windows.Forms.Label()
        Me.STF_f_st = New System.Windows.Forms.Label()
        Me.STF_st_0 = New System.Windows.Forms.Label()
        Me.STF_st = New System.Windows.Forms.Label()
        Me.Btn_STF_Back = New System.Windows.Forms.Button()
        Me.tp_TTS = New System.Windows.Forms.TabPage()
        Me.TTS_korr_p3 = New System.Windows.Forms.Label()
        Me.TTS_korr_p2 = New System.Windows.Forms.Label()
        Me.TTS_korr_p1 = New System.Windows.Forms.Label()
        Me.TTS_tts_p3 = New System.Windows.Forms.Label()
        Me.TTS_tts_p2 = New System.Windows.Forms.Label()
        Me.TTS_tts_p1 = New System.Windows.Forms.Label()
        Me.TTS_rs_par3 = New System.Windows.Forms.Label()
        Me.TTS_rs_par2 = New System.Windows.Forms.Label()
        Me.TTS_delta_tts = New System.Windows.Forms.Label()
        Me.TTS_rs_par1 = New System.Windows.Forms.Label()
        Me.Btn_TTS_Back = New System.Windows.Forms.Button()
        Me.tp_EIS = New System.Windows.Forms.TabPage()
        Me.EIS_m_w_soll_neu = New System.Windows.Forms.Label()
        Me.EIS_m_eis_soll_neu = New System.Windows.Forms.Label()
        Me.EIS_t_w_soll_neu = New System.Windows.Forms.Label()
        Me.tp_MSG = New System.Windows.Forms.TabPage()
        Me.Btn_MSG_Back = New System.Windows.Forms.Button()
        Me.tbLogFile = New System.Windows.Forms.TextBox()
        Me.TabControl.SuspendLayout()
        Me.tp_ALG.SuspendLayout()
        Me.tp_RMF.SuspendLayout()
        Me.tp_STF.SuspendLayout()
        Me.tp_TTS.SuspendLayout()
        Me.tp_EIS.SuspendLayout()
        Me.tp_MSG.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.tp_ALG)
        Me.TabControl.Controls.Add(Me.tp_RMF)
        Me.TabControl.Controls.Add(Me.tp_STF)
        Me.TabControl.Controls.Add(Me.tp_TTS)
        Me.TabControl.Controls.Add(Me.tp_EIS)
        Me.TabControl.Controls.Add(Me.tp_MSG)
        Me.TabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl.Location = New System.Drawing.Point(0, 0)
        Me.TabControl.Multiline = True
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(1280, 637)
        Me.TabControl.TabIndex = 0
        Me.TabControl.TabStop = False
        '
        'tp_ALG
        '
        Me.tp_ALG.BackgroundImage = Global.WinBack.My.Resources.Resources.TTS_Allgemein
        Me.tp_ALG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tp_ALG.Controls.Add(Me.ALG_BerechneteWerte)
        Me.tp_ALG.Controls.Add(Me.ALG_e_soll_neu)
        Me.tp_ALG.Controls.Add(Me.ALG_w_soll_neu)
        Me.tp_ALG.Controls.Add(Me.ALG_t_neu_vor_eis)
        Me.tp_ALG.Controls.Add(Me.ALG_t_rezept)
        Me.tp_ALG.Controls.Add(Me.ALG_t_delta)
        Me.tp_ALG.Controls.Add(Me.ALG_delta_rmf)
        Me.tp_ALG.Controls.Add(Me.ALG_delta_tts)
        Me.tp_ALG.Controls.Add(Me.lbl_Header)
        Me.tp_ALG.Controls.Add(Me.Btn_ALG_Back)
        Me.tp_ALG.Controls.Add(Me.Btn_MSG)
        Me.tp_ALG.Controls.Add(Me.BtnTTS)
        Me.tp_ALG.Controls.Add(Me.BtnRMF)
        Me.tp_ALG.Location = New System.Drawing.Point(4, 23)
        Me.tp_ALG.Name = "tp_ALG"
        Me.tp_ALG.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_ALG.Size = New System.Drawing.Size(1272, 610)
        Me.tp_ALG.TabIndex = 0
        Me.tp_ALG.Text = "Calc_ALG"
        Me.tp_ALG.UseVisualStyleBackColor = True
        '
        'ALG_BerechneteWerte
        '
        Me.ALG_BerechneteWerte.AutoSize = True
        Me.ALG_BerechneteWerte.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ALG_BerechneteWerte.ForeColor = System.Drawing.Color.Black
        Me.ALG_BerechneteWerte.Location = New System.Drawing.Point(797, 256)
        Me.ALG_BerechneteWerte.Name = "ALG_BerechneteWerte"
        Me.ALG_BerechneteWerte.Size = New System.Drawing.Size(142, 18)
        Me.ALG_BerechneteWerte.TabIndex = 14
        Me.ALG_BerechneteWerte.Text = "Berechnete Werte "
        '
        'ALG_e_soll_neu
        '
        Me.ALG_e_soll_neu.AutoSize = True
        Me.ALG_e_soll_neu.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ALG_e_soll_neu.ForeColor = System.Drawing.Color.RoyalBlue
        Me.ALG_e_soll_neu.Location = New System.Drawing.Point(797, 307)
        Me.ALG_e_soll_neu.Name = "ALG_e_soll_neu"
        Me.ALG_e_soll_neu.Size = New System.Drawing.Size(85, 18)
        Me.ALG_e_soll_neu.TabIndex = 13
        Me.ALG_e_soll_neu.Text = "e_soll_neu"
        '
        'ALG_w_soll_neu
        '
        Me.ALG_w_soll_neu.AutoSize = True
        Me.ALG_w_soll_neu.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ALG_w_soll_neu.ForeColor = System.Drawing.Color.RoyalBlue
        Me.ALG_w_soll_neu.Location = New System.Drawing.Point(797, 281)
        Me.ALG_w_soll_neu.Name = "ALG_w_soll_neu"
        Me.ALG_w_soll_neu.Size = New System.Drawing.Size(87, 18)
        Me.ALG_w_soll_neu.TabIndex = 12
        Me.ALG_w_soll_neu.Text = "w_soll_neu"
        '
        'ALG_t_neu_vor_eis
        '
        Me.ALG_t_neu_vor_eis.AutoSize = True
        Me.ALG_t_neu_vor_eis.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ALG_t_neu_vor_eis.ForeColor = System.Drawing.Color.RoyalBlue
        Me.ALG_t_neu_vor_eis.Location = New System.Drawing.Point(797, 178)
        Me.ALG_t_neu_vor_eis.Name = "ALG_t_neu_vor_eis"
        Me.ALG_t_neu_vor_eis.Size = New System.Drawing.Size(109, 18)
        Me.ALG_t_neu_vor_eis.TabIndex = 11
        Me.ALG_t_neu_vor_eis.Text = "t_neu_vor_eis"
        '
        'ALG_t_rezept
        '
        Me.ALG_t_rezept.AutoSize = True
        Me.ALG_t_rezept.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ALG_t_rezept.ForeColor = System.Drawing.Color.RoyalBlue
        Me.ALG_t_rezept.Location = New System.Drawing.Point(528, 281)
        Me.ALG_t_rezept.Name = "ALG_t_rezept"
        Me.ALG_t_rezept.Size = New System.Drawing.Size(67, 18)
        Me.ALG_t_rezept.TabIndex = 10
        Me.ALG_t_rezept.Text = "t_rezept"
        '
        'ALG_t_delta
        '
        Me.ALG_t_delta.AutoSize = True
        Me.ALG_t_delta.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ALG_t_delta.ForeColor = System.Drawing.Color.RoyalBlue
        Me.ALG_t_delta.Location = New System.Drawing.Point(528, 178)
        Me.ALG_t_delta.Name = "ALG_t_delta"
        Me.ALG_t_delta.Size = New System.Drawing.Size(56, 18)
        Me.ALG_t_delta.TabIndex = 9
        Me.ALG_t_delta.Text = "t_delta"
        '
        'ALG_delta_rmf
        '
        Me.ALG_delta_rmf.AutoSize = True
        Me.ALG_delta_rmf.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ALG_delta_rmf.ForeColor = System.Drawing.Color.RoyalBlue
        Me.ALG_delta_rmf.Location = New System.Drawing.Point(256, 279)
        Me.ALG_delta_rmf.Name = "ALG_delta_rmf"
        Me.ALG_delta_rmf.Size = New System.Drawing.Size(74, 18)
        Me.ALG_delta_rmf.TabIndex = 8
        Me.ALG_delta_rmf.Text = "delta_rmf"
        '
        'ALG_delta_tts
        '
        Me.ALG_delta_tts.AutoSize = True
        Me.ALG_delta_tts.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ALG_delta_tts.ForeColor = System.Drawing.Color.RoyalBlue
        Me.ALG_delta_tts.Location = New System.Drawing.Point(256, 178)
        Me.ALG_delta_tts.Name = "ALG_delta_tts"
        Me.ALG_delta_tts.Size = New System.Drawing.Size(69, 18)
        Me.ALG_delta_tts.TabIndex = 7
        Me.ALG_delta_tts.Text = "delta_tts"
        '
        'lbl_Header
        '
        Me.lbl_Header.AutoSize = True
        Me.lbl_Header.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Header.Location = New System.Drawing.Point(62, 22)
        Me.lbl_Header.Name = "lbl_Header"
        Me.lbl_Header.Size = New System.Drawing.Size(437, 24)
        Me.lbl_Header.TabIndex = 6
        Me.lbl_Header.Text = "WinBack Schüttwasser-Korrektur/Berechnung"
        '
        'Btn_ALG_Back
        '
        Me.Btn_ALG_Back.Location = New System.Drawing.Point(1078, 515)
        Me.Btn_ALG_Back.Name = "Btn_ALG_Back"
        Me.Btn_ALG_Back.Size = New System.Drawing.Size(94, 44)
        Me.Btn_ALG_Back.TabIndex = 5
        Me.Btn_ALG_Back.TabStop = False
        Me.Btn_ALG_Back.Text = "Schliessen"
        Me.Btn_ALG_Back.UseVisualStyleBackColor = True
        '
        'Btn_MSG
        '
        Me.Btn_MSG.Location = New System.Drawing.Point(66, 515)
        Me.Btn_MSG.Name = "Btn_MSG"
        Me.Btn_MSG.Size = New System.Drawing.Size(92, 44)
        Me.Btn_MSG.TabIndex = 3
        Me.Btn_MSG.TabStop = False
        Me.Btn_MSG.Text = "Log-File"
        Me.Btn_MSG.UseVisualStyleBackColor = True
        '
        'BtnTTS
        '
        Me.BtnTTS.Location = New System.Drawing.Point(66, 178)
        Me.BtnTTS.Name = "BtnTTS"
        Me.BtnTTS.Size = New System.Drawing.Size(92, 44)
        Me.BtnTTS.TabIndex = 1
        Me.BtnTTS.Text = "TTS- Berechnung"
        Me.BtnTTS.UseVisualStyleBackColor = True
        '
        'BtnRMF
        '
        Me.BtnRMF.Location = New System.Drawing.Point(66, 281)
        Me.BtnRMF.Name = "BtnRMF"
        Me.BtnRMF.Size = New System.Drawing.Size(92, 44)
        Me.BtnRMF.TabIndex = 2
        Me.BtnRMF.TabStop = False
        Me.BtnRMF.Text = "RMF- Berechnung"
        Me.BtnRMF.UseVisualStyleBackColor = True
        '
        'tp_RMF
        '
        Me.tp_RMF.BackgroundImage = Global.WinBack.My.Resources.Resources.TTS_RaumMehl
        Me.tp_RMF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tp_RMF.Controls.Add(Me.RMF_delta_rmf)
        Me.tp_RMF.Controls.Add(Me.RMF_basis)
        Me.tp_RMF.Controls.Add(Me.RMF_delta_temp)
        Me.tp_RMF.Controls.Add(Me.RMF_st_delta)
        Me.tp_RMF.Controls.Add(Me.RMF_rt_delta)
        Me.tp_RMF.Controls.Add(Me.RMF_rt_diff)
        Me.tp_RMF.Controls.Add(Me.RMF_f_rt)
        Me.tp_RMF.Controls.Add(Me.RMF_rt_0)
        Me.tp_RMF.Controls.Add(Me.RMF_rt)
        Me.tp_RMF.Controls.Add(Me.RMF_mt_delta)
        Me.tp_RMF.Controls.Add(Me.RMF_mt_diff)
        Me.tp_RMF.Controls.Add(Me.RMF_fk_m)
        Me.tp_RMF.Controls.Add(Me.RMF_at_m)
        Me.tp_RMF.Controls.Add(Me.RMF_f_mt)
        Me.tp_RMF.Controls.Add(Me.RMF_mt_0)
        Me.tp_RMF.Controls.Add(Me.RMF_mt)
        Me.tp_RMF.Controls.Add(Me.Btn_RMF_Back)
        Me.tp_RMF.Controls.Add(Me.Btn_STF)
        Me.tp_RMF.Location = New System.Drawing.Point(4, 23)
        Me.tp_RMF.Name = "tp_RMF"
        Me.tp_RMF.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_RMF.Size = New System.Drawing.Size(1272, 610)
        Me.tp_RMF.TabIndex = 1
        Me.tp_RMF.Text = "Calc_RMF"
        Me.tp_RMF.UseVisualStyleBackColor = True
        '
        'RMF_delta_rmf
        '
        Me.RMF_delta_rmf.AutoSize = True
        Me.RMF_delta_rmf.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_delta_rmf.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_delta_rmf.Location = New System.Drawing.Point(972, 309)
        Me.RMF_delta_rmf.Name = "RMF_delta_rmf"
        Me.RMF_delta_rmf.Size = New System.Drawing.Size(74, 18)
        Me.RMF_delta_rmf.TabIndex = 24
        Me.RMF_delta_rmf.Text = "delta_rmf"
        '
        'RMF_basis
        '
        Me.RMF_basis.AutoSize = True
        Me.RMF_basis.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_basis.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_basis.Location = New System.Drawing.Point(766, 412)
        Me.RMF_basis.Name = "RMF_basis"
        Me.RMF_basis.Size = New System.Drawing.Size(84, 18)
        Me.RMF_basis.TabIndex = 23
        Me.RMF_basis.Text = "basis_wert"
        '
        'RMF_delta_temp
        '
        Me.RMF_delta_temp.AutoSize = True
        Me.RMF_delta_temp.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_delta_temp.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_delta_temp.Location = New System.Drawing.Point(766, 309)
        Me.RMF_delta_temp.Name = "RMF_delta_temp"
        Me.RMF_delta_temp.Size = New System.Drawing.Size(86, 18)
        Me.RMF_delta_temp.TabIndex = 22
        Me.RMF_delta_temp.Text = "delta_temp"
        '
        'RMF_st_delta
        '
        Me.RMF_st_delta.AutoSize = True
        Me.RMF_st_delta.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_st_delta.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_st_delta.Location = New System.Drawing.Point(556, 412)
        Me.RMF_st_delta.Name = "RMF_st_delta"
        Me.RMF_st_delta.Size = New System.Drawing.Size(64, 18)
        Me.RMF_st_delta.TabIndex = 21
        Me.RMF_st_delta.Text = "st_delta"
        '
        'RMF_rt_delta
        '
        Me.RMF_rt_delta.AutoSize = True
        Me.RMF_rt_delta.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_rt_delta.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_rt_delta.Location = New System.Drawing.Point(556, 309)
        Me.RMF_rt_delta.Name = "RMF_rt_delta"
        Me.RMF_rt_delta.Size = New System.Drawing.Size(62, 18)
        Me.RMF_rt_delta.TabIndex = 20
        Me.RMF_rt_delta.Text = "rt_delta"
        '
        'RMF_rt_diff
        '
        Me.RMF_rt_diff.AutoSize = True
        Me.RMF_rt_diff.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_rt_diff.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_rt_diff.Location = New System.Drawing.Point(300, 309)
        Me.RMF_rt_diff.Name = "RMF_rt_diff"
        Me.RMF_rt_diff.Size = New System.Drawing.Size(50, 18)
        Me.RMF_rt_diff.TabIndex = 19
        Me.RMF_rt_diff.Text = "rt_diff"
        '
        'RMF_f_rt
        '
        Me.RMF_f_rt.AutoSize = True
        Me.RMF_f_rt.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_f_rt.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_f_rt.Location = New System.Drawing.Point(105, 434)
        Me.RMF_f_rt.Name = "RMF_f_rt"
        Me.RMF_f_rt.Size = New System.Drawing.Size(32, 18)
        Me.RMF_f_rt.TabIndex = 18
        Me.RMF_f_rt.Text = "f_rt"
        '
        'RMF_rt_0
        '
        Me.RMF_rt_0.AutoSize = True
        Me.RMF_rt_0.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_rt_0.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_rt_0.Location = New System.Drawing.Point(105, 375)
        Me.RMF_rt_0.Name = "RMF_rt_0"
        Me.RMF_rt_0.Size = New System.Drawing.Size(35, 18)
        Me.RMF_rt_0.TabIndex = 17
        Me.RMF_rt_0.Text = "rt_0"
        '
        'RMF_rt
        '
        Me.RMF_rt.AutoSize = True
        Me.RMF_rt.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_rt.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_rt.Location = New System.Drawing.Point(105, 268)
        Me.RMF_rt.Name = "RMF_rt"
        Me.RMF_rt.Size = New System.Drawing.Size(19, 18)
        Me.RMF_rt.TabIndex = 16
        Me.RMF_rt.Text = "rt"
        '
        'RMF_mt_delta
        '
        Me.RMF_mt_delta.AutoSize = True
        Me.RMF_mt_delta.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_mt_delta.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_mt_delta.Location = New System.Drawing.Point(556, 61)
        Me.RMF_mt_delta.Name = "RMF_mt_delta"
        Me.RMF_mt_delta.Size = New System.Drawing.Size(68, 18)
        Me.RMF_mt_delta.TabIndex = 15
        Me.RMF_mt_delta.Text = "mt_delta"
        '
        'RMF_mt_diff
        '
        Me.RMF_mt_diff.AutoSize = True
        Me.RMF_mt_diff.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_mt_diff.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_mt_diff.Location = New System.Drawing.Point(300, 61)
        Me.RMF_mt_diff.Name = "RMF_mt_diff"
        Me.RMF_mt_diff.Size = New System.Drawing.Size(56, 18)
        Me.RMF_mt_diff.TabIndex = 14
        Me.RMF_mt_diff.Text = "mt_diff"
        '
        'RMF_fk_m
        '
        Me.RMF_fk_m.AutoSize = True
        Me.RMF_fk_m.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_fk_m.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_fk_m.Location = New System.Drawing.Point(404, 101)
        Me.RMF_fk_m.Name = "RMF_fk_m"
        Me.RMF_fk_m.Size = New System.Drawing.Size(41, 18)
        Me.RMF_fk_m.TabIndex = 13
        Me.RMF_fk_m.Text = "fk_m"
        '
        'RMF_at_m
        '
        Me.RMF_at_m.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_at_m.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_at_m.Location = New System.Drawing.Point(428, 185)
        Me.RMF_at_m.Name = "RMF_at_m"
        Me.RMF_at_m.Size = New System.Drawing.Size(41, 18)
        Me.RMF_at_m.TabIndex = 12
        Me.RMF_at_m.Text = "at_m"
        Me.RMF_at_m.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'RMF_f_mt
        '
        Me.RMF_f_mt.AutoSize = True
        Me.RMF_f_mt.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_f_mt.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_f_mt.Location = New System.Drawing.Point(105, 185)
        Me.RMF_f_mt.Name = "RMF_f_mt"
        Me.RMF_f_mt.Size = New System.Drawing.Size(38, 18)
        Me.RMF_f_mt.TabIndex = 11
        Me.RMF_f_mt.Text = "f_mt"
        '
        'RMF_mt_0
        '
        Me.RMF_mt_0.AutoSize = True
        Me.RMF_mt_0.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_mt_0.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_mt_0.Location = New System.Drawing.Point(105, 127)
        Me.RMF_mt_0.Name = "RMF_mt_0"
        Me.RMF_mt_0.Size = New System.Drawing.Size(41, 18)
        Me.RMF_mt_0.TabIndex = 10
        Me.RMF_mt_0.Text = "mt_0"
        '
        'RMF_mt
        '
        Me.RMF_mt.AutoSize = True
        Me.RMF_mt.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMF_mt.ForeColor = System.Drawing.Color.RoyalBlue
        Me.RMF_mt.Location = New System.Drawing.Point(105, 19)
        Me.RMF_mt.Name = "RMF_mt"
        Me.RMF_mt.Size = New System.Drawing.Size(25, 18)
        Me.RMF_mt.TabIndex = 9
        Me.RMF_mt.Text = "mt"
        '
        'Btn_RMF_Back
        '
        Me.Btn_RMF_Back.Location = New System.Drawing.Point(1078, 515)
        Me.Btn_RMF_Back.Name = "Btn_RMF_Back"
        Me.Btn_RMF_Back.Size = New System.Drawing.Size(94, 44)
        Me.Btn_RMF_Back.TabIndex = 4
        Me.Btn_RMF_Back.TabStop = False
        Me.Btn_RMF_Back.Text = "Zurück"
        Me.Btn_RMF_Back.UseVisualStyleBackColor = True
        '
        'Btn_STF
        '
        Me.Btn_STF.Location = New System.Drawing.Point(467, 515)
        Me.Btn_STF.Name = "Btn_STF"
        Me.Btn_STF.Size = New System.Drawing.Size(94, 44)
        Me.Btn_STF.TabIndex = 2
        Me.Btn_STF.TabStop = False
        Me.Btn_STF.Text = "Sauer Temperatur"
        Me.Btn_STF.UseVisualStyleBackColor = True
        '
        'tp_STF
        '
        Me.tp_STF.BackgroundImage = Global.WinBack.My.Resources.Resources.TTS_Sauerteig
        Me.tp_STF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tp_STF.Controls.Add(Me.STF_st_delta)
        Me.tp_STF.Controls.Add(Me.STF_st_diff)
        Me.tp_STF.Controls.Add(Me.STF_fk_s)
        Me.tp_STF.Controls.Add(Me.STF_at_s)
        Me.tp_STF.Controls.Add(Me.STF_f_st)
        Me.tp_STF.Controls.Add(Me.STF_st_0)
        Me.tp_STF.Controls.Add(Me.STF_st)
        Me.tp_STF.Controls.Add(Me.Btn_STF_Back)
        Me.tp_STF.Location = New System.Drawing.Point(4, 23)
        Me.tp_STF.Name = "tp_STF"
        Me.tp_STF.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_STF.Size = New System.Drawing.Size(1272, 610)
        Me.tp_STF.TabIndex = 4
        Me.tp_STF.Text = "Calc_STF"
        Me.tp_STF.UseVisualStyleBackColor = True
        '
        'STF_st_delta
        '
        Me.STF_st_delta.AutoSize = True
        Me.STF_st_delta.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.STF_st_delta.ForeColor = System.Drawing.Color.RoyalBlue
        Me.STF_st_delta.Location = New System.Drawing.Point(621, 150)
        Me.STF_st_delta.Name = "STF_st_delta"
        Me.STF_st_delta.Size = New System.Drawing.Size(64, 18)
        Me.STF_st_delta.TabIndex = 22
        Me.STF_st_delta.Text = "st_delta"
        '
        'STF_st_diff
        '
        Me.STF_st_diff.AutoSize = True
        Me.STF_st_diff.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.STF_st_diff.ForeColor = System.Drawing.Color.RoyalBlue
        Me.STF_st_diff.Location = New System.Drawing.Point(365, 150)
        Me.STF_st_diff.Name = "STF_st_diff"
        Me.STF_st_diff.Size = New System.Drawing.Size(52, 18)
        Me.STF_st_diff.TabIndex = 21
        Me.STF_st_diff.Text = "st_diff"
        '
        'STF_fk_s
        '
        Me.STF_fk_s.AutoSize = True
        Me.STF_fk_s.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.STF_fk_s.ForeColor = System.Drawing.Color.RoyalBlue
        Me.STF_fk_s.Location = New System.Drawing.Point(469, 190)
        Me.STF_fk_s.Name = "STF_fk_s"
        Me.STF_fk_s.Size = New System.Drawing.Size(37, 18)
        Me.STF_fk_s.TabIndex = 20
        Me.STF_fk_s.Text = "fk_s"
        '
        'STF_at_s
        '
        Me.STF_at_s.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.STF_at_s.ForeColor = System.Drawing.Color.RoyalBlue
        Me.STF_at_s.Location = New System.Drawing.Point(493, 274)
        Me.STF_at_s.Name = "STF_at_s"
        Me.STF_at_s.Size = New System.Drawing.Size(41, 18)
        Me.STF_at_s.TabIndex = 19
        Me.STF_at_s.Text = "at_s"
        Me.STF_at_s.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'STF_f_st
        '
        Me.STF_f_st.AutoSize = True
        Me.STF_f_st.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.STF_f_st.ForeColor = System.Drawing.Color.RoyalBlue
        Me.STF_f_st.Location = New System.Drawing.Point(170, 274)
        Me.STF_f_st.Name = "STF_f_st"
        Me.STF_f_st.Size = New System.Drawing.Size(34, 18)
        Me.STF_f_st.TabIndex = 18
        Me.STF_f_st.Text = "f_st"
        '
        'STF_st_0
        '
        Me.STF_st_0.AutoSize = True
        Me.STF_st_0.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.STF_st_0.ForeColor = System.Drawing.Color.RoyalBlue
        Me.STF_st_0.Location = New System.Drawing.Point(170, 216)
        Me.STF_st_0.Name = "STF_st_0"
        Me.STF_st_0.Size = New System.Drawing.Size(37, 18)
        Me.STF_st_0.TabIndex = 17
        Me.STF_st_0.Text = "st_0"
        '
        'STF_st
        '
        Me.STF_st.AutoSize = True
        Me.STF_st.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.STF_st.ForeColor = System.Drawing.Color.RoyalBlue
        Me.STF_st.Location = New System.Drawing.Point(170, 108)
        Me.STF_st.Name = "STF_st"
        Me.STF_st.Size = New System.Drawing.Size(21, 18)
        Me.STF_st.TabIndex = 16
        Me.STF_st.Text = "st"
        '
        'Btn_STF_Back
        '
        Me.Btn_STF_Back.Location = New System.Drawing.Point(1078, 515)
        Me.Btn_STF_Back.Name = "Btn_STF_Back"
        Me.Btn_STF_Back.Size = New System.Drawing.Size(94, 44)
        Me.Btn_STF_Back.TabIndex = 3
        Me.Btn_STF_Back.TabStop = False
        Me.Btn_STF_Back.Text = "Zurück"
        Me.Btn_STF_Back.UseVisualStyleBackColor = True
        '
        'tp_TTS
        '
        Me.tp_TTS.BackgroundImage = Global.WinBack.My.Resources.Resources.TTS_TeigTempSteuerung
        Me.tp_TTS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tp_TTS.Controls.Add(Me.TTS_korr_p3)
        Me.tp_TTS.Controls.Add(Me.TTS_korr_p2)
        Me.tp_TTS.Controls.Add(Me.TTS_korr_p1)
        Me.tp_TTS.Controls.Add(Me.TTS_tts_p3)
        Me.tp_TTS.Controls.Add(Me.TTS_tts_p2)
        Me.tp_TTS.Controls.Add(Me.TTS_tts_p1)
        Me.tp_TTS.Controls.Add(Me.TTS_rs_par3)
        Me.tp_TTS.Controls.Add(Me.TTS_rs_par2)
        Me.tp_TTS.Controls.Add(Me.TTS_delta_tts)
        Me.tp_TTS.Controls.Add(Me.TTS_rs_par1)
        Me.tp_TTS.Controls.Add(Me.Btn_TTS_Back)
        Me.tp_TTS.Location = New System.Drawing.Point(4, 23)
        Me.tp_TTS.Name = "tp_TTS"
        Me.tp_TTS.Size = New System.Drawing.Size(1272, 610)
        Me.tp_TTS.TabIndex = 2
        Me.tp_TTS.Text = "Calc_TTS"
        Me.tp_TTS.UseVisualStyleBackColor = True
        '
        'TTS_korr_p3
        '
        Me.TTS_korr_p3.AutoSize = True
        Me.TTS_korr_p3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_korr_p3.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_korr_p3.Location = New System.Drawing.Point(308, 430)
        Me.TTS_korr_p3.Name = "TTS_korr_p3"
        Me.TTS_korr_p3.Size = New System.Drawing.Size(62, 18)
        Me.TTS_korr_p3.TabIndex = 14
        Me.TTS_korr_p3.Text = "korr_p3"
        '
        'TTS_korr_p2
        '
        Me.TTS_korr_p2.AutoSize = True
        Me.TTS_korr_p2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_korr_p2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_korr_p2.Location = New System.Drawing.Point(308, 245)
        Me.TTS_korr_p2.Name = "TTS_korr_p2"
        Me.TTS_korr_p2.Size = New System.Drawing.Size(62, 18)
        Me.TTS_korr_p2.TabIndex = 13
        Me.TTS_korr_p2.Text = "korr_p2"
        '
        'TTS_korr_p1
        '
        Me.TTS_korr_p1.AutoSize = True
        Me.TTS_korr_p1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_korr_p1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_korr_p1.Location = New System.Drawing.Point(308, 61)
        Me.TTS_korr_p1.Name = "TTS_korr_p1"
        Me.TTS_korr_p1.Size = New System.Drawing.Size(62, 18)
        Me.TTS_korr_p1.TabIndex = 12
        Me.TTS_korr_p1.Text = "korr_p1"
        '
        'TTS_tts_p3
        '
        Me.TTS_tts_p3.AutoSize = True
        Me.TTS_tts_p3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_tts_p3.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_tts_p3.Location = New System.Drawing.Point(107, 495)
        Me.TTS_tts_p3.Name = "TTS_tts_p3"
        Me.TTS_tts_p3.Size = New System.Drawing.Size(51, 18)
        Me.TTS_tts_p3.TabIndex = 11
        Me.TTS_tts_p3.Text = "tts_p3"
        '
        'TTS_tts_p2
        '
        Me.TTS_tts_p2.AutoSize = True
        Me.TTS_tts_p2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_tts_p2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_tts_p2.Location = New System.Drawing.Point(107, 312)
        Me.TTS_tts_p2.Name = "TTS_tts_p2"
        Me.TTS_tts_p2.Size = New System.Drawing.Size(51, 18)
        Me.TTS_tts_p2.TabIndex = 10
        Me.TTS_tts_p2.Text = "tts_p2"
        '
        'TTS_tts_p1
        '
        Me.TTS_tts_p1.AutoSize = True
        Me.TTS_tts_p1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_tts_p1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_tts_p1.Location = New System.Drawing.Point(107, 127)
        Me.TTS_tts_p1.Name = "TTS_tts_p1"
        Me.TTS_tts_p1.Size = New System.Drawing.Size(51, 18)
        Me.TTS_tts_p1.TabIndex = 9
        Me.TTS_tts_p1.Text = "tts_p1"
        '
        'TTS_rs_par3
        '
        Me.TTS_rs_par3.AutoSize = True
        Me.TTS_rs_par3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_rs_par3.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_rs_par3.Location = New System.Drawing.Point(107, 389)
        Me.TTS_rs_par3.Name = "TTS_rs_par3"
        Me.TTS_rs_par3.Size = New System.Drawing.Size(61, 18)
        Me.TTS_rs_par3.TabIndex = 8
        Me.TTS_rs_par3.Text = "rs_par3"
        '
        'TTS_rs_par2
        '
        Me.TTS_rs_par2.AutoSize = True
        Me.TTS_rs_par2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_rs_par2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_rs_par2.Location = New System.Drawing.Point(107, 205)
        Me.TTS_rs_par2.Name = "TTS_rs_par2"
        Me.TTS_rs_par2.Size = New System.Drawing.Size(61, 18)
        Me.TTS_rs_par2.TabIndex = 7
        Me.TTS_rs_par2.Text = "rs_par2"
        '
        'TTS_delta_tts
        '
        Me.TTS_delta_tts.AutoSize = True
        Me.TTS_delta_tts.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_delta_tts.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_delta_tts.Location = New System.Drawing.Point(563, 246)
        Me.TTS_delta_tts.Name = "TTS_delta_tts"
        Me.TTS_delta_tts.Size = New System.Drawing.Size(69, 18)
        Me.TTS_delta_tts.TabIndex = 6
        Me.TTS_delta_tts.Text = "delta_tts"
        '
        'TTS_rs_par1
        '
        Me.TTS_rs_par1.AutoSize = True
        Me.TTS_rs_par1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TTS_rs_par1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TTS_rs_par1.Location = New System.Drawing.Point(107, 21)
        Me.TTS_rs_par1.Name = "TTS_rs_par1"
        Me.TTS_rs_par1.Size = New System.Drawing.Size(61, 18)
        Me.TTS_rs_par1.TabIndex = 5
        Me.TTS_rs_par1.Text = "rs_par1"
        '
        'Btn_TTS_Back
        '
        Me.Btn_TTS_Back.Location = New System.Drawing.Point(1078, 515)
        Me.Btn_TTS_Back.Name = "Btn_TTS_Back"
        Me.Btn_TTS_Back.Size = New System.Drawing.Size(94, 44)
        Me.Btn_TTS_Back.TabIndex = 4
        Me.Btn_TTS_Back.TabStop = False
        Me.Btn_TTS_Back.Text = "Zurück"
        Me.Btn_TTS_Back.UseVisualStyleBackColor = True
        '
        'tp_EIS
        '
        Me.tp_EIS.Controls.Add(Me.EIS_m_w_soll_neu)
        Me.tp_EIS.Controls.Add(Me.EIS_m_eis_soll_neu)
        Me.tp_EIS.Controls.Add(Me.EIS_t_w_soll_neu)
        Me.tp_EIS.Location = New System.Drawing.Point(4, 23)
        Me.tp_EIS.Name = "tp_EIS"
        Me.tp_EIS.Size = New System.Drawing.Size(1272, 610)
        Me.tp_EIS.TabIndex = 3
        Me.tp_EIS.Text = "Calc_EIS"
        Me.tp_EIS.UseVisualStyleBackColor = True
        '
        'EIS_m_w_soll_neu
        '
        Me.EIS_m_w_soll_neu.AutoSize = True
        Me.EIS_m_w_soll_neu.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EIS_m_w_soll_neu.ForeColor = System.Drawing.Color.RoyalBlue
        Me.EIS_m_w_soll_neu.Location = New System.Drawing.Point(606, 276)
        Me.EIS_m_w_soll_neu.Name = "EIS_m_w_soll_neu"
        Me.EIS_m_w_soll_neu.Size = New System.Drawing.Size(107, 18)
        Me.EIS_m_w_soll_neu.TabIndex = 15
        Me.EIS_m_w_soll_neu.Text = "m_w_soll_neu"
        '
        'EIS_m_eis_soll_neu
        '
        Me.EIS_m_eis_soll_neu.AutoSize = True
        Me.EIS_m_eis_soll_neu.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EIS_m_eis_soll_neu.ForeColor = System.Drawing.Color.RoyalBlue
        Me.EIS_m_eis_soll_neu.Location = New System.Drawing.Point(606, 240)
        Me.EIS_m_eis_soll_neu.Name = "EIS_m_eis_soll_neu"
        Me.EIS_m_eis_soll_neu.Size = New System.Drawing.Size(117, 18)
        Me.EIS_m_eis_soll_neu.TabIndex = 14
        Me.EIS_m_eis_soll_neu.Text = "m_eis_soll_neu"
        '
        'EIS_t_w_soll_neu
        '
        Me.EIS_t_w_soll_neu.AutoSize = True
        Me.EIS_t_w_soll_neu.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EIS_t_w_soll_neu.ForeColor = System.Drawing.Color.RoyalBlue
        Me.EIS_t_w_soll_neu.Location = New System.Drawing.Point(606, 315)
        Me.EIS_t_w_soll_neu.Name = "EIS_t_w_soll_neu"
        Me.EIS_t_w_soll_neu.Size = New System.Drawing.Size(100, 18)
        Me.EIS_t_w_soll_neu.TabIndex = 13
        Me.EIS_t_w_soll_neu.Text = "t_w_soll_neu"
        '
        'tp_MSG
        '
        Me.tp_MSG.Controls.Add(Me.Btn_MSG_Back)
        Me.tp_MSG.Controls.Add(Me.tbLogFile)
        Me.tp_MSG.Location = New System.Drawing.Point(4, 23)
        Me.tp_MSG.Name = "tp_MSG"
        Me.tp_MSG.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_MSG.Size = New System.Drawing.Size(1272, 610)
        Me.tp_MSG.TabIndex = 5
        Me.tp_MSG.Text = "Calc_MSG"
        Me.tp_MSG.UseVisualStyleBackColor = True
        '
        'Btn_MSG_Back
        '
        Me.Btn_MSG_Back.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_MSG_Back.Location = New System.Drawing.Point(1170, 558)
        Me.Btn_MSG_Back.Name = "Btn_MSG_Back"
        Me.Btn_MSG_Back.Size = New System.Drawing.Size(94, 44)
        Me.Btn_MSG_Back.TabIndex = 6
        Me.Btn_MSG_Back.TabStop = False
        Me.Btn_MSG_Back.Text = "Zurück"
        Me.Btn_MSG_Back.UseVisualStyleBackColor = True
        '
        'tbLogFile
        '
        Me.tbLogFile.Dock = System.Windows.Forms.DockStyle.Left
        Me.tbLogFile.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLogFile.Location = New System.Drawing.Point(3, 3)
        Me.tbLogFile.Multiline = True
        Me.tbLogFile.Name = "tbLogFile"
        Me.tbLogFile.ReadOnly = True
        Me.tbLogFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbLogFile.Size = New System.Drawing.Size(992, 604)
        Me.tbLogFile.TabIndex = 7
        '
        'wb_ChargenWasserTemp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 637)
        Me.Controls.Add(Me.TabControl)
        Me.Name = "wb_ChargenWasserTemp"
        Me.Text = "Berechnung der Wasser-Solltemperatur"
        Me.TabControl.ResumeLayout(False)
        Me.tp_ALG.ResumeLayout(False)
        Me.tp_ALG.PerformLayout()
        Me.tp_RMF.ResumeLayout(False)
        Me.tp_RMF.PerformLayout()
        Me.tp_STF.ResumeLayout(False)
        Me.tp_STF.PerformLayout()
        Me.tp_TTS.ResumeLayout(False)
        Me.tp_TTS.PerformLayout()
        Me.tp_EIS.ResumeLayout(False)
        Me.tp_EIS.PerformLayout()
        Me.tp_MSG.ResumeLayout(False)
        Me.tp_MSG.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl As wb_TabControl
    Friend WithEvents tp_ALG As Windows.Forms.TabPage
    Friend WithEvents tp_RMF As Windows.Forms.TabPage
    Friend WithEvents tp_TTS As Windows.Forms.TabPage
    Friend WithEvents tp_EIS As Windows.Forms.TabPage
    Friend WithEvents BtnRMF As Windows.Forms.Button
    Friend WithEvents BtnTTS As Windows.Forms.Button
    Friend WithEvents tp_STF As Windows.Forms.TabPage
    Friend WithEvents Btn_STF As Windows.Forms.Button
    Friend WithEvents Btn_RMF_Back As Windows.Forms.Button
    Friend WithEvents Btn_STF_Back As Windows.Forms.Button
    Friend WithEvents Btn_TTS_Back As Windows.Forms.Button
    Friend WithEvents lbl_Header As Windows.Forms.Label
    Friend WithEvents Btn_ALG_Back As Windows.Forms.Button
    Friend WithEvents Btn_MSG As Windows.Forms.Button
    Friend WithEvents tp_MSG As Windows.Forms.TabPage
    Friend WithEvents Btn_MSG_Back As Windows.Forms.Button
    Friend WithEvents TTS_rs_par3 As Windows.Forms.Label
    Friend WithEvents TTS_rs_par2 As Windows.Forms.Label
    Friend WithEvents TTS_delta_tts As Windows.Forms.Label
    Friend WithEvents TTS_rs_par1 As Windows.Forms.Label
    Friend WithEvents TTS_korr_p3 As Windows.Forms.Label
    Friend WithEvents TTS_korr_p2 As Windows.Forms.Label
    Friend WithEvents TTS_korr_p1 As Windows.Forms.Label
    Friend WithEvents TTS_tts_p3 As Windows.Forms.Label
    Friend WithEvents TTS_tts_p2 As Windows.Forms.Label
    Friend WithEvents TTS_tts_p1 As Windows.Forms.Label
    Friend WithEvents ALG_delta_tts As Windows.Forms.Label
    Friend WithEvents ALG_delta_rmf As Windows.Forms.Label
    Friend WithEvents RMF_mt_delta As Windows.Forms.Label
    Friend WithEvents RMF_mt_diff As Windows.Forms.Label
    Friend WithEvents RMF_fk_m As Windows.Forms.Label
    Friend WithEvents RMF_at_m As Windows.Forms.Label
    Friend WithEvents RMF_f_mt As Windows.Forms.Label
    Friend WithEvents RMF_mt_0 As Windows.Forms.Label
    Friend WithEvents RMF_mt As Windows.Forms.Label
    Friend WithEvents RMF_delta_rmf As Windows.Forms.Label
    Friend WithEvents RMF_basis As Windows.Forms.Label
    Friend WithEvents RMF_delta_temp As Windows.Forms.Label
    Friend WithEvents RMF_st_delta As Windows.Forms.Label
    Friend WithEvents RMF_rt_delta As Windows.Forms.Label
    Friend WithEvents RMF_rt_diff As Windows.Forms.Label
    Friend WithEvents RMF_f_rt As Windows.Forms.Label
    Friend WithEvents RMF_rt_0 As Windows.Forms.Label
    Friend WithEvents RMF_rt As Windows.Forms.Label
    Friend WithEvents ALG_t_neu_vor_eis As Windows.Forms.Label
    Friend WithEvents ALG_t_rezept As Windows.Forms.Label
    Friend WithEvents ALG_t_delta As Windows.Forms.Label
    Friend WithEvents STF_st_delta As Windows.Forms.Label
    Friend WithEvents STF_st_diff As Windows.Forms.Label
    Friend WithEvents STF_fk_s As Windows.Forms.Label
    Friend WithEvents STF_at_s As Windows.Forms.Label
    Friend WithEvents STF_f_st As Windows.Forms.Label
    Friend WithEvents STF_st_0 As Windows.Forms.Label
    Friend WithEvents STF_st As Windows.Forms.Label
    Friend WithEvents tbLogFile As Windows.Forms.TextBox
    Friend WithEvents ALG_BerechneteWerte As Windows.Forms.Label
    Friend WithEvents ALG_e_soll_neu As Windows.Forms.Label
    Friend WithEvents ALG_w_soll_neu As Windows.Forms.Label
    Friend WithEvents EIS_m_w_soll_neu As Windows.Forms.Label
    Friend WithEvents EIS_m_eis_soll_neu As Windows.Forms.Label
    Friend WithEvents EIS_t_w_soll_neu As Windows.Forms.Label
End Class
