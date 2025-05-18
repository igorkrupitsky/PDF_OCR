<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PDF_OCR
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFolderIn = New System.Windows.Forms.TextBox()
        Me.txtFolderOut = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTesseractPath = New System.Windows.Forms.TextBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.btnIn = New System.Windows.Forms.Button()
        Me.btnOut = New System.Windows.Forms.Button()
        Me.btnTesseract = New System.Windows.Forms.Button()
        Me.fldFrom = New System.Windows.Forms.FolderBrowserDialog()
        Me.dOpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.txtGhostscriptPath = New System.Windows.Forms.TextBox()
        Me.btnGhostscript = New System.Windows.Forms.Button()
        Me.chkOcrWithText = New System.Windows.Forms.CheckBox()
        Me.lbTesseract = New System.Windows.Forms.LinkLabel()
        Me.lbGhostscript = New System.Windows.Forms.LinkLabel()
        Me.chkMerge = New System.Windows.Forms.CheckBox()
        Me.chkBookmarks = New System.Windows.Forms.CheckBox()
        Me.chkResize = New System.Windows.Forms.CheckBox()
        Me.chkCreateInParentFolder = New System.Windows.Forms.CheckBox()
        Me.chkDeleteSourceFiles = New System.Windows.Forms.CheckBox()
        Me.gbMerge = New System.Windows.Forms.GroupBox()
        Me.chkLogTime = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnStop = New System.Windows.Forms.LinkLabel()
        Me.lbCount = New System.Windows.Forms.Label()
        Me.gbMerge.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 20)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "In Folder"
        '
        'txtFolderIn
        '
        Me.txtFolderIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFolderIn.Location = New System.Drawing.Point(144, 15)
        Me.txtFolderIn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFolderIn.Name = "txtFolderIn"
        Me.txtFolderIn.Size = New System.Drawing.Size(734, 26)
        Me.txtFolderIn.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtFolderIn, "File share or folder name")
        '
        'txtFolderOut
        '
        Me.txtFolderOut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFolderOut.Location = New System.Drawing.Point(144, 58)
        Me.txtFolderOut.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFolderOut.Name = "txtFolderOut"
        Me.txtFolderOut.Size = New System.Drawing.Size(734, 26)
        Me.txtFolderOut.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtFolderOut, "File share or folder name")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 63)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 20)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Out Folder"
        '
        'txtTesseractPath
        '
        Me.txtTesseractPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTesseractPath.Location = New System.Drawing.Point(144, 98)
        Me.txtTesseractPath.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTesseractPath.Name = "txtTesseractPath"
        Me.txtTesseractPath.Size = New System.Drawing.Size(734, 26)
        Me.txtTesseractPath.TabIndex = 11
        '
        'btnProcess
        '
        Me.btnProcess.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnProcess.Location = New System.Drawing.Point(144, 183)
        Me.btnProcess.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(736, 40)
        Me.btnProcess.TabIndex = 12
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'btnIn
        '
        Me.btnIn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnIn.Location = New System.Drawing.Point(890, 12)
        Me.btnIn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnIn.Name = "btnIn"
        Me.btnIn.Size = New System.Drawing.Size(52, 35)
        Me.btnIn.TabIndex = 13
        Me.btnIn.Text = "..."
        Me.btnIn.UseVisualStyleBackColor = True
        '
        'btnOut
        '
        Me.btnOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOut.Location = New System.Drawing.Point(890, 55)
        Me.btnOut.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnOut.Name = "btnOut"
        Me.btnOut.Size = New System.Drawing.Size(52, 35)
        Me.btnOut.TabIndex = 14
        Me.btnOut.Text = "..."
        Me.btnOut.UseVisualStyleBackColor = True
        '
        'btnTesseract
        '
        Me.btnTesseract.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTesseract.Location = New System.Drawing.Point(890, 95)
        Me.btnTesseract.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTesseract.Name = "btnTesseract"
        Me.btnTesseract.Size = New System.Drawing.Size(52, 35)
        Me.btnTesseract.TabIndex = 18
        Me.btnTesseract.Text = "..."
        Me.btnTesseract.UseVisualStyleBackColor = True
        '
        'dOpenFile
        '
        Me.dOpenFile.FileName = "OpenFileDialog1"
        '
        'txtOutput
        '
        Me.txtOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutput.Location = New System.Drawing.Point(24, 355)
        Me.txtOutput.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutput.Size = New System.Drawing.Size(916, 204)
        Me.txtOutput.TabIndex = 19
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(24, 577)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(820, 15)
        Me.ProgressBar1.TabIndex = 20
        '
        'txtGhostscriptPath
        '
        Me.txtGhostscriptPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGhostscriptPath.Location = New System.Drawing.Point(144, 143)
        Me.txtGhostscriptPath.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtGhostscriptPath.Name = "txtGhostscriptPath"
        Me.txtGhostscriptPath.Size = New System.Drawing.Size(734, 26)
        Me.txtGhostscriptPath.TabIndex = 25
        '
        'btnGhostscript
        '
        Me.btnGhostscript.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGhostscript.Location = New System.Drawing.Point(890, 140)
        Me.btnGhostscript.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnGhostscript.Name = "btnGhostscript"
        Me.btnGhostscript.Size = New System.Drawing.Size(52, 35)
        Me.btnGhostscript.TabIndex = 26
        Me.btnGhostscript.Text = "..."
        Me.btnGhostscript.UseVisualStyleBackColor = True
        '
        'chkOcrWithText
        '
        Me.chkOcrWithText.AutoSize = True
        Me.chkOcrWithText.Location = New System.Drawing.Point(22, 235)
        Me.chkOcrWithText.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkOcrWithText.Name = "chkOcrWithText"
        Me.chkOcrWithText.Size = New System.Drawing.Size(168, 24)
        Me.chkOcrWithText.TabIndex = 27
        Me.chkOcrWithText.Text = "OCR PDF with text"
        Me.chkOcrWithText.UseVisualStyleBackColor = True
        '
        'lbTesseract
        '
        Me.lbTesseract.AutoSize = True
        Me.lbTesseract.Location = New System.Drawing.Point(18, 103)
        Me.lbTesseract.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbTesseract.Name = "lbTesseract"
        Me.lbTesseract.Size = New System.Drawing.Size(108, 20)
        Me.lbTesseract.TabIndex = 28
        Me.lbTesseract.TabStop = True
        Me.lbTesseract.Text = "Tesseract exe"
        '
        'lbGhostscript
        '
        Me.lbGhostscript.AutoSize = True
        Me.lbGhostscript.Location = New System.Drawing.Point(20, 148)
        Me.lbGhostscript.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbGhostscript.Name = "lbGhostscript"
        Me.lbGhostscript.Size = New System.Drawing.Size(120, 20)
        Me.lbGhostscript.TabIndex = 29
        Me.lbGhostscript.TabStop = True
        Me.lbGhostscript.Text = "Ghostscript exe"
        '
        'chkMerge
        '
        Me.chkMerge.AutoSize = True
        Me.chkMerge.Location = New System.Drawing.Point(207, 234)
        Me.chkMerge.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkMerge.Name = "chkMerge"
        Me.chkMerge.Size = New System.Drawing.Size(80, 24)
        Me.chkMerge.TabIndex = 30
        Me.chkMerge.Text = "Merge"
        Me.chkMerge.UseVisualStyleBackColor = True
        '
        'chkBookmarks
        '
        Me.chkBookmarks.AutoSize = True
        Me.chkBookmarks.Checked = True
        Me.chkBookmarks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBookmarks.Location = New System.Drawing.Point(9, 29)
        Me.chkBookmarks.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkBookmarks.Name = "chkBookmarks"
        Me.chkBookmarks.Size = New System.Drawing.Size(148, 24)
        Me.chkBookmarks.TabIndex = 31
        Me.chkBookmarks.Text = "Add Bookmarks"
        Me.chkBookmarks.UseVisualStyleBackColor = True
        '
        'chkResize
        '
        Me.chkResize.AutoSize = True
        Me.chkResize.Checked = True
        Me.chkResize.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkResize.Location = New System.Drawing.Point(9, 65)
        Me.chkResize.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkResize.Name = "chkResize"
        Me.chkResize.Size = New System.Drawing.Size(84, 24)
        Me.chkResize.TabIndex = 32
        Me.chkResize.Text = "Resize"
        Me.chkResize.UseVisualStyleBackColor = True
        '
        'chkCreateInParentFolder
        '
        Me.chkCreateInParentFolder.AutoSize = True
        Me.chkCreateInParentFolder.Location = New System.Drawing.Point(170, 29)
        Me.chkCreateInParentFolder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkCreateInParentFolder.Name = "chkCreateInParentFolder"
        Me.chkCreateInParentFolder.Size = New System.Drawing.Size(199, 24)
        Me.chkCreateInParentFolder.TabIndex = 33
        Me.chkCreateInParentFolder.Text = "Create in Parent Folder"
        Me.chkCreateInParentFolder.UseVisualStyleBackColor = True
        '
        'chkDeleteSourceFiles
        '
        Me.chkDeleteSourceFiles.AutoSize = True
        Me.chkDeleteSourceFiles.Location = New System.Drawing.Point(170, 65)
        Me.chkDeleteSourceFiles.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkDeleteSourceFiles.Name = "chkDeleteSourceFiles"
        Me.chkDeleteSourceFiles.Size = New System.Drawing.Size(174, 24)
        Me.chkDeleteSourceFiles.TabIndex = 34
        Me.chkDeleteSourceFiles.Text = "Delete Source Files"
        Me.chkDeleteSourceFiles.UseVisualStyleBackColor = True
        '
        'gbMerge
        '
        Me.gbMerge.Controls.Add(Me.chkBookmarks)
        Me.gbMerge.Controls.Add(Me.chkResize)
        Me.gbMerge.Controls.Add(Me.chkDeleteSourceFiles)
        Me.gbMerge.Controls.Add(Me.chkCreateInParentFolder)
        Me.gbMerge.Location = New System.Drawing.Point(486, 234)
        Me.gbMerge.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbMerge.Name = "gbMerge"
        Me.gbMerge.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbMerge.Size = New System.Drawing.Size(398, 112)
        Me.gbMerge.TabIndex = 35
        Me.gbMerge.TabStop = False
        Me.gbMerge.Text = "Merge"
        '
        'chkLogTime
        '
        Me.chkLogTime.AutoSize = True
        Me.chkLogTime.Location = New System.Drawing.Point(22, 269)
        Me.chkLogTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkLogTime.Name = "chkLogTime"
        Me.chkLogTime.Size = New System.Drawing.Size(100, 24)
        Me.chkLogTime.TabIndex = 36
        Me.chkLogTime.Text = "Log Time"
        Me.chkLogTime.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStop.AutoSize = True
        Me.btnStop.Location = New System.Drawing.Point(854, 572)
        Me.btnStop.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(43, 20)
        Me.btnStop.TabIndex = 41
        Me.btnStop.TabStop = True
        Me.btnStop.Text = "Stop"
        Me.btnStop.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnStop.Visible = False
        '
        'lbCount
        '
        Me.lbCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbCount.AutoSize = True
        Me.lbCount.Location = New System.Drawing.Point(904, 572)
        Me.lbCount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbCount.Name = "lbCount"
        Me.lbCount.Size = New System.Drawing.Size(36, 20)
        Me.lbCount.TabIndex = 42
        Me.lbCount.Text = "000"
        Me.lbCount.Visible = False
        '
        'PDF_OCR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(960, 595)
        Me.Controls.Add(Me.lbCount)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.chkLogTime)
        Me.Controls.Add(Me.gbMerge)
        Me.Controls.Add(Me.chkMerge)
        Me.Controls.Add(Me.lbGhostscript)
        Me.Controls.Add(Me.lbTesseract)
        Me.Controls.Add(Me.chkOcrWithText)
        Me.Controls.Add(Me.btnGhostscript)
        Me.Controls.Add(Me.txtGhostscriptPath)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.btnTesseract)
        Me.Controls.Add(Me.btnOut)
        Me.Controls.Add(Me.btnIn)
        Me.Controls.Add(Me.btnProcess)
        Me.Controls.Add(Me.txtTesseractPath)
        Me.Controls.Add(Me.txtFolderOut)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtFolderIn)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "PDF_OCR"
        Me.Text = "PDF OCR"
        Me.gbMerge.ResumeLayout(False)
        Me.gbMerge.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFolderIn As System.Windows.Forms.TextBox
    Friend WithEvents txtFolderOut As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTesseractPath As System.Windows.Forms.TextBox
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents btnIn As System.Windows.Forms.Button
    Friend WithEvents btnOut As System.Windows.Forms.Button
    Friend WithEvents btnTesseract As System.Windows.Forms.Button
    Friend WithEvents fldFrom As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents dOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents txtGhostscriptPath As System.Windows.Forms.TextBox
    Friend WithEvents btnGhostscript As System.Windows.Forms.Button
    Friend WithEvents chkOcrWithText As System.Windows.Forms.CheckBox
    Friend WithEvents lbTesseract As System.Windows.Forms.LinkLabel
    Friend WithEvents lbGhostscript As System.Windows.Forms.LinkLabel
    Friend WithEvents chkMerge As System.Windows.Forms.CheckBox
    Friend WithEvents chkBookmarks As System.Windows.Forms.CheckBox
    Friend WithEvents chkResize As System.Windows.Forms.CheckBox
    Friend WithEvents chkCreateInParentFolder As System.Windows.Forms.CheckBox
    Friend WithEvents chkDeleteSourceFiles As System.Windows.Forms.CheckBox
    Friend WithEvents gbMerge As System.Windows.Forms.GroupBox
    Friend WithEvents chkLogTime As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents btnStop As LinkLabel
    Friend WithEvents lbCount As Label
End Class
