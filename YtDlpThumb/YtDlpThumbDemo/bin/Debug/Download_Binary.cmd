@echo off
setlocal enabledelayedexpansion

:: Cartella principale dello script, rimuove eventuale backslash finale
set DEST=%~dp0
if "%DEST:~-1%"=="\" set DEST=%DEST:~0,-1%


:MENU
cls
echo ===============================
echo   Downloader yt-dlp + FFmpeg
echo ===============================
echo.
echo  [1] Scarica yt-dlp (Stable)
echo  [2] Scarica yt-dlp (Nightly)
echo  [3] Scarica FFmpeg + FFprobe
echo  [0] Esci
echo.
set /p scelta=Seleziona un'opzione: 

if "%scelta%"=="1" goto YTDLP_STABLE
if "%scelta%"=="2" goto YTDLP_NIGHTLY
if "%scelta%"=="3" goto FFMPEG
if "%scelta%"=="0" goto FINE
goto MENU

:YTDLP_STABLE
curl -L -o "%DEST%\yt-dlp.exe" "https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe"
goto MENU
:YTDLP_NIGHTLY
curl -L -o "%DEST%\yt-dlp.exe" "https://github.com/yt-dlp/yt-dlp-nightly-builds/releases/latest/download/yt-dlp.exe"
goto MENU
:FFMPEG
    echo === Scarico FFmpeg + FFprobe ===
	curl -L -o "%DEST%\ffmpeg.zip" "https://github.com/yt-dlp/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl.zip"
	echo 
	cls
    echo Estrazione solo di ffmpeg.exe e ffprobe.exe...
    powershell -Command ^
      "Add-Type -A 'System.IO.Compression.FileSystem';" ^
      "$zip='%DEST%\ffmpeg.zip';" ^
      "$out='%DEST%\tmp';" ^
      "If(Test-Path $out){Remove-Item $out -Recurse -Force};" ^
      "[IO.Compression.ZipFile]::ExtractToDirectory($zip,$out);" ^
      "Copy-Item (Get-ChildItem $out -Recurse -Filter ffmpeg.exe | Select-Object -First 1).FullName '%DEST%\ffmpeg.exe' -Force;" ^
      "Copy-Item (Get-ChildItem $out -Recurse -Filter ffprobe.exe | Select-Object -First 1).FullName '%DEST%\ffprobe.exe' -Force;" ^
      "Remove-Item $zip;" ^
      "Remove-Item $out -Recurse -Force"
goto MENU

:FINE
echo.
echo Uscita dal programma.
exit /b
