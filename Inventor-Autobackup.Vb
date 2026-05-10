' Sayfanın en başına bunu ekliyoruz.
Public Declare PtrSafe Function SetTimer Lib "user32" (ByVal hwnd As LongPtr, ByVal nIDEvent As LongPtr, ByVal uElapse As Long, ByVal lpTimerFunc As LongPtr) As LongPtr
Public Declare PtrSafe Function KillTimer Lib "user32" (ByVal hwnd As LongPtr, ByVal nIDEvent As LongPtr, ByVal uElapse As Long, ByVal lpTimerFunc As LongPtr) As Long

Public TimerID As LongPtr
Public Const BackupIntervalMs As Long = 300000 ' 5 Dakika

' Timer Callback
Public Sub TimerProc(ByVal hwnd As LongPtr, ByVal uMsg As Long, ByVal idEvent As LongPtr, ByVal dwTime As Long)
    On Error Resume Next ' Timer içinde hata olursa Inventor'ı çökertmemesi için bunu ekledik.
    Call BackupAllOpenDocs
End Sub 

Public Sub BackupAllOpenDocs()
    ThisApplication.StatusBarText = "Otomatik yedekleme yapılıyor, lütfen bekleyin..."

    Dim doc As Document
    Dim backupFolder As String
    Dim fileName As String
    
    ' DINAMIK YOL: user/Belgelerim yolunda bir klasör açar 
    backupFolder = Environ("USERPROFILE") & "\Documents\InventorBackups\"
    
    ' Klasör yoksa "backupFolder" adında bir klasör oluşturduk.
    If Dir(backupFolder, vbDirectory) = "" Then
        Dim fso As Object
        Set fso = CreateObject("Scripting.FileSystemObject")
        fso.CreateFolder backupFolder
    End If

   For Each doc In ThisApplication.Documents
        ' Dosya daha önce kaydedilmiş veya değişiklik yapılmış mı kontrolü yaptık.
        If doc.FullFileName <> "" And doc.Dirty = True Then
            
            ' Sadece belirli uzantılara/tiplere sahipse işlem yap, bunlar inventor part assembly ve drawingler.
            If doc.DocumentType = kPartDocumentObject Or _
               doc.DocumentType = kAssemblyDocumentObject Or _
               doc.DocumentType = kDrawingDocumentObject Then
               
                On Error Resume Next
                
                ' Dosya uzantısını dinamik olarak al (.ipt, .iam vb.)
                Dim ext As String
                ext = Mid(doc.FullFileName, InStrRev(doc.FullFileName, ".") + 1)
                
                ' Dosya ismini temizle (DisplayName bazen uzantı içerebilir)
                Dim cleanName As String
                cleanName = doc.DisplayName
                If InStr(cleanName, ".") > 0 Then cleanName = Left(cleanName, InStrRev(cleanName, ".") - 1)
                
                ' Backup dosya yolunu kendi uzantısı ile oluşturduk.
                fileName = backupFolder & cleanName & "_" & Format(Now, "yyyyMMdd_HHmmss") & "." & ext
                
                ' Yedeklemeyi yap
                doc.SaveAs backupFileName:=fileName, SaveCopyAs:=True
            End If
        End If
    Next
    ThisApplication.StatusBarText = ""
End Sub

Public Sub StartAutoBackup()
    If TimerID = 0 Then
        TimerID = SetTimer(0, 0, BackupIntervalMs, AddressOf TimerProc)
        MsgBox "Otomatik yedekleme sistemi aktif edildi.", vbInformation, "Inventor AutoBackup"
    Else
        MsgBox "Sistem zaten çalışıyor.", vbExclamation
    End If
End Sub

Public Sub StopAutoBackup()
    If TimerID <> 0 Then
        KillTimer 0, TimerID
        TimerID = 0
        MsgBox "Yedekleme durduruldu.", vbInformation
    End If
End Sub