# GhostHand (Windows)

> A Windows-native AI desktop assistant. Focus an app, press **Ctrl + Win**, and tell it what to do.

GhostHand reads accessible UI controls via Windows UI Automation, selects the optimal actions using the **Jev** decision model (`typesafe-ai/jev`) via **Vercel AI Gateway**, types, clicks, and verifies the outcome in real time.

Before any critical or irreversible step (*Submit, Apply, Send, Pay, Delete, Post, Install, Confirm*), GhostHand pauses and asks you to approve. Routine steps execute automatically.

---

## Download & Installation

1. **Download the latest release:**
   Download `GhostHand-v0.1.0-win-x64.zip` from the [Releases](https://github.com/dushyantzz/Ghosthand/releases/latest) page.
2. **Extract the archive:**
   Extract the zip file to any folder on your PC (e.g. `C:\GhostHand`).
   *(No .NET runtime installation required — everything is self-contained and compiled with ReadyToRun).*
3. **Configure your API Key:**
   Copy `.env.example` to `.env` in the extracted folder and add your Vercel AI Gateway API key:
   ```ini
   AI_GATEWAY_API_KEY=vck_your_api_key_here
   AI_GATEWAY_ZERO_DATA_RETENTION=false
   ```
4. **Test your setup:**
   Double-click `CHECK_CONNECTION.bat` (or run `GhostHand.Cli.exe check`). It will test the connection to Jev via Vercel AI Gateway.
5. **Start GhostHand:**
   Double-click `START_GHOSTHAND.bat` (or run `GhostHand.App.exe`). GhostHand runs silently in your Windows System Tray.

---

## How to Use

1. **Focus any application** on your PC (Notepad, Calculator, Google Chrome, Microsoft Edge, Spotify, etc.).
2. Press the global chord:
   $$\mathbf{Ctrl} + \mathbf{Win}$$
3. The dark acrylic GhostHand popup will appear immediately above your target app.
4. Type your instruction, for example:
   - *"Write a meeting agenda for tomorrow's sprint review"*
   - *"Calculate 450 * 12 + 85"*
   - *"Search for Adele on Spotify"*
5. Press **Enter** to submit.
6. **Kill Switch:** Press **Ctrl + Win** again, press **Esc**, or click **Stop** at any moment to cancel automation immediately.

---

## Safety & Invariants

GhostHand is built with strict safety gates:
- **Plain-Code Risk Policy:** Actions involving sensitive verbs (*Submit, Apply, Send, Pay, Buy, Delete, Remove, Post, Install, Run, Confirm*) unconditionally require human confirmation.
- **Confirmation Modal:** Displays the exact action, target control, and window title before execution. Press **Enter** to approve or **Esc** to reject.
- **Privacy & Redaction:** Password fields (`IsPassword=true`) and credit cards / tokens are never captured or sent to the model.
- **App Deny-List:** Password managers (1Password, Bitwarden, KeePass, etc.) are strictly blocked from automation.
- **Local Audit Log:** Every action, decision, and risk score is logged locally to `%LOCALAPPDATA%\GhostHand\audit`.

---

## Architecture & Windows Stack

| Concern | Windows Implementation |
|---|---|
| **Language & Runtime** | C# on .NET 8 LTS (`net8.0-windows10.0.19041.0`) |
| **UI** | WPF acrylic popup + system tray integration |
| **Screen Reading** | Windows UI Automation (`FlaUI.UIA3`) with `CacheRequest` batching |
| **OCR Fallback** | `Windows.Media.Ocr` (built-in, private, local on-device) |
| **Input & Execution** | UIA Control Patterns (Invoke, Value, Toggle, Scroll) with `SendInput` fallback |
| **Global Hotkey** | Low-level keyboard hook (`WH_KEYBOARD_LL`) with pure chord state machine & Start-menu suppression |
| **AI Decision Model** | `typesafe-ai/jev` via Vercel AI Gateway (`/v1/evaluate`) |

---

## Building from Source

### Prerequisites
- Windows 10 (build 19041+) or Windows 11 (x64 or ARM64)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Build & Test
```powershell
git clone https://github.com/dushyantzz/Ghosthand.git
cd Ghosthand

# Run all 71 unit and integration tests
dotnet test windows/GhostHand.sln

# Publish self-contained ReadyToRun release
dotnet publish windows/src/GhostHand.App/GhostHand.App.csproj -c Release -r win-x64 --self-contained true -p:PublishReadyToRun=true -o dist/GhostHand-win-x64
```

## License

Licensed under the [MIT License](LICENSE).
