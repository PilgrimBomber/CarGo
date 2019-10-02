; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "Car|Go"
!define PRODUCT_VERSION "1.0"
!define PRODUCT_PUBLISHER "Blue Screen Games"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\CarGo.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "Icon.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\CarGo.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "CarGoInstaller.exe"
InstallDir "$PROGRAMFILES\Car|Go"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Section "Hauptgruppe" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite try
  File "bin\x64\Release\CarGo.exe"
  File "bin\x64\Release\CarGo.pdb"
  SetOutPath "$INSTDIR\Content"
  File "bin\x64\Release\Content\Cargo_test.xnb"
  SetOutPath "$INSTDIR\Content\fonts"
  File "bin\x64\Release\Content\fonts\Arial.xnb"
  SetOutPath "$INSTDIR\Content\sounds"
  File "bin\x64\Release\Content\sounds\Car_Accelerate.xnb"
  File "bin\x64\Release\Content\sounds\Car_Background.xnb"
  File "bin\x64\Release\Content\sounds\Car_Boost.xnb"
  File "bin\x64\Release\Content\sounds\Car_Horn.xnb"
  File "bin\x64\Release\Content\sounds\Car_Horn2.xnb"
  File "bin\x64\Release\Content\sounds\Car_Horn3.xnb"
  File "bin\x64\Release\Content\sounds\Enemy_Growl.xnb"
  File "bin\x64\Release\Content\sounds\Enemy_Monster_Hit.xnb"
  File "bin\x64\Release\Content\sounds\Flamethrower.xnb"
  File "bin\x64\Release\Content\sounds\Lone_Wolf.xnb"
  File "bin\x64\Release\Content\sounds\RocketLauncher_EXPLOSION!!!.xnb"
  File "bin\x64\Release\Content\sounds\RocketLauncher_Launch.xnb"
  File "bin\x64\Release\Content\sounds\Shockwave.xnb"
  File "bin\x64\Release\Content\sounds\Trap_Launch.xnb"
  SetOutPath "$INSTDIR\Content\textures"
  File "bin\x64\Release\Content\textures\Background_Sand.xnb"
  File "bin\x64\Release\Content\textures\Background_Street_Bottom.xnb"
  File "bin\x64\Release\Content\textures\Background_Street_Middle.xnb"
  File "bin\x64\Release\Content\textures\Background_Street_Top.xnb"
  File "bin\x64\Release\Content\textures\Cactus.xnb"
  File "bin\x64\Release\Content\textures\CactusCompletlyRip.xnb"
  File "bin\x64\Release\Content\textures\CactusRip.xnb"
  File "bin\x64\Release\Content\textures\Cargo.xnb"
  File "bin\x64\Release\Content\textures\Car_BigSize.xnb"
  File "bin\x64\Release\Content\textures\Car_MediumSize.xnb"
  File "bin\x64\Release\Content\textures\Car_SmallSize.xnb"
  File "bin\x64\Release\Content\textures\CreditScreen.xnb"
  File "bin\x64\Release\Content\textures\EnemyFast.xnb"
  File "bin\x64\Release\Content\textures\Enemy_Dummy.xnb"
  File "bin\x64\Release\Content\textures\Enemy_Zombie.xnb"
  File "bin\x64\Release\Content\textures\Enemy_Zombie_Fast.xnb"
  File "bin\x64\Release\Content\textures\Enemy_Zombie_Fast_Animation.xnb"
  File "bin\x64\Release\Content\textures\Enemy_Zombie_Slow.xnb"
  File "bin\x64\Release\Content\textures\Enemy_Zombie_Slow_Animation.xnb"
  File "bin\x64\Release\Content\textures\Explosion.xnb"
  File "bin\x64\Release\Content\textures\Explosion_Animation.xnb"
  File "bin\x64\Release\Content\textures\Flamethrower_Animation.xnb"
  File "bin\x64\Release\Content\textures\MainMenuCarrier_0.xnb"
  File "bin\x64\Release\Content\textures\MainMenuCarrier_03.xnb"
  File "bin\x64\Release\Content\textures\Menu_Background.xnb"
  File "bin\x64\Release\Content\textures\Menu_Controls.xnb"
  File "bin\x64\Release\Content\textures\Menu_Defeatscreen.xnb"
  File "bin\x64\Release\Content\textures\Menu_Selection_BoxBox.xnb"
  File "bin\x64\Release\Content\textures\Menu_Select_Flamethrower.xnb"
  File "bin\x64\Release\Content\textures\Menu_Select_Shockwave.xnb"
  File "bin\x64\Release\Content\textures\Menu_Victoryscreen.xnb"
  File "bin\x64\Release\Content\textures\Mod_Front_Big_Bumper.xnb"
  File "bin\x64\Release\Content\textures\Mod_Front_Big_Spikes.xnb"
  File "bin\x64\Release\Content\textures\Mod_Front_Bumper.xnb"
  File "bin\x64\Release\Content\textures\Mod_Front_Small_Bumper.xnb"
  File "bin\x64\Release\Content\textures\Mod_Front_Small_Spikes.xnb"
  File "bin\x64\Release\Content\textures\Mod_Front_Spikes.xnb"
  File "bin\x64\Release\Content\textures\Rock.xnb"
  File "bin\x64\Release\Content\textures\Rocket.xnb"
  File "bin\x64\Release\Content\textures\Shockwave.xnb"
  File "bin\x64\Release\Content\textures\Shockwave_Animation.xnb"
  File "bin\x64\Release\Content\textures\Skull.xnb"
  File "bin\x64\Release\Content\textures\SkullRip.xnb"
  File "bin\x64\Release\Content\textures\Splashscreen_0.xnb"
  File "bin\x64\Release\Content\textures\TurtleMine.xnb"
  SetOutPath "$INSTDIR"
  File "bin\x64\Release\MonoGame.Framework.dll"
  File "bin\x64\Release\MonoGame.Framework.xml"
  File "bin\x64\Release\SharpDX.Direct2D1.dll"
  File "bin\x64\Release\SharpDX.Direct2D1.xml"
  File "bin\x64\Release\SharpDX.Direct3D11.dll"
  File "bin\x64\Release\SharpDX.Direct3D11.xml"
  File "bin\x64\Release\SharpDX.dll"
  File "bin\x64\Release\SharpDX.DXGI.dll"
  File "bin\x64\Release\SharpDX.DXGI.xml"
  File "bin\x64\Release\SharpDX.MediaFoundation.dll"
  File "bin\x64\Release\SharpDX.MediaFoundation.xml"
  File "bin\x64\Release\SharpDX.XAudio2.dll"
  File "bin\x64\Release\SharpDX.XAudio2.xml"
  File "bin\x64\Release\SharpDX.XInput.dll"
  File "bin\x64\Release\SharpDX.XInput.xml"
  File "bin\x64\Release\SharpDX.xml"
SectionEnd

Section -AdditionalIcons
  CreateDirectory "$SMPROGRAMS\Car|Go"
  CreateShortCut "$SMPROGRAMS\Car|Go\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\CarGo.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\CarGo.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) wurde erfolgreich deinstalliert."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "M�chten Sie $(^Name) und alle seinen Komponenten deinstallieren?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\SharpDX.xml"
  Delete "$INSTDIR\SharpDX.XInput.xml"
  Delete "$INSTDIR\SharpDX.XInput.dll"
  Delete "$INSTDIR\SharpDX.XAudio2.xml"
  Delete "$INSTDIR\SharpDX.XAudio2.dll"
  Delete "$INSTDIR\SharpDX.MediaFoundation.xml"
  Delete "$INSTDIR\SharpDX.MediaFoundation.dll"
  Delete "$INSTDIR\SharpDX.DXGI.xml"
  Delete "$INSTDIR\SharpDX.DXGI.dll"
  Delete "$INSTDIR\SharpDX.dll"
  Delete "$INSTDIR\SharpDX.Direct3D11.xml"
  Delete "$INSTDIR\SharpDX.Direct3D11.dll"
  Delete "$INSTDIR\SharpDX.Direct2D1.xml"
  Delete "$INSTDIR\SharpDX.Direct2D1.dll"
  Delete "$INSTDIR\MonoGame.Framework.xml"
  Delete "$INSTDIR\MonoGame.Framework.dll"
  Delete "$INSTDIR\Content\textures\TurtleMine.xnb"
  Delete "$INSTDIR\Content\textures\Splashscreen_0.xnb"
  Delete "$INSTDIR\Content\textures\SkullRip.xnb"
  Delete "$INSTDIR\Content\textures\Skull.xnb"
  Delete "$INSTDIR\Content\textures\Shockwave_Animation.xnb"
  Delete "$INSTDIR\Content\textures\Shockwave.xnb"
  Delete "$INSTDIR\Content\textures\Rocket.xnb"
  Delete "$INSTDIR\Content\textures\Rock.xnb"
  Delete "$INSTDIR\Content\textures\Mod_Front_Spikes.xnb"
  Delete "$INSTDIR\Content\textures\Mod_Front_Small_Spikes.xnb"
  Delete "$INSTDIR\Content\textures\Mod_Front_Small_Bumper.xnb"
  Delete "$INSTDIR\Content\textures\Mod_Front_Bumper.xnb"
  Delete "$INSTDIR\Content\textures\Mod_Front_Big_Spikes.xnb"
  Delete "$INSTDIR\Content\textures\Mod_Front_Big_Bumper.xnb"
  Delete "$INSTDIR\Content\textures\Menu_Victoryscreen.xnb"
  Delete "$INSTDIR\Content\textures\Menu_Select_Shockwave.xnb"
  Delete "$INSTDIR\Content\textures\Menu_Select_Flamethrower.xnb"
  Delete "$INSTDIR\Content\textures\Menu_Selection_BoxBox.xnb"
  Delete "$INSTDIR\Content\textures\Menu_Defeatscreen.xnb"
  Delete "$INSTDIR\Content\textures\Menu_Controls.xnb"
  Delete "$INSTDIR\Content\textures\Menu_Background.xnb"
  Delete "$INSTDIR\Content\textures\MainMenuCarrier_03.xnb"
  Delete "$INSTDIR\Content\textures\MainMenuCarrier_0.xnb"
  Delete "$INSTDIR\Content\textures\Flamethrower_Animation.xnb"
  Delete "$INSTDIR\Content\textures\Explosion_Animation.xnb"
  Delete "$INSTDIR\Content\textures\Explosion.xnb"
  Delete "$INSTDIR\Content\textures\Enemy_Zombie_Slow_Animation.xnb"
  Delete "$INSTDIR\Content\textures\Enemy_Zombie_Slow.xnb"
  Delete "$INSTDIR\Content\textures\Enemy_Zombie_Fast_Animation.xnb"
  Delete "$INSTDIR\Content\textures\Enemy_Zombie_Fast.xnb"
  Delete "$INSTDIR\Content\textures\Enemy_Zombie.xnb"
  Delete "$INSTDIR\Content\textures\Enemy_Dummy.xnb"
  Delete "$INSTDIR\Content\textures\EnemyFast.xnb"
  Delete "$INSTDIR\Content\textures\CreditScreen.xnb"
  Delete "$INSTDIR\Content\textures\Car_SmallSize.xnb"
  Delete "$INSTDIR\Content\textures\Car_MediumSize.xnb"
  Delete "$INSTDIR\Content\textures\Car_BigSize.xnb"
  Delete "$INSTDIR\Content\textures\Cargo.xnb"
  Delete "$INSTDIR\Content\textures\CactusRip.xnb"
  Delete "$INSTDIR\Content\textures\CactusCompletlyRip.xnb"
  Delete "$INSTDIR\Content\textures\Cactus.xnb"
  Delete "$INSTDIR\Content\textures\Background_Street_Top.xnb"
  Delete "$INSTDIR\Content\textures\Background_Street_Middle.xnb"
  Delete "$INSTDIR\Content\textures\Background_Street_Bottom.xnb"
  Delete "$INSTDIR\Content\textures\Background_Sand.xnb"
  Delete "$INSTDIR\Content\sounds\Trap_Launch.xnb"
  Delete "$INSTDIR\Content\sounds\Shockwave.xnb"
  Delete "$INSTDIR\Content\sounds\RocketLauncher_Launch.xnb"
  Delete "$INSTDIR\Content\sounds\RocketLauncher_EXPLOSION!!!.xnb"
  Delete "$INSTDIR\Content\sounds\Lone_Wolf.xnb"
  Delete "$INSTDIR\Content\sounds\Flamethrower.xnb"
  Delete "$INSTDIR\Content\sounds\Enemy_Monster_Hit.xnb"
  Delete "$INSTDIR\Content\sounds\Enemy_Growl.xnb"
  Delete "$INSTDIR\Content\sounds\Car_Horn3.xnb"
  Delete "$INSTDIR\Content\sounds\Car_Horn2.xnb"
  Delete "$INSTDIR\Content\sounds\Car_Horn.xnb"
  Delete "$INSTDIR\Content\sounds\Car_Boost.xnb"
  Delete "$INSTDIR\Content\sounds\Car_Background.xnb"
  Delete "$INSTDIR\Content\sounds\Car_Accelerate.xnb"
  Delete "$INSTDIR\Content\fonts\Arial.xnb"
  Delete "$INSTDIR\Content\Cargo_test.xnb"
  Delete "$INSTDIR\CarGo.pdb"
  Delete "$INSTDIR\CarGo.exe"

  Delete "$SMPROGRAMS\Car|Go\Uninstall.lnk"

  RMDir "$SMPROGRAMS\Car|Go"
  RMDir "$INSTDIR\Content\textures"
  RMDir "$INSTDIR\Content\sounds"
  RMDir "$INSTDIR\Content\fonts"
  RMDir "$INSTDIR\Content"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd