@echo off
title GhostHand Launcher
cd /d "%~dp0"

echo ===================================================
echo Starting GhostHand (Windows Native Desktop Assistant)
echo ===================================================
echo.
if not exist ".env" (
    if "%AI_GATEWAY_API_KEY%"=="" (
        echo [WARNING] No .env file or AI_GATEWAY_API_KEY environment variable detected.
        echo Please make sure you have created .env with your Vercel AI Gateway key.
        echo See .env.example or README.txt for instructions.
        echo.
        pause
    )
)

echo Starting GhostHand.App in background...
echo Once started, focus ANY app (Notepad, Calculator, etc.) and press:
echo.
echo     Ctrl + Win
echo.
echo GhostHand will appear immediately!
echo.
start "" "%~dp0GhostHand.App.exe"
timeout /t 2 >nul
exit
