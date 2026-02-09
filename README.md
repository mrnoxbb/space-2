# Space Venue Manager (Concept)

 codex/create-desktop-app-for-billiards-venue-pnjysf
This repository contains a WPF concept for the **Space** billiards & PlayStation venue. The app reflects the MVP scope: live timers, item management, cash transactions, and daily/monthly financial statements in Egyptian Pounds (EGP) with an RTL toggle for Arabic support.

HEAD
This repository contains a WPF concept for the **Space** billiards & PlayStation venue. The app reflects the MVP scope: live timers, item management, cash transactions, and daily/monthly financial statements in Egyptian Pounds (EGP) with an RTL toggle for Arabic support.

This repository contains a WPF concept for the **Space** billiards & PlayStation venue. The UI and data bindings reflect the MVP scope: live timers, item sales, cash tracking, and daily statements in Egyptian Pounds (EGP) with an RTL toggle for Arabic support.
 main
 main

## Highlights

- **Live station timing & billing** for 3 pool tables and 2 PlayStations.
 codex/create-desktop-app-for-billiards-venue-pnjysf

 HEAD
 main
- **Customer session tracking** with start/pause/stop/reset and CSV export.
- **Item catalog CRUD** and quick sales recording.
- **Cash register summary** with deposit/withdrawal tracking.
- **Daily and monthly statements** with CSV exports.
 codex/create-desktop-app-for-billiards-venue-pnjysf


- **Item sales panel** for snacks and drinks.
- **Cash register summary** with opening, deposits, withdrawals, and expected closing.
- **Daily statement** snapshot and export action.
 main
 main
- **Space-themed styling** with dark nebula backgrounds and gold accents.

## Project Structure

```
SpaceVenueApp/
├── App.xaml
├── MainWindow.xaml
 codex/create-desktop-app-for-billiards-venue-pnjysf
├── Converters/
├── Models/
├── Services/
├── ViewModels/
└── SpaceVenueApp.csproj
installer/
├── SpaceVenueInstaller.iss
scripts/
├── build-installer.ps1
└── publish.ps1
=======
 HEAD
├── Converters/
├── Models/
├── Services/

├── Models/
 main
├── ViewModels/
└── SpaceVenueApp.csproj
 main
```

## Notes

 codex/create-desktop-app-for-billiards-venue-pnjysf

 HEAD
 main
- SQLite storage is created in the user profile under `LocalApplicationData/SpaceVenueApp`.
- Exported CSV files are written to `LocalApplicationData/SpaceVenueApp/Reports`.
- Currency formatting uses the `ar-EG` culture with Arabic numerals when RTL is enabled.

 codex/create-desktop-app-for-billiards-venue-pnjysf
## Build an EXE (self-contained)

Run the publish script to create a self-contained Windows executable in the `publish/` folder:

```powershell
./scripts/publish.ps1 -Configuration Release -Runtime win-x64
```

This uses `dotnet publish` with single-file output and bundles the .NET runtime.

## Build an Installer (Inno Setup)

1. Install [Inno Setup](https://jrsoftware.org/isinfo.php) and ensure `iscc` is on your PATH.
2. Build the installer (this runs publish first):

```powershell
./scripts/build-installer.ps1 -Configuration Release -Runtime win-x64
```

The installer will be created under `installer/Output/SpaceVenueSetup.exe`.


 main
## Next Steps (Suggested)

- Add receipt printing and PDF exports.
- Integrate authentication roles for staff and managers.
- Add rich reporting filters and analytics dashboards.
 codex/create-desktop-app-for-billiards-venue-pnjysf


- This is a UI-focused concept. The data is currently seeded in `MainViewModel` and uses a `DispatcherTimer` to simulate real-time billing.
- Export functionality is stubbed (shows a confirmation dialog) and can be wired to actual PDF/CSV generation later.
- Currency formatting uses the `ar-EG` culture to show EGP formatting by default.

## Next Steps (Suggested)

- Add persistence (SQLite) for sessions, items, and cash transactions.
- Implement receipt printing and detailed session history.
- Add authentication roles for staff and managers.
 main
 main

