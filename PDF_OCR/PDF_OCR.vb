Imports System.IO
Imports System.Net
Imports System.Text
Imports iTextSharp.text.pdf
Imports System.Drawing.Imaging

Public Class PDF_OCR

	Dim bLogFileUsed As Boolean = False
	Dim oLogFile As System.IO.StreamWriter
	Dim oAppSetting As New AppSetting()

	Dim sGhostscriptPath As String = "C:\Program Files\gs\gs10.01.1\bin\gswin64.exe" 'https://ghostscript.com/download/gsdnld.html
	Dim sTesseractPath As String = "C:\Program Files\Tesseract-OCR\tesseract.exe"	'https://github.com/UB-Mannheim/tesseract/wiki
	Dim sFolderIn As String = ""
	Dim sFolderOut As String = ""

	Private sFolderDone As String = ""
	Private sFolderFailed As String = ""

	Private sFolderProcess As String = ""
	Private sFolderProcessLocal As String = ""

    Private iFileNumber As Integer = 0
    Private bStop As Boolean = False
    Private iTimeOutSec As Integer = 600 '10 min timeout

    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		oAppSetting.SetValue("TesseractPath", txtTesseractPath.Text)
		oAppSetting.SetValue("GhostscriptPath", txtGhostscriptPath.Text)
		oAppSetting.SetValue("FolderIn", txtFolderIn.Text)
		oAppSetting.SetValue("FolderOut", txtFolderOut.Text)
        oAppSetting.SetValue("OcrWithText", IIf(chkOcrWithText.Checked, "1", "0"))
        oAppSetting.SetValue("LogTime", IIf(chkLogTime.Checked, "1", "0"))

        oAppSetting.SetValue("Merge", IIf(chkMerge.Checked, "1", "0"))
		oAppSetting.SetValue("Bookmarks", IIf(chkBookmarks.Checked, "1", "0"))
		oAppSetting.SetValue("Resize", IIf(chkResize.Checked, "1", "0"))
		oAppSetting.SetValue("DeleteSourceFiles", IIf(chkDeleteSourceFiles.Checked, "1", "0"))
		oAppSetting.SetValue("CreateInParentFolder", IIf(chkCreateInParentFolder.Checked, "1", "0"))

		oAppSetting.SaveData()
	End Sub

	Private Sub Form4_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		oAppSetting.LoadData()

		txtFolderIn.Text = oAppSetting.GetValue("FolderIn")
		txtFolderOut.Text = oAppSetting.GetValue("FolderOut")

		txtTesseractPath.Text = oAppSetting.GetValue("TesseractPath")
		If txtTesseractPath.Text = "" And IO.File.Exists(sTesseractPath) Then
			txtTesseractPath.Text = sTesseractPath
		End If

		txtGhostscriptPath.Text = oAppSetting.GetValue("GhostscriptPath")
		If txtGhostscriptPath.Text = "" And IO.File.Exists(sGhostscriptPath) Then
			txtGhostscriptPath.Text = sGhostscriptPath
		End If

        chkOcrWithText.Checked = oAppSetting.GetValue("OcrWithText") = "1"
        chkLogTime.Checked = oAppSetting.GetValue("LogTime") = "1"

        chkMerge.Checked = oAppSetting.GetValue("Merge") = "1"
		chkBookmarks.Checked = oAppSetting.GetValue("Bookmarks") = "1"
		chkResize.Checked = oAppSetting.GetValue("Resize") = "1"
		chkCreateInParentFolder.Checked = oAppSetting.GetValue("CreateInParentFolder") = "1"
		chkDeleteSourceFiles.Checked = oAppSetting.GetValue("DeleteSourceFiles") = "1"
		MergeChecked()

	End Sub

	Private Sub SetFolderTextbox(ByRef o As TextBox, sParentFolder As String, sFolderName As String)
		If o.Text <> "" Then
			Exit Sub
		End If

		Dim sFolderPath As String = IO.Path.Combine(sParentFolder, sFolderName)
		If IO.Directory.Exists(sFolderPath) = False Then
			Exit Sub
		End If

		o.Text = sFolderPath
	End Sub


	Private Sub btnProcess_Click(sender As System.Object, e As System.EventArgs) Handles btnProcess.Click

		sTesseractPath = txtTesseractPath.Text
		If sTesseractPath = "" OrElse IO.File.Exists(sTesseractPath) = False Then
			MsgBox("TesseractPath is missing")
			Exit Sub
		End If

		sFolderIn = txtFolderIn.Text
		If sFolderIn = "" OrElse IO.Directory.Exists(sFolderIn) = False Then
			MsgBox("In folder is missing")
			Exit Sub
		End If

		sFolderOut = txtFolderOut.Text
		If sFolderOut = "" OrElse IO.Directory.Exists(sFolderOut) = False Then
			MsgBox("Out folder is missing")
			Exit Sub
		End If

		sFolderDone = IO.Path.Combine(sFolderOut, "Done")
		sFolderFailed = IO.Path.Combine(sFolderOut, "Failed")

		sFolderProcess = IO.Path.Combine(sFolderOut, "Processing")
		sFolderProcessLocal = IO.Path.Combine(sFolderProcess, System.Environment.MachineName)
		If IO.Directory.Exists(sFolderProcessLocal) = False Then
			IO.Directory.CreateDirectory(sFolderProcessLocal)
		End If

		'Try to Delete files older than 5 min
		Dim oProcessFolders As String() = IO.Directory.GetDirectories(sFolderProcess)
		For Each sFolder In oProcessFolders
			DeleteOldProcessFiles(sFolder)
		Next

		'Get iFileCount
		Dim iFileCount As Integer = 0
		CountProcessFiles(sFolderIn, iFileCount)
		If iFileCount = 0 Then
            ProgressBar1.Value = 0
            lbCount.Text = ""
            Log("No Files to process")
            Exit Sub
		End If

		txtOutput.Text = ""

		'Setup Log File
		Dim sLogFileName As String = GetNowString() & ".csv"
		Dim sLogFilePath As String = IO.Path.Combine(sFolderOut, sLogFileName)
		oLogFile = New System.IO.StreamWriter(sLogFilePath, True)
		oLogFile.WriteLine("sep=" & vbTab)

		'Progresss bar
		iFileNumber = 0
        ProgressBar1.Maximum = iFileCount
        lbCount.Visible = True
        btnStop.Visible = True
        bStop = False

        'Process!!
        ProcessFiles(sFolderIn)

		oLogFile.Close()

		If bLogFileUsed = False Then
			'Log file is empty
			TryDelete(sLogFilePath)
		End If

        ProgressBar1.Value = 0
        lbCount.Visible = False
        lbCount.Text = ""
        btnStop.Visible = False

        Log("Done!")

	End Sub

	Private Function GetNowString() As String
		Return Now.Month.ToString() & "-" & _
		  Now.Day.ToString() & "-" & _
		  Now.Year.ToString() & "_" & _
		  Now.Hour.ToString() & "-" & _
		  Now.Minute.ToString() & "-" & _
		  Now.Second.ToString()
	End Function

	Private Sub DeleteOldProcessFiles(ByVal sInFolder As String)
		Dim oFiles As String() = IO.Directory.GetFiles(sInFolder)

		For i As Integer = 0 To oFiles.Length - 1
			Dim sFile As String = oFiles(i)

			Try
				If GetFileLastMinutes(sFile) > 5 Then
					TryDelete(sFile)
				End If

			Catch ex As Exception
				'Ignore it is being used
			End Try
		Next

		Dim oFolders As String() = IO.Directory.GetDirectories(sInFolder)
		For Each sFolder In oFolders
			DeleteOldProcessFiles(sFolder)
		Next

	End Sub

	Private Function CanProccessFile(sFile As String) As Boolean
		Dim sExt As String = GetExtFromFileName(sFile)
		If CheckOr(sExt.ToUpper(), "BMP,PNM,PNG,JFIF,JPEG,JPG,TIFF,TIF,GIF,PDF") = False Then
			Return False
		End If

		Dim sFileName As String = sFile.Replace(sFolderIn & "\", "")

		Dim oProcessFolders As String() = IO.Directory.GetDirectories(sFolderProcess)
		For Each sFolder In oProcessFolders
			Dim sProcessFile As String = IO.Path.Combine(sFolder, sFileName)
			If System.IO.File.Exists(sProcessFile) Then
				Return False
			End If
		Next

		Dim sFailedFile As String = IO.Path.Combine(sFolderFailed, sFileName)
		Dim sDoneFile As String = IO.Path.Combine(sFolderDone, GetBaseNameFromFileName(sFileName) & ".pdf")
		If System.IO.File.Exists(sFailedFile) OrElse System.IO.File.Exists(sDoneFile) Then
			Return False
		End If

		Return True
	End Function

    Private Sub CountProcessFiles(ByVal sInFolder As String, ByRef iFileCount As Integer)
        If CanProcessFolder(sInFolder) Then
            Dim oFiles As String() = IO.Directory.GetFiles(sInFolder)
            For i As Integer = 0 To oFiles.Length - 1
                Dim sFile As String = oFiles(i)
                If CanProccessFile(sFile) Then
                    iFileCount += 1
                End If
            Next
        End If

        Dim oFolders As String() = IO.Directory.GetDirectories(sInFolder)
        For Each sFolder In oFolders
            CountProcessFiles(sFolder, iFileCount)
        Next
    End Sub

    Private Function CanProcessFolder(ByVal sInSubFolder As String) As Boolean
		If chkMerge.Checked = False Then
			Return True
		End If

		Dim sSubFolderDone As String = GetTargetSubFolder(sFolderDone, sInSubFolder)
		Dim sMergeDonePath As String = GetMergeOutFilePath(sSubFolderDone)
		If IO.File.Exists(sMergeDonePath) Then
			'Merge file for the folder already exisits
			Return False
		End If

		Dim sSubFolderProcess As String = GetTargetSubFolder(sFolderProcessLocal, sInSubFolder)
		If IO.Directory.Exists(sSubFolderProcess) AndAlso IO.Directory.GetFiles(sSubFolderProcess).Length > 0 Then
			'There are files in the processing sub-folder
			Return False
		End If

		Return True
	End Function

	Private Sub ProcessFiles(ByVal sInSubFolder As String)

        If bStop Then
            Exit Sub
        End If

        If CanProcessFolder(sInSubFolder) Then

			Dim oFiles As String() = IO.Directory.GetFiles(sInSubFolder)
			Dim bProcessedFile As Boolean = False

			For i As Integer = 0 To oFiles.Length - 1
				Dim sFile As String = oFiles(i)
				If CanProccessFile(sFile) Then
					Dim sFileName As String = sFile.Replace(sFolderIn & "\", "")
					Dim sProcessFile As String = IO.Path.Combine(sFolderProcessLocal, sFileName)

                    Try
                        CreateFolderIfDoesNotExist(sProcessFile)
                        IO.File.Copy(sFile, sProcessFile)
                        ProcessFile(sProcessFile, sFileName)
                        bProcessedFile = True
                    Catch ex As Exception
                        Log(sProcessFile, "Could not process file. Skip to the next one. " & ex.Message)
                    End Try

                    'Show progress
                    iFileNumber += 1
                    ProgressBar1.Value = iFileNumber
                    lbCount.Text = iFileNumber.ToString()
                    lbCount.Refresh()
                    Application.DoEvents()

                    If bStop Then
                        Log(sInSubFolder, "User stopped.")
                        Exit Sub
                    End If
                End If
			Next

            If bProcessedFile AndAlso chkMerge.Checked Then
                Try
                    Dim dtStart As DateTime = DateTime.Now
                    Merge(sInSubFolder)
                    Dim iProcessSeconds As Integer = DateTime.Now.Subtract(dtStart).TotalSeconds
                    Log(sInSubFolder, "Merged in " & iProcessSeconds & " secs")
                Catch ex As Exception
                    Log(sInSubFolder, "Merge failed. " & ex.Message)
                End Try
            End If

        End If

		Dim oFolders As String() = IO.Directory.GetDirectories(sInSubFolder)
		For Each sFolder In oFolders
			ProcessFiles(sFolder)
		Next
	End Sub

	Private Function GetTargetSubFolder(ByVal sTargetFolder As String, ByVal sInSubFolder As String) As String
		Dim sInSubFolderName As String = ""
		If sInSubFolder <> sFolderIn Then
			sInSubFolderName = sInSubFolder.Replace(sFolderIn & "\", "")
		End If

		Dim sSubFolderDone As String = sTargetFolder
		If sInSubFolderName <> "" Then
			sSubFolderDone = IO.Path.Combine(sTargetFolder, sInSubFolderName)
		End If

		Return sSubFolderDone
	End Function

	Private Function GetMergeOutFilePath(ByVal sFolderPath As String) As String
		Dim oFolderInfo As New System.IO.DirectoryInfo(sFolderPath)

		If chkCreateInParentFolder.Checked Then
			Return oFolderInfo.Parent.FullName & "\" & oFolderInfo.Name & ".pdf"
		Else
			Return sFolderPath & "\" & oFolderInfo.Name & ".pdf"
		End If
	End Function

	Sub Merge(ByVal sInSubFolder As String)
		Dim sSubFolderDone As String = GetTargetSubFolder(sFolderDone, sInSubFolder)

		If IO.Directory.Exists(sSubFolderDone) = False Then
			Exit Sub
		End If

		Dim oOutFiles As String() = IO.Directory.GetFiles(sSubFolderDone)
		If oOutFiles.Length = 0 Then
			Exit Sub
		End If

		Dim sMergeOutFilePath As String = GetMergeOutFilePath(sSubFolderDone)

        Dim oDeleteFiles As New ArrayList()
        Dim oPdfDoc As New iTextSharp.text.Document()
		Dim oPdfWriter As PdfWriter = PdfWriter.GetInstance(oPdfDoc, New FileStream(sMergeOutFilePath, FileMode.Create))
		oPdfDoc.Open()

		System.Array.Sort(Of String)(oOutFiles)
		For i As Integer = 0 To oOutFiles.Length - 1
			Dim sFromFilePath As String = oOutFiles(i)
			Dim oFileInfo As New FileInfo(sFromFilePath)
			Dim sBookmarkTitle As String = GetBaseNameFromFileName(oFileInfo.Name)
            AddPdf(sFromFilePath, oPdfDoc, oPdfWriter, sBookmarkTitle)

            oDeleteFiles.Add(sFromFilePath)
        Next

		Try
			oPdfDoc.Close()
			oPdfWriter.Close()
		Catch ex As Exception
			Log(ex.Message)
		End Try

		If chkDeleteSourceFiles.Checked Then
            For Each sFromFilePath As String In oDeleteFiles
                If IO.File.Exists(sFromFilePath) Then
                    Try
                        IO.File.Delete(sFromFilePath)
                    Catch ex As Exception
                        Log(sFromFilePath, "Could not delete, " & ex.Message)
                    End Try
                End If
            Next
        End If
	End Sub

	Sub AddPdf(ByVal sInFilePath As String, ByRef oPdfDoc As iTextSharp.text.Document, ByRef oPdfWriter As PdfWriter, ByVal sBookmarkTitle As String)

		AddBookmark(oPdfDoc, sBookmarkTitle)

		Dim oDirectContent As iTextSharp.text.pdf.PdfContentByte = oPdfWriter.DirectContent
		Dim oPdfReader As iTextSharp.text.pdf.PdfReader = New iTextSharp.text.pdf.PdfReader(sInFilePath)
		Dim iNumberOfPages As Integer = oPdfReader.NumberOfPages
		Dim iPage As Integer = 0

		Do While (iPage < iNumberOfPages)
			iPage += 1

			Dim iRotation As Integer = oPdfReader.GetPageRotation(iPage)
			Dim oPdfImportedPage As iTextSharp.text.pdf.PdfImportedPage = oPdfWriter.GetImportedPage(oPdfReader, iPage)

			If chkResize.Checked Then
				If (oPdfImportedPage.Width <= oPdfImportedPage.Height) Then
					oPdfDoc.SetPageSize(iTextSharp.text.PageSize.LETTER)
				Else
					oPdfDoc.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate())
				End If

				oPdfDoc.NewPage()

				Dim iWidthFactor As Single = oPdfDoc.PageSize.Width / oPdfReader.GetPageSize(iPage).Width
				Dim iHeightFactor As Single = oPdfDoc.PageSize.Height / oPdfReader.GetPageSize(iPage).Height
				Dim iFactor As Single = Math.Min(iWidthFactor, iHeightFactor)

				Dim iOffsetX As Single = (oPdfDoc.PageSize.Width - (oPdfImportedPage.Width * iFactor)) / 2
				Dim iOffsetY As Single = (oPdfDoc.PageSize.Height - (oPdfImportedPage.Height * iFactor)) / 2

				oDirectContent.AddTemplate(oPdfImportedPage, iFactor, 0, 0, iFactor, iOffsetX, iOffsetY)

			Else
				oPdfDoc.SetPageSize(oPdfReader.GetPageSizeWithRotation(iPage))
				oPdfDoc.NewPage()

				If (iRotation = 90) Or (iRotation = 270) Then
					oDirectContent.AddTemplate(oPdfImportedPage, 0, -1.0F, 1.0F, 0, 0, oPdfReader.GetPageSizeWithRotation(iPage).Height)
				Else
					oDirectContent.AddTemplate(oPdfImportedPage, 1.0F, 0, 0, 1.0F, 0, 0)
				End If
			End If
		Loop

	End Sub

	Sub AddBookmark(ByRef oPdfDoc As iTextSharp.text.Document, ByVal sBookmarkTitle As String)
		If chkBookmarks.Checked = False Then
			Exit Sub
		End If

		Dim oChapter As New iTextSharp.text.Chapter("", 0)
		oChapter.NumberDepth = 0
		oChapter.BookmarkTitle = sBookmarkTitle
		oPdfDoc.Add(oChapter)
	End Sub

	Sub CreateFolderIfDoesNotExist(ByVal sFile As String)
		Dim oFileInfo As New FileInfo(sFile)
		Dim sFolder As String = oFileInfo.DirectoryName

		If IO.Directory.Exists(sFolder) = False Then
			IO.Directory.CreateDirectory(sFolder)
		End If
	End Sub

	Private Sub ProcessFile(ByVal sFilePath As String, sFileName As String)

		Dim dtStart As DateTime = DateTime.Now
		Dim bTiff As Boolean = False
		Dim sProcessFile As String = sFilePath
		Dim sExt As String = GetExtFromFileName(sFileName)
		If sExt = "pdf" Then
			If chkOcrWithText.Checked = False AndAlso PdfHasText(sFilePath) Then
				'PDF already has txt - do not process
				Log(sFilePath, "PDF already has text")

				Dim sDoneFile As String = IO.Path.Combine(sFolderDone, sFileName)
				CreateFolderIfDoesNotExist(sDoneFile)
				IO.File.Move(sFilePath, sDoneFile) 'Move to Done so that it will not be processed again
				Exit Sub
			Else
				bTiff = True
                sProcessFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString("N") & ".tiff")
                PdfToTiff(sFilePath, sProcessFile)
                If IO.File.Exists(sProcessFile) = False Then
                    Log(sFilePath, "TIFF file could not be generated")
                    sProcessFile = ""
                End If
            End If
		End If

        Dim sTempOutFile As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString("N"))
        Dim sTempOutFilePath As String = sTempOutFile & ".pdf"

        Dim sError As String = ""

        If sProcessFile <> "" Then
            sError = RunDosCommandAsynch(sTesseractPath, """" & sProcessFile & """ """ & sTempOutFile & """ pdf") '10 min timeout
        End If

        Dim bSuccess As Boolean = IO.File.Exists(sTempOutFilePath) AndAlso GetFileSize(sTempOutFilePath) > 10

        If bSuccess AndAlso bTiff AndAlso PdfHasText(sTempOutFilePath) = False AndAlso PdfHasText(sFilePath) Then
            'New PDF has no text but the old one did
            Log(sFilePath, "Could not extract text from images. " & sError)
            sTempOutFilePath = sFilePath
        End If

        If bSuccess Then
			sFileName = GetBaseNameFromFileName(sFileName) & ".pdf"
			Dim sDoneFile As String = IO.Path.Combine(sFolderDone, sFileName)
			CreateFolderIfDoesNotExist(sDoneFile)
			IO.File.Move(sTempOutFilePath, sDoneFile) 'Done

			Dim iProcessSeconds As Integer = DateTime.Now.Subtract(dtStart).TotalSeconds
			Log(sDoneFile, "Done in " & iProcessSeconds & " sec")

			TryDelete(sFilePath)
		Else
			Dim sFailedFile As String = IO.Path.Combine(sFolderFailed, sFileName)
			CreateFolderIfDoesNotExist(sFailedFile)
			IO.File.Move(sFilePath, sFailedFile)	'Failed

			Log(sFailedFile, sError)
			LogFile(sFailedFile, sError)
		End If

		'Delete temp files
		If bTiff Then TryDelete(sProcessFile) ' tiff file
		TryDelete(sTempOutFilePath)	'PDF
	End Sub

	Private Sub LogFile(ByVal sFile As String, ByVal sMsg As String)
		sMsg = Replace(sMsg, vbCrLf, " ")
		sMsg = Replace(sMsg, vbLf, " ")
		sMsg = Replace(sMsg, vbCr, " ")
		sMsg = Replace(sMsg, vbTab, " ")
		sMsg = Replace(sMsg, """", "")

		bLogFileUsed = True

		sFile = "=HYPERLINK(""" & sFile & """,""" & sFile & """)"

        If chkLogTime.Checked Then
            oLogFile.WriteLine(Now() & vbTab & sFile & vbTab & """" & sMsg & """")
        Else
            oLogFile.WriteLine(sFile & vbTab & """" & sMsg & """")
        End If

    End Sub

    Private Sub Log(sFile As String, Optional ByVal sMsg As String = "")

        If chkLogTime.Checked Then
            txtOutput.AppendText(Now() & vbTab)
        End If

        If sMsg = "" Then
            txtOutput.AppendText(sFile & vbCrLf)
        Else
            txtOutput.AppendText(sFile & vbTab & sMsg & vbCrLf)
        End If
    End Sub

    Private Sub TryDelete(sPath As String)
		Try
			If IO.File.Exists(sPath) Then
				IO.File.Delete(sPath)
			End If
		Catch ex As Exception
			'Ignore
		End Try
	End Sub

	Private Function GetFileSize(sPath As String) As Integer
		Dim oFileInfo As New FileInfo(sPath)
		Return oFileInfo.Length
	End Function

	Private Function GetFileLastMinutes(sPath As String) As Integer
		Dim oFileInfo As New FileInfo(sPath)
		Return Now().Subtract(oFileInfo.LastWriteTime).Minutes
	End Function

	Private Function CheckOr(ByVal sValue As String, ByVal sList As String) As Boolean
		Dim oList As String() = sList.Split(",")
		For i As Integer = 0 To oList.Length - 1
			If oList(i) = sValue Then
				Return True
			End If
		Next
		Return False
	End Function

	Private Function GetBaseNameFromFileName(ByVal s As String) As String
		If s = "" Then
			Return ""
		End If

		Dim iPos As Integer = s.LastIndexOf(".")
		If iPos = -1 Then
			Return s
		End If

		Return s.Substring(0, iPos)
	End Function

	Private Function GetExtFromFileName(ByVal s As String) As String
		If s = "" Then
			Return ""
		End If

		Dim iPos As Integer = s.LastIndexOf(".")
		If iPos = -1 Then
			Return ""
		End If

		Return s.Substring(iPos + 1)
	End Function

	Private Function GetTempFolderPath() As String
		Dim sAssPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
		Dim sBaseFolder As String = System.IO.Path.GetDirectoryName(sAssPath)
		Dim sFolder As String = IO.Path.Combine(sBaseFolder, "Temp")

		If IO.Directory.Exists(sFolder) = False Then
			IO.Directory.CreateDirectory(sFolder)
		End If

		Return sFolder
	End Function

    Function RunDosCommandAsynch(sExeFilePath As String, sArguments As String) As String

        Dim sError As String = ""
        Dim oProcess As Process = New Process()
        oProcess.StartInfo.UseShellExecute = False
        oProcess.StartInfo.RedirectStandardOutput = True
        oProcess.StartInfo.RedirectStandardError = True
        oProcess.StartInfo.FileName = sExeFilePath
        oProcess.StartInfo.Arguments = sArguments
        oProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        oProcess.StartInfo.CreateNoWindow = True

        oProcess.Start()
        oProcess.WaitForExit(1000 * iTimeOutSec)
        If oProcess.HasExited = False Then
            oProcess.Kill()
            sError = "Timeout after " + iTimeOutSec + " seconds."
        End If

        If String.IsNullOrEmpty(sError) Then
            sError = oProcess.StandardError.ReadToEnd()
        End If

        'sRet = oProcess.StandardOutput.ReadToEnd()

        If oProcess.ExitCode <> 0 AndAlso String.IsNullOrEmpty(sError) Then
            sError = "ExitCode: " + oProcess.ExitCode
        End If

        oProcess.Close()

        Return sError
    End Function

    Sub RunDosCommand(sExeFilePath As String, sArguments As String, ByRef sError As String)

        Dim sRet As String = ""
        Dim oProcess As Process = New Process()
        oProcess.StartInfo.UseShellExecute = True
        oProcess.StartInfo.FileName = sExeFilePath
        oProcess.StartInfo.Arguments = sArguments
        oProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        oProcess.StartInfo.CreateNoWindow = True

        oProcess.Start()
        oProcess.WaitForExit(1000 * iTimeOutSec)
        If oProcess.HasExited = False Then
            oProcess.Kill()
            sError = "Timeout after " + iTimeOutSec + " seconds."
        End If

        If oProcess.ExitCode <> 0 AndAlso String.IsNullOrEmpty(sError) Then
            sError = "ExitCode: " + oProcess.ExitCode
        End If

        oProcess.Close()
    End Sub

    Function PdfHasText(ByVal sInPdf As String) As Boolean
		Dim doc As New PdfReader(sInPdf)
		'Dim sb As New StringBuilder()
		Dim bRet As Boolean = False

		For iPage = 1 To doc.NumberOfPages
			Dim pg As PdfDictionary = doc.GetPageN(iPage)
			Dim ir As Object = pg.Get(PdfName.CONTENTS)
			Dim value As PdfObject = doc.GetPdfObject(ir.Number)
			If value.IsStream() Then
				Dim stream As PRStream = value
				Dim streamBytes As Byte() = PdfReader.GetStreamBytes(stream)
				Dim tokenizer = New PRTokeniser(New RandomAccessFileOrArray(streamBytes))

				Try
					While tokenizer.NextToken()
						If tokenizer.TokenType = PRTokeniser.TK_STRING Then
							Dim str As String = tokenizer.StringValue
							If str <> "" Then
								bRet = True
								Exit While
							End If
							'sb.Append(str)
						End If
					End While
				Catch ex As Exception
					'Ignore
				Finally
					tokenizer.Close()
				End Try

				If bRet Then
					Exit For
				End If
			End If
		Next
		doc.Close()

		Return bRet
		'Return sb.ToString()
	End Function

	Sub PdfToTiff(ByVal sInPdf As String, ByVal sOutTiff As String)
		Dim sError As String = ""
        'https://ghostscript.com/doc/9.21/Devices.htm
        RunDosCommand(sGhostscriptPath, "-dNOPAUSE -q -r300 -sDEVICE=tiff24nc -dBATCH -sOutputFile=""" & sOutTiff & """ """ & sInPdf & """ -c quit", sError)
    End Sub

	'Sub PdfToTiff_old(ByVal sInPdf As String, ByVal sOutTiff As String)
	'	Dim oImages As ArrayList = GetPdfImages(sInPdf)
	'	If oImages.Count = 0 Then
	'		'No Images found
	'		Exit Sub
	'	End If

	'	Dim bitmap As Bitmap = Image.FromFile(oImages(0))
	'	Dim byteStream As MemoryStream = New MemoryStream()
	'	bitmap.Save(byteStream, System.Drawing.Imaging.ImageFormat.Tiff)

	'	Dim tiff As Image = Image.FromStream(byteStream)

	'	Dim oParams As EncoderParameters = New EncoderParameters(2)
	'	oParams.Param(0) = New EncoderParameter(Imaging.Encoder.Compression, EncoderValue.CompressionCCITT4)
	'	oParams.Param(1) = New EncoderParameter(Imaging.Encoder.SaveFlag, EncoderValue.MultiFrame)
	'	tiff.Save(sOutTiff, GetEncoderInfo("image/tiff"), oParams)

	'	bitmap.Dispose()

	'	'Next Page
	'	For i = 1 To oImages.Count - 1
	'		Dim bitmap2 As Bitmap = Image.FromFile(oImages(i))
	'		oParams.Param(1) = New EncoderParameter(Imaging.Encoder.SaveFlag, EncoderValue.FrameDimensionPage)
	'		tiff.SaveAdd(bitmap2, oParams)
	'		bitmap2.Dispose()
	'	Next

	'	'Flush 
	'	Dim oFlushParams As EncoderParameters = New EncoderParameters(1)
	'	oFlushParams.Param(0) = New EncoderParameter(Imaging.Encoder.SaveFlag, EncoderValue.Flush)
	'	tiff.SaveAdd(oFlushParams)
	'End Sub

	'Function GetPdfImages(sInPdf As String) As ArrayList
	'	Dim doc As New PdfReader(sInPdf)
	'	Dim images As New ArrayList()

	'	Dim sTempOutFile As String = System.IO.Path.Combine(sFolderProcessLocal, Guid.NewGuid().ToString("N"))
	'	For iPage = 1 To doc.NumberOfPages

	'		Dim sTempOutFilePath As String = sTempOutFile & "" & iPage & ".bmp"
	'		Dim sError As String = ""
	'		RunDosCommand(sGhostscriptPath, "-dNOPAUSE -q -r300 -sDEVICE=bmp16m -dBATCH -dFirstPage=" & iPage & " -dLastPage=" & iPage & " -sOutputFile=""" & sTempOutFilePath & """ """ & sInPdf & """ -c quit", sError, 60)
	'		If IO.File.Exists(sTempOutFilePath) AndAlso GetFileSize(sTempOutFilePath) > 10 Then
	'			images.Add(sTempOutFilePath)
	'		End If
	'	Next
	'	doc.Close()

	'	Return images
	'End Function

	'Private Function GetEncoderInfo(mimeType As String) As System.Drawing.Imaging.ImageCodecInfo
	'	Dim encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()
	'	For i As Integer = 0 To encoders.Length - 1
	'		If encoders(i).MimeType = mimeType Then
	'			Return encoders(i)
	'		End If
	'	Next
	'	Return Nothing
	'End Function


	'Sub WindowsSearch()
	'	'Windows Search searvice need to be running

	'	Dim cn As New Data.OleDb.OleDbConnection("Provider=Search.CollatorDSO;Extended Properties=""Application=Windows""")
	'	Dim sql As String = "SELECT System.ItemName, System.ItemType FROM SystemIndex WHERE scope ='file:C:/' AND System.ItemName LIKE '%Test%' "
	'	'System.DateModified
	'	'System.KindText
	'	'System.Filename
	'	'System.FileExtension = '.docx
	'	'System.Size
	'	'System.ItemUrl
	'	'DIRECTORY = 'C:\Users\...\Documents'

	'	'PDF iFilter 64 11.0.01
	'	'https://blog.techhit.com/55696-indexing-and-searching-pdf-content-using-windows-search
	'	'https://supportdownloads.adobe.com/detail.jsp?ftpID=5542

	'	cn.Open()
	'	Dim cm As New Data.OleDb.OleDbCommand(sql, cn)

	'	'https://support.microsoft.com/en-us/help/4022168/windows-7-sp1-windows-server-2008-r2-sp1-update-kb4022168
	'	Dim dr As Data.OleDb.OleDbDataReader = cm.ExecuteReader()

	'	Dim s As String = ""

	'	While dr.Read()
	'		s += dr.GetValue(0) & vbCrLf
	'	End While

	'	cn.Close()

	'	MsgBox(s)

	'	'Free Microsoft Search Server 2010 Express
	'	'https://www.microsoft.com/en-us/download/details.aspx?id=18914

	'	'Windows Search on windows server 2012
	'	'https://social.technet.microsoft.com/Forums/en-US/3e705b51-7218-407e-b5c8-429e20557ed4/how-to-configure-indexing-service-in-windows-server-2012?forum=winserver8gen

	'	'Control - Panel - Indexing Options
	'	'rundll32.exe shell32.dll,Control_RunDLL srchadmin.dll
	'End Sub

	'Dim b As Boolean = PdfHasText("C:\Temp\MCPTranscript.pdf")
	'PdfToTiff("C:\Temp\Current_Month_Check_Log_Files_2594.pdf", "C:\Temp\Current_Month_Check_Log_Files_2594.tiff")

	'tesseract
	'https://github.com/tesseract-ocr/tesseract/wiki/FAQ#how-do-i-produce-searchable-pdf-output
	'Download: https://github.com/UB-Mannheim/tesseract/wiki 
	'C:\Program Files (x86)\Tesseract-OCR
	'tesseract phototest.tif phototest pdf
	'tesseract C:\Temp\out.tiff C:\Temp\out.pdf pdf
	'Files Supported: BMP, PNM, PNG, JFIF, JPEG, and TIFF. GIF
	'PDF output was added in version 3.03.


	Private Sub btnIn_Click(sender As System.Object, e As System.EventArgs) Handles btnIn.Click
		fldFrom.SelectedPath = txtFolderIn.Text
		fldFrom.ShowDialog()
		txtFolderIn.Text = fldFrom.SelectedPath
	End Sub


	Private Sub btnOut_Click(sender As System.Object, e As System.EventArgs) Handles btnOut.Click
		fldFrom.SelectedPath = txtFolderOut.Text
		fldFrom.ShowDialog()
		txtFolderOut.Text = fldFrom.SelectedPath
	End Sub

	Private Sub btnExe_Click(sender As System.Object, e As System.EventArgs) Handles btnTesseract.Click
		Dim oFileInfo As New FileInfo(txtTesseractPath.Text)
		dOpenFile.InitialDirectory = oFileInfo.DirectoryName
		dOpenFile.FileName = oFileInfo.Name
		dOpenFile.ShowDialog()

		If IO.File.Exists(dOpenFile.FileName) Then
			txtTesseractPath.Text = dOpenFile.FileName
		End If
	End Sub

	Private Sub btnGhostscript_Click(sender As Object, e As EventArgs) Handles btnGhostscript.Click
		Dim oFileInfo As New FileInfo(txtGhostscriptPath.Text)
		dOpenFile.InitialDirectory = oFileInfo.DirectoryName
		dOpenFile.FileName = oFileInfo.Name
		dOpenFile.ShowDialog()

		If IO.File.Exists(dOpenFile.FileName) Then
			txtGhostscriptPath.Text = dOpenFile.FileName
		End If
	End Sub

	Private Sub lbTesseract_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbTesseract.LinkClicked
		Process.Start("https://github.com/UB-Mannheim/tesseract/wiki")
	End Sub

	Private Sub lbGhostscript_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbGhostscript.LinkClicked
		Process.Start("https://ghostscript.com/download/gsdnld.html")
	End Sub

	Private Sub btnMerge_Click(sender As Object, e As EventArgs)
		MsgBox("to be developed...")
	End Sub

	Private Sub chkMerge_CheckedChanged(sender As Object, e As EventArgs) Handles chkMerge.CheckedChanged
		MergeChecked()
	End Sub

	Private Sub MergeChecked()
		gbMerge.Visible = chkMerge.Checked
	End Sub

    Private Sub btnStop_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles btnStop.LinkClicked
        bStop = True
    End Sub
End Class
