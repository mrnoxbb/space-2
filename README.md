# Space Venue Manager (Concept)

This repository contains a WPF concept for the **Space** billiards & PlayStation venue. The app reflects the MVP scope: live timers, item management, cash transactions, and daily/monthly financial statements in Egyptian Pounds (EGP) with an RTL toggle for Arabic support.

## Highlights

- **Live station timing & billing** for 3 pool tables and 2 PlayStations.
- **Customer session tracking** with start/pause/stop/reset and CSV export.
- **Item catalog CRUD** and quick sales recording.
- **Cash register summary** with deposit/withdrawal tracking.
- **Daily and monthly statements** with CSV exports.
- **Space-themed styling** with dark nebula backgrounds and gold accents.

## Project Structure

```
SpaceVenueApp/
├── App.xaml
├── MainWindow.xaml
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
```

## Notes

- SQLite storage is created in the user profile under `LocalApplicationData/SpaceVenueApp`.
- Exported CSV files are written to `LocalApplicationData/SpaceVenueApp/Reports`.
- Currency formatting uses the `ar-EG` culture with Arabic numerals when RTL is enabled.

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

## Next Steps (Suggested)

- Add receipt printing and PDF exports.
- Integrate authentication roles for staff and managers.
- Add rich reporting filters and analytics dashboards.

