; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "ImageManipulator"
!define PRODUCT_VERSION "1.0.0"
!define PRODUCT_PUBLISHER "J-Teks"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\ImageManipulator.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
; MUI 1.67 compatible ------
!include "MUI.nsh"
!include LogicLib.nsh

RequestExecutionLevel user

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
BrandingText " "
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "license.txt"
; Directory page
;!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\ImageManipulator.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "Korean"
; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "ImageManipulatorSetup_${__DATE__}.exe"
InstallDir "$PROGRAMFILES64\JTeks\ImageManipulator\"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show



Section "MainSection" SEC01

  Call CheckAndInstallDotNet
  
  
  SetOverwrite ifnewer

  CreateDirectory "$SMPROGRAMS\ImageManipulator"
  CreateDirectory "$SMPROGRAMS\ImageManipulator\ko-KR"
 
  SetOutPath "$INSTDIR\ko-KR"
  File "..\Runtime\x64\Release\ko-KR\LibraryGlobalization.resources.dll"
  
  SetOutPath "$INSTDIR"
  
  File "..\Runtime\x64\Release\apidsp_windows.dll"
  File "..\Runtime\x64\Release\apidsp_windows_x64.dll"
  File "..\Runtime\x64\Release\BitMiracle.LibTiff.NET.dll"
  File "..\Runtime\x64\Release\ClearCanvas.Common.dll"
  File "..\Runtime\x64\Release\ClearCanvas.Dicom.dll"
  File "..\Runtime\x64\Release\cvextern.dll"
  File "..\Runtime\x64\Release\Emgu.CV.UI.dll"
  File "..\Runtime\x64\Release\Emgu.CV.World.dll"
  File "..\Runtime\x64\Release\ExifLib.dll"
  File "..\Runtime\x64\Release\hasp_net_windows.dll"
  File "..\Runtime\x64\Release\hasp_windows_98985.dll"
  File "..\Runtime\x64\Release\hasp_windows_x64_98985.dll"
  File "..\Runtime\x64\Release\ImageManipulator.exe"
  File "..\Runtime\x64\Release\ImageManipulator.exe.config"
  File "..\Runtime\x64\Release\ImageManipulator.pdb"
  File "..\Runtime\x64\Release\JTeksControls.dll"
  File "..\Runtime\x64\Release\JTeksControls.pdb"
  
  File "..\Runtime\x64\Release\JTeksEmguCV.dll"
  File "..\Runtime\x64\Release\JTeksEmguCV.pdb"
  File "..\Runtime\x64\Release\JTeksLib.dll"
  File "..\Runtime\x64\Release\JTeksLib.pdb"

  File "..\Runtime\x64\Release\JTeksSplineGraph.dll"
  File "..\Runtime\x64\Release\JTeksSplineGraph.pdb"
  File "..\Runtime\x64\Release\LibraryGlobalization.dll"
  File "..\Runtime\x64\Release\LibraryGlobalization.pdb"
  
  CreateShortCut "$SMPROGRAMS\ImageManipulator\ImageManipulator.lnk" "$INSTDIR\ImageManipulator.exe"
  CreateShortCut "$DESKTOP\ImageManipulator.lnk" "$INSTDIR\ImageManipulator.exe"
SectionEnd

Section -AdditionalIcons
  CreateShortCut "$SMPROGRAMS\ImageManipulator\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section Post

  #Call CheckAndInstallDotNet
  #ExecWait '"$INSTDIR\DotNetInstallerSetup.exe" /passive /norestart /a' $0
  #ExecShell "runas" "$INSTDIR\DotNetInstallerSetup.exe" $0
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\ImageManipulator.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\ImageManipulator.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"

SectionEnd
      
Function CheckAndInstallDotNet
    ; Magic numbers from http://msdn.microsoft.com/en-us/library/ee942965.aspx
    ClearErrors
    ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" "Release"

    IfErrors NotDetected

    ${If} $0 >= 378389
        DetailPrint "Microsoft .NET Framework 4.5.2 is installed ($0)"
    ${Else}
    NotDetected:
     MessageBox MB_ICONINFORMATION|MB_OK "Microsoft .NET Framework 4.5.2가 존재하지 않습니다. 설치 후 다시 실행해 주시기 바랍니다."
     Abort
        #DetailPrint "Installing Microsoft .NET Framework 4.5.2"
        #ExecShell "runas" "$INSTDIR\DotNetInstallerSetup.exe.exe" $0
        #SetDetailsPrint listonly

        #ExecWait '"$INSTDIR\dotNetFx452-x86-x64-AllOS-ENU.exe" /passive /norestart /a' $0
        #ExecShell "runas" "$INSTDIR\dotNetFx452-x86-x64-AllOS-ENU.exe" $0
        #ExecWait '"INSTDIR\DotNetInstallerSetup.exe"' $0


        #${If} $0 == 3010
        #${OrIf} $0 == 1641
         #   DetailPrint "Microsoft .NET Framework 4.5.2 installer requested reboot"
         #   SetRebootFlag true
        #${EndIf}
        #SetDetailsPrint lastused
        #DetailPrint "Microsoft .NET Framework 4.5.2 installer returned $0"
    ${EndIf}

FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name)는(은) 완전히 제거되었습니다."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "$(^Name)을(를) 제거하시겠습니까?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\ko-KR\*.*"
  Delete "$INSTDIR\*.*"
  
  RMDir "$INSTDIR\ko-KR"
  RMDir "$INSTDIR"
  #RMDir "$PROGRAMFILES64\J-Teks"
  
  Delete "$LOCALAPPDATA\ImageManipulator\*.*"
  RMDir "$LOCALAPPDATA\ImageManipulator"
  
  Delete "$DESKTOP\ImageManipulator.exe"
  Delete "$PROFILE\Desktop\ImageManipulator.lnk"
  
  RMDir "$PROGRAMFILES64\JTeks"
  
  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd