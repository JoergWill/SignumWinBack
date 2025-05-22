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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wb_ChargenWasserTemp))
        TabControl = New wb_TabControl()
        tp_ALG = New System.Windows.Forms.TabPage()
        ALG_error_rmf = New System.Windows.Forms.Label()
        ALG_Error_tts = New System.Windows.Forms.Label()
        ALG_Error = New System.Windows.Forms.Label()
        lbl_e_soll_neu = New System.Windows.Forms.Label()
        lbl_w_soll_neu = New System.Windows.Forms.Label()
        ALG_BerechneteWerte = New System.Windows.Forms.Label()
        ALG_e_soll_neu = New System.Windows.Forms.Label()
        ALG_w_soll_neu = New System.Windows.Forms.Label()
        ALG_t_neu_vor_eis = New System.Windows.Forms.Label()
        ALG_t_rezept = New System.Windows.Forms.Label()
        ALG_t_delta = New System.Windows.Forms.Label()
        ALG_delta_rmf = New System.Windows.Forms.Label()
        ALG_delta_tts = New System.Windows.Forms.Label()
        lbl_Header = New System.Windows.Forms.Label()
        Btn_ALG_Back = New System.Windows.Forms.Button()
        Btn_MSG = New System.Windows.Forms.Button()
        BtnTTS = New System.Windows.Forms.Button()
        BtnRMF = New System.Windows.Forms.Button()
        tp_RMF = New System.Windows.Forms.TabPage()
        RMF_delta_rmf = New System.Windows.Forms.Label()
        RMF_basis = New System.Windows.Forms.Label()
        RMF_delta_temp = New System.Windows.Forms.Label()
        RMF_st_delta = New System.Windows.Forms.Label()
        RMF_rt_delta = New System.Windows.Forms.Label()
        RMF_rt_diff = New System.Windows.Forms.Label()
        RMF_f_rt = New System.Windows.Forms.Label()
        RMF_rt_0 = New System.Windows.Forms.Label()
        RMF_rt = New System.Windows.Forms.Label()
        RMF_mt_delta = New System.Windows.Forms.Label()
        RMF_mt_diff = New System.Windows.Forms.Label()
        RMF_fk_m = New System.Windows.Forms.Label()
        RMF_at_m = New System.Windows.Forms.Label()
        RMF_f_mt = New System.Windows.Forms.Label()
        RMF_mt_0 = New System.Windows.Forms.Label()
        RMF_mt = New System.Windows.Forms.Label()
        Btn_RMF_Back = New System.Windows.Forms.Button()
        Btn_STF = New System.Windows.Forms.Button()
        tp_STF = New System.Windows.Forms.TabPage()
        STF_st_delta = New System.Windows.Forms.Label()
        STF_st_diff = New System.Windows.Forms.Label()
        STF_fk_s = New System.Windows.Forms.Label()
        STF_at_s = New System.Windows.Forms.Label()
        STF_f_st = New System.Windows.Forms.Label()
        STF_st_0 = New System.Windows.Forms.Label()
        STF_st = New System.Windows.Forms.Label()
        Btn_STF_Back = New System.Windows.Forms.Button()
        tp_TTS = New System.Windows.Forms.TabPage()
        TTS_korr_p3 = New System.Windows.Forms.Label()
        TTS_korr_p2 = New System.Windows.Forms.Label()
        TTS_korr_p1 = New System.Windows.Forms.Label()
        TTS_tts_p3 = New System.Windows.Forms.Label()
        TTS_tts_p2 = New System.Windows.Forms.Label()
        TTS_tts_p1 = New System.Windows.Forms.Label()
        TTS_rs_par3 = New System.Windows.Forms.Label()
        TTS_rs_par2 = New System.Windows.Forms.Label()
        TTS_delta_tts = New System.Windows.Forms.Label()
        TTS_rs_par1 = New System.Windows.Forms.Label()
        Btn_TTS_Back = New System.Windows.Forms.Button()
        tp_EIS = New System.Windows.Forms.TabPage()
        EIS_m_w_soll_neu = New System.Windows.Forms.Label()
        EIS_m_eis_soll_neu = New System.Windows.Forms.Label()
        EIS_t_w_soll_neu = New System.Windows.Forms.Label()
        tp_MSG = New System.Windows.Forms.TabPage()
        Btn_MSG_Back = New System.Windows.Forms.Button()
        tbLogFile = New System.Windows.Forms.TextBox()
        TabControl.SuspendLayout()
        tp_ALG.SuspendLayout()
        tp_RMF.SuspendLayout()
        tp_STF.SuspendLayout()
        tp_TTS.SuspendLayout()
        tp_EIS.SuspendLayout()
        tp_MSG.SuspendLayout()
        SuspendLayout()
        ' 
        ' TabControl
        ' 
        TabControl.Controls.Add(tp_ALG)
        TabControl.Controls.Add(tp_RMF)
        TabControl.Controls.Add(tp_STF)
        TabControl.Controls.Add(tp_TTS)
        TabControl.Controls.Add(tp_EIS)
        TabControl.Controls.Add(tp_MSG)
        TabControl.Dock = System.Windows.Forms.DockStyle.Fill
        TabControl.Location = New System.Drawing.Point(0, 0)
        TabControl.Multiline = True
        TabControl.Name = "TabControl"
        TabControl.SelectedIndex = 0
        TabControl.Size = New System.Drawing.Size(1280, 637)
        TabControl.TabIndex = 0
        TabControl.TabStop = False
        ' 
        ' tp_ALG
        ' 
        tp_ALG.BackgroundImage = CType(resources.GetObject("tp_ALG.BackgroundImage"), Drawing.Image)
        tp_ALG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        tp_ALG.Controls.Add(ALG_error_rmf)
        tp_ALG.Controls.Add(ALG_Error_tts)
        tp_ALG.Controls.Add(ALG_Error)
        tp_ALG.Controls.Add(lbl_e_soll_neu)
        tp_ALG.Controls.Add(lbl_w_soll_neu)
        tp_ALG.Controls.Add(ALG_BerechneteWerte)
        tp_ALG.Controls.Add(ALG_e_soll_neu)
        tp_ALG.Controls.Add(ALG_w_soll_neu)
        tp_ALG.Controls.Add(ALG_t_neu_vor_eis)
        tp_ALG.Controls.Add(ALG_t_rezept)
        tp_ALG.Controls.Add(ALG_t_delta)
        tp_ALG.Controls.Add(ALG_delta_rmf)
        tp_ALG.Controls.Add(ALG_delta_tts)
        tp_ALG.Controls.Add(lbl_Header)
        tp_ALG.Controls.Add(Btn_ALG_Back)
        tp_ALG.Controls.Add(Btn_MSG)
        tp_ALG.Controls.Add(BtnTTS)
        tp_ALG.Controls.Add(BtnRMF)
        tp_ALG.Location = New System.Drawing.Point(4, 23)
        tp_ALG.Name = "tp_ALG"
        tp_ALG.Padding = New System.Windows.Forms.Padding(3)
        tp_ALG.Size = New System.Drawing.Size(1272, 610)
        tp_ALG.TabIndex = 0
        tp_ALG.Text = "Calc_ALG"
        tp_ALG.UseVisualStyleBackColor = True
        ' 
        ' ALG_error_rmf
        ' 
        ALG_error_rmf.AutoSize = True
        ALG_error_rmf.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_error_rmf.ForeColor = Drawing.Color.Red
        ALG_error_rmf.Location = New System.Drawing.Point(174, 245)
        ALG_error_rmf.Name = "ALG_error_rmf"
        ALG_error_rmf.Size = New System.Drawing.Size(75, 18)
        ALG_error_rmf.TabIndex = 19
        ALG_error_rmf.Text = "error_rmf"
        ' 
        ' ALG_Error_tts
        ' 
        ALG_Error_tts.AutoSize = True
        ALG_Error_tts.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_Error_tts.ForeColor = Drawing.Color.Red
        ALG_Error_tts.Location = New System.Drawing.Point(175, 141)
        ALG_Error_tts.Name = "ALG_Error_tts"
        ALG_Error_tts.Size = New System.Drawing.Size(70, 18)
        ALG_Error_tts.TabIndex = 18
        ALG_Error_tts.Text = "error_tts"
        ' 
        ' ALG_Error
        ' 
        ALG_Error.AutoSize = True
        ALG_Error.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_Error.ForeColor = Drawing.Color.Red
        ALG_Error.Location = New System.Drawing.Point(718, 142)
        ALG_Error.Name = "ALG_Error"
        ALG_Error.Size = New System.Drawing.Size(44, 18)
        ALG_Error.TabIndex = 17
        ALG_Error.Text = "error"
        ' 
        ' lbl_e_soll_neu
        ' 
        lbl_e_soll_neu.AutoSize = True
        lbl_e_soll_neu.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        lbl_e_soll_neu.ForeColor = Drawing.Color.Black
        lbl_e_soll_neu.Location = New System.Drawing.Point(688, 307)
        lbl_e_soll_neu.Name = "lbl_e_soll_neu"
        lbl_e_soll_neu.Size = New System.Drawing.Size(76, 17)
        lbl_e_soll_neu.TabIndex = 16
        lbl_e_soll_neu.Text = "Menge Eis"
        ' 
        ' lbl_w_soll_neu
        ' 
        lbl_w_soll_neu.AutoSize = True
        lbl_w_soll_neu.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        lbl_w_soll_neu.ForeColor = Drawing.Color.Black
        lbl_w_soll_neu.Location = New System.Drawing.Point(688, 281)
        lbl_w_soll_neu.Name = "lbl_w_soll_neu"
        lbl_w_soll_neu.Size = New System.Drawing.Size(106, 17)
        lbl_w_soll_neu.TabIndex = 15
        lbl_w_soll_neu.Text = "Menge Wasser"
        ' 
        ' ALG_BerechneteWerte
        ' 
        ALG_BerechneteWerte.AutoSize = True
        ALG_BerechneteWerte.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_BerechneteWerte.ForeColor = Drawing.Color.Black
        ALG_BerechneteWerte.Location = New System.Drawing.Point(688, 257)
        ALG_BerechneteWerte.Name = "ALG_BerechneteWerte"
        ALG_BerechneteWerte.Size = New System.Drawing.Size(142, 18)
        ALG_BerechneteWerte.TabIndex = 14
        ALG_BerechneteWerte.Text = "Berechnete Werte "
        ' 
        ' ALG_e_soll_neu
        ' 
        ALG_e_soll_neu.AutoSize = True
        ALG_e_soll_neu.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_e_soll_neu.ForeColor = Drawing.Color.RoyalBlue
        ALG_e_soll_neu.Location = New System.Drawing.Point(797, 307)
        ALG_e_soll_neu.Name = "ALG_e_soll_neu"
        ALG_e_soll_neu.Size = New System.Drawing.Size(85, 18)
        ALG_e_soll_neu.TabIndex = 13
        ALG_e_soll_neu.Text = "e_soll_neu"
        ' 
        ' ALG_w_soll_neu
        ' 
        ALG_w_soll_neu.AutoSize = True
        ALG_w_soll_neu.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_w_soll_neu.ForeColor = Drawing.Color.RoyalBlue
        ALG_w_soll_neu.Location = New System.Drawing.Point(797, 281)
        ALG_w_soll_neu.Name = "ALG_w_soll_neu"
        ALG_w_soll_neu.Size = New System.Drawing.Size(87, 18)
        ALG_w_soll_neu.TabIndex = 12
        ALG_w_soll_neu.Text = "w_soll_neu"
        ' 
        ' ALG_t_neu_vor_eis
        ' 
        ALG_t_neu_vor_eis.AutoSize = True
        ALG_t_neu_vor_eis.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_t_neu_vor_eis.ForeColor = Drawing.Color.RoyalBlue
        ALG_t_neu_vor_eis.Location = New System.Drawing.Point(797, 178)
        ALG_t_neu_vor_eis.Name = "ALG_t_neu_vor_eis"
        ALG_t_neu_vor_eis.Size = New System.Drawing.Size(109, 18)
        ALG_t_neu_vor_eis.TabIndex = 11
        ALG_t_neu_vor_eis.Text = "t_neu_vor_eis"
        ' 
        ' ALG_t_rezept
        ' 
        ALG_t_rezept.AutoSize = True
        ALG_t_rezept.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_t_rezept.ForeColor = Drawing.Color.RoyalBlue
        ALG_t_rezept.Location = New System.Drawing.Point(528, 281)
        ALG_t_rezept.Name = "ALG_t_rezept"
        ALG_t_rezept.Size = New System.Drawing.Size(67, 18)
        ALG_t_rezept.TabIndex = 10
        ALG_t_rezept.Text = "t_rezept"
        ' 
        ' ALG_t_delta
        ' 
        ALG_t_delta.AutoSize = True
        ALG_t_delta.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_t_delta.ForeColor = Drawing.Color.RoyalBlue
        ALG_t_delta.Location = New System.Drawing.Point(528, 178)
        ALG_t_delta.Name = "ALG_t_delta"
        ALG_t_delta.Size = New System.Drawing.Size(56, 18)
        ALG_t_delta.TabIndex = 9
        ALG_t_delta.Text = "t_delta"
        ' 
        ' ALG_delta_rmf
        ' 
        ALG_delta_rmf.AutoSize = True
        ALG_delta_rmf.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_delta_rmf.ForeColor = Drawing.Color.RoyalBlue
        ALG_delta_rmf.Location = New System.Drawing.Point(256, 279)
        ALG_delta_rmf.Name = "ALG_delta_rmf"
        ALG_delta_rmf.Size = New System.Drawing.Size(74, 18)
        ALG_delta_rmf.TabIndex = 8
        ALG_delta_rmf.Text = "delta_rmf"
        ' 
        ' ALG_delta_tts
        ' 
        ALG_delta_tts.AutoSize = True
        ALG_delta_tts.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        ALG_delta_tts.ForeColor = Drawing.Color.RoyalBlue
        ALG_delta_tts.Location = New System.Drawing.Point(256, 178)
        ALG_delta_tts.Name = "ALG_delta_tts"
        ALG_delta_tts.Size = New System.Drawing.Size(69, 18)
        ALG_delta_tts.TabIndex = 7
        ALG_delta_tts.Text = "delta_tts"
        ' 
        ' lbl_Header
        ' 
        lbl_Header.AutoSize = True
        lbl_Header.Font = New System.Drawing.Font("Arial", 15.75F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        lbl_Header.Location = New System.Drawing.Point(62, 22)
        lbl_Header.Name = "lbl_Header"
        lbl_Header.Size = New System.Drawing.Size(437, 24)
        lbl_Header.TabIndex = 6
        lbl_Header.Text = "WinBack Schüttwasser-Korrektur/Berechnung"
        ' 
        ' Btn_ALG_Back
        ' 
        Btn_ALG_Back.Location = New System.Drawing.Point(66, 515)
        Btn_ALG_Back.Name = "Btn_ALG_Back"
        Btn_ALG_Back.Size = New System.Drawing.Size(92, 44)
        Btn_ALG_Back.TabIndex = 5
        Btn_ALG_Back.TabStop = False
        Btn_ALG_Back.Text = "Schliessen"
        Btn_ALG_Back.UseVisualStyleBackColor = True
        ' 
        ' Btn_MSG
        ' 
        Btn_MSG.Location = New System.Drawing.Point(164, 515)
        Btn_MSG.Name = "Btn_MSG"
        Btn_MSG.Size = New System.Drawing.Size(92, 44)
        Btn_MSG.TabIndex = 3
        Btn_MSG.TabStop = False
        Btn_MSG.Text = "Log-File"
        Btn_MSG.UseVisualStyleBackColor = True
        ' 
        ' BtnTTS
        ' 
        BtnTTS.Location = New System.Drawing.Point(66, 178)
        BtnTTS.Name = "BtnTTS"
        BtnTTS.Size = New System.Drawing.Size(92, 44)
        BtnTTS.TabIndex = 1
        BtnTTS.Text = "TTS- Berechnung"
        BtnTTS.UseVisualStyleBackColor = True
        ' 
        ' BtnRMF
        ' 
        BtnRMF.Location = New System.Drawing.Point(66, 281)
        BtnRMF.Name = "BtnRMF"
        BtnRMF.Size = New System.Drawing.Size(92, 44)
        BtnRMF.TabIndex = 2
        BtnRMF.TabStop = False
        BtnRMF.Text = "RMF- Berechnung"
        BtnRMF.UseVisualStyleBackColor = True
        ' 
        ' tp_RMF
        ' 
        tp_RMF.BackgroundImage = CType(resources.GetObject("tp_RMF.BackgroundImage"), Drawing.Image)
        tp_RMF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        tp_RMF.Controls.Add(RMF_delta_rmf)
        tp_RMF.Controls.Add(RMF_basis)
        tp_RMF.Controls.Add(RMF_delta_temp)
        tp_RMF.Controls.Add(RMF_st_delta)
        tp_RMF.Controls.Add(RMF_rt_delta)
        tp_RMF.Controls.Add(RMF_rt_diff)
        tp_RMF.Controls.Add(RMF_f_rt)
        tp_RMF.Controls.Add(RMF_rt_0)
        tp_RMF.Controls.Add(RMF_rt)
        tp_RMF.Controls.Add(RMF_mt_delta)
        tp_RMF.Controls.Add(RMF_mt_diff)
        tp_RMF.Controls.Add(RMF_fk_m)
        tp_RMF.Controls.Add(RMF_at_m)
        tp_RMF.Controls.Add(RMF_f_mt)
        tp_RMF.Controls.Add(RMF_mt_0)
        tp_RMF.Controls.Add(RMF_mt)
        tp_RMF.Controls.Add(Btn_RMF_Back)
        tp_RMF.Controls.Add(Btn_STF)
        tp_RMF.Location = New System.Drawing.Point(4, 23)
        tp_RMF.Name = "tp_RMF"
        tp_RMF.Padding = New System.Windows.Forms.Padding(3)
        tp_RMF.Size = New System.Drawing.Size(1272, 610)
        tp_RMF.TabIndex = 1
        tp_RMF.Text = "Calc_RMF"
        tp_RMF.UseVisualStyleBackColor = True
        ' 
        ' RMF_delta_rmf
        ' 
        RMF_delta_rmf.AutoSize = True
        RMF_delta_rmf.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_delta_rmf.ForeColor = Drawing.Color.RoyalBlue
        RMF_delta_rmf.Location = New System.Drawing.Point(972, 309)
        RMF_delta_rmf.Name = "RMF_delta_rmf"
        RMF_delta_rmf.Size = New System.Drawing.Size(74, 18)
        RMF_delta_rmf.TabIndex = 24
        RMF_delta_rmf.Text = "delta_rmf"
        ' 
        ' RMF_basis
        ' 
        RMF_basis.AutoSize = True
        RMF_basis.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_basis.ForeColor = Drawing.Color.RoyalBlue
        RMF_basis.Location = New System.Drawing.Point(766, 412)
        RMF_basis.Name = "RMF_basis"
        RMF_basis.Size = New System.Drawing.Size(84, 18)
        RMF_basis.TabIndex = 23
        RMF_basis.Text = "basis_wert"
        ' 
        ' RMF_delta_temp
        ' 
        RMF_delta_temp.AutoSize = True
        RMF_delta_temp.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_delta_temp.ForeColor = Drawing.Color.RoyalBlue
        RMF_delta_temp.Location = New System.Drawing.Point(766, 309)
        RMF_delta_temp.Name = "RMF_delta_temp"
        RMF_delta_temp.Size = New System.Drawing.Size(86, 18)
        RMF_delta_temp.TabIndex = 22
        RMF_delta_temp.Text = "delta_temp"
        ' 
        ' RMF_st_delta
        ' 
        RMF_st_delta.AutoSize = True
        RMF_st_delta.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_st_delta.ForeColor = Drawing.Color.RoyalBlue
        RMF_st_delta.Location = New System.Drawing.Point(556, 412)
        RMF_st_delta.Name = "RMF_st_delta"
        RMF_st_delta.Size = New System.Drawing.Size(64, 18)
        RMF_st_delta.TabIndex = 21
        RMF_st_delta.Text = "st_delta"
        ' 
        ' RMF_rt_delta
        ' 
        RMF_rt_delta.AutoSize = True
        RMF_rt_delta.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_rt_delta.ForeColor = Drawing.Color.RoyalBlue
        RMF_rt_delta.Location = New System.Drawing.Point(556, 309)
        RMF_rt_delta.Name = "RMF_rt_delta"
        RMF_rt_delta.Size = New System.Drawing.Size(62, 18)
        RMF_rt_delta.TabIndex = 20
        RMF_rt_delta.Text = "rt_delta"
        ' 
        ' RMF_rt_diff
        ' 
        RMF_rt_diff.AutoSize = True
        RMF_rt_diff.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_rt_diff.ForeColor = Drawing.Color.RoyalBlue
        RMF_rt_diff.Location = New System.Drawing.Point(300, 309)
        RMF_rt_diff.Name = "RMF_rt_diff"
        RMF_rt_diff.Size = New System.Drawing.Size(50, 18)
        RMF_rt_diff.TabIndex = 19
        RMF_rt_diff.Text = "rt_diff"
        ' 
        ' RMF_f_rt
        ' 
        RMF_f_rt.AutoSize = True
        RMF_f_rt.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_f_rt.ForeColor = Drawing.Color.RoyalBlue
        RMF_f_rt.Location = New System.Drawing.Point(105, 434)
        RMF_f_rt.Name = "RMF_f_rt"
        RMF_f_rt.Size = New System.Drawing.Size(32, 18)
        RMF_f_rt.TabIndex = 18
        RMF_f_rt.Text = "f_rt"
        ' 
        ' RMF_rt_0
        ' 
        RMF_rt_0.AutoSize = True
        RMF_rt_0.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_rt_0.ForeColor = Drawing.Color.RoyalBlue
        RMF_rt_0.Location = New System.Drawing.Point(105, 375)
        RMF_rt_0.Name = "RMF_rt_0"
        RMF_rt_0.Size = New System.Drawing.Size(35, 18)
        RMF_rt_0.TabIndex = 17
        RMF_rt_0.Text = "rt_0"
        ' 
        ' RMF_rt
        ' 
        RMF_rt.AutoSize = True
        RMF_rt.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_rt.ForeColor = Drawing.Color.RoyalBlue
        RMF_rt.Location = New System.Drawing.Point(105, 268)
        RMF_rt.Name = "RMF_rt"
        RMF_rt.Size = New System.Drawing.Size(19, 18)
        RMF_rt.TabIndex = 16
        RMF_rt.Text = "rt"
        ' 
        ' RMF_mt_delta
        ' 
        RMF_mt_delta.AutoSize = True
        RMF_mt_delta.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_mt_delta.ForeColor = Drawing.Color.RoyalBlue
        RMF_mt_delta.Location = New System.Drawing.Point(556, 61)
        RMF_mt_delta.Name = "RMF_mt_delta"
        RMF_mt_delta.Size = New System.Drawing.Size(68, 18)
        RMF_mt_delta.TabIndex = 15
        RMF_mt_delta.Text = "mt_delta"
        ' 
        ' RMF_mt_diff
        ' 
        RMF_mt_diff.AutoSize = True
        RMF_mt_diff.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_mt_diff.ForeColor = Drawing.Color.RoyalBlue
        RMF_mt_diff.Location = New System.Drawing.Point(300, 61)
        RMF_mt_diff.Name = "RMF_mt_diff"
        RMF_mt_diff.Size = New System.Drawing.Size(56, 18)
        RMF_mt_diff.TabIndex = 14
        RMF_mt_diff.Text = "mt_diff"
        ' 
        ' RMF_fk_m
        ' 
        RMF_fk_m.AutoSize = True
        RMF_fk_m.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_fk_m.ForeColor = Drawing.Color.RoyalBlue
        RMF_fk_m.Location = New System.Drawing.Point(404, 101)
        RMF_fk_m.Name = "RMF_fk_m"
        RMF_fk_m.Size = New System.Drawing.Size(41, 18)
        RMF_fk_m.TabIndex = 13
        RMF_fk_m.Text = "fk_m"
        ' 
        ' RMF_at_m
        ' 
        RMF_at_m.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_at_m.ForeColor = Drawing.Color.RoyalBlue
        RMF_at_m.Location = New System.Drawing.Point(428, 185)
        RMF_at_m.Name = "RMF_at_m"
        RMF_at_m.Size = New System.Drawing.Size(41, 18)
        RMF_at_m.TabIndex = 12
        RMF_at_m.Text = "at_m"
        RMF_at_m.TextAlign = Drawing.ContentAlignment.TopRight
        ' 
        ' RMF_f_mt
        ' 
        RMF_f_mt.AutoSize = True
        RMF_f_mt.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_f_mt.ForeColor = Drawing.Color.RoyalBlue
        RMF_f_mt.Location = New System.Drawing.Point(105, 185)
        RMF_f_mt.Name = "RMF_f_mt"
        RMF_f_mt.Size = New System.Drawing.Size(38, 18)
        RMF_f_mt.TabIndex = 11
        RMF_f_mt.Text = "f_mt"
        ' 
        ' RMF_mt_0
        ' 
        RMF_mt_0.AutoSize = True
        RMF_mt_0.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_mt_0.ForeColor = Drawing.Color.RoyalBlue
        RMF_mt_0.Location = New System.Drawing.Point(105, 127)
        RMF_mt_0.Name = "RMF_mt_0"
        RMF_mt_0.Size = New System.Drawing.Size(41, 18)
        RMF_mt_0.TabIndex = 10
        RMF_mt_0.Text = "mt_0"
        ' 
        ' RMF_mt
        ' 
        RMF_mt.AutoSize = True
        RMF_mt.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        RMF_mt.ForeColor = Drawing.Color.RoyalBlue
        RMF_mt.Location = New System.Drawing.Point(105, 19)
        RMF_mt.Name = "RMF_mt"
        RMF_mt.Size = New System.Drawing.Size(25, 18)
        RMF_mt.TabIndex = 9
        RMF_mt.Text = "mt"
        ' 
        ' Btn_RMF_Back
        ' 
        Btn_RMF_Back.Location = New System.Drawing.Point(367, 515)
        Btn_RMF_Back.Name = "Btn_RMF_Back"
        Btn_RMF_Back.Size = New System.Drawing.Size(94, 44)
        Btn_RMF_Back.TabIndex = 4
        Btn_RMF_Back.TabStop = False
        Btn_RMF_Back.Text = "Zurück"
        Btn_RMF_Back.UseVisualStyleBackColor = True
        ' 
        ' Btn_STF
        ' 
        Btn_STF.Location = New System.Drawing.Point(467, 515)
        Btn_STF.Name = "Btn_STF"
        Btn_STF.Size = New System.Drawing.Size(94, 44)
        Btn_STF.TabIndex = 2
        Btn_STF.TabStop = False
        Btn_STF.Text = "Sauer Temperatur"
        Btn_STF.UseVisualStyleBackColor = True
        ' 
        ' tp_STF
        ' 
        tp_STF.BackgroundImage = CType(resources.GetObject("tp_STF.BackgroundImage"), Drawing.Image)
        tp_STF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        tp_STF.Controls.Add(STF_st_delta)
        tp_STF.Controls.Add(STF_st_diff)
        tp_STF.Controls.Add(STF_fk_s)
        tp_STF.Controls.Add(STF_at_s)
        tp_STF.Controls.Add(STF_f_st)
        tp_STF.Controls.Add(STF_st_0)
        tp_STF.Controls.Add(STF_st)
        tp_STF.Controls.Add(Btn_STF_Back)
        tp_STF.Location = New System.Drawing.Point(4, 23)
        tp_STF.Name = "tp_STF"
        tp_STF.Padding = New System.Windows.Forms.Padding(3)
        tp_STF.Size = New System.Drawing.Size(1272, 610)
        tp_STF.TabIndex = 4
        tp_STF.Text = "Calc_STF"
        tp_STF.UseVisualStyleBackColor = True
        ' 
        ' STF_st_delta
        ' 
        STF_st_delta.AutoSize = True
        STF_st_delta.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        STF_st_delta.ForeColor = Drawing.Color.RoyalBlue
        STF_st_delta.Location = New System.Drawing.Point(621, 150)
        STF_st_delta.Name = "STF_st_delta"
        STF_st_delta.Size = New System.Drawing.Size(64, 18)
        STF_st_delta.TabIndex = 22
        STF_st_delta.Text = "st_delta"
        ' 
        ' STF_st_diff
        ' 
        STF_st_diff.AutoSize = True
        STF_st_diff.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        STF_st_diff.ForeColor = Drawing.Color.RoyalBlue
        STF_st_diff.Location = New System.Drawing.Point(365, 150)
        STF_st_diff.Name = "STF_st_diff"
        STF_st_diff.Size = New System.Drawing.Size(52, 18)
        STF_st_diff.TabIndex = 21
        STF_st_diff.Text = "st_diff"
        ' 
        ' STF_fk_s
        ' 
        STF_fk_s.AutoSize = True
        STF_fk_s.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        STF_fk_s.ForeColor = Drawing.Color.RoyalBlue
        STF_fk_s.Location = New System.Drawing.Point(469, 190)
        STF_fk_s.Name = "STF_fk_s"
        STF_fk_s.Size = New System.Drawing.Size(37, 18)
        STF_fk_s.TabIndex = 20
        STF_fk_s.Text = "fk_s"
        ' 
        ' STF_at_s
        ' 
        STF_at_s.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        STF_at_s.ForeColor = Drawing.Color.RoyalBlue
        STF_at_s.Location = New System.Drawing.Point(493, 274)
        STF_at_s.Name = "STF_at_s"
        STF_at_s.Size = New System.Drawing.Size(41, 18)
        STF_at_s.TabIndex = 19
        STF_at_s.Text = "at_s"
        STF_at_s.TextAlign = Drawing.ContentAlignment.TopRight
        ' 
        ' STF_f_st
        ' 
        STF_f_st.AutoSize = True
        STF_f_st.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        STF_f_st.ForeColor = Drawing.Color.RoyalBlue
        STF_f_st.Location = New System.Drawing.Point(170, 274)
        STF_f_st.Name = "STF_f_st"
        STF_f_st.Size = New System.Drawing.Size(34, 18)
        STF_f_st.TabIndex = 18
        STF_f_st.Text = "f_st"
        ' 
        ' STF_st_0
        ' 
        STF_st_0.AutoSize = True
        STF_st_0.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        STF_st_0.ForeColor = Drawing.Color.RoyalBlue
        STF_st_0.Location = New System.Drawing.Point(170, 216)
        STF_st_0.Name = "STF_st_0"
        STF_st_0.Size = New System.Drawing.Size(37, 18)
        STF_st_0.TabIndex = 17
        STF_st_0.Text = "st_0"
        ' 
        ' STF_st
        ' 
        STF_st.AutoSize = True
        STF_st.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        STF_st.ForeColor = Drawing.Color.RoyalBlue
        STF_st.Location = New System.Drawing.Point(170, 108)
        STF_st.Name = "STF_st"
        STF_st.Size = New System.Drawing.Size(21, 18)
        STF_st.TabIndex = 16
        STF_st.Text = "st"
        ' 
        ' Btn_STF_Back
        ' 
        Btn_STF_Back.Location = New System.Drawing.Point(367, 515)
        Btn_STF_Back.Name = "Btn_STF_Back"
        Btn_STF_Back.Size = New System.Drawing.Size(94, 44)
        Btn_STF_Back.TabIndex = 3
        Btn_STF_Back.TabStop = False
        Btn_STF_Back.Text = "Zurück"
        Btn_STF_Back.UseVisualStyleBackColor = True
        ' 
        ' tp_TTS
        ' 
        tp_TTS.BackgroundImage = CType(resources.GetObject("tp_TTS.BackgroundImage"), Drawing.Image)
        tp_TTS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        tp_TTS.Controls.Add(TTS_korr_p3)
        tp_TTS.Controls.Add(TTS_korr_p2)
        tp_TTS.Controls.Add(TTS_korr_p1)
        tp_TTS.Controls.Add(TTS_tts_p3)
        tp_TTS.Controls.Add(TTS_tts_p2)
        tp_TTS.Controls.Add(TTS_tts_p1)
        tp_TTS.Controls.Add(TTS_rs_par3)
        tp_TTS.Controls.Add(TTS_rs_par2)
        tp_TTS.Controls.Add(TTS_delta_tts)
        tp_TTS.Controls.Add(TTS_rs_par1)
        tp_TTS.Controls.Add(Btn_TTS_Back)
        tp_TTS.Location = New System.Drawing.Point(4, 23)
        tp_TTS.Name = "tp_TTS"
        tp_TTS.Size = New System.Drawing.Size(1272, 610)
        tp_TTS.TabIndex = 2
        tp_TTS.Text = "Calc_TTS"
        tp_TTS.UseVisualStyleBackColor = True
        ' 
        ' TTS_korr_p3
        ' 
        TTS_korr_p3.AutoSize = True
        TTS_korr_p3.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_korr_p3.ForeColor = Drawing.Color.RoyalBlue
        TTS_korr_p3.Location = New System.Drawing.Point(308, 430)
        TTS_korr_p3.Name = "TTS_korr_p3"
        TTS_korr_p3.Size = New System.Drawing.Size(62, 18)
        TTS_korr_p3.TabIndex = 14
        TTS_korr_p3.Text = "korr_p3"
        ' 
        ' TTS_korr_p2
        ' 
        TTS_korr_p2.AutoSize = True
        TTS_korr_p2.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_korr_p2.ForeColor = Drawing.Color.RoyalBlue
        TTS_korr_p2.Location = New System.Drawing.Point(308, 245)
        TTS_korr_p2.Name = "TTS_korr_p2"
        TTS_korr_p2.Size = New System.Drawing.Size(62, 18)
        TTS_korr_p2.TabIndex = 13
        TTS_korr_p2.Text = "korr_p2"
        ' 
        ' TTS_korr_p1
        ' 
        TTS_korr_p1.AutoSize = True
        TTS_korr_p1.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_korr_p1.ForeColor = Drawing.Color.RoyalBlue
        TTS_korr_p1.Location = New System.Drawing.Point(308, 61)
        TTS_korr_p1.Name = "TTS_korr_p1"
        TTS_korr_p1.Size = New System.Drawing.Size(62, 18)
        TTS_korr_p1.TabIndex = 12
        TTS_korr_p1.Text = "korr_p1"
        ' 
        ' TTS_tts_p3
        ' 
        TTS_tts_p3.AutoSize = True
        TTS_tts_p3.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_tts_p3.ForeColor = Drawing.Color.RoyalBlue
        TTS_tts_p3.Location = New System.Drawing.Point(107, 495)
        TTS_tts_p3.Name = "TTS_tts_p3"
        TTS_tts_p3.Size = New System.Drawing.Size(51, 18)
        TTS_tts_p3.TabIndex = 11
        TTS_tts_p3.Text = "tts_p3"
        ' 
        ' TTS_tts_p2
        ' 
        TTS_tts_p2.AutoSize = True
        TTS_tts_p2.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_tts_p2.ForeColor = Drawing.Color.RoyalBlue
        TTS_tts_p2.Location = New System.Drawing.Point(107, 312)
        TTS_tts_p2.Name = "TTS_tts_p2"
        TTS_tts_p2.Size = New System.Drawing.Size(51, 18)
        TTS_tts_p2.TabIndex = 10
        TTS_tts_p2.Text = "tts_p2"
        ' 
        ' TTS_tts_p1
        ' 
        TTS_tts_p1.AutoSize = True
        TTS_tts_p1.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_tts_p1.ForeColor = Drawing.Color.RoyalBlue
        TTS_tts_p1.Location = New System.Drawing.Point(107, 127)
        TTS_tts_p1.Name = "TTS_tts_p1"
        TTS_tts_p1.Size = New System.Drawing.Size(51, 18)
        TTS_tts_p1.TabIndex = 9
        TTS_tts_p1.Text = "tts_p1"
        ' 
        ' TTS_rs_par3
        ' 
        TTS_rs_par3.AutoSize = True
        TTS_rs_par3.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_rs_par3.ForeColor = Drawing.Color.RoyalBlue
        TTS_rs_par3.Location = New System.Drawing.Point(107, 389)
        TTS_rs_par3.Name = "TTS_rs_par3"
        TTS_rs_par3.Size = New System.Drawing.Size(61, 18)
        TTS_rs_par3.TabIndex = 8
        TTS_rs_par3.Text = "rs_par3"
        ' 
        ' TTS_rs_par2
        ' 
        TTS_rs_par2.AutoSize = True
        TTS_rs_par2.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_rs_par2.ForeColor = Drawing.Color.RoyalBlue
        TTS_rs_par2.Location = New System.Drawing.Point(107, 205)
        TTS_rs_par2.Name = "TTS_rs_par2"
        TTS_rs_par2.Size = New System.Drawing.Size(61, 18)
        TTS_rs_par2.TabIndex = 7
        TTS_rs_par2.Text = "rs_par2"
        ' 
        ' TTS_delta_tts
        ' 
        TTS_delta_tts.AutoSize = True
        TTS_delta_tts.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_delta_tts.ForeColor = Drawing.Color.RoyalBlue
        TTS_delta_tts.Location = New System.Drawing.Point(563, 246)
        TTS_delta_tts.Name = "TTS_delta_tts"
        TTS_delta_tts.Size = New System.Drawing.Size(69, 18)
        TTS_delta_tts.TabIndex = 6
        TTS_delta_tts.Text = "delta_tts"
        ' 
        ' TTS_rs_par1
        ' 
        TTS_rs_par1.AutoSize = True
        TTS_rs_par1.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        TTS_rs_par1.ForeColor = Drawing.Color.RoyalBlue
        TTS_rs_par1.Location = New System.Drawing.Point(107, 21)
        TTS_rs_par1.Name = "TTS_rs_par1"
        TTS_rs_par1.Size = New System.Drawing.Size(61, 18)
        TTS_rs_par1.TabIndex = 5
        TTS_rs_par1.Text = "rs_par1"
        ' 
        ' Btn_TTS_Back
        ' 
        Btn_TTS_Back.Location = New System.Drawing.Point(367, 515)
        Btn_TTS_Back.Name = "Btn_TTS_Back"
        Btn_TTS_Back.Size = New System.Drawing.Size(94, 44)
        Btn_TTS_Back.TabIndex = 4
        Btn_TTS_Back.TabStop = False
        Btn_TTS_Back.Text = "Zurück"
        Btn_TTS_Back.UseVisualStyleBackColor = True
        ' 
        ' tp_EIS
        ' 
        tp_EIS.Controls.Add(EIS_m_w_soll_neu)
        tp_EIS.Controls.Add(EIS_m_eis_soll_neu)
        tp_EIS.Controls.Add(EIS_t_w_soll_neu)
        tp_EIS.Location = New System.Drawing.Point(4, 23)
        tp_EIS.Name = "tp_EIS"
        tp_EIS.Size = New System.Drawing.Size(1272, 610)
        tp_EIS.TabIndex = 3
        tp_EIS.Text = "Calc_EIS"
        tp_EIS.UseVisualStyleBackColor = True
        ' 
        ' EIS_m_w_soll_neu
        ' 
        EIS_m_w_soll_neu.AutoSize = True
        EIS_m_w_soll_neu.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        EIS_m_w_soll_neu.ForeColor = Drawing.Color.RoyalBlue
        EIS_m_w_soll_neu.Location = New System.Drawing.Point(606, 276)
        EIS_m_w_soll_neu.Name = "EIS_m_w_soll_neu"
        EIS_m_w_soll_neu.Size = New System.Drawing.Size(107, 18)
        EIS_m_w_soll_neu.TabIndex = 15
        EIS_m_w_soll_neu.Text = "m_w_soll_neu"
        ' 
        ' EIS_m_eis_soll_neu
        ' 
        EIS_m_eis_soll_neu.AutoSize = True
        EIS_m_eis_soll_neu.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        EIS_m_eis_soll_neu.ForeColor = Drawing.Color.RoyalBlue
        EIS_m_eis_soll_neu.Location = New System.Drawing.Point(606, 240)
        EIS_m_eis_soll_neu.Name = "EIS_m_eis_soll_neu"
        EIS_m_eis_soll_neu.Size = New System.Drawing.Size(117, 18)
        EIS_m_eis_soll_neu.TabIndex = 14
        EIS_m_eis_soll_neu.Text = "m_eis_soll_neu"
        ' 
        ' EIS_t_w_soll_neu
        ' 
        EIS_t_w_soll_neu.AutoSize = True
        EIS_t_w_soll_neu.Font = New System.Drawing.Font("Arial", 11.25F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CByte(0))
        EIS_t_w_soll_neu.ForeColor = Drawing.Color.RoyalBlue
        EIS_t_w_soll_neu.Location = New System.Drawing.Point(606, 315)
        EIS_t_w_soll_neu.Name = "EIS_t_w_soll_neu"
        EIS_t_w_soll_neu.Size = New System.Drawing.Size(100, 18)
        EIS_t_w_soll_neu.TabIndex = 13
        EIS_t_w_soll_neu.Text = "t_w_soll_neu"
        ' 
        ' tp_MSG
        ' 
        tp_MSG.Controls.Add(Btn_MSG_Back)
        tp_MSG.Controls.Add(tbLogFile)
        tp_MSG.Location = New System.Drawing.Point(4, 23)
        tp_MSG.Name = "tp_MSG"
        tp_MSG.Padding = New System.Windows.Forms.Padding(3)
        tp_MSG.Size = New System.Drawing.Size(1272, 610)
        tp_MSG.TabIndex = 5
        tp_MSG.Text = "Calc_MSG"
        tp_MSG.UseVisualStyleBackColor = True
        ' 
        ' Btn_MSG_Back
        ' 
        Btn_MSG_Back.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
        Btn_MSG_Back.Location = New System.Drawing.Point(1170, 558)
        Btn_MSG_Back.Name = "Btn_MSG_Back"
        Btn_MSG_Back.Size = New System.Drawing.Size(94, 44)
        Btn_MSG_Back.TabIndex = 6
        Btn_MSG_Back.TabStop = False
        Btn_MSG_Back.Text = "Zurück"
        Btn_MSG_Back.UseVisualStyleBackColor = True
        ' 
        ' tbLogFile
        ' 
        tbLogFile.Dock = System.Windows.Forms.DockStyle.Left
        tbLogFile.Font = New System.Drawing.Font("Courier New", 8.25F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CByte(0))
        tbLogFile.Location = New System.Drawing.Point(3, 3)
        tbLogFile.Multiline = True
        tbLogFile.Name = "tbLogFile"
        tbLogFile.ReadOnly = True
        tbLogFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        tbLogFile.Size = New System.Drawing.Size(992, 604)
        tbLogFile.TabIndex = 7
        ' 
        ' wb_ChargenWasserTemp
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(1280, 637)
        Controls.Add(TabControl)
        Name = "wb_ChargenWasserTemp"
        Text = "Berechnung der Wasser-Solltemperatur"
        TabControl.ResumeLayout(False)
        tp_ALG.ResumeLayout(False)
        tp_ALG.PerformLayout()
        tp_RMF.ResumeLayout(False)
        tp_RMF.PerformLayout()
        tp_STF.ResumeLayout(False)
        tp_STF.PerformLayout()
        tp_TTS.ResumeLayout(False)
        tp_TTS.PerformLayout()
        tp_EIS.ResumeLayout(False)
        tp_EIS.PerformLayout()
        tp_MSG.ResumeLayout(False)
        tp_MSG.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl As wb_TabControl
    Friend WithEvents tp_ALG As System.Windows.Forms.TabPage
    Friend WithEvents tp_RMF As System.Windows.Forms.TabPage
    Friend WithEvents tp_TTS As System.Windows.Forms.TabPage
    Friend WithEvents tp_EIS As System.Windows.Forms.TabPage
    Friend WithEvents BtnRMF As System.Windows.Forms.Button
    Friend WithEvents BtnTTS As System.Windows.Forms.Button
    Friend WithEvents tp_STF As System.Windows.Forms.TabPage
    Friend WithEvents Btn_STF As System.Windows.Forms.Button
    Friend WithEvents Btn_RMF_Back As System.Windows.Forms.Button
    Friend WithEvents Btn_STF_Back As System.Windows.Forms.Button
    Friend WithEvents Btn_TTS_Back As System.Windows.Forms.Button
    Friend WithEvents lbl_Header As System.Windows.Forms.Label
    Friend WithEvents Btn_ALG_Back As System.Windows.Forms.Button
    Friend WithEvents Btn_MSG As System.Windows.Forms.Button
    Friend WithEvents tp_MSG As System.Windows.Forms.TabPage
    Friend WithEvents Btn_MSG_Back As System.Windows.Forms.Button
    Friend WithEvents TTS_rs_par3 As System.Windows.Forms.Label
    Friend WithEvents TTS_rs_par2 As System.Windows.Forms.Label
    Friend WithEvents TTS_delta_tts As System.Windows.Forms.Label
    Friend WithEvents TTS_rs_par1 As System.Windows.Forms.Label
    Friend WithEvents TTS_korr_p3 As System.Windows.Forms.Label
    Friend WithEvents TTS_korr_p2 As System.Windows.Forms.Label
    Friend WithEvents TTS_korr_p1 As System.Windows.Forms.Label
    Friend WithEvents TTS_tts_p3 As System.Windows.Forms.Label
    Friend WithEvents TTS_tts_p2 As System.Windows.Forms.Label
    Friend WithEvents TTS_tts_p1 As System.Windows.Forms.Label
    Friend WithEvents ALG_delta_tts As System.Windows.Forms.Label
    Friend WithEvents ALG_delta_rmf As System.Windows.Forms.Label
    Friend WithEvents RMF_mt_delta As System.Windows.Forms.Label
    Friend WithEvents RMF_mt_diff As System.Windows.Forms.Label
    Friend WithEvents RMF_fk_m As System.Windows.Forms.Label
    Friend WithEvents RMF_at_m As System.Windows.Forms.Label
    Friend WithEvents RMF_f_mt As System.Windows.Forms.Label
    Friend WithEvents RMF_mt_0 As System.Windows.Forms.Label
    Friend WithEvents RMF_mt As System.Windows.Forms.Label
    Friend WithEvents RMF_delta_rmf As System.Windows.Forms.Label
    Friend WithEvents RMF_basis As System.Windows.Forms.Label
    Friend WithEvents RMF_delta_temp As System.Windows.Forms.Label
    Friend WithEvents RMF_st_delta As System.Windows.Forms.Label
    Friend WithEvents RMF_rt_delta As System.Windows.Forms.Label
    Friend WithEvents RMF_rt_diff As System.Windows.Forms.Label
    Friend WithEvents RMF_f_rt As System.Windows.Forms.Label
    Friend WithEvents RMF_rt_0 As System.Windows.Forms.Label
    Friend WithEvents RMF_rt As System.Windows.Forms.Label
    Friend WithEvents ALG_t_neu_vor_eis As System.Windows.Forms.Label
    Friend WithEvents ALG_t_rezept As System.Windows.Forms.Label
    Friend WithEvents ALG_t_delta As System.Windows.Forms.Label
    Friend WithEvents STF_st_delta As System.Windows.Forms.Label
    Friend WithEvents STF_st_diff As System.Windows.Forms.Label
    Friend WithEvents STF_fk_s As System.Windows.Forms.Label
    Friend WithEvents STF_at_s As System.Windows.Forms.Label
    Friend WithEvents STF_f_st As System.Windows.Forms.Label
    Friend WithEvents STF_st_0 As System.Windows.Forms.Label
    Friend WithEvents STF_st As System.Windows.Forms.Label
    Friend WithEvents tbLogFile As System.Windows.Forms.TextBox
    Friend WithEvents ALG_BerechneteWerte As System.Windows.Forms.Label
    Friend WithEvents ALG_e_soll_neu As System.Windows.Forms.Label
    Friend WithEvents ALG_w_soll_neu As System.Windows.Forms.Label
    Friend WithEvents EIS_m_w_soll_neu As System.Windows.Forms.Label
    Friend WithEvents EIS_m_eis_soll_neu As System.Windows.Forms.Label
    Friend WithEvents EIS_t_w_soll_neu As System.Windows.Forms.Label
    Friend WithEvents lbl_e_soll_neu As System.Windows.Forms.Label
    Friend WithEvents lbl_w_soll_neu As System.Windows.Forms.Label
    Friend WithEvents ALG_Error As System.Windows.Forms.Label
    Friend WithEvents ALG_error_rmf As System.Windows.Forms.Label
    Friend WithEvents ALG_Error_tts As System.Windows.Forms.Label
End Class
